using BusinessLayer.Abstract;
using EntityLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Linq;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        ProductListModel pmodel = new ProductListModel();
        public AdminController(IProductService productService,ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;

        }
        public IActionResult ProductList()
        {
            pmodel.Products = _productService.GetAll();
            return View(pmodel);

        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductModel model)
        {
            if (ModelState.IsValid )
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl
                };
                _productService.Create(product);
                return RedirectToAction("ProductList");
            }
            return View(model);
          

        }
        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetById((int)id);
            if(entity == null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult EditProduct(ProductModel model)
        {
            var product = _productService.GetById(model.Id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = model.Name;
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;
            product.Price = model.Price;

            _productService.Update(product);
            return RedirectToAction("ProductList");
        }
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var deleted = _productService.GetById(id);
            if (deleted != null)
            {
                _productService.Delete(deleted);
            }
            return RedirectToAction("ProductList");
        }

       public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories = _categoryService.GetAll()

            });
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();

        }
        [HttpPost]
        public IActionResult AddCategory(CategoryModel category)
        {
            var entity = new Category()
            {
                Name = category.Name
            };
            _categoryService.Create(entity);
            return RedirectToAction("CategoryList");

        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var entity = _categoryService.GetByIdWithProducts(id);

            return View(new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Products = entity.Products.ToList()
               
            }); 

        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            _categoryService.Update(entity);

            return RedirectToAction("CategoryList");

        }

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {

            var catdel = _categoryService.GetById(id);
            if (catdel != null)
            {
                _categoryService.Delete(catdel);
            }
            return RedirectToAction("CategoryList");

        }


        }
}