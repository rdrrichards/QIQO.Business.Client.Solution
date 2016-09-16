using Prism.Events;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Wrappers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace QIQO.Business.Module.Orders.Services
{
    public class WorkingOrderService : IWorkingOrderService
    {
        private readonly IEventAggregator event_aggregator;

        private Dictionary<string, OrderWrapper> open_orders;
        public WorkingOrderService(IEventAggregator event_aggtr)
        {
            event_aggregator = event_aggtr;
            Initialize();
        }

        private void Initialize()
        {
            open_orders = new Dictionary<string, OrderWrapper>();
        }

        public OrderWrapper GetOrder(string order_key)
        {
            if (open_orders.ContainsKey(order_key))
                return open_orders[order_key];
            else
                return null;
        }

        public bool OpenOrder(OrderWrapper order)
        {
            if (!open_orders.ContainsValue(order))
            {
                string new_key = GenOrderKey();
                order.OrderNumber = new_key;
                open_orders.Add(new_key, order);
                event_aggregator.GetEvent<OpenOrderServiceEvent>().Publish(open_orders.Count);
                return true;
            }
            return false;
        }

        public bool CloseOrder(OrderWrapper order)
        {
            if (open_orders.ContainsValue(order))
            {
                var key = open_orders.FirstOrDefault(x => x.Value == order).Key;
                open_orders.Remove(key);
                event_aggregator.GetEvent<OpenOrderServiceEvent>().Publish(open_orders.Count);
                return true;
            }
            return false;
        }

        public ObservableCollection<OrderWrapper> GetWorkingOrders()
        {
            var working_ords = new ObservableCollection<OrderWrapper>();
            foreach (var order_entry in open_orders)
            {
                working_ords.Add(order_entry.Value);
            }
            return working_ords;
        }

        private string GenOrderKey()
        {
            return $"Open Order {open_orders.Count + 1}";
        }

        //private Order InitNewOrder()
        //{
        //    return new Order()
        //    {
        //        OrderEntryDate = DateTime.Now,
        //        OrderStatusDate = DateTime.Now,
        //        DeliverByDate = DateTime.Now.AddDays(7)
        //    };
        //}
    }
}
