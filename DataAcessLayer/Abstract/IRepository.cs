﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Abstract
{
   public interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetAll(Expression<Func<T ,bool>>filter=null);

        List<T> List();

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
