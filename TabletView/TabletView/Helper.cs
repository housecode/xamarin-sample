using Xamarin.Forms;

namespace TabletView {
    public interface ISetup {
        string BasePath { get; }
        ScreenModel MainScreen { get; }
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
