using CommunityToolkit.Maui.Markup;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloMaui.Demos.Layouts
{

    public class VerticalStackLaoyutDemo : ContentPage
    {
        public VerticalStackLaoyutDemo()
        {
            var verticalLayout = new VerticalStackLayout();

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


            Content = verticalLayout;

        }
    }

}
