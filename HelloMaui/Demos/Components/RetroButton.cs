using Microsoft.Maui.Controls.Shapes;

namespace YourApp.Controls;

/// <summary>
/// A retro-inspired button with a simple 3D effect.
/// </summary>
public sealed class RetroButton : ContentView
{
    private readonly Border _face;
    private readonly Border _depth;
    private readonly Label _label;

    /// <summary>
    /// Occurs when the button is clicked.
    /// </summary>
    public event EventHandler? Clicked;

    /// <summary>
    /// Gets or sets the button text.
    /// </summary>
    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Gets or sets the text color.
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the font size.
    /// </summary>
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the font attributes.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal text alignment.
    /// </summary>
    public TextAlignment HorizontalTextAlignment
    {
        get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
        set => SetValue(HorizontalTextAlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the vertical text alignment.
    /// </summary>
    public TextAlignment VerticalTextAlignment
    {
        get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
        set => SetValue(VerticalTextAlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the text padding.
    /// </summary>
    public Thickness TextPadding
    {
        get => (Thickness)GetValue(TextPaddingProperty);
        set => SetValue(TextPaddingProperty, value);
    }

    public static readonly BindableProperty FaceBrushProperty =
    BindableProperty.Create(
        nameof(FaceBrush),
        typeof(Brush),
        typeof(RetroButton),
        CreateDefaultFaceBrush(),
        propertyChanged: (b, _, n) =>
            ((RetroButton)b)._face.Background = (Brush)n);

    public static readonly BindableProperty DepthBrushProperty =
        BindableProperty.Create(
            nameof(DepthBrush),
            typeof(Brush),
            typeof(RetroButton),
            CreateDefaultDepthBrush(),
            propertyChanged: (b, _, n) =>
                ((RetroButton)b)._depth.Background = (Brush)n);

    private static Brush CreateDefaultFaceBrush() =>
    new LinearGradientBrush(
    [
        new GradientStop(Color.FromArgb("#F1F1F1"), 0.0f),
        new GradientStop(Color.FromArgb("#DADADA"), 0.5f),
        new GradientStop(Color.FromArgb("#C2C2C2"), 1.0f)
    ],
    new Point(0.2, 0),
    new Point(1, 1));

    private static Brush CreateDefaultDepthBrush() =>
        new LinearGradientBrush(
        [
            new GradientStop(Color.FromArgb("#8A8A8A"), 0.0f),
            new GradientStop(Color.FromArgb("#666666"), 1.0f)
        ],
        new Point(0, 0),
        new Point(1, 1));

    /// <summary>
    /// Gets or sets the brush used for the button face.
    /// </summary>
    public Brush FaceBrush
    {
        get => (Brush)GetValue(FaceBrushProperty);
        set => SetValue(FaceBrushProperty, value);
    }

    /// <summary>
    /// Gets or sets the brush used for the button depth.
    /// </summary>
    public Brush DepthBrush
    {
        get => (Brush)GetValue(DepthBrushProperty);
        set => SetValue(DepthBrushProperty, value);
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(RetroButton),
            null,
            propertyChanged: (b, _, n) =>
                ((RetroButton)b)._label.Text = (string?)n);

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(RetroButton),
            Colors.Black,
            propertyChanged: (b, _, n) =>
                ((RetroButton)b)._label.TextColor = (Color)n);

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(
            nameof(FontSize),
            typeof(double),
            typeof(RetroButton),
            new Label().FontSize,
            propertyChanged: (b, _, n) =>
                ((RetroButton)b)._label.FontSize = (double)n);

    public static readonly BindableProperty FontAttributesProperty =
        BindableProperty.Create(
            nameof(FontAttributes),
            typeof(FontAttributes),
            typeof(RetroButton),
            FontAttributes.None,
            propertyChanged: (b, _, n) =>
                ((RetroButton)b)._label.FontAttributes = (FontAttributes)n);

    public static readonly BindableProperty HorizontalTextAlignmentProperty =
        BindableProperty.Create(
            nameof(HorizontalTextAlignment),
            typeof(TextAlignment),
            typeof(RetroButton),
            TextAlignment.Center,
            propertyChanged: (b, _, n) =>
                ((RetroButton)b)._label.HorizontalTextAlignment = (TextAlignment)n);

    public static readonly BindableProperty VerticalTextAlignmentProperty =
        BindableProperty.Create(
            nameof(VerticalTextAlignment),
            typeof(TextAlignment),
            typeof(RetroButton),
            TextAlignment.Center,
            propertyChanged: (b, _, n) =>
                ((RetroButton)b)._label.VerticalTextAlignment = (TextAlignment)n);

    public static readonly BindableProperty TextPaddingProperty =
        BindableProperty.Create(
            nameof(TextPadding),
            typeof(Thickness),
            typeof(RetroButton),
            new Thickness(24, 12),
            propertyChanged: (b, _, n) =>
                ((RetroButton)b)._label.Padding = (Thickness)n);

    public RetroButton()
    {
        _label = new Label();

        _face = new Border
        {
            Background = CreateDefaultFaceBrush(),
            Stroke = Color.FromArgb("#C77700"),
            StrokeThickness = 2,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(14)
            },
            Content = _label
        };

        _depth = new Border
        {
            Background = CreateDefaultDepthBrush(),
            StrokeThickness = 0,
            Margin = new Thickness(0, 6, 0, 0),
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(14)
            }
        };

        var tap = new TapGestureRecognizer();

        tap.Tapped += async (_, _) =>
        {
            await _face.TranslateToAsync(0, 6, 50);
            await Task.Delay(40);
            await _face.TranslateToAsync(0, 0, 50);

            Clicked?.Invoke(this, EventArgs.Empty);
        };

        _face.GestureRecognizers.Add(tap);

        Content = new Grid
        {
            Children =
            {
                _depth,
                _face,
            }
        };
    }
}