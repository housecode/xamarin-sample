using System;
using Xamarin.Forms;

namespace NetLibTest {
    public partial class NetLibTestPage : ContentPage {
        void Handle_Clicked(object sender, EventArgs e) {
            var hc = Housecode.Net.CrossSetup.Current;
            hc.Helper.Log("It's Working....");
            hc.Helper.Log(hc.Helper.BasePath);
            hc.Helper.Log("Width: " + hc.Screen.ScreenWidth);
            hc.Helper.Log("Height: " + hc.Screen.ScreenHeight);
            hc.Helper.Log("DPI: " + hc.Screen.DPI);
            //hc.Helper.Log("IsLandscape: " + hc.Screen.IsLandscape);
            var cv = new Housecode.Net.Controls.CardView();
        }

        public NetLibTestPage() {
            InitializeComponent();
        }
    }
}
