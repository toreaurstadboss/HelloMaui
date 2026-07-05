using CommunityToolkit.Maui.Markup;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloMaui
{

    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Content = new Label()
                .Text("Hello MAUI")
                .TextColor(Color.FromArgb("10203030"))
                .FontSize(48)
                .Center()
                .TextCenter();
        }
    }

}
