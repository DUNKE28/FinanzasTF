using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Service
{
    public interface IService<T>
    {
        bool Save(T entity);
        bool Update(T entity);
        bool DeleteById(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}