using HelloMaui.Demos.CollectionViews;
using HelloMaui.Pages;

namespace HelloMaui
{

    public class AppShell : Shell
    {
     
        public AppShell(IServiceProvider serviceProvider)
        {
            Items.Add(new ShellContent
            {
                ContentTemplate = new DataTemplate(() => serviceProvider.GetRequiredService<ListPage>())
            });

            CreateRoutes();
        }

        public static string GetRoute<T>() where T : BaseContentPage
        {
            if (typeof(T) == typeof(DetailsPage))
            {
                return $"//{nameof(ListPage)}/{nameof(DetailsPage)}";

            }
            else if (typeof(T) == typeof(ListPage))
            {
                return $"//{nameof(ListPage)}";
            }
            else
            {
                throw new NotImplementedException($"Route for {typeof(T).Name} is not defined in the routing table.");
            }
        }

        static void CreateRoutes()
        {
            Routing.RegisterRoute(GetRoute<ListPage>(), typeof(ListPage));
            Routing.RegisterRoute(GetRoute<DetailsPage>(), typeof(DetailsPage));
        }

    }
}
