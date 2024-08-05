using Core.DTOs.Paging;
using Core.DTOs.Products;
using Core.Services.Implementations;
using Core.Services.Interfaces;
using Core.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace MyStore.Controllers
{
    public class ProductController : SiteBaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
                _productService = productService;
        }

        #region products

        [HttpGet("filter-products")]
        public async Task<IActionResult> GetProducts(string? searchterm, string? sortCloumn, string? sortOrder, int page, int pageSize)
        {
            var request = new BasePaging(page, pageSize,searchterm,sortCloumn, sortOrder);
            var products = await _productService.GetAllAsync(request);

            return JsonResponseStatus.Success(products);
        }

        #endregion
    }
}
