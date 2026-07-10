//using CommunityToolkit.Maui.Markup;
//using HelloMaui.Pages;


//namespace HelloMaui;

//public class MainMenuPage : ContentPage
//{
//    public MainMenuPage()
//    {
//        Title = "Main Menu";

//        var openDemoButton = new Button()
//            .Text("Open CollectionsViewDemo1");

//        openDemoButton.Clicked += async (_, _) =>
//        {
//            await Navigation.PushAsync(new ListPage(new ViewModels.ListViewModel(null)));
//        };

//        Content = new VerticalStackLayout
//        {
//            Spacing = 16,
//            Padding = new Thickness(24),
//            VerticalOptions = LayoutOptions.Center,
//            HorizontalOptions = LayoutOptions.Fill,
//            Children =
//            {
//                new Label()
//                    .Text("Hello Maui")
//                    .FontSize(28)
//                    .TextCenter(),

//                openDemoButton
//            }
//        };
//    }
//}