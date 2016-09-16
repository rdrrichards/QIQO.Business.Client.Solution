using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QIQO.Business.Module.General
{
    /// <summary>
    /// Interaction logic for Address.xaml
    /// </summary>
    public partial class AddressControl : UserControl
    {
        public AddressControl()
        {
            InitializeComponent();
        }

        public IEnumerable<AddressPostal> States
        {
            get { return (IEnumerable<AddressPostal>)GetValue(StatesProperty); }
            set { SetValue(StatesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for States.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatesProperty =
            DependencyProperty.Register("States", typeof(IEnumerable<AddressPostal>), typeof(AddressControl));

        public AddressWrapper Address
        {
            get { return (AddressWrapper)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Address.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register("Address", typeof(AddressWrapper), typeof(AddressControl));


    }
}
