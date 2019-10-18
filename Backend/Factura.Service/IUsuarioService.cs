using System;
using System.Collections.Generic;
using System.Text;
using Factura.Domain;

namespace Factura.Service
{
    public interface IUsuarioService:IService<Usuario>
    {
        Usuario Login(string email, string contraseña);
        Usuario GetByRUC(long RUC);
    }
}