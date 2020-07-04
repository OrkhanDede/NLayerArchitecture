using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerArchitecture.Application.Interfaces;
using NLayerArchitecture.Application.Models;
using NLayerArchitecture.Core.Logger;

namespace NLayerArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IAppLogger<CategoryController> _appLogger;
        public CategoryController(ICategoryService categoryService, IAppLogger<CategoryController> appLogger)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _appLogger = appLogger ?? throw new ArgumentNullException(nameof(appLogger));
        }
        [HttpPost]
        public async Task<IActionResult> Post(CategoryModel categoryModel)
        {
            var newCategory = await _categoryService.CreateAsync(categoryModel);
            return Ok(newCategory);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetCategoryListAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null) return NotFound();
            return Ok(category);
        }
    }
}
