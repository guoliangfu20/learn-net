using ProductManagement.Products;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace ProductManagement.Web.Pages.Products
{
    public class CreateEditProductViewModal
    {
        [SelectItems("Categories")]
        [DisplayName("Category")]
        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(ProductConsts.MaxNameLength)]
        public string Name { get; set; }

        public float Price { get; set; }
        public bool IsFreeCargo { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseTime { get; set; }
        public ProductStockState StockState { get; set; }
    }
}
