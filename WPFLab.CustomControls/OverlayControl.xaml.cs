using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WPFLab.CustomControls
{
    /// <summary>
    /// Interaction logic for OverlayControl.xaml
    /// </summary>
    public partial class OverlayControl : UserControl
    {
        public OverlayControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SourceCollectionProperty = DependencyProperty.Register("SourceCollection", typeof(IEnumerable<object>), typeof(OverlayControl));
        public IEnumerable<object> SourceCollection
        {
            get { return (IEnumerable<object>)GetValue(SourceCollectionProperty); }
            set { SetValue(SourceCollectionProperty, value); }
        }
    }
}
