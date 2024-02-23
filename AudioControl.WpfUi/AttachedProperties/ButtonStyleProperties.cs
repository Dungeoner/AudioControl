using System.Windows.Media;
using System.Windows;

namespace AudioControl.WpfUi.AttachedProperties
{
    public static class ButtonStyleProperties
    {
        private const string IconPropertyName = "Icon";
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(IconPropertyName,
                                                typeof(Geometry),
                                                typeof(ButtonStyleProperties),
                                                new(default(Geometry)));

        public static void SetIcon(UIElement target, Geometry value)
            => target.SetValue(IconProperty, value);

        public static Geometry GetIcon(UIElement target)
            => (Geometry)target.GetValue(IconProperty);

        public static readonly DependencyProperty OnMoouseOverBackgroundProperty =
        DependencyProperty.RegisterAttached("OnMouseOverBackground",
                                            typeof(Brush),
                                            typeof(ButtonStyleProperties),
                                            new(default(Brush)));
        public static Brush GetOnMouseOverBackground(UIElement target) =>
                (Brush)target.GetValue(OnMoouseOverBackgroundProperty);

        public static void SetOnMouseOverBackground(UIElement target, Brush value) =>
            target.SetValue(OnMoouseOverBackgroundProperty, value);

        public static readonly DependencyProperty OnMoouseOverForegroundProperty =
        DependencyProperty.RegisterAttached("OnMoouseOverForeground",
                                            typeof(Brush),
                                            typeof(ButtonStyleProperties),
                                            new(default(Brush)));

        public static Brush GetOnMoouseOverForeground(UIElement target) =>
                (Brush)target.GetValue(OnMoouseOverForegroundProperty);

        public static void SetOnMoouseOverForeground(UIElement target, Brush value) =>
            target.SetValue(OnMoouseOverForegroundProperty, value);

        public static readonly DependencyProperty OnPressedBackgroundProperty =
        DependencyProperty.RegisterAttached("OnPressedBackground",
                                            typeof(Brush),
                                            typeof(ButtonStyleProperties),
                                            new(default(Brush)));

        public static Brush GetOnPressedBackground(UIElement target) =>
                (Brush)target.GetValue(OnMoouseOverBackgroundProperty);

        public static void SetOnPressedBackground(UIElement target, Brush value) =>
            target.SetValue(OnMoouseOverBackgroundProperty, value);

        public static readonly DependencyProperty OnPressedForegroundProperty =
        DependencyProperty.RegisterAttached("OnPressedForeground",
                                            typeof(Brush),
                                            typeof(ButtonStyleProperties),
                                            new(default(Brush)));

        public static Brush GetOnPressedForeground(UIElement target) =>
                (Brush)target.GetValue(OnMoouseOverForegroundProperty);

        public static void SetOnPressedForeground(UIElement target, Brush value) =>
            target.SetValue(OnMoouseOverForegroundProperty, value);

        public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.RegisterAttached("CornerRadius",
                                            typeof(CornerRadius),
                                            typeof(ButtonStyleProperties),
                                            new(default(CornerRadius)));

        public static CornerRadius GetCornerRadius(UIElement target) =>
                (CornerRadius)target.GetValue(CornerRadiusProperty);

        public static void SetCornerRadius(UIElement target, CornerRadius value) =>
            target.SetValue(CornerRadiusProperty, value);
    }
}
