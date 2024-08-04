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


        public async Task<FilterProdcutsDTO> FilterProducts(FilterProdcutsDTO filter)
        {
            var productsQuery = _productRepository.GetEntitiesQuery().AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                productsQuery = productsQuery.Where(s => s.ProductName.Contains(filter.Title));

            productsQuery = productsQuery.Where(s => s.Price >= filter.StartPrice && s.Price <= filter.EndPrice);

            var count = (int)Math.Ceiling(productsQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var products = await productsQuery.Paging(pager).ToListAsync();

            return filter.SetProducts(products).SetPaging(pager);


        }
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
