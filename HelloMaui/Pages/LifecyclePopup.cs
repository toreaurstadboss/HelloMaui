using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;

namespace HelloMaui.Demos.CollectionViews
{

    public partial class ListPage
    {
        class LifecyclePopup : Popup
        {
            public LifecyclePopup(string message, PopupPlacement placement)
            {
                var label = new Label()
                        .Text(message)
                        .FontSize(24)
                        .Top();

                if (placement == PopupPlacement.Center)
                {
                    label.Center().TextCenter();
                }
                else
                {
                    label.Top().TextTop();
                }

                Content = new Border
                {
                    Padding = new Thickness(20),
                    Background = Colors.White,
                    Stroke = Colors.LightGray,
                    StrokeThickness = 1,
                    Content = label
                };

                _ = CloseAfterDelayAsync();
            }

            private async Task CloseAfterDelayAsync()
            {
                //await Task.Delay(TimeSpan.FromSeconds(new Random().Next(1, 4)));
                //await CloseAsync();
            }

        }

    }

}
