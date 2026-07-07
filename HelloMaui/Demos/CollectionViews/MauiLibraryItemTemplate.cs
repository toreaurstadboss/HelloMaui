
using CommunityToolkit.Maui.Markup;
using HelloMaui.Helpers;
using Microsoft.Maui.Controls.Shapes;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace HelloMaui.Demos.CollectionViews
{

    public class MauiLibraryItemTemplate : DataTemplate
    {

        const int imageRadius = 36;
        const int imagePadding = 12;

        public MauiLibraryItemTemplate() : base(() => CreateGridTemplate())
        {
        }

        private static Border CreateGridTemplate()
        {
            var verticalBottomPanel = new VerticalStackLayout()
               .Row(Row.Description)
               .Column(Column.Text);

            verticalBottomPanel.Children.Add(new Button().Text("Add to favorites ⭐").Style(MauiStyleHelpers.GetApplicationStyle<Style>("CheesyClassicButton")));
            verticalBottomPanel.Children.Add(new Label()
                        .Margin(new Thickness(12, 0, 0, 0))
                        .TextTop()
                        .TextStart()
                        .Bind(Label.TextProperty,
                            getter: (LibraryModel model) => model.Description,
                            mode: BindingMode.OneWay));

            var itemGrid = new Grid
            {
                RowDefinitions = Rows.Define(
                    (Row.Title, 36),
                    (Row.Description, 76),
                    (Row.BottomPadding, 8)),
                ColumnDefinitions = Columns.Define(
                    (Column.Icon, (imageRadius * 2) + (imagePadding * 2)),
                    (Column.Text, Star)),
                RowSpacing = 4,
                ColumnSpacing = 0,
                Children =
                {
                    new Image()
                        .Row(Row.Title).RowSpan(2)
                        .Column(Column.Icon)
                        .Center()
                        .Aspect(Aspect.AspectFit)
                        .Size(imageRadius * 2)
                        .Bind(Image.SourceProperty,
                            getter: (LibraryModel model) => model.ImageSource,
                            mode: BindingMode.OneWay),

                    new Label
                    {
                        FontSize = 14,
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(12, 6, 0, 2)
                    }
                        .Row(Row.Title)
                        .Column(Column.Text)
                        .TextTop()
                        .TextStart()
                        .TextColor(Colors.HotPink)
                        .Bind(Label.TextProperty,
                            getter: (LibraryModel model) => model.Title,
                            mode: BindingMode.OneWay),

                    verticalBottomPanel
                   
                }
            };        
            
            var itemBorder = new Border
            {
                Background = Colors.White,
                Stroke = Colors.Gainsboro,
                StrokeThickness = 1,
                Padding = 12,
                Margin = new Thickness(0, 4),
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(18)
                },
                Shadow = new Shadow
                {
                    Offset = new Point(0, 4),
                    Radius = 10,
                    Opacity = 0.18f
                },
                Content = itemGrid
            };

            VisualStateManager.SetVisualStateGroups(itemBorder, new VisualStateGroupList
            {
                new VisualStateGroup
                {
                    Name = "CommonStates",
                    States =
                    {
                        new VisualState
                        {
                            Name = "Normal"
                        },
                        new VisualState
                        {
                            Name = "Selected",
                            Setters =
                            {
                                new Setter
                                {
                                    Property = Border.BackgroundProperty,
                                    Value = new SolidColorBrush(Color.FromArgb("#DCEEFF"))
                                },
                                new Setter
                                {
                                    Property = Border.StrokeProperty,
                                    Value = new SolidColorBrush(Color.FromArgb("#4A90E2"))
                                }
                            }
                        }
                    }
                }
            });

            return itemBorder;
        }

        enum Column { Icon, Text }

        enum Row { Title, Description, BottomPadding }

    }

}