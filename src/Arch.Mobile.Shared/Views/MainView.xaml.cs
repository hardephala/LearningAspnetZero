using Xamarin.Forms;

namespace Arch.Views
{
    public partial class MainView : FlyoutPage, IXamarinView
    {
        public MainView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
