using _0_Freamwork.Domain;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long,  Product>
    {
        EditProduct GetDetail(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
