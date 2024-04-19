using System.Windows;

namespace AudioControl.WpfUi.Core.Controls
{
    public class RoundedButton : IconButton
    {
        static RoundedButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundedButton),
               new FrameworkPropertyMetadata(typeof(RoundedButton)));
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius),
                                                typeof(CornerRadius),
                                                typeof(RoundedButton));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
    }
}
