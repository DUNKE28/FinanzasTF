using System.Collections.Generic;
using Factura.Domain;
using Factura.Repository;

namespace Factura.Service.Implementation
{
    public class CostoService : ICostoService
    {
        private ICostoRepository costoRepository;
        public CostoService(ICostoRepository costoRepository)
        {
            this.costoRepository=costoRepository;
        }
            public bool DeleteById(int id)
        {
            return costoRepository.DeleteById(id);
        }

        public IEnumerable<Costo> GetAll()
        {
            return costoRepository.GetAll();
        }

        public Costo GetById(int id)
        {
            return costoRepository.GetById(id);
        }

        public bool Save(Costo entity)
        {
            return costoRepository.Save(entity);
        }

        public bool Update(Costo entity)
        {
            return costoRepository.Update(entity);
        }
    }
}