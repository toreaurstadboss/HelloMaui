using CommunityToolkit.Maui.Markup;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloMaui
{

    public class HorizontalStackLaoyutDemo : ContentPage
    {
        public HorizontalStackLaoyutDemo()
        {
            var verticalLayout = new VerticalStackLayout()
                .BackgroundColor(Colors.Snow);

            verticalLayout.Children.Add(new Image()
                .Size(300, 185)
                .Aspect(Aspect.AspectFit)
                .Source("dotnet_bot.png"));

            verticalLayout.Children.Add(
                new Label()
                .FontSize(52)
                .Text("Hello MAUI", Colors.DarkBlue));

            verticalLayout.Children.Add(
                new Entry()
                .TextColor(Colors.Black)
                .Placeholder("Notes", Colors.LightGray));

            var horizontalLayout = new HorizontalStackLayout()
            {
                Spacing = 12,
                BackgroundColor = Colors.Green
            };

            horizontalLayout.Children.Add(new Entry()
                .Placeholder("First Entry", Colors.DarkGray)
                .TextColor(Colors.Black));

            horizontalLayout.Children.Add(new Entry()
               .Placeholder("Second Entry", Colors.DarkGray)
               .TextColor(Colors.Black));

            horizontalLayout.Children.Add(new Entry()
               .Placeholder("Third Entry", Colors.DarkGray)
               .TextColor(Colors.Black));

            verticalLayout.Children.Add(horizontalLayout);

            Content = verticalLayout;

        }
    }

}
