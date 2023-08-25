using ProductManagement.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace ProductManagement.Products
{
    public class ProductAppService : ProductManagementAppService, IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRespository;
        private readonly IRepository<Category, Guid> _categoryRespository;

        public ProductAppService(
            IRepository<Product, Guid> productRespository,
            IRepository<Category, Guid> categoryRepository)
        {
            _productRespository = productRespository;
            _categoryRespository = categoryRepository;
        }

        public async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto pageRequest)
        {
            var queryable = await _productRespository
                .WithDetailsAsync(c => c.Category);

            queryable = queryable
                .Skip(pageRequest.SkipCount)
                .Take(pageRequest.MaxResultCount)
                .OrderBy(pageRequest.Sorting ?? nameof(Product.Name));

            var products = await AsyncExecuter.ToListAsync(queryable);
            var count = await _productRespository.GetCountAsync();

            return new PagedResultDto<ProductDto>(
                  count,
                  ObjectMapper.Map<List<Product>, List<ProductDto>>(products));
        }


        public async Task CreateAsync(CreateUpdateProductDto input)
        {
            await _productRespository.InsertAsync(
                ObjectMapper.Map<CreateUpdateProductDto, Product>(input));
        }

        public async Task<ProductDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Product, ProductDto>(await _productRespository.GetAsync(id));
        }

        public async Task<List<ProductDto>> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            var queryable = await _productRespository.GetQueryableAsync();

            var query = from product in queryable
                        where product.Name.Contains(name)
                        orderby product.Name
                        select product;
            var products = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Product>, List<ProductDto>>(products);
        }

        public async Task<ListResultDto<CategoryLookupDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRespository.GetListAsync();
            return new ListResultDto<CategoryLookupDto>(
                    ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories));
        }

        public async Task UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            var product = await _productRespository.GetAsync(id);
            ObjectMapper.Map(input, product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRespository.DeleteAsync(id);
        }
    }
}
