using BAL.Services.Contracts;
using BOL.CATEGORIES;
using BOL.ComponentModels;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.ComponentModels.Pages;
using DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Components;
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

        public async Task GetSecCategoriesByFirstCategoryId(CategoryVM categoryVM, ChangeEventArgs events)
        {
            if (events != null)
            {
                int.TryParse(events.Value.ToString(), out int firstCatId);
                categoryVM.FirstCategoryID = firstCatId;
                categoryVM.SecondCategoryID = 0;
            }
            categoryVM.SecondCategories.Clear();
            categoryVM.ThirdCategories.Clear();
            categoryVM.FourthCategories.Clear();
            categoryVM.FifthCategories.Clear();
            categoryVM.SixthCategories.Clear();
            if (categoryVM.FirstCategoryID > 0)
                categoryVM.SecondCategories = await _categoryRepository
                    .GetSecCategoriesByFirstCategoryId(categoryVM.FirstCategoryID);
        }

        public async Task GetOtherCategoriesBySeconCategoryId(CategoryVM categoryVM)
        {
            categoryVM.ThirdCategories.Clear();
            categoryVM.FourthCategories.Clear();
            categoryVM.FifthCategories.Clear();
            categoryVM.SixthCategories.Clear();
            if (categoryVM.SecondCategoryID > 0)
            {
                await GetThirdCategoriesBySeconCategoryId(categoryVM);
                await GetForthCategoriesBySecondCategoryId(categoryVM);
                await GetFifthCategoriesBySecondCategoryId(categoryVM);
                await GetSixthCategoriesBySecondCategoryId(categoryVM);
            }
        }

        public void GetOtherCategoriesToUpdate(CategoryVM categoryVM)
        {
            categoryVM.ThirdCategory = null;
            categoryVM.FourthCategory = null;
            categoryVM.FifthCategory = null;
            categoryVM.SixthCategory = null;

            if (categoryVM.ThirdCategories != null)
            {
                var cat = categoryVM.ThirdCategories.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
                if (cat.Any())
                    categoryVM.ThirdCategory = String.Join(',', cat);
            }

            if (categoryVM.FourthCategories != null)
            {
                var cat = categoryVM.FourthCategories.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
                if (cat.Any())
                    categoryVM.FourthCategory = String.Join(',', cat);
            }

            if (categoryVM.FifthCategories != null)
            {
                var cat = categoryVM.FifthCategories.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
                if (cat.Any())
                    categoryVM.FifthCategory = String.Join(',', cat);
            }

            if (categoryVM.SixthCategories != null)
            {
                var cat = categoryVM.SixthCategories.Where(x => x.IsSelected).Select(x => x.Id).ToArray();
                if (cat.Any())
                    categoryVM.SixthCategory = String.Join(',', cat);
            }
        }

        public void MarkAllCategoriesSelected(CategoryVM categoryVM)
        {
            if (categoryVM.ThirdCategories != null)
            {
                foreach (var cat in categoryVM.ThirdCategories)
                    cat.IsSelected = true;
            }

            if (categoryVM.FourthCategories != null)
            {
                foreach (var cat in categoryVM.FourthCategories)
                    cat.IsSelected = true;
            }

            if (categoryVM.FifthCategories != null)
            {
                foreach (var cat in categoryVM.FifthCategories)
                    cat.IsSelected = true;
            }

            if (categoryVM.SixthCategories != null)
            {
                foreach (var cat in categoryVM.SixthCategories)
                    cat.IsSelected = true;
            }
        }

        #region private methods
        private async Task GetThirdCategoriesBySeconCategoryId(CategoryVM categoryVM)
        {
            categoryVM.ThirdCategory = categoryVM.ThirdCategory ?? "";
            string[] catIds = categoryVM.ThirdCategory.Split(",");
            var thirdCategories = await _categoryRepository.GetThirdCategoriesBySeconCategoryId(categoryVM.SecondCategoryID);

            if (thirdCategories.Any())
            {
                categoryVM.ThirdCategories = thirdCategories.Select((x) => new SelectItem
                {
                    Id = x.ThirdCategoryID,
                    Name = x.Name,
                    IsSelected = catIds.Contains(Convert.ToString(x.ThirdCategoryID))
                }).ToList();
            }
        }

        private async Task GetForthCategoriesBySecondCategoryId(CategoryVM categoryVM)
        {
            categoryVM.FourthCategory = categoryVM.FourthCategory ?? "";
            string[] catIds = categoryVM.FourthCategory.Split(",");
            var fourthCategories = await _categoryRepository.GetForthCategoriesBySecondCategoryId(categoryVM.SecondCategoryID);

            if (fourthCategories.Any())
            {
                categoryVM.FourthCategories = fourthCategories.Select((x) => new SelectItem
                {
                    Id = x.FourthCategoryID,
                    Name = x.Name,
                    IsSelected = catIds.Contains(Convert.ToString(x.FourthCategoryID))
                }).ToList();
            }
        }

        private async Task GetFifthCategoriesBySecondCategoryId(CategoryVM categoryVM)
        {
            categoryVM.FifthCategory = categoryVM.FifthCategory ?? "";
            string[] catIds = categoryVM.FifthCategory.Split(",");
            var fifthCategories = await _categoryRepository.GetFifthCategoriesBySecondCategoryId(categoryVM.SecondCategoryID);

            if (fifthCategories.Any())
            {
                categoryVM.FifthCategories = fifthCategories.Select((x) => new SelectItem
                {
                    Id = x.FifthCategoryID,
                    Name = x.Name,
                    IsSelected = catIds.Contains(Convert.ToString(x.FifthCategoryID))
                }).ToList();
            }
        }

        private async Task GetSixthCategoriesBySecondCategoryId(CategoryVM categoryVM)
        {
            categoryVM.SixthCategory = categoryVM.SixthCategory ?? "";
            string[] catIds = Convert.ToString(categoryVM.SixthCategory).Split(",");
            var sixthCategories = await _categoryRepository.GetSixthCategoriesBySecondCategoryId(categoryVM.SecondCategoryID);

            if (sixthCategories.Any())
            {
                categoryVM.SixthCategories = sixthCategories.Select((x) => new SelectItem
                {
                    Id = x.SixthCategoryID,
                    Name = x.Name,
                    IsSelected = catIds.Contains(Convert.ToString(x.SixthCategoryID))
                }).ToList();
            }
        }
        #endregion
    }
}
