using Microsoft.AspNetCore.Mvc;
using DAL.CATEGORIES;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace COM.Components.CMS
{
    public class CmsFooterMenuOne : ViewComponent
    {
        private readonly CategoriesDbContext categoryContext;

        public CmsFooterMenuOne(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pages = await categoryContext.Pages.ToListAsync();

            return View(pages);
        }
    }
}