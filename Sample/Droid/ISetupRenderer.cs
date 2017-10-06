using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Sample.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(ISetupRenderer))]
namespace Sample.Droid {
    public class ISetupRenderer : ISetup {
        public string BasePath => Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public ScreenModel MainScreen {
			get {
				var res = Android.App.Application.Context.Resources;

				IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
				var rotation = windowManager.DefaultDisplay.Rotation;
				bool isLandscape = rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270;

				ScreenModel model = new ScreenModel {
					Width = res.DisplayMetrics.WidthPixels,
					Height = res.DisplayMetrics.HeightPixels,
					DPI = (int)res.DisplayMetrics.DensityDpi,
					IsLandscape = isLandscape
				};

				return model;
			}
        }

        public void Log(string message, bool isError) {
            if(isError)
                Android.Util.Log.Error("Housecode.Net", message);
            else
                Android.Util.Log.Verbose("Housecode.Net", message);
        }
    }
}
