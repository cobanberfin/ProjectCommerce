using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
   public interface ICartService
    {
        void Initializer(string userId); //kart kaydı

        Cart GetCartByUserId(string userId);

        void AddToCart( string userId , int productId,int quantity);
    }
}
