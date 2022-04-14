using BusinessLayer.Abstract;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;

        }

        public IActionResult List(string category, int page=1 )
        {
            const int pageSize = 3;
            return View(new ProductListModel()
            {
                PageInfo = new PageInfo()
                {
                   TotalProducts=_productService.GetCountByCategory(category),
                   CurrentPage=page ,
                   ItemsPerPage =pageSize,
                   CurrentCategory = category

                },




                Products = _productService.GetProductsByCategory(category, page, pageSize)
            });
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails((int)id);
            if (product == null)
            {
                return NotFound();
            }


            return View(new ProductDetailsModel()
            {
                Product = product,
                Category = product.Category
            }) ;
            
           

        }
    }
}


