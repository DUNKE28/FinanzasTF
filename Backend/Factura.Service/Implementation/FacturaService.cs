using System;
using System.Collections.Generic;
using System.Text;
using Factura.Domain;
using Factura.Repository;

namespace Factura.Service.Implementation
{
    public class FacturaService : IFacturaService
    {
        private IFacturaRepository facturaRepository;

        public FacturaService(IFacturaRepository facturaRepository)
        {
            this.facturaRepository = facturaRepository;
        }

        public bool DeleteById(int id)
        {
            return facturaRepository.DeleteById(id);
        }

        public IEnumerable<Factura> GetAll()
        {
            return facturaRepository.GetAll();
        }

        public Factura GetById(int id)
        {
            return facturaRepository.GetById(id);
        }

        public bool Save(Factura entity)
        {
            return facturaRepository.Save(entity);
        }

        public bool Update(Factura entity)
        {
            return facturaRepository.Update(entity);
        }
    }
}