using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Housecode.Net.API {
    ///
	/// Created by Cahyo Agung
	/// @link      http://www.housecode.net
	/// @copyright Copyright (c) 2017 Housecode
	/// @version   1.0.0
	/// 
	/// This Library need following packages
	/// - Microsoft.net.http
	/// - Newtonsoft.Json
	///
	/// Rest Service to simplify proccess
    ///
	public class WebService : IDisposable {
        /// Contructor and set default service config
		public WebService() {
			RestConfig = new ServiceConfig();
			Client = RestClient(RestConfig);
		}

		private ServiceConfig RestConfig { get; set; }
        /// set config
        /// this feature is not yet ready to implementation
        /// it is under development
        private void SetConfig(ServiceConfig config) {
			RestConfig = config;
			Client = RestClient(RestConfig);
		}

		private HttpClient Client { get; set; }

		private HttpClient RestClient(ServiceConfig config) {
			var handler = new HttpClientHandler { AllowAutoRedirect = config.AllowRedirect };
			if (config.UseCookie) {
				handler.UseCookies = config.UseCookie;
				handler.CookieContainer = config.Cookie;
			}
			if (config.UseProxy) {
			    handler.UseProxy = config.UseProxy;
			    handler.Proxy = config.Proxy;
			}

			var client = new HttpClient(handler);
			if (!string.IsNullOrWhiteSpace(config.Connection)) {
				client.DefaultRequestHeaders.TryAddWithoutValidation("Connection", config.Connection);
			}
			if (!string.IsNullOrWhiteSpace(config.Accept)) {
				client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", config.Accept);
			}
			if (!string.IsNullOrWhiteSpace(config.AcceptEncoding)) {
				client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", config.AcceptEncoding);
			}
			if (!string.IsNullOrWhiteSpace(config.UserAgent)) {
				client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", config.UserAgent);
			}
			if (!string.IsNullOrWhiteSpace(config.AcceptCharset)) {
				client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", config.AcceptCharset);
			}

			if (config.UseAuth) {
				if (config.Authentication == null) {
					throw new Exception("Authentication cannot be null.");
				}
				client.DefaultRequestHeaders.Add(config.Authentication.AuthKey, config.Authentication.AuthValue);
			}

			return client;
		}

		/// post data object with parameter
		public Task<T> PostDataObject<T>(string url, object param) {
			return PostPutData<T>(url, true, null, param, false);
		}

		/// post data multipart with parameter
		public Task<T> PostDataMultipart<T>(string url, Dictionary<string, object> dictParam) {
			return PostPutData<T>(url, false, dictParam, null, false);
		}

		/// put data object with parameter
		public Task<T> PutDataObject<T>(string url, object param) {
			return PostPutData<T>(url, true, null, param, true);
		}

		/// put data multipart with parameter
		public Task<T> PutDataMultipart<T>(string url, Dictionary<string, object> dictParam) {
			return PostPutData<T>(url, false, dictParam, null, true);
		}

		private Task<T> PostPutData<T>(string url, bool isPureObject, Dictionary<string, object> dictParam, object param, bool isUpdate) {
            return Task.Run(async () => {
				HttpContent setContent = null;
				if (isPureObject) { // post or put object as JSON
					if (param == null) { return default(T); }
					var json = ApiUtils.SerializeObject(param);
					setContent = new StringContent(json, Encoding.UTF8, RestConfig.Accept);
				} else { // post or put object as multipart form
					if (dictParam == null) { return default(T); }
					var content = new MultipartFormDataContent("HousecodeAPI" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
					foreach (var par in dictParam) {
						if (par.Value is FileParam) {
                            var file = par.Value as FileParam;
							var byteContent = new ByteArrayContent(file.File);
							byteContent.Headers.Add("Content-Type", "application/octet-stream");
							byteContent.Headers.ContentEncoding.Add(RestConfig.AcceptCharset);
							content.Add(byteContent, par.Key, file.FileName);
						} else {
							content.Add(new StringContent(string.Format("{0}", par.Value), Encoding.UTF8, RestConfig.Accept), par.Key);
						}
					}
					setContent = content;
				}

				if (setContent == null) { return default(T); }

				// get response
				HttpResponseMessage response;
				if (isUpdate) {
					response = await Client.PutAsync(url, setContent);
				} else {
					response = await Client.PostAsync(url, setContent);
				}
				// if not equals 'Success Status Code (e.g: 200, 201, etc)' then fire exception
				response.EnsureSuccessStatusCode();
				var data = await ApiUtils.DeserializeObject<T>(response);

				return data;
            });
		}

		public Task<byte[]> GetByteArrayData(string url) {
			return Task.Run(async () => {
				// get response
                var response = await Client.GetByteArrayAsync(url);
				return response;
			});
		}

		/// get data object
		public Task<T> GetData<T>(string url) {
            return Task.Run(async () => {
				// get response
				var response = await Client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
				// if status code not equals 'Success Status Code (e.g: 200, 201, etc)' then fire exception
				response.EnsureSuccessStatusCode();
				var data = await ApiUtils.DeserializeObject<T>(response);
				return data;
            });
		}

		/// delete data object
		public Task<T> DeleteData<T>(string url) {
			return Task.Run(async () => {
				// get response
				var response = await Client.DeleteAsync(url);
				// if status code not equals 'Success Status Code (e.g: 200, 201, etc)' then fire exception
				response.EnsureSuccessStatusCode();
				var data = await ApiUtils.DeserializeObject<T>(response);
				return data;
			});
		}

		/// dispose object
		public void Dispose() {
			if (Client != null) {
				Client.Dispose();
				Client = null;
			}
		}
	}
}
