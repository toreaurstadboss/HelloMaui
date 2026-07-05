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
                .Text("This is a label")
                .Center()
                .TextCenter();
        }
    }

}
