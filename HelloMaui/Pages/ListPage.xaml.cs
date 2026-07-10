
using CommunityToolkit.Maui.Alerts;
using HelloMaui.ViewModels;

namespace HelloMaui.Pages;

public partial class ListPage : BaseContentPage<ListViewModel>
{

	public ListPage(ListViewModel viewmodel) : base(viewmodel)
	{
		InitializeComponent();
	}

    async void HandleSearchBarDoubleTapped(object? sender, EventArgs e)
	{
		await Toast.Make(".NET MAUI Rules!").Show();
	}

}