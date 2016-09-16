using QIQO.Business.Client.Wrappers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QIQO.Business.Module.General
{
    /// <summary>
    /// Interaction logic for FeeScheduleControl.xaml
    /// </summary>
    public partial class FeeScheduleControl : UserControl
    {
        public FeeScheduleControl()
        {
            InitializeComponent();
        }

        public IEnumerable<FeeScheduleWrapper> FeeSchedules
        {
            get { return (IEnumerable<FeeScheduleWrapper>)GetValue(FeeSchedulesProperty); }
            set { SetValue(FeeSchedulesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FeeSchedules.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FeeSchedulesProperty =
            DependencyProperty.Register("FeeSchedules", typeof(IEnumerable<FeeScheduleWrapper>), typeof(FeeScheduleControl));


    }
}
