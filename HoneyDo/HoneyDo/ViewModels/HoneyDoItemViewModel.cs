using HoneyDo.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HoneyDo.ViewModels
{
    public class HoneyDoItemViewModel : BaseViewModel
    {

        public HoneyDoItem HoneyDoItem { get; set; }
        public Command SaveItemCommand { get; set; }
        public Command DeleteItemCommand { get; set; }

        public HoneyDoItemViewModel(HoneyDoItem honeyDoItem = null)
        {
            HoneyDoItem = honeyDoItem;

            SaveItemCommand = new Command(async () => await ExecuteSaveItemCommand());
            DeleteItemCommand = new Command(async () => await ExecuteDeleteItemCommand());
        }

        async Task ExecuteSaveItemCommand()
        {
            IsBusy = true;

            try
            {
                if (HoneyDoItem.Id == 0)
                {
                    await DataStore.AddItemAsync(HoneyDoItem);
                }
                else
                {
                    await DataStore.UpdateItemAsync(HoneyDoItem);
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

            await Shell.Current.GoToAsync("..");
        }

        public async Task ExecuteDeleteItemCommand()
        {
            IsBusy = true;

            try
            {
                await DataStore.DeleteItemAsync(HoneyDoItem.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}
