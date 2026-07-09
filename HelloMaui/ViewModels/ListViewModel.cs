using CommunityToolkit.Mvvm.Input;
using HelloMaui.Models;
using HelloMaui.Pages;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace HelloMaui.ViewModels
{

    public class ListViewModel : BaseViewModel
    {
        public ObservableCollection<LibraryModel> MauiLibraries = new();

        public ICommand UserStoppedTypingCommand { get; }
        public ICommand RefreshViewRefreshingCommand { get; }
        public ICommand SelectionChangedCommand { get; }

        private bool _isSearchBarEnabled = true;
        private bool _isRefreshViewRefreshing;

        private object? _selectedItem;

        public object? SelectedItem
        {
            get => _selectedItem;
            set =>  SetProperty(ref _selectedItem, value);

        }

        public bool IsRefreshViewRefreshing
        {
            get => _isRefreshViewRefreshing;
            set => SetProperty(ref _isRefreshViewRefreshing, value);
        }

        public bool IsSearchBarEnabled
        {
            get => _isSearchBarEnabled;
            set => SetProperty(ref _isSearchBarEnabled, value);
        }

        private string? _searchBarText = string.Empty;

        public string? SearchBarText
        {
            get => _searchBarText;
            set => SetProperty(ref _searchBarText, value);
        }

        public ListViewModel()
        {
            UserStoppedTypingCommand = new RelayCommand(() => UserStoppedTyping());
            RefreshViewRefreshingCommand = new AsyncRelayCommand(async () =>
            {
                await RefreshViewRefreshing();
            });

            SelectionChangedCommand = new AsyncRelayCommand(async () =>
            {
                await HandleSelectionChanged();
            });

        }

        private async Task RefreshViewRefreshing()
        {

            try
            {
                IsSearchBarEnabled = true;
                //var refreshView = (RefreshView)sender!;

                MauiLibraries.Clear();

                foreach (var library in CreateLibraries())
                {
                    MauiLibraries.Add(library);
                }


                if (MauiLibraries.Any(lib => lib.Title == "UraniumUI.Icons.FontAwesome"))
                {

                   IsRefreshViewRefreshing = false;
                    IsSearchBarEnabled = true;
                    return;
                }

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
            }.OrderBy(x => x.Title).ToList();

                await Task.Delay(500);

                for (int i = 0; i < moreLibaries.Count; i++)
                {
                    var lib = moreLibaries[i];
                    MauiLibraries.Insert(0, lib);
                }



                IsSearchBarEnabled = true;

                IsRefreshViewRefreshing = false;

            }
            finally
            {
                IsRefreshViewRefreshing = false;
                IsSearchBarEnabled = true;
            }

        }

        static List<LibraryModel> CreateLibraries() => CreateNugetList().OrderBy(x => x.Title).ToList();

        private static Uri GetNuGetPackageIconUri(string packageName, string version)
        {
            var uri = new Uri($"https://api.nuget.org/v3-flatcontainer/{packageName.ToLowerInvariant()}/{version}/icon");
            Trace.WriteLine(uri);
            return uri;
        }

        private static List<LibraryModel> CreateNugetList()
        {
            return new()
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
        }

        private async Task HandleSelectionChanged()
        {
            if (SelectedItem is LibraryModel libraryModel)
            {
                await Shell.Current.GoToAsync(AppShell.GetRoute<DetailsPage>(), new Dictionary<string, object>
                {
                    { DetailsViewModel.LibraryModelKey, libraryModel }
                });
            }

            SelectedItem = null;
        }


        private void UserStoppedTyping()
        {
            MauiLibraries.Clear();

            if (string.IsNullOrWhiteSpace(SearchBarText))
            {
                foreach (var library in CreateLibraries())
                {
                    MauiLibraries.Add(library);
                }
                return;
            }
            else
            {
                foreach (var library in CreateLibraries().Where(x => x.Title.Contains(SearchBarText, StringComparison.OrdinalIgnoreCase)))
                {
                    MauiLibraries.Add(library);
                }
            }
        }

    }
}
