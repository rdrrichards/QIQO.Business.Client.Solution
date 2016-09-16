using System.ComponentModel;

namespace QIQO.Business.Client.Core.UI
{
    public interface IValidatableTrackingObject : IRevertibleChangeTracking, INotifyPropertyChanged
    {
        bool IsValid { get; }
    }
}
