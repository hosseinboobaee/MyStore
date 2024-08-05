using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DTOs.Paging;
using Core.DTOs.Products;
using Core.Extensions;
using Core.Services.Interfaces;
using Data.Entities.Product;
using Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Core.Services.Implementations
{
    public class ProductService : IProductService
    {

        #region constructor

        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;

        public ProductService(IGenericRepository<Product> productRepository, IGenericRepository<ProductCategory> productCategoryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        #endregion

        #region product

        public async Task AddProduct(Product product)
        {
            await _productRepository.AddEntity(product);
            await _productRepository.SaveChanges();
        }

        public async Task UpdateProduct(Product product)
        {
            _productRepository.UpdateEntity(product);
            await _productRepository.SaveChanges();
        }

        public async Task<IEnumerable<Product>> GetAllAsync(BasePaging request)
        {
            var query = _productRepository.GetEntitiesQuery().AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(s => s.ProductName.Contains(request.SearchTerm));
            }
            if(request.SortOrder?.ToLower() == "desc")
                query = query.OrderByDescending(GetSortProperty(request));
            else
                query = query.OrderBy(GetSortProperty(request));
            var result = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            return result;
        }
        private Expression<Func<Product, object>> GetSortProperty(BasePaging request)
            => request.SortColumn?.ToLower() switch
            {
                "name" => Product => Product.ProductName,
                _ => Product => Product.Id,
            };
  
            #endregion


            #region dispose

            public void Dispose()
        {
            _productRepository?.Dispose();
            _productCategoryRepository?.Dispose();
        }

        #endregion
    }
}
