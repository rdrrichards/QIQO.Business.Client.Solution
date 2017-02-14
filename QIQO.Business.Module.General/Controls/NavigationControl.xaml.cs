using System.Windows;
using System.Windows.Controls;

namespace QIQO.Business.Module.General
{
    /// <summary>
    /// Interaction logic for NavigationControl.xaml
    /// </summary>
    public partial class NavigationControl : UserControl
    {
        public NavigationControl()
        {
            InitializeComponent();
        }

        public string ButtonImage
        {
            get { return (string)GetValue(ButtonImageProperty); }
            set { SetValue(ButtonImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonImageProperty =
            DependencyProperty.Register("ButtonImage", typeof(string), typeof(NavigationControl), 
                new PropertyMetadata("/QIQO.Business.Client.Core.UI;component/Images/48/individual.png"));

        public string ButtonLabel
        {
            get { return (string)GetValue(ButtonLabelProperty); }
            set { SetValue(ButtonLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonLabelProperty =
            DependencyProperty.Register("ButtonLabel", typeof(string), typeof(NavigationControl), new PropertyMetadata("Click Me!"));


    }
}
