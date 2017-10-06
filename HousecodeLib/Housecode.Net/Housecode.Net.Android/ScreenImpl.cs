using Android.Content;
using Android.Runtime;
using Android.Views;
using Housecode.Net.Abstractions;

namespace Housecode.Net {
    [Preserve(AllMembers = true)]
    public class ScreenImpl : IScreen {
        private readonly ScreenModel MainScreen;

        public ScreenImpl() {
			var res = Android.App.Application.Context.Resources;

			IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
			var rotation = windowManager.DefaultDisplay.Rotation;
			bool isLandscape = rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270;

			MainScreen = new ScreenModel {
				Width = res.DisplayMetrics.WidthPixels,
				Height = res.DisplayMetrics.HeightPixels,
				DPI = (int)res.DisplayMetrics.DensityDpi,
				IsLandscape = isLandscape
			};
        }

        public int ScreenWidth { get { return MainScreen.Width; } }

        public int ScreenHeight { get { return MainScreen.Height; } }

        public int DPI { get { return MainScreen.DPI; } }

        //public bool IsLandscape { get { return MainScreen.IsLandscape; } }
    }

    internal class ScreenModel{
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public int DPI { get; set; } = 0;
        public bool IsLandscape { get; set; } = false;
    }
}
