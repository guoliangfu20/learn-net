using ProductManagement.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ProductManagement.Data
{
    public class ProductManagementDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Category, Guid> _categoryRespository;

        private readonly IRepository<Product, Guid> _productRespository;


        public ProductManagementDataSeedContributor(
            IRepository<Category, Guid> categoryRespository,
            IRepository<Product, Guid> productRespository)
        {
            _categoryRespository = categoryRespository;
            _productRespository = productRespository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _categoryRespository.CountAsync() > 0)
            {
                return;
            }
            var monitors = new Category { Name = "Monitors" };
            var printers = new Category { Name = "Printers" };

            //throw new NotImplementedException();
        }
    }
}
