using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Sample {
    public partial class SamplePage : ContentPage {
        public SamplePage() {
            InitializeComponent();
            Title = "Sample Page";
            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}
