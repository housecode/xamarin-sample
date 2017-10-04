﻿using Xamarin.Forms;

namespace TabletView {
    public class CardView : Frame {
        public CardView() {
			Padding = 0;
			switch (Device.RuntimePlatform) {
				case "iOS":
    				HasShadow = true;
    				OutlineColor = Color.Transparent;
    				BackgroundColor = Color.Transparent;
				break;
			}
			HorizontalOptions = LayoutOptions.Start;
        }
    }
}
