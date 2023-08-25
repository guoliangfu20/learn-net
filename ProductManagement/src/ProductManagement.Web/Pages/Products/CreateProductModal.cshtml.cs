using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Products;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Web.Pages.Products
{
    public class CreateProductModalModel : ProductManagementPageModel
    {
        [BindProperty]
        public CreateEditProductViewModal Product { get; set; }

        public SelectListItem[] Categories { get; set; }


        private readonly IProductAppService _productAppService;

        public CreateProductModalModel(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        public async Task OnGetAsync()
        {
            Product = new CreateEditProductViewModal
            {
                ReleaseTime = Clock.Now,
                StockState = ProductStockState.PreOrder
            };

            var categoryLookup = await _productAppService.GetCategoriesAsync();

            Categories = categoryLookup.Items
                   .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                   .ToArray();


        }


        public void OnGet()
        {
        }
    }
}
