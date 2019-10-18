using System.Collections.Generic;

namespace Factura.Repository
{
    public interface IRepository<T>
    {
        bool Save(T entity);
        bool Update(T entity);
        bool DeleteById(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}