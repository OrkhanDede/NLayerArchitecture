using NLayerArchitecture.Application.Interfaces;
using NLayerArchitecture.Application.Models;
using NLayerArchitecture.Core.Logger;
using NLayerArchitecture.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NLayerArchitecture.Application.Mapper;
using NLayerArchitecture.Core.Entities;

namespace NLayerArchitecture.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAppLogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository,
                                IAppLogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CategoryModel> CreateAsync(CategoryModel categoryModel)
        {
            await ValidateCategoryIfExist(categoryModel);

            var mappedEntity = ObjectMapper.Mapper.Map<Category>(categoryModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _categoryRepository.AddAsync(mappedEntity);
            _logger.LogInformation($"{newEntity.Id} Entity successfully added");

            var newMappedEntity = ObjectMapper.Mapper.Map<CategoryModel>(newEntity);
            return newMappedEntity;
        }

        public async Task<CategoryModel> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var mapper = ObjectMapper.Mapper.Map<CategoryModel>(category);
            return mapper;
        }
        public async Task<CategoryModel> GetCategoryWithProducts(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryWithProductsAsync(categoryId);
            var mapper = ObjectMapper.Mapper.Map<CategoryModel>(category);
            return mapper;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoryListAsync()
        {
            var category = await _categoryRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CategoryModel>>(category);
            return mapped;
        }

        
        #region private methods
        private async Task ValidateCategoryIfExist(CategoryModel categoryModel)
        {
            var existingEntity = await _categoryRepository.GetByIdAsync(categoryModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{categoryModel.ToString()} with this id already exists");
        }

        private void ValidateCategorytIfNotExist(CategoryModel categoryModel)
        {
            var existingEntity = _categoryRepository.GetByIdAsync(categoryModel.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{categoryModel.ToString()} with this id is not exists");
        }
        #endregion
    }
}
