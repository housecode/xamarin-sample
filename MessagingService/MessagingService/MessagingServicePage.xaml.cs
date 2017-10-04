using System;
using Xamarin.Forms;

namespace MessagingService {
    public partial class MessagingServicePage : ContentPage {
        private void SmsClicked(object sender, EventArgs e) {
            MessagingService.SendSMS(txtInput.Text, "Hello World!");
        }

		private void CallClicked(object sender, EventArgs e) {
            MessagingService.CallTo(txtInput.Text);
		}

		private void MailClicked(object sender, EventArgs e) {
            MessagingService.SendEmailWithBody(txtInput.Text, "Test", false, "Hello World!");
		}

		private void ShareClicked(object sender, EventArgs e) {
            Helper.Share(txtInput.Text, new Uri("https://www.google.com"));
		}

        public MessagingServicePage() {
            InitializeComponent();
            Title = "MessagingService";
            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}
