using Housecode.Net.Abstractions;
using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation.Diagnostics;

namespace Housecode.Net {
    public class HelperImpl : IHelper {
        public string BasePath {
            get {
                return Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            }
        }

        public void Log(string message, bool isError = false) {
            var tag = isError ? "Housecode.Net_ERROR: " : "Housecode.Net: ";
            System.Diagnostics.Debug.WriteLine(tag + message);
        }

        public void Share(string title, string url) {
            var dtm = DataTransferManager.GetForCurrentView();
            dtm.DataRequested += (sender, e) => {
                e.Request.Data.Properties.Title = title;
                e.Request.Data.Properties.Description = "Choose an App to share";
                e.Request.Data.SetWebLink(new Uri(url));
            };

            DataTransferManager.ShowShareUI();
        }
    }
}
