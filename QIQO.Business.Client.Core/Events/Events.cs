using Prism.Events;
using QIQO.Custom.Controls;
using System.Collections.Generic;

namespace QIQO.Business.Client.Core
{
    public class OrderUpdatedEvent : PubSubEvent<string> { }
    public class OrderDeletedEvent : PubSubEvent<string> { }
    public class OrderLoadedEvent : PubSubEvent<string> { }
    public class OrderUnloadingEvent : PubSubEvent<object> { }
    //public class OrderNewOrderAddEvent : PubSubEvent<string> { }
    //public class OrderNewOrderCancelEvent : PubSubEvent<string> { }
    //public class OrderNewOrderCompleteEvent : PubSubEvent<string> { }
    public class OpenOrderServiceEvent : PubSubEvent<int> { }
    //public class OrderErrorEvent : PubSubEvent<string> { }

    //public class ProductUpdatedEvent : PubSubEvent<string> { }
    public class ProductDeletedEvent : PubSubEvent<string> { }
    public class ProductLoadedEvent : PubSubEvent<string> { }
    public class ProductNewProductAddEvent : PubSubEvent<string> { }
    public class ProductNewProductCancelEvent : PubSubEvent<string> { }
    public class ProductNewProductCompleteEvent : PubSubEvent<string> { }

    public class AccountUpdatedEvent : PubSubEvent<string> { }
    public class AccountDeletedEvent : PubSubEvent<string> { }
    public class AccountLoadedEvent : PubSubEvent<string> { }
    public class AccountNewOrderEvent : PubSubEvent<string> { }
    public class RecentAccountServiceEvent : PubSubEvent<int> { }
    public class OpenAccountServiceEvent : PubSubEvent<int> { }

    //public class CompanyUpdatedEvent : PubSubEvent<string> { }
    public class CompanyDeletedEvent : PubSubEvent<string> { }
    public class CompanyLoadedEvent : PubSubEvent<string> { }
    public class CompanySavedEvent : PubSubEvent<string> { }

    public class GeneralErrorEvent : PubSubEvent<string> { }
    public class GeneralMessageEvent : PubSubEvent<string> { }

    public class CalendarContextChangedEvent : PubSubEvent<IEnumerable<QIQODate>> { }
    public class InvoiceUnloadingEvent : PubSubEvent<object> { }
    public class InvoiceLoadedEvent : PubSubEvent<string> { }
    public class InvoiceDeletedEvent : PubSubEvent<string> { }
    public class InvoiceUpdatedEvent : PubSubEvent<string> { }
    public class OpenInvoiceServiceEvent : PubSubEvent<int> { }
    public class NavigationEvent : PubSubEvent<string> { }
}
