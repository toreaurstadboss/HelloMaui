using CommunityToolkit.Mvvm.Input;
using HelloMaui.Models;
using System.Windows.Input;

namespace HelloMaui.ViewModels
{
    public class DetailsViewModel : BaseViewModel, IQueryAttributable
    {

        public const string LibraryModelKey = nameof(LibraryModelKey);

        private string _libraryTitleLabel;
        private string _libraryDescriptionLabel;
        private ImageSource _libraryImageSource;

        public ICommand BackButtonCommand { get; }

        public DetailsViewModel()
        {
            BackButtonCommand = new AsyncRelayCommand(async () =>
            {
                await HandleBackButton();
            });
        }

        async Task HandleBackButton()
        {
            await Shell.Current.GoToAsync("..", animate: true);
        }

        public ImageSource LibraryImageSource
        {
            get => _libraryImageSource;
            set => SetProperty<ImageSource>(ref _libraryImageSource, value);
        }

        public string LibraryTitle
        {
            get => _libraryTitleLabel;
            set => SetProperty(ref _libraryTitleLabel, value);
        }

        public string LibraryDescription
        {
            get => _libraryDescriptionLabel;
            set => SetProperty(ref _libraryDescriptionLabel, value);
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
