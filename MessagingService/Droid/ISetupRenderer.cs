using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using MessagingService.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(ISetupRenderer))]
namespace MessagingService.Droid {
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

        public void Share(string title, Uri uri) {
			var sharingIntent = new Intent();
			sharingIntent.SetAction(Intent.ActionSend);
			sharingIntent.SetType("text/plain");
			sharingIntent.PutExtra(Intent.ExtraText, title + "\n" + uri.AbsoluteUri);
			Forms.Context.StartActivity(Intent.CreateChooser(sharingIntent, "Choose an App to share"));
        }
    }
}
