using System.Diagnostics;

namespace HelloMaui
{
    public partial class App : Application
    {
        public AppShell Shell { get; }

        public App(AppShell shell)
        {
            InitializeComponent();
            Shell = shell;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(Shell);
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