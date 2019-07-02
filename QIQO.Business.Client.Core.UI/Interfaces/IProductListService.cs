using QIQO.Business.Client.Entities;
using System.Collections.Generic;

namespace QIQO.Business.Client.Core.UI
{
    public interface IProductListService
    {
        List<Product> ProductList { get; }
    }
}