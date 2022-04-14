using DataAcessLayer.Abstract;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Concrete
{
    public class EfCoreProductDal : EfCoreGenericRepository<Product>, IProductDal
    {
        public int GetCountByCategory(string category)
        {
            using (var context = new Context())
            {
                var products = context.Products.AsQueryable();  
                if (!string.IsNullOrEmpty(category))
                {


                    products = products.Include(i => i.Category).Where(i => i.Category.Name.ToLower() == category.ToLower());
                }
                return products.Count();
            }
        }

        public Product GetProductDetails(int id)
        {
            using (var context = new Context())
            {
                return context.Products.Where(i => i.Id == id).Include(i => i.Category).FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            using (var context = new Context())
            {
                var products = context.Products.AsQueryable(); //ekstradan sorgu ekleneıblır
                if (!string.IsNullOrEmpty(category))
                {
                   
                   
                    products = products.Include(i => i.Category).Where(i => i.Category.Name.ToLower() == category.ToLower());
                }
                return products.Skip((page-1) * pageSize).Take(pageSize).ToList(); 
            }
        }
    }
}
