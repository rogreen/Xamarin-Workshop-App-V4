using HoneyDo.Models;
using HoneyDo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HoneyDo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HoneyDoItemsPage : ContentPage
    {
        HoneyDoItemsViewModel viewModel;

        public HoneyDoItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new HoneyDoItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

        private async void CompletedCheckBox_CheckedChanged(System.Object sender, CheckedChangedEventArgs e)
        {
            var honeyDoItemViewModel = new HoneyDoItemViewModel();
            honeyDoItemViewModel.HoneyDoItem = ((HoneyDoItem)((CheckBox)sender).BindingContext);

            await honeyDoItemViewModel.ExecuteDeleteItemCommand();

            ((CheckBox)sender).IsChecked = false;
            viewModel.LoadItemsCommand.Execute(null);
        }

        private async void SwipeItem_Invoked(System.Object sender, System.EventArgs e)
        {
            var honeyDoItemViewModel = new HoneyDoItemViewModel();
            honeyDoItemViewModel.HoneyDoItem = ((HoneyDoItem)((SwipeItem)sender).BindingContext);

            await honeyDoItemViewModel.ExecuteDeleteItemCommand();
            viewModel.LoadItemsCommand.Execute(null);
        }

    }
}