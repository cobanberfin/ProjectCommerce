using DataAcessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Concrete.EfCore
{
    public class EfCoreOrderDal :EfCoreGenericRepository<Order>,IOrderDal
    {
    }
}
