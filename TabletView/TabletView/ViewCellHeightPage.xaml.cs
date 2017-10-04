using Xamarin.Forms;

namespace TabletView {
    public partial class ViewCellHeightPage : ContentPage {
        public ViewCellHeightPage() {
            InitializeComponent();

            // set header template and item template
            PhoneList.GroupHeaderTemplate = new DataTemplate(() => new ItemTemplate(false));
            PhoneList.ItemTemplate = new DataTemplate(() => new ItemTemplate(true));

            TabList.GroupHeaderTemplate = new DataTemplate(() => new ItemTemplate(false));
            TabList.ItemTemplate = new DataTemplate(() => new ItemTemplate(true));

            // set binding context
            BindingContext = new ViewModel();

            // set page title
            Title = "Tablet View";
            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}
