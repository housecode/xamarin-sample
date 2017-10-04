using System;
using WebService.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ISetupRenderer))]
namespace WebService.iOS {
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

        public bool WriteallBytes(byte[] data, string filePath) {
			if (data == null)
				throw new Exception("Data cannot be null.");

			System.IO.File.WriteAllBytes(filePath, data);

			return true;
        }
    }
}
