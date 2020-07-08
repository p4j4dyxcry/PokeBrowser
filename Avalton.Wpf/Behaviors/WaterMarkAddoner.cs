using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Avalton.Wpf.Behaviors
{
    internal class WatermarkAdorner : Adorner
    {
        private readonly ContentPresenter _contentPresenter;

        public WatermarkAdorner(TextBox adornedElement, object watermark) :
            base(adornedElement)
        {
            IsHitTestVisible = false;

            if (watermark is string str)
            {
                watermark = new TextBlock()
                {
                    Foreground = adornedElement.Foreground,
                    Margin = adornedElement.Padding,
                    Text = str,
                };
            }

            _contentPresenter = new ContentPresenter
            {
                Content = watermark,
                VerticalAlignment = VerticalAlignment.Center,
                Opacity = 0.25,
            };

            var binding = new Binding(nameof(IsVisible))
            {
                Source = adornedElement,
                Converter = new BooleanToVisibilityConverter()
            };
            SetBinding(VisibilityProperty, binding);
        }

        protected override int VisualChildrenCount => 1;

        private Control Control => AdornedElement as Control;

        protected override Visual GetVisualChild(int index)
        {
            return _contentPresenter;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _contentPresenter.Measure(Control.RenderSize);
            return Control.RenderSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _contentPresenter.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }
}