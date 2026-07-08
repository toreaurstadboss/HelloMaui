namespace HelloMaui
{

    public class AppShell : Shell
    {
        public AppShell(IServiceProvider serviceProvider)
        {
            Items.Add(new ShellContent
            {
                ContentTemplate = new DataTemplate(() => serviceProvider.GetRequiredService<MainMenuPage>())
            });
        }
    }
}
