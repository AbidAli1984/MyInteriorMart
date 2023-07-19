using BAL.Services.Contracts;
using BOL.CATEGORIES;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.ComponentModels.Pages;
using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task GetCategoriesForIndexPage(IndexVM indexVM)
        {
            indexVM.Services = await _categoryRepository.GetSecCategoriesByFirstCategoryId(Constants.Cat_Services);
            indexVM.Contractors = await _categoryRepository.GetSecCategoriesByFirstCategoryId(Constants.Cat_Contractors);
            indexVM.Dealers = await _categoryRepository.GetSecCategoriesByFirstCategoryId(Constants.Cat_Dealers);
        }

        public async Task<IList<FirstCategory>> GetFirstCategoriesAsync()
        {
            return await _categoryRepository.GetFirstCategoriesAsync();
        }

        public async Task GetSecCategoriesByFirstCategoryId(CategoryVM categoryVM)
        {
            categoryVM.SecondCategories.Clear();
            categoryVM.ThirdCategories.Clear();
            categoryVM.FourthCategories.Clear();
            categoryVM.FifthCategories.Clear();
            categoryVM.SixthCategories.Clear();
            if (categoryVM.Category.FirstCategoryID > 0)
                categoryVM.SecondCategories = await _categoryRepository
                    .GetSecCategoriesByFirstCategoryId(categoryVM.Category.FirstCategoryID);
        }

        public async Task GetOtherCategoriesBySeconCategoryId(CategoryVM categoryVM)
        {
            categoryVM.ThirdCategories.Clear();
            categoryVM.FourthCategories.Clear();
            categoryVM.FifthCategories.Clear();
            categoryVM.SixthCategories.Clear();
            await GetThirdCategoriesByThirdCategoryId(categoryVM);
            await GetForthCategoriesBySecondCategoryId(categoryVM);
            await GetFifthCategoriesByForthCategoryId(categoryVM);
            await GetSixthCategoriesBySecondCategoryId(categoryVM);
        }

        public void GetOtherCategoriesToUpdate(CategoryVM categoryVM)
        {
            categoryVM.Category.ThirdCategories = null;
            categoryVM.Category.FourthCategories = null;
            categoryVM.Category.FifthCategories = null;
            categoryVM.Category.SixthCategories = null;

            if (categoryVM.ThirdCategories != null && categoryVM.ThirdCategories.Count > 0)
            {
                var cat = categoryVM.ThirdCategories.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
                if (cat.Any())
                    categoryVM.Category.ThirdCategories = String.Join(',', cat);
            }

            if (categoryVM.FourthCategories != null && categoryVM.FourthCategories.Count > 0)
            {
                var cat = categoryVM.FourthCategories.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
                if (cat.Any())
                    categoryVM.Category.FourthCategories = String.Join(',', cat);
            }

            if (categoryVM.FifthCategories != null && categoryVM.FifthCategories.Count > 0)
            {
                var cat = categoryVM.FifthCategories.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
                if (cat.Any())
                    categoryVM.Category.FifthCategories = String.Join(',', cat);
            }

            if (categoryVM.SixthCategories != null && categoryVM.SixthCategories.Count > 0)
            {
                var cat = categoryVM.SixthCategories.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
                if (cat.Any())
                    categoryVM.Category.SixthCategories = String.Join(',', cat);
            }
        }

        private async Task GetThirdCategoriesByThirdCategoryId(CategoryVM categoryVM)
        {
            if (categoryVM.Category.SecondCategoryID > 0)
            {
                var thirdCategories = await _categoryRepository
                    .GetThirdCategoriesBySeconCategoryId(categoryVM.Category.SecondCategoryID);
                if (thirdCategories.Any())
                {
                    categoryVM.ThirdCategories = thirdCategories.Select((x) => new OtherCategories
                    {
                        Id = x.ThirdCategoryID,
                        Name = x.Name
                    }).ToList();
                }
            }
        }

        private async Task GetForthCategoriesBySecondCategoryId(CategoryVM categoryVM)
        {
            var fourthCategories = await _categoryRepository
                    .GetForthCategoriesBySecondCategoryId(categoryVM.Category.SecondCategoryID);
            if (fourthCategories.Any())
            {
                categoryVM.FourthCategories = fourthCategories.Select((x) => new OtherCategories
                {
                    Id = x.FourthCategoryID,
                    Name = x.Name
                }).ToList();
            }
        }

        private async Task GetFifthCategoriesByForthCategoryId(CategoryVM categoryVM)
        {
            var fifthCategories = await _categoryRepository
                    .GetFifthCategoriesBySecondCategoryId(categoryVM.Category.SecondCategoryID);
            if (fifthCategories.Any())
            {
                categoryVM.FifthCategories = fifthCategories.Select((x) => new OtherCategories
                {
                    Id = x.FifthCategoryID,
                    Name = x.Name
                }).ToList();
            }
        }

        private async Task GetSixthCategoriesBySecondCategoryId(CategoryVM categoryVM)
        {
            var sixthCategories = await _categoryRepository
                    .GetSixthCategoriesBySecondCategoryId(categoryVM.Category.SecondCategoryID);
            if (sixthCategories.Any())
            {
                categoryVM.SixthCategories = sixthCategories.Select((x) => new OtherCategories
                {
                    Id = x.SixthCategoryID,
                    Name = x.Name
                }).ToList();
            }
        }
    }
}
