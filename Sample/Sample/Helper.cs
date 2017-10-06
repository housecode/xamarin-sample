using System;
using Xamarin.Forms;

namespace Sample {
    public interface ISetup {
        string BasePath { get; }
        ScreenModel MainScreen { get; }
        void Log(string message, bool isError);
    }

	// screen model
	// to store screen size and orientation
	public class ScreenModel {
		public int Width { get; set; } = 0;
		public int Height { get; set; } = 0;
		public int DPI { get; set; } = 0;
		public bool IsLandscape { get; set; } = false;
	}

    // static class to get screen model
    public static class Helper {
		// ISetup implementation exists in each specified project as a DependencyService
		private static ISetup Setup {
            get {
                return DependencyService.Get<ISetup>();
            }
        }

        public static string BasePath {
            get { return Setup.BasePath; }
        }

        public static ScreenModel MainScreen {
            get { return Setup.MainScreen; }
        }

        public static void Log(string message, bool isError = false) {
            Setup.Log(message, isError);
        }

        public static void Log(Exception ex) {
            Log(ex.Message + "\n" + ex.StackTrace, true);
        }
    }

}
