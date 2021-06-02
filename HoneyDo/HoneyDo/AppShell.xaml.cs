using HoneyDo.Views;
using Xamarin.Forms;

namespace HoneyDo
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HoneyDoItemPage), typeof(HoneyDoItemPage));
        }

    }
}
