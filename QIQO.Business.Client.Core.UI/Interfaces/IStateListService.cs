using QIQO.Business.Client.Entities;
using System.Collections.Generic;

namespace QIQO.Business.Client.Core.UI
{
    public interface IStateListService
    {
        List<AddressPostal> StateList { get; }
    }
}
