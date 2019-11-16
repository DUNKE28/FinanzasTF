using System;
using System.Collections.Generic;
using System.Text;
using Factura.Domain;

namespace Factura.Service
{
    public interface IFacturaService : IService<Factu>
    {
         Factu GetLastResult();
         IEnumerable<Factu> GetByIdUsuario(int idUser);
    }
}