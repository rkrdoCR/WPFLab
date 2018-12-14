using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFLab.CustomControls
{
    public class OverlayAdorner : Adorner
    {
        private FrameworkElement _content;

        public OverlayAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return _content == null ? 0 : 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0) throw new ArgumentOutOfRangeException();
            return _content;
        }

        public FrameworkElement Content
        {
            get { return _content; }
            set
            {
                if (_content != null)
                {
                    RemoveVisualChild(_content);
                }
                _content = value;
                if (_content != null)
                {
                    AddVisualChild(_content);
                }
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _content.Arrange(new Rect(new Point(0, 0), finalSize));
            return base.ArrangeOverride(finalSize);
        }
    }
}
