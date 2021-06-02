using HoneyDo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace HoneyDo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HoneyDoItemPage : ContentPage
    {
        HoneyDoItemViewModel viewModel;

        public HoneyDoItemPage()
        {
            InitializeComponent();

            BindingContext = viewModel = App.SelectedItemViewModel;

            if (Device.RuntimePlatform == Device.Android)
            {
                SaveButton.CornerRadius = 75;
                CompletedButton.CornerRadius = 75;
            }
            if (Device.RuntimePlatform == Device.UWP)
            {
                SaveButton.CornerRadius = 15;
                CompletedButton.CornerRadius = 15;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetPickers();
        }

        private void SetPickers()
        {
            switch (viewModel.HoneyDoItem.AssignedTo)
            {
                case "Me":
                    AssignedToPicker.SelectedIndex = 0;
                    break;
                case "You":
                    AssignedToPicker.SelectedIndex = 1;
                    break;
                case "Us":
                    AssignedToPicker.SelectedIndex = 2;
                    break;
                case "Nobody":
                    AssignedToPicker.SelectedIndex = 3;
                    break;
                default:
                    break;
            }

            switch (viewModel.HoneyDoItem.Priority)
            {
                case "Low":
                    PriorityPicker.SelectedIndex = 0;
                    break;
                case "Medium":
                    PriorityPicker.SelectedIndex = 1;
                    break;
                case "High":
                    PriorityPicker.SelectedIndex = 2;
                    break;
                default:
                    break;
            }

            switch (viewModel.HoneyDoItem.Category)
            {
                case "Home indoors":
                    CategoryPicker.SelectedIndex = 0;
                    break;
                case "Home outdoors":
                    CategoryPicker.SelectedIndex = 1;
                    break;
                case "Errand":
                    CategoryPicker.SelectedIndex = 2;
                    break;
                default:
                    break;
            }
        }

        private void OnAssignedToPickerSelectedIndexChanged(Object sender, EventArgs e)
        {
            viewModel.HoneyDoItem.AssignedTo = ((Picker)sender).SelectedItem.ToString();
        }

        private void OnPriorityPickerSelectedIndexChanged(Object sender, EventArgs e)
        {
            viewModel.HoneyDoItem.Priority = ((Picker)sender).SelectedItem.ToString();
        }

        private void OnCategoryPickerSelectedIndexChanged(Object sender, EventArgs e)
        {
            viewModel.HoneyDoItem.Category = ((Picker)sender).SelectedItem.ToString();
        }

    }
}