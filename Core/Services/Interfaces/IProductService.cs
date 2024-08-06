using Core.DTOs.Paging;
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
        Task<IEnumerable<Product>> GetAllProduct();
        Task<IEnumerable<Product>> GetAllAsync(BasePaging request);

        #endregion
    }
}