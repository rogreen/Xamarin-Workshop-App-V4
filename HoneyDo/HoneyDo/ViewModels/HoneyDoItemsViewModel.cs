using HoneyDo.Models;
using HoneyDo.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HoneyDo.ViewModels
{
    public class HoneyDoItemsViewModel : BaseViewModel
    {
        public ObservableCollection<HoneyDoItem> HoneyDoItems { get; set; }

        public Command LoadItemsCommand { get; set; }
        public Command<HoneyDoItem> ItemTapped { get; }
        public Command AddItemCommand { get; }
        public Command DeleteItemCommand { get; }

        public HoneyDoItemsViewModel()
        {
            HoneyDoItems = new ObservableCollection<HoneyDoItem>();

            DataStore.Initialize();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<HoneyDoItem>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            DeleteItemCommand = new Command(OnDeleteItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                HoneyDoItems.Clear();
                var honeyDoItems = await DataStore.GetItemsAsync();
                foreach (var item in honeyDoItems)
                {
                    HoneyDoItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnItemSelected(HoneyDoItem item)
        {
            if (item == null)
                return;

            App.SelectedItemViewModel = new HoneyDoItemViewModel();
            App.SelectedItemViewModel.HoneyDoItem = item;
            // This will push the HoneyDoItemPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(HoneyDoItemPage)}");
        }

        private async void OnAddItem(object obj)
        {
            var newHoneyDoItem = new HoneyDoItem()
            {
                AssignedTo = "Nobody",
                Priority = "Medium",
                Category = "Errand",
                DueDate = DateTime.Today.AddDays(7)
            };
            App.SelectedItemViewModel = new HoneyDoItemViewModel();
            App.SelectedItemViewModel.HoneyDoItem = newHoneyDoItem;

            await Shell.Current.GoToAsync($"{nameof(HoneyDoItemPage)}");
        }

        private async void OnDeleteItem(object obj)
        {
            IsBusy = true;

            try
            {
                await DataStore.DeleteItemAsync(App.SelectedItemViewModel.HoneyDoItem.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
