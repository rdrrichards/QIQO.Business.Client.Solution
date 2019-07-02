
using System.ServiceModel;
using System.Threading;

namespace QIQO.Business.Client.Core.UI
{
    public abstract class UserClientBase<T> : ClientBase<T> where T : class
    {
        public UserClientBase()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            var header = new MessageHeader<string>(userName);

            var contextScope =
                            new OperationContextScope(InnerChannel);

            OperationContext.Current.OutgoingMessageHeaders.Add(
                                      header.GetUntypedHeader("String", "System"));
        }
    }
}
