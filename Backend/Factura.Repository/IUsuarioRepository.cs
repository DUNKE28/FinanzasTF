using Factura.Domain;
using System.Collections.Generic;

namespace Factura.Repository
{
    public interface IUsuarioRepository:IRepository<Usuario>
    {
        Usuario Login(string email, string contraseña);
        Usuario GetByRUC(long RUC);
    }
}