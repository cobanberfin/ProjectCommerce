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
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category>, ICategoryDal
    {
        public Category GetByIdWithProducts(int id)  //cat ve oan aıt urn bılgılerı glck
        {
           using(var context = new Context())
            {
                return context.Categories.Where(i => i.Id == id).Include(i => i.Products).FirstOrDefault();
            }
        }
    }
}
