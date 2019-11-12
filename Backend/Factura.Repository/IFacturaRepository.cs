using Factura.Domain;
using System.Collections.Generic;

namespace Factura.Repository
{
    public interface IFacturaRepository:IRepository<Factu>
    {
        Factu GetLastResult();
    }
}