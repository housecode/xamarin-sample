using System;
using Acr.UserDialogs;
using Xamarin.Forms;
using SQLite;
using System.Collections.Generic;

namespace WebService {
    public partial class WebService : ContentPage {
        // set value to TRUE if you want to use Housecode.Net example API
        private readonly bool UseExample = false;

		public WebService() {
			InitializeComponent();
			Title = "WebService";
			NavigationPage.SetBackButtonTitle(this, "");

            if (UseExample) {
                using (var db = new API.DB()) {
                    db.CreateTable<CustomerModel>();
                }
            }
		}

        private async void GetClicked(object sender, EventArgs e) {
            UserDialogs.Instance.ShowLoading("Get Data");

            if (string.IsNullOrWhiteSpace(txtURL.Text) && !UseExample) {
                await DisplayAlert("Warning", "URL is empty.", "OK");
                UserDialogs.Instance.HideLoading();
                return;
            }

            // keyword 'using' make sure all resource will be disposed at the end of this block proccess
			using (var web = new API.WebService()) {
                try {
                    if(!UseExample) {
                        var data = await web.GetData<object>(txtURL.Text);
						if (data != null)
							Helper.Log(data.ToString());
                    } else {
                        var data = await web.GetData<List<CustomerModel>>("http://api.housecode.net/customer");
                        if (data != null) {
                            using (var db = new API.DB()) {
								foreach (var obj in data) {
                                    try {
										var dbData = db.InsertDB<CustomerModel>(obj);
										if (dbData != null)
                                            Helper.Log(Newtonsoft.Json.JsonConvert.SerializeObject(dbData));
                                    } catch (Exception) {}
								}
                            }
                        }
                    }
                } catch (Exception ex) {
                    Helper.Log(ex);
                }
            }

            UserDialogs.Instance.HideLoading();
        }

        private async void PutClicked(object sender, EventArgs e) {
            UserDialogs.Instance.ShowLoading("Put Data");

			if (string.IsNullOrWhiteSpace(txtURL.Text)) {
				await DisplayAlert("Warning", "URL is empty.", "OK");
                UserDialogs.Instance.HideLoading();
				return;
			}

            if (string.IsNullOrWhiteSpace(txtBody.Text)) {
                await DisplayAlert("Warning", "Put method need body parameter.", "OK");
                UserDialogs.Instance.HideLoading();
                return;
            }
            
			using (var web = new API.WebService()) {
				try {
                    var data = await web.PutDataObject<object>(txtURL.Text, txtBody.Text);
					if (data != null)
						Helper.Log(data.ToString());
				} catch (Exception ex) {
					Helper.Log(ex);
				}
			}

            UserDialogs.Instance.HideLoading();
		}

        private async void PostClicked(object sender, EventArgs e) {
            UserDialogs.Instance.ShowLoading("Post Data");

			if (string.IsNullOrWhiteSpace(txtURL.Text)) {
				await DisplayAlert("Warning", "URL is empty.", "OK");
                UserDialogs.Instance.HideLoading();
				return;
			}

            if (string.IsNullOrWhiteSpace(txtBody.Text)) {
                await DisplayAlert("Warning", "Post method need body parameter.", "OK");
                UserDialogs.Instance.HideLoading();
                return;
            }

			using (var web = new API.WebService()) {
				try {
                    var data = await web.PostDataObject<object>(txtURL.Text, txtBody.Text);
					if (data != null)
						Helper.Log(data.ToString());
				} catch (Exception ex) {
					Helper.Log(ex);
				}
			}

            UserDialogs.Instance.HideLoading();
		}

        private async void DeleteClicked(object sender, EventArgs e) {
            UserDialogs.Instance.ShowLoading("Delete Data");

			if (string.IsNullOrWhiteSpace(txtURL.Text)) {
				await DisplayAlert("Warning", "URL is empty.", "OK");
                UserDialogs.Instance.HideLoading();
				return;
			}

			using (var web = new API.WebService()) {
				try {
                    var data = await web.DeleteData<object>(txtURL.Text);
					if (data != null)
						Helper.Log(data.ToString());
				} catch (Exception ex) {
					Helper.Log(ex);
				}
			}

            UserDialogs.Instance.HideLoading();
		}
    }

    // customer model is a class model to store data from http://api.housecode.net/customer
    // each property name must be same as JSON field name
    // it is used for JSON serialize mapping
	public class CustomerModel {
        [PrimaryKey]
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        // class model for POST or PUT process
        // contain all property except 'id'
        // use this class model to passing POST body parameter
        // or PUT body parameter
        public class PostModel {
            public string first_name { get; private set; }
			public string last_name { get; private set; }
			public string phone { get; private set; }
			public string mail { get; private set; }
			public string address { get; private set; }
			public string city { get; private set; }
			public string state { get; private set; }

            public PostModel(CustomerModel model) {
                first_name = model.first_name;
                last_name = model.last_name;
                phone = model.phone;
                mail = model.mail;
                address = model.address;
                city = model.city;
                state = model.state;
            }
        }
    }
}
