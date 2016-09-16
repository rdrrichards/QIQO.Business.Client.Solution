using QIQO.Business.Client.Wrappers;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Orders.Services
{
    public interface IWorkingOrderService
    {
        OrderWrapper GetOrder(string order_key);
        bool OpenOrder(OrderWrapper order);
        ObservableCollection<OrderWrapper> GetWorkingOrders();
        bool CloseOrder(OrderWrapper order);
    }
}