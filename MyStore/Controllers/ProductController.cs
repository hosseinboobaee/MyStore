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
        public async Task<IActionResult> GetProducts([FromQuery] FilterProdcutsDTO filter)
        {
            var products = await _productService.FilterProducts(filter);

            return JsonResponseStatus.Success(products);
        }

        #endregion
    }
}
