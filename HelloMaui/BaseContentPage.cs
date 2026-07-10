using HelloMaui.ViewModels;

namespace HelloMaui
{

    public abstract class BaseContentPage<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {

        public BaseContentPage(TViewModel viewModel)
        {
            SafeAreaEdges = SafeAreaEdges.All;
            BindingContext = viewModel;
        }

    }
}
