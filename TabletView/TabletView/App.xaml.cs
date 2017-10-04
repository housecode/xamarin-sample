using Xamarin.Forms;

namespace TabletView {
    public partial class App : Application {
        public App() {
            InitializeComponent();

			// this page shows how to create dynamic ListView height
			// using dynamic ViewCell height
			// NOTE: for WinPhone please implement ISetup interface
			//       to WinPhone project as a DependencyService
			MainPage = new NavigationPage(new ViewCellHeightPage());
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
