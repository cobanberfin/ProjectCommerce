using DataAcessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Concrete
{
    public class EfCoreGenericRepository<T> : IRepository<T> where T:class ,new()
    {
        Context db = new Context();
        DbSet<T> _object;
        public EfCoreGenericRepository()
        {
            _object = db.Set<T>();

        }
        public void Create(T entity)
        {
            _object.Add(entity);
            db.SaveChanges();
            
        }

        public void Delete(T entity)
        {
            _object.Remove(entity);
             db.SaveChanges();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                   ? _object.ToList()
                   : _object.Where(filter).ToList();

        }

        public T GetById(int id)
        {
            return _object.Find(id);
        }

        public List<T> List()
        {
           
                return _object.ToList();
            
        }

        public  virtual void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
