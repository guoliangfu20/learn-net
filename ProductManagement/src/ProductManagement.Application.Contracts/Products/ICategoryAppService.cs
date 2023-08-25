using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProductManagement.Products
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<PagedResultDto<CategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<CategoryDto> GetAsync(Guid id);
        Task CreateAsync(CreateUpdateCategoryDto input);
        Task UpdateAsync(Guid id, CreateUpdateCategoryDto input);
        Task DeleteAsync(Guid id);
    }
}
