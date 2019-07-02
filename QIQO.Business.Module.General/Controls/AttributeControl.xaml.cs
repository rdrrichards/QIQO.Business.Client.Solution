using QIQO.Business.Client.Wrappers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QIQO.Business.Module.General
{
    /// <summary>
    /// Interaction logic for AttributeControl.xaml
    /// </summary>
    public partial class AttributeControl : UserControl
    {
        public AttributeControl()
        {
            InitializeComponent();
        }

        public IEnumerable<EntityAttributeWrapper> Attributes
        {
            get { return (IEnumerable<EntityAttributeWrapper>)GetValue(AttributesProperty); }
            set { SetValue(AttributesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Attributes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AttributesProperty =
            DependencyProperty.Register("Attributes", typeof(IEnumerable<EntityAttributeWrapper>), typeof(AttributeControl));


    }
}
