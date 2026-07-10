using HelloMaui.ViewModels;

namespace HelloMaui.Pages;

public partial class DetailsPage : BaseContentPage<DetailsViewModel>
{
	public DetailsPage(DetailsViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}