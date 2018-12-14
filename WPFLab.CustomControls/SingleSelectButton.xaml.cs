using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFLab.CustomControls
{
    /// <summary>
    /// Interaction logic for SingleSelectButton.xaml
    /// </summary>
    public partial class SingleSelectButton : UserControl
    {
        OverlayAdorner _adorner;
        
        public SingleSelectButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty DefaultLabelProperty = DependencyProperty.Register("DefaultLabel", typeof(string), typeof(SingleSelectButton));
        public string DefaultLabel
        {
            get { return (string)GetValue(DefaultLabelProperty); }
            set { SetValue(DefaultLabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(SingleSelectButton));
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty OverlayProperty = DependencyProperty.Register("Overlay", typeof(object), typeof(SingleSelectButton));
        public object Overlay
        {
            get { return GetValue(OverlayProperty); }
            set { SetValue(OverlayProperty, value); }
        }

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SingleSelectButton));
        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        void RaiseValueChangedEvent(ToggleButton singleSelectBtn)
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ValueChangedEvent, singleSelectBtn);
            RaiseEvent(newEventArgs);
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button)
            {
                ToggleButton singleSelectBtn = (ToggleButton)Template.FindName("btn", this);

                _adorner.Content = null;
                AdornerLayer.GetAdornerLayer(Parent as UIElement).Remove(_adorner);
                singleSelectBtn.Content = ((Button)e.OriginalSource).Content;
                Label = (string)((Button)e.OriginalSource).Content;
                singleSelectBtn.IsChecked = false;

                RaiseValueChangedEvent(singleSelectBtn);
            }
        }

        private void Btn_Checked(object sender, RoutedEventArgs e)
        {
            SolidColorBrush overlayBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            overlayBrush.Opacity = 0.5;

            FrameworkElement overlay = (FrameworkElement)Overlay;
            overlay.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(button_click));
            overlay.AddHandler(Button.ClickEvent, new RoutedEventHandler(button_click));

            _adorner = new OverlayAdorner(Parent as UIElement)
            {
                Content = overlay,
            };
            AdornerLayer.GetAdornerLayer(Parent as UIElement).Add(_adorner);
        }
    }
}
