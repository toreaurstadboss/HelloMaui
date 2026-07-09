using CommunityToolkit.Maui.Markup;
using HelloMaui.Models;
using HelloMaui.ViewModels;


namespace HelloMaui.Pages
{

    public class DetailsPage : BaseContentPage<DetailsViewModel>
    {

        public DetailsPage(DetailsViewModel viewModel) : base(viewModel)
        {
            this.Bind(DetailsPage.TitleProperty, getter: (DetailsViewModel viewModel) => viewModel.LibraryTitle);


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
                        .Bind(Button.CommandProperty,
                            getter: (DetailsViewModel vm) => vm.BackButtonCommand);

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
                        .Bind(Image.SourceProperty, getter: (DetailsViewModel viewModel) => viewModel.LibraryImageSource),

                    new Label()
                        .TextCenter()
                        .Center()
                        .Font(bold: true, size: 24)
                        .Bind(Label.TextProperty, getter: (DetailsViewModel viewModel) => viewModel.LibraryTitle),

                    new Label()
                        .TextCenter()
                        .Center()
                        .Font(italic: true, size: 16)
                        .Bind(Label.TextProperty, getter: (DetailsViewModel viewModel) => viewModel.LibraryDescription),
                    
                    new Button()
                        .Text("Back")
                        .Bind(Button.CommandProperty, getter: (DetailsViewModel viewModel) => viewModel.BackButtonCommand)

                }

            }
            .Center()
            .Padding(12);
        }    

       
    }

}
