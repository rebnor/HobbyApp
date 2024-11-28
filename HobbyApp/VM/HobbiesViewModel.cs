using HobbyApp.Command;
using HobbyApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HobbyApp.VM
{
    public class HobbiesViewModel : ViewModelBase
    {
        /*
         * Constructor
         * Adds 3 Hobbies to the collection
         * and initializes 4 Delegate Commands
         */
        public HobbiesViewModel() {
            Hobbies.Add(new HobbyItemViewModel(new Hobby() { Name = "Paint", Description = "Oil painting and drawing" }));
            Hobbies.Add(new HobbyItemViewModel(new Hobby() { Name = "Music", Description = "Playing piano" }));
            Hobbies.Add(new HobbyItemViewModel(new Hobby() { Name = "Gym", Description = "1-2 times a week" }));

            AddCommand = new DelegateCommand(AddHobby, CanAdd);
            DeleteCommand = new DelegateCommand(DeleteHobby, CanDelete);
            EditCommand = new DelegateCommand(EditHobby, CanEdit);
            SaveCommand = new DelegateCommand(SaveHobby, CanSave);
        }

        /*
         * The selected Hobby
         * Only visible when a hobby is selected(not null)
         */
        private HobbyItemViewModel selectedHobby;
        public HobbyItemViewModel SelectedHobby
        {
            get => selectedHobby;
            set
            {
                selectedHobby = value;
                RaisePropertyChanged(nameof(selectedHobby));
                RaisePropertyChanged(nameof(SelectedHobbyVisibility));
                DeleteCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
        }
        public Visibility SelectedHobbyVisibility => SelectedHobby is null ? Visibility.Hidden : Visibility.Visible;

        /*
         * Collection of Hobbies stored in an ObservableCollection
         */
        private ObservableCollection<HobbyItemViewModel> hobbies = new();
        public ObservableCollection<HobbyItemViewModel> Hobbies
        {
            get => hobbies;
            set
            {
                hobbies = value;
                RaisePropertyChanged(nameof(hobbies));
            }
        }

        /*
         * Add a hobby with textbox input value - Name & Description
         * Name is required before "Add Hobby" button is enabled.
         * If Description is null, a messagebox asks to save without it.
         */
        public string? nameInput;
        public string? NameInput
        {
            get => nameInput;
            set
            {
                nameInput = value;
                RaisePropertyChanged(nameof(nameInput));
                AddCommand.RaiseCanExecuteChanged();
            }
        }
        public string? descriptionInput;
        public string? DescriptionInput
        {
            get => descriptionInput;
            set
            {
                descriptionInput = value;
                RaisePropertyChanged(nameof(descriptionInput));
                AddCommand.RaiseCanExecuteChanged();
            }
        }
        public DelegateCommand AddCommand { get; }
        private void AddHobby(object? parameter)
        {
            if (DescriptionInput is null)
            {
                var result = MessageBox.Show(
                    $"You haven't described the hobby '{NameInput}'. Would you like to save it without Description?",
                    "No Description in Hobby",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result is MessageBoxResult.No) return;
            }
          
            Hobbies.Add(new HobbyItemViewModel(new Hobby { Name = NameInput, Description = DescriptionInput }));
            SelectedHobby = Hobbies.Last();
            NameInput = DescriptionInput = null;
        }
        private bool CanAdd(object? parameter) => !string.IsNullOrWhiteSpace(NameInput);

        /*
         * Edit a hobby
         * bool IsReadOnly is 'true' when not editing, and 'false' when editing is enabled.
         * bool IsEditing indicates if the hobby is in edit mode. Setting it to 'true' allows editing.
         */
        public DelegateCommand EditCommand { get; }
        public bool IsReadOnly => !IsEditing;

        private bool isEditing;
        public bool IsEditing
        {
            get => isEditing;
            set
            {
                isEditing = value;
                RaisePropertyChanged(nameof(IsEditing));
                RaisePropertyChanged(nameof(IsReadOnly));
                EditCommand.RaiseCanExecuteChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        private void EditHobby(object? parameter) => IsEditing = SelectedHobby is not null;
        private bool CanEdit(object? parameter) => SelectedHobby is not null && !IsEditing;

        /*
         * Save changes to the selected hobby and disable editing.
         */
        public DelegateCommand SaveCommand { get; }
        private void SaveHobby(object? parameter)
        {
            if (SelectedHobby is not null)
            {
                IsEditing = false;
                MessageBox.Show($"Changes saved on hobby '{SelectedHobby.Name}'");
            }
        }
        private bool CanSave(object? parameter) => SelectedHobby is not null && IsEditing;

        /*
         * Delete the selected hobby from the collection.
         */
        public DelegateCommand DeleteCommand { get; }
        private void DeleteHobby(object? parameter)
        {
            if (SelectedHobby is not null)
            {
                Hobbies.Remove(SelectedHobby);
                SelectedHobby = null;
            }
        }
        private bool CanDelete(object? parameter) => SelectedHobby is not null;

    }
}
