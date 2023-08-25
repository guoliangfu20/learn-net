using ProductManagement.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductManagement.Products
{
    public class CreateUpdateCategoryDto
    {
        [Required]
        [StringLength(CategoryConsts.MaxNameLength)]
        public string Name { get; set; }
    }
}
