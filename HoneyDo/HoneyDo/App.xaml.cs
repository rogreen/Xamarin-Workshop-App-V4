using HoneyDo.Services;
using HoneyDo.ViewModels;
using Xamarin.Forms;

namespace HoneyDo
{
    public partial class App : Application
    {
        public static HoneyDoItemViewModel SelectedItemViewModel;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<RestApiDataStore>();
            //DependencyService.Register<SQLiteDataStore>();
            //DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
