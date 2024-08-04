using System.Collections.Generic;
using Core.DTOs.Paging;
using Data.Entities.Product;

namespace Core.DTOs.Products
{
    public class FilterProdcutsDTO : BasePaging
    {
        public string Title { get; set; }

        public int StartPrice { get; set; }

        public int EndPrice { get; set; }

        public virtual List<Product> Products { get; set; }

        public FilterProdcutsDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.PageCount = paging.PageCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.ActivePage = paging.ActivePage;
            return this;
        }

        public FilterProdcutsDTO SetProducts(List<Product> products)
        {
            Products = products;
            return this;
        }
    }
}
