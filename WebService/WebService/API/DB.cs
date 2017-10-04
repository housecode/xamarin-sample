using System;
using System.Collections.Generic;
using System.Reflection;

namespace WebService.API {
    public class DB: IDisposable {
        private string DBPath => System.IO.Path.Combine(Helper.BasePath, "Housecode.db3");
        private readonly SQLite.SQLiteConnection Connection;

        public DB() {
            Connection = new SQLite.SQLiteConnection(DBPath);
        }

		public int CreateTable<T>() {
			return Connection.CreateTable<T>();
		}

		public int DropTable<T>() {
			return Connection.DropTable<T>();
		}

		public T SelectDB<T>(object key) where T : new() {
			var obj = Connection.Get<T>(key);
			if (EqualityComparer<T>.Default.Equals(obj, default(T))) {
				return default(T);
			}
			return obj;
		}

		public T InsertDB<T>(object obj) where T : new() {
			var table = Connection.Table<T>().Table;
			var duplicate = false;
            var msg = "";

            // check if object was saved to DB
			try {
				// get primary key
				// if object has not primary key, it will throw exception
				Type t = obj.GetType();
				PropertyInfo prop = t.GetRuntimeProperty(table.PK.Name);
				var key = prop.GetValue(obj);
                msg = key + " at table '" + table.TableName + "'";
				var res = SelectDB<T>(key);
				duplicate = true;
			} catch (Exception) {
				duplicate = false;
			}

            // throw exception if object is duplicated
			if (duplicate)
                throw new Exception("Duplicate primary key " + (string.IsNullOrWhiteSpace(msg) ? "" : msg));

            // insert object and return the saved object with it's primary key
			var x = Connection.Insert(obj);
			if (x <= 0) { return default(T); }
			string sql = @"select last_insert_rowid()";
			var lastId = Connection.ExecuteScalar<long>(sql);
			return Connection.Get<T>(lastId);
		}

		public T UpdateDB<T>(object obj) where T : new() {
            // get primary key
			var map = Connection.GetMapping(obj.GetType());
			var pk = map.PK.GetValue(obj);

            // update the object
			var x = Connection.Update(obj);

            // return 1 if success, 0 otherwise
			if (x <= 0) { return default(T); }

            // return the updated object
			return Connection.Get<T>(pk);
		}

		public MessageModel DeleteDB(object obj) {
            var msg = new MessageModel();
			try {
				var x = Connection.Delete(obj);
				if (x <= 0) {
					msg.SetMessage(false, x + " data deleted.");
					return msg;
				}
				msg.SetMessage(true, x + " data updated.");
			} catch (Exception ex) {
				msg.SetMessage(true, ex.Message);
			}
			return msg;
		}

        public void Dispose() {
            if (Connection != null) {
                Connection.Close();
                Connection.Dispose();
            }
        }
    }

	public class MessageModel {
		public string Message { get; private set; }
		public bool IsError { get; private set; }

		public void SetMessage(bool isError, string message) {
			Message = message;
			IsError = isError;
		}
	}
}
