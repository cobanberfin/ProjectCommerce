using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using ShoesShop.WebUI.Models;

namespace PresentationLayer.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;
        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(new CategoryListViewModel()
            {
                SelectedCategory =RouteData.Values["category"]?.ToString(),
                Categories = _categoryService.GetAll()

            });


        }
    }
}