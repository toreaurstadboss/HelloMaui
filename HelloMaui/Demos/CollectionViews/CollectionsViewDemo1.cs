using CommunityToolkit.Maui.Alerts; 
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace HelloMaui.Demos.CollectionViews
{

    public class CollectionsViewDemo1 : BaseContentPage, IDisposable
    {
        private readonly Label _selectionStatusLabel = new()
        {
            Text = "Tap an item to see the selected package.",
        };

        private static Uri GetNuGetPackageIconUri(string packageName, string version)
        {
            return new Uri($"https://api.nuget.org/v3-flatcontainer/{packageName.ToLowerInvariant()}/{version}/icon");
        }

        public CollectionsViewDemo1()
        {
            BackgroundColor = Colors.Snow;

            var collectionView = new CollectionView
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Always,

                Header = new Label()
                    .Text("MAUI Nuget libraries & Others")
                    .Paddings(bottom: 8)
                    .Font(size: 24, family: "RobotoFlex")
                    .Center()
                    .TextCenter(),

                SelectionMode = SelectionMode.Single,
                ItemTemplate = new MauiLibraryItemTemplate(),
            };

            collectionView.ItemsSource = MauiLibraries;
            collectionView.SelectionChanged += HandleCollectionView_SelectionChanged;

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

            Content = new Grid
            {
                RowDefinitions = Rows.Define(
                    (Row.Header, Auto),
                    (Row.List, Star),
                    (Row.Status, Auto)),
                RowSpacing = 12,
                Padding = new Thickness(16, 12),
                Children =
                {
                    collectionView.Row(Row.List),
                    statusChip.Row(Row.Status)
                }
            };
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

        ObservableCollection<LibraryModel> MauiLibraries = new()
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

        enum Row { Header, List, Status }

    }

}
