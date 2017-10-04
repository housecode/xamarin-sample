using Xamarin.Forms;

namespace ViewCellHeight {
    public partial class ItemTemplate : ViewCell {

        public ItemTemplate(bool isSubGroup) {
            InitializeComponent();

            // hide some views from xaml
			if (isSubGroup) {
				SubDate.IsVisible = true;
				SubUs.IsVisible = true;
				HeadDate.IsVisible = false;
				UsBack.IsVisible = false;
				UsLabel.IsVisible = false;
			} else {
				SubDate.IsVisible = false;
				SubUs.IsVisible = false;
				HeadDate.IsVisible = true;
				UsBack.IsVisible = true;
				UsLabel.IsVisible = true;
                // set ViewCell height just for group header and iOS only
                // android does not need it
				if (Device.RuntimePlatform == "iOS") {
					Height = GetHeight();
				}
			}
		}

        // get ViewCell height
		private double GetHeight() {
			double x = Helper.MainScreen.DPI <= 2 ? 0.32 : 0.3;

			var h = Helper.MainScreen.IsLandscape ? (Helper.MainScreen.Height * x) : (Helper.MainScreen.Width * x);

			return h;
		}
    }
}
