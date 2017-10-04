using Xamarin.Forms;

namespace ViewCellHeight {
    public partial class ViewCellHeightPage : ContentPage {
        public ViewCellHeightPage() {
            InitializeComponent();

            // set header template and item template
            MyList.GroupHeaderTemplate = new DataTemplate(() => new ItemTemplate(false));
            MyList.ItemTemplate = new DataTemplate(() => new ItemTemplate(true));

            // set binding context
            BindingContext = new ViewModel();

            // set page title
            Title = "Dynamic ViewCell Height";
            NavigationPage.SetBackButtonTitle(this, "");
        }
    }
}
