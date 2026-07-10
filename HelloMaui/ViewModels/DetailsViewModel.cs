using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloMaui.Models;
using HelloMaui.Pages;
using System.Windows.Input;

namespace HelloMaui.ViewModels
{
    public partial class DetailsViewModel : BaseViewModel, IQueryAttributable
    {

        public const string LibraryModelKey = nameof(LibraryModelKey);

        [ObservableProperty]
        string? _libraryTitle;

        [ObservableProperty]    
        string? _libraryDescription;

        [ObservableProperty]
        ImageSource? _libraryImageSource;

        public DetailsViewModel()
        {            
        }

        [RelayCommand]
        async Task HandleBackButton()
        {
            await Shell.Current.GoToAsync("..", animate: true);
        } 

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var libraryModel = (LibraryModel)query[LibraryModelKey];

            LibraryImageSource = libraryModel.ImageSource;
            LibraryTitle = libraryModel.Title;
            LibraryDescription = libraryModel.Description;
        }

    }
}
