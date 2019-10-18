using Factura.Domain;

namespace Factura.Repository
{
    public interface IUsuarioRepository:IRepository<Usuario>
    {
        Usuario Login(string email, string contraseña);
        Usuario GetByRUC(long RUC);
    }
}