using Housecode.Net.Abstractions;
using Windows.Graphics.Display;
using Windows.UI.Xaml;

namespace Housecode.Net {
    public class ScreenImpl : IScreen {
        private readonly ScreenModel MainScreen;

        public ScreenImpl() {
            var win = Window.Current;
            var display = DisplayInformation.GetForCurrentView();

            MainScreen = new ScreenModel {
                Width = (int)win.Bounds.Width,
                Height = (int)win.Bounds.Height,
                IsLandscape = display.CurrentOrientation != DisplayOrientations.Portrait,
                DPI = (int)display.DiagonalSizeInInches
            };
        }

        public int ScreenWidth { get { return MainScreen.Width; } }

        public int ScreenHeight { get { return MainScreen.Height; } }

        public int DPI { get { return MainScreen.DPI; } }

        public bool IsLandscape { get { return MainScreen.IsLandscape; } }
    }

    internal class ScreenModel {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public int DPI { get; set; } = 0;
        public bool IsLandscape { get; set; } = false;
    }
}
