using System;
namespace Housecode.Net.Abstractions {
    public interface IScreen {
		int ScreenWidth { get; }
		int ScreenHeight { get; }
		int DPI { get; }
		//bool IsLandscape { get; }
    }
}
