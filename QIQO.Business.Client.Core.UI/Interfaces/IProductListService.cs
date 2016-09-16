using System.Collections.Generic;
using QIQO.Business.Client.Entities;

namespace QIQO.Business.Client.Core.UI
{
    public interface IProductListService
    {
        List<Product> ProductList { get; }
    }
}