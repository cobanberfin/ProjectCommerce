using BusinessLayer.Abstract;
using DataAcessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;
        public CartManager(ICartDal cartdal)
        {
            _cartDal = cartdal;

        }

        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart = _cartDal.GetByUserId(userId);
            if(cart != null)
            {
                var index = cart.CartItems.FindIndex(i => i.ProductId == productId);
                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }
                _cartDal.Update(cart);
            }
           
        }

        public void Initializer(string userId)
        {
            _cartDal.Create(new Cart() { UserId = userId });
        }

        Cart ICartService.GetCartByUserId(string userId)
        {
            return _cartDal.GetByUserId(userId);
        }
    }
}
