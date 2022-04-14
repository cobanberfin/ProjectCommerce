using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
   public interface ICategoryService
    {
        List<Category> GetAll();
         Category GetById(int id);
         Category GetByIdWithProducts(int id); //cat ıleıgl urnlr
      
        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
