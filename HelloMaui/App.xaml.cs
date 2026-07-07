using HelloMaui.Demos.CollectionViews;
using HelloMaui.Demos.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace HelloMaui
{
    public partial class App : Application
    {


        public App()
        {
            try
            {
                InitializeComponent();
              
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                Debug.WriteLine(err);
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new CollectionsViewDemo1());
        }

        protected override void OnSleep()
        {
            Trace.WriteLine("** APP SLEEP-ed **");
        }

        protected override void OnStart()
        {
            Trace.WriteLine("** APP STARTED **");

        }

        protected override void OnResume()
        {
            Trace.WriteLine("** APP RESUMED **");
        }


    }
}