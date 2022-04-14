using DataAcessLayer.Abstract;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Concrete.EfCore
{
    public class EfCoreCartDal : EfCoreGenericRepository<Cart>, ICartDal
    {

        public override void Update(Cart entity)
        {
            using (var db = new Context())
            {
                db.Carts.Update(entity);
                db.SaveChanges();
            }
        }
            public Cart GetByUserId(string userId)
            {
                using (var db = new Context())
                {
                    return db.Carts.Include(i => i.CartItems).ThenInclude(i => i.product).FirstOrDefault(i => i.UserId == userId);
                }
            }

        } } 
