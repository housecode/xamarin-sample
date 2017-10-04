using Xamarin.Forms;

namespace CardView {
    public interface ISetup {
        string BasePath { get; }
        ScreenModel MainScreen { get; }
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
    }

}
