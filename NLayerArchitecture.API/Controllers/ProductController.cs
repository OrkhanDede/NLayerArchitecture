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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAppLogger<ProductController> _appLogger;
        public ProductController(IProductService productService, IAppLogger<ProductController> appLogger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _appLogger = appLogger ?? throw new ArgumentNullException(nameof(appLogger));
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pList = await _productService.GetProductListAsync();
            return Ok(pList);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var product = await _productService.GetProductByCategoryAsync(categoryId);
            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpGet("withcategory/{productId}")]
        public async Task<IActionResult> GetWithCategory(int productId)
        {
            var product = await _productService.GetProductWithCategoryAsync(productId);
            if (product == null) return NotFound();
            return Ok(product);
        }
        [HttpGet("withcategory")]
        public async Task<IActionResult> GetAllWithCategory()
        {
            var product = await _productService.GetProductsWithCategoryAsync();
            if (product == null) return NotFound();
            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> Post(ProductModel productModel)
        {
            var newEntity = await _productService.CreateAsync(productModel);
            return Ok(newEntity);
        }
        
    }
}
