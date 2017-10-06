using System;
using Foundation;
using Housecode.Net.Abstractions;
using UIKit;

namespace Housecode.Net {
    [Preserve(AllMembers = true)]
    public class ScreenImpl : IScreen {
        private readonly ScreenModel MainScreen;

        public ScreenImpl() {
			var size = UIScreen.MainScreen.Bounds;
			var screen = UIScreen.MainScreen;
			var ori = UIApplication.SharedApplication.StatusBarOrientation;

			MainScreen = new ScreenModel {
				DPI = (int)screen.Scale,
				Width = (int)size.Width,
				Height = (int)size.Height,
				IsLandscape = ori != UIInterfaceOrientation.Portrait
			};
        }

        public int ScreenWidth { get { return MainScreen.Width; } }

        public int ScreenHeight { get { return MainScreen.Height; } }

        public int DPI { get { return MainScreen.DPI; } }

        //public bool IsLandscape { get { return MainScreen.IsLandscape; } }
    }

	internal class ScreenModel {
		public int Width { get; set; } = 0;
		public int Height { get; set; } = 0;
		public int DPI { get; set; } = 0;
		public bool IsLandscape { get; set; } = false;
	}
}
