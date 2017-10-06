using System;
using Foundation;
using Housecode.Net.Abstractions;
using UIKit;

namespace Housecode.Net {
    [Preserve(AllMembers = true)]
    public class HelperImpl : IHelper {
        public string BasePath { get { return Environment.GetFolderPath(Environment.SpecialFolder.Personal); } }

		public void Log(string message, bool isError = false) {
			var tag = isError ? "Housecode.Net_ERROR: " : "Housecode.Net: ";
			Console.WriteLine(tag + message);
		}

		public void Share(string title, string url) {
			var uri = new Uri(url);
			NSObject[] activitiesItems = {
				new NSString(title),
				new NSUrl(uri.AbsoluteUri)
			};
			var activityController = new UIActivityViewController(activitiesItems, null);
			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(activityController, true, null);
		}
    }
}
