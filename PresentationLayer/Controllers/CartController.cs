using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Identity;
using PresentationLayer.Models;
using System.Linq;

namespace PresentationLayer.Controllers
{
    [Authorize]  
    public class CartController : Controller
    {
        private ICartService _cartService;
        private UserManager<ApplicationUser> _userManager;
        public CartController(ICartService cartService , UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var cart = _cartService.GetCartByUserId(_userManager.GetUserId(User));

            return View(new CartModel
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.product.Id,
                    Name = i.product.Name,
                    Price = (decimal)i.product.Price,
                    ImageUrl = i.product.ImageUrl,
                    Quantity = i.Quantity
                }).ToList()
            });
        }
        [HttpPost]
        public IActionResult AddToCart(string userId, int productId, int quantity)
        {
            _cartService.AddToCart(_userManager.GetUserId(User),productId,quantity);
            return RedirectToAction("Index");
        }
    }
}

