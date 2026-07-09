using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Markup;
using HelloMaui.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace HelloMaui.Demos.CollectionViews
{

    public partial class ListPage : BaseContentPage<ListViewModel>, IDisposable
    {
        private bool _hasShownAppearingPopup;
        private bool _hasShownNavigatedToPopup;

        private CollectionView _collectionsView1;

        private SearchBar _searchBar;

        private Grid _searchBarWrapper;       

        private readonly Label _selectionStatusLabel = new()
        {
            Text = "Tap an item to see the selected package.",
        };       

        public ListPage(ListViewModel viewModel) : base(viewModel)
        {
            BackgroundColor = Color.FromArgb("#c4c6f5");
           
            _searchBarWrapper = new Grid().BackgroundColor(Colors.Snow).Padding(4);

            var searchBar = new SearchBar()
                .Placeholder("Enter search for Nuget")
                .AppThemeColorBinding(SearchBar.TextColorProperty, Colors.Black, Colors.White)
                .Center()
                .TextCenter()

                .Behaviors(new UserStoppedTypingBehavior
                {
                    StoppedTypingTimeThreshold = 1000,
                    ShouldDismissKeyboardAutomatically = true,
                    BindingContext = viewModel
                }
                .Bind(UserStoppedTypingBehavior.CommandProperty, getter: (ListViewModel vm) => vm.UserStoppedTypingCommand))
                .Bind(SearchBar.IsEnabledProperty, getter: (ListViewModel vm) => vm.IsSearchBarEnabled)
                .Bind(SearchBar.TextProperty, getter: (ListViewModel vm) => vm.SearchBarText,
                setter: (ListViewModel vm, string? text) => vm.SearchBarText = text)
                .Assign(out _searchBar);

            // Add SearchBar to wrapper
            _searchBarWrapper.Add(searchBar);

            // Add tap/mouse click gesture to wrapper (works on emulator)
            _searchBarWrapper.GestureRecognizers.Add(new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new Command(async () =>
                {
                    await Toast.Make("Mouse click or tap detected!",
                        CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
                })
            });

            // Add your double‑tap gesture too (still works on real devices)
            _searchBarWrapper.GestureRecognizers.Add(new TapGestureRecognizer
            {
                NumberOfTapsRequired = 2,
                Command = new Command(async () =>
                {
                    await Toast.Make("You double tapped man! .NET MAUI rules!",
                        CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
                })
            });

            // Now use the wrapper as the CollectionView header
            _collectionsView1 = new CollectionView
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Always,
                Header = _searchBarWrapper,
                SelectionMode = SelectionMode.Single,
                ItemTemplate = new MauiLibraryItemTemplate(),
            };

            _collectionsView1.Bind(CollectionView.ItemsSourceProperty, getter: (ListViewModel vm) => vm.MauiLibraries);
            _collectionsView1.Bind(CollectionView.SelectionChangedCommandProperty, getter: (ListViewModel vm) => vm.SelectionChangedCommand);
            _collectionsView1.Bind(CollectionView.SelectedItemProperty, getter: (ListViewModel vm) => vm.SelectedItem,
                setter: (ListViewModel vm, object? selectedItem) => vm.SelectedItem = selectedItem);

            var statusChip = new Border
            {
                Background = new SolidColorBrush(Color.FromArgb("#EEF5FF")),
                Stroke = new SolidColorBrush(Color.FromArgb("#A8C7FF")),
                StrokeThickness = 1,
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(18)
                },
                Shadow = new Shadow
                {
                    Offset = new Point(0, 2),
                    Radius = 6,
                    Opacity = 0.12f
                },
                Padding = new Thickness(14, 10),
                Content = _selectionStatusLabel
                    .Font(size: 12)
                    .TextCenter()
                    .TextColor(Color.FromArgb("#1F3B63"))
            };

            var grid = new Grid
            {
                RowDefinitions = Rows.Define(
                    (Row.Header, Auto),
                    (Row.List, Star),
                    (Row.Status, Auto)),
                RowSpacing = 12,
                Padding = new Thickness(16, 12),
                Children =
                {
                    _collectionsView1.Row(Row.List),
                    statusChip.Row(Row.Status)
                }
            };

            Content = new RefreshView
            {
                Content = grid
            }
            .Bind(RefreshView.IsRefreshingProperty, getter: (ListViewModel vm) => vm.IsRefreshViewRefreshing,
                setter: (ListViewModel vm, bool isRefreshing) =>  vm.IsRefreshViewRefreshing = isRefreshing)
            .Bind(RefreshView.CommandProperty, getter: (ListViewModel vm) => vm.RefreshViewRefreshingCommand)
            .Margins(12, 24, 12, 0);
        }  

        public void Dispose()
        {

        }       

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_hasShownAppearingPopup)
            {
                return;
            }

            _hasShownAppearingPopup = true;
            // this.ShowPopup(new LifecyclePopup("OnAppearing", PopupPlacement.Center));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.ShowPopup(new LifecyclePopup("OnDisappearing", PopupPlacement.Center));
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            if (_hasShownNavigatedToPopup)
            {
                return;
            }

            _hasShownNavigatedToPopup = true;
            //  this.ShowPopup(new LifecyclePopup("OnNavigatedTo", PopupPlacement.Top));
        }

        protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
        {
            base.OnNavigatedFrom(args);
            // this.ShowPopup(new LifecyclePopup("OnNavigatedFrom", PopupPlacement.Top));

        }

        enum Row { Header, List, Status }

        enum PopupPlacement { Top, Center }

    }

}
