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
            return new Window(new MainPage());
        }
    }
}