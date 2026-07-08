using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace HelloMaui.Demos.CollectionViews
{

    public class CollectionsViewDemo1 : BaseContentPage, IDisposable
    {
        private bool _hasShownAppearingPopup;
        private bool _hasShownNavigatedToPopup;

        private CollectionView _collectionsView1;

        private ObservableCollection<LibraryModel> MauiLibraries = new();

        private SearchBar _searchBar;

        private readonly Label _selectionStatusLabel = new()
        {
            Text = "Tap an item to see the selected package.",
        };

        private static Uri GetNuGetPackageIconUri(string packageName, string version)
        {
            var uri = new Uri($"https://api.nuget.org/v3-flatcontainer/{packageName.ToLowerInvariant()}/{version}/icon");
            Trace.WriteLine(uri);
            return uri;
        }

        public CollectionsViewDemo1()
        {
            BackgroundColor = Color.FromArgb("#c4c6f5");

            _collectionsView1 = new CollectionView
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Always,

                Header = new SearchBar()
                    .Placeholder("Enter search for Nuget")
                    .AppThemeColorBinding(SearchBar.TextColorProperty, Colors.Black, Colors.White)
                    .Center()
                    .TextCenter()
                    .Behaviors(new UserStoppedTypingBehavior
                    {
                        StoppedTypingTimeThreshold = 1000,
                        ShouldDismissKeyboardAutomatically = true,
                        Command = new Command(() => UserStoppedTyping())
                    })
                    .Assign(out _searchBar),

                SelectionMode = SelectionMode.Single,
                ItemTemplate = new MauiLibraryItemTemplate(),
            };

            foreach (var library in CreateLibraries())
            {
                MauiLibraries.Add(library);
            }

            _collectionsView1.ItemsSource = MauiLibraries;
            _collectionsView1.SelectionChanged += HandleCollectionView_SelectionChanged;

            //var implicitButtonStyle = new Style(typeof(Button));
            //implicitButtonStyle.Setters.Add(new Setter
            //{
            //    Property = Button.BackgroundColorProperty,
            //    Value = Colors.Goldenrod
            //});
            //implicitButtonStyle.Setters.Add(new Setter
            //{
            //    Property = Button.TextColorProperty,
            //    Value = Colors.Purple
            //});

            //collectionView.Resources.Add(implicitButtonStyle);

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
            }.Invoke(refreshView => refreshView.Refreshing += RefreshView_Refreshing)
            .Margins(12, 24, 12, 0);
        }

        private void UserStoppedTyping()
        {
            MauiLibraries.Clear();
            string searchTerm = _searchBar.Text;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                foreach (var library in CreateLibraries())
                {
                    MauiLibraries.Add(library);
                }
                return;
            }
            else
            {
                foreach (var library in CreateLibraries().Where(x => x.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                {
                    MauiLibraries.Add(library);
                }                
            }            
        }

        private async void RefreshView_Refreshing(object? sender, EventArgs e)
        {
            var refreshView = (RefreshView)sender!;

            MauiLibraries.Clear();

            foreach (var library in CreateLibraries())
            {
                MauiLibraries.Add(library);
            }

            if (MauiLibraries.Any(lib => lib.Title == "UraniumUI.Icons.FontAwesome"))
            {
                refreshView.IsRefreshing = false;
                return;
            }

            await Task.Delay(500);

            var moreLibaries = new List<LibraryModel>
            {              

                // UraniumUI packages (all verified to have icons)

                new()
                {
                    Title = "UraniumUI",
                    Description = "Modern UI controls for .NET MAUI.",
                    ImageSource = ImageSource.FromUri(
                        GetNuGetPackageIconUri("UraniumUI", "3.0.0"))
                },
                new()
                {
                    Title = "UraniumUI.Material",
                    Description = "Material-style controls for .NET MAUI.",
                    ImageSource = ImageSource.FromUri(
                        GetNuGetPackageIconUri("UraniumUI.Material", "3.0.0"))
                },               
                new()
                {
                    Title = "UraniumUI.Icons.FontAwesome",
                    Description = "FontAwesome icon set for .NET MAUI.",
                    ImageSource = ImageSource.FromUri(
                        GetNuGetPackageIconUri("UraniumUI.Icons.FontAwesome", "3.0.0"))
                }
            };

            for (int i = 0; i < moreLibaries.Count; i++)
            {
                var lib = moreLibaries[i];
                MauiLibraries.Insert(0,lib);
            }

            _collectionsView1.ItemsSource = moreLibaries;

            refreshView.IsRefreshing = false;

        }

        private async void HandleCollectionView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            ArgumentNullException.ThrowIfNull(sender);

            var collectionView = (CollectionView)sender;

            if (e.CurrentSelection.FirstOrDefault() is LibraryModel libraryModel)
            {
                _selectionStatusLabel.Text = $"Selected: {libraryModel.Title}";
                Debug.WriteLine($"Selected package: {libraryModel.Title}");

                await Toast.Make("Tapped", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            }

            await Task.Delay(2000);
            collectionView.SelectedItem = null;

        }

        public void Dispose()
        {

        }

        static List<LibraryModel> CreateLibraries() => new()
        {

            new()
            {
                Title = "CommunityToolkit.Maui",
                Description = "Extensions, behaviors, converters and helpers for .NET MAUI.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("CommunityToolkit.Maui", "14.2.0"))
            },
            new()
            {
                Title = "CommunityToolkit.Maui.Markup",
                Description = "Build MAUI UIs fluently in C# without XAML.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("CommunityToolkit.Maui.Markup", "7.0.1"))
            },
            new()
            {
                Title = "Prism.Maui",
                Description = "MVVM, navigation and dependency injection framework.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("Prism.Maui", "9.0.537"))
            },
            new()
            {
                Title = "ReactiveUI",
                Description = "Reactive MVVM framework for modern applications.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("ReactiveUI", "23.2.28"))
            },
            new()
            {
                Title = "Refit",
                Description = "Type-safe REST API client generation.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("Refit", "13.1.0"))
            },
            new()
            {
                Title = "MonkeyCache",
                Description = "Simple cross-platform caching for mobile apps.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("MonkeyCache", "2.1.1"))
            },
            new()
            {
                Title = "SQLite-net",
                Description = "Lightweight SQLite access for local storage.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("SQLite-net", "1.6.292"))
            },
            new()
            {
                Title = "UraniumUI",
                Description = "Material-inspired controls and layouts for MAUI.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("UraniumUI", "3.0.0"))
            },
            new()
            {
                Title = "SkiaSharp",
                Description = "2D graphics, charts and custom drawing support.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("SkiaSharp", "4.148.0"))
            },
            new()
            {
                Title = "Syncfusion MAUI",
                Description = "Enterprise-grade charts, grids and UI controls.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("Syncfusion.Maui.Core", "33.2.15"))
            },
            new()
            {
                Title = "CommunityToolkit.Mvvm",
                Description = "Source generators and MVVM helpers for .NET apps.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("CommunityToolkit.Mvvm", "8.4.2"))
            },
            new()
            {
                Title = "Microsoft.Maui.Controls",
                Description = "Core .NET MAUI UI controls and base classes.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("Microsoft.Maui.Controls", "10.0.80"))
            },
            new()
            {
                Title = "Microsoft.Extensions.Logging.Debug",
                Description = "Debug logger provider for development builds.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("Microsoft.Extensions.Logging.Debug", "10.0.9"))
            },
            new()
            {
                Title = "Newtonsoft.Json",
                Description = "Popular JSON serialization library for .NET.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("Newtonsoft.Json", "13.0.4"))
            },
            new()
            {
                Title = "Serilog",
                Description = "Structured logging for .NET applications.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("Serilog", "4.3.1"))
            },
            new()
            {
                Title = "CsvHelper",
                Description = "Fast and flexible CSV reading and writing.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("CsvHelper", "33.1.0"))
            },
            new()
            {
                Title = "Polly",
                Description = "Resilience and transient-fault-handling library.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("Polly", "8.7.0"))
            },
            new()
            {
                Title = "Humanizer",
                Description = "Human-friendly formatting helpers for .NET.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("Humanizer", "3.0.10"))
            },
            new()
            {
                Title = "AutoMapper",
                Description = "Conventions-based object mapping library.",
                ImageSource = ImageSource.FromUri(GetNuGetPackageIconUri("AutoMapper", "16.2.0"))
            }

        };

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


        class LifecyclePopup : Popup
        {
            public LifecyclePopup(string message, PopupPlacement placement)
            {
                var label = new Label()
                        .Text(message)
                        .FontSize(24)
                        .Top();

                if (placement == PopupPlacement.Center)
                {
                    label.Center().TextCenter();
                }
                else
                {
                    label.Top().TextTop();
                }

                Content = new Border
                {
                    Padding = new Thickness(20),
                    Background = Colors.White,
                    Stroke = Colors.LightGray,
                    StrokeThickness = 1,
                    Content = label
                };

                _ = CloseAfterDelayAsync();
            }

            private async Task CloseAfterDelayAsync()
            {
                await Task.Delay(TimeSpan.FromSeconds(new Random().Next(1, 4)));
                await CloseAsync();
            }

        }

    }

}
