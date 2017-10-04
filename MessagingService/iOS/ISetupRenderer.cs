using System;
using Foundation;
using MessagingService.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ISetupRenderer))]
namespace MessagingService.iOS {
	public class ISetupRenderer : ISetup {
        string ISetup.BasePath => Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        ScreenModel ISetup.MainScreen {
            get {
				var size = UIScreen.MainScreen.Bounds;
				var screen = UIScreen.MainScreen;
				var ori = UIApplication.SharedApplication.StatusBarOrientation;

				var model = new ScreenModel {
					DPI = (int)screen.Scale,
					Width = (int)size.Width,
					Height = (int)size.Height,
					IsLandscape = ori != UIInterfaceOrientation.Portrait
				};

				return model;
            }
        }

        public void Log(string message, bool isError) {
            var tag = isError ? "Housecode.Net_ERROR: " : "Housecode.Net: ";
            Console.WriteLine(tag + message);
        }

        public void Share(string title, Uri uri) {
			NSObject[] activitiesItems = {
				new NSString(title),
				new NSUrl(uri.AbsoluteUri)
			};
			var activityController = new UIActivityViewController(activitiesItems, null);
			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(activityController, true, null);
        }
    }
}
