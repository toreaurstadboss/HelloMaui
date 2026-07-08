using CommunityToolkit.Maui.Markup;
using HelloMaui.Demos.CollectionViews;


namespace HelloMaui.Demos.Details
{

    public class DetailsPage : BaseContentPage, IQueryAttributable
    {

        public const string LibraryModelKey = nameof(LibraryModelKey);

        private Label _libraryTitleLabel;
        private Label _libraryDescriptionLabel;
        private Image _libraryImage;

        public DetailsPage()
        {
            //            Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
            //            {
            //#if ANDROID
            //                TextOverride = "<<List"
            //#else
            //                TextOverride = "List"
            //#endif
            //            });

            Title = "List"; //the page needs a dummy page to even show the title view grid shown next


            Shell.SetBackButtonBehavior(this, new BackButtonBehavior { TextOverride = "◀️" });

            var backbutton = new Button()
                        .Text("Back")
                        .BackgroundColor(Colors.LemonChiffon)
                        .TextColor(Colors.Purple)
                        .Invoke(button => button.Clicked += HandleDetailsBackButtonClicked);

            backbutton.BorderColor = Colors.Black;
            backbutton.BorderWidth = 2;

            Content = new VerticalStackLayout
            {
                Spacing = 32,

                Children =
                {
                    new Image()
                        .Center()
                        .Size(250)
                        .Assign(out _libraryImage),

                    new Label()
                        .TextCenter()
                        .Center()
                        .Font(bold: true, size: 24)
                        .Assign(out _libraryTitleLabel),

                    new Label()
                        .TextCenter()
                        .Center()
                        .Font(italic: true, size: 16)
                        .Assign(out _libraryDescriptionLabel),

                    backbutton                   

                }

            }
            .Center()
            .Padding(12);
        }

        private object SetupBackBtnLook()
        {
            throw new NotImplementedException();
        }

        private async void HandleDetailsBackButtonClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..", animate: true);
        }

        //private static Grid CreateTitleViewGrid()
        //{
        //    var grid = new Grid
        //    {
        //        Padding = new Thickness(10, 0),
        //        ColumnDefinitions =
        //        {
        //            new ColumnDefinition { Width = GridLength.Auto },
        //            new ColumnDefinition { Width = GridLength.Star }
        //        }
        //    };

        //    // Back button
        //    var backButton = new Button
        //    {
        //        Text = "<<List",
        //        BackgroundColor = Colors.Transparent,
        //        TextColor = Colors.White,
        //        Padding = new Thickness(0),
        //        FontSize = 16
        //    };

        //    backButton.Clicked += async (_, __) =>
        //    {
        //        await Shell.Current.GoToAsync("..");
        //    };

        //    // Title text (optional)
        //    var titleLabel = new Label
        //    {
        //        Text = "Your Title",
        //        VerticalTextAlignment = TextAlignment.Center,
        //        HorizontalTextAlignment = TextAlignment.Start,
        //        TextColor = Colors.White,
        //        FontSize = 18
        //    };

        //    grid.Add(backButton);
        //    grid.Add(titleLabel);
        //    Grid.SetColumn(titleLabel, 1);

        //    return grid;
        //}

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var libraryModel = (LibraryModel)query[LibraryModelKey];

            _libraryImage.Source = libraryModel.ImageSource;
            _libraryTitleLabel.Text = libraryModel.Title;
            _libraryDescriptionLabel.Text = libraryModel.Description;

            Title = libraryModel.Title;
        }

    }

}
