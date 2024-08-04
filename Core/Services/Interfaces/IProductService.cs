using Core.DTOs.Products;
using Data.Entities.Product;
using System;
using System.Threading.Tasks;


namespace Core.Services.Interfaces
{
    public interface IProductService : IDisposable
    {
        #region product

        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task<FilterProdcutsDTO> FilterProducts(FilterProdcutsDTO filter);

        #endregion
    }
}