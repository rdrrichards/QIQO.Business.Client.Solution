using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QIQO.Custom.Controls
{
    public class QIQOCalender : Calendar
    {
        static QIQOCalender()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QIQOCalender), new FrameworkPropertyMetadata(typeof(QIQOCalender)));
        }

        public Brush DateBrush
        {
            get { return (Brush)GetValue(DateBrushProperty); }
            set { SetValue(DateBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateBrushProperty =
            DependencyProperty.Register("DateBrush", typeof(Brush), typeof(QIQOCalender));


        // We need a list of dates that will be bold in the calendar
        public ObservableCollection<QIQODate> EventDates
        {
            get { return (ObservableCollection<QIQODate>)GetValue(EventDatesProperty); }
            set { SetValue(EventDatesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EventDates.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventDatesProperty =
            DependencyProperty.Register("EventDates", 
                typeof(ObservableCollection<QIQODate>), 
                typeof(QIQOCalender),
                new FrameworkPropertyMetadata(null, 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault & FrameworkPropertyMetadataOptions.AffectsParentArrange, 
                    new PropertyChangedCallback(EventDatesCollectionChanged), 
                    new CoerceValueCallback(CoerceEventDatesCollection)));

        private static object CoerceEventDatesCollection(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        private static void EventDatesCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var qiqo_cal = d as QIQOCalender;
            if (e.OldValue != null)
            {
                var date_coll = (INotifyCollectionChanged)e.OldValue;
                //date_coll.CollectionChanged -= EventDates_CollectionChanged;
                qiqo_cal.Refresh();
            }

            if (e.NewValue != null)
            {
                var date_coll = (ObservableCollection<QIQODate>)e.NewValue;

                //foreach (var date in date_coll)
                //{
                //    Console.WriteLine(date.Date.ToString());
                //}

                //date_coll.CollectionChanged += EventDates_CollectionChanged;
                qiqo_cal.Refresh();
                //Refresh(qiqo_cal);
            }
        }

        //private static void EventDates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{

        //}

        private void Refresh()
        {
            var realDisplayDate = DisplayDate;
            DisplayDate = DateTime.Today.AddMonths(-1);
            DisplayDate = realDisplayDate;
        }
    }
}
