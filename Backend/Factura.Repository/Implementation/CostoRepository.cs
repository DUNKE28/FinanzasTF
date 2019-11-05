using System;
using System.Collections.Generic;
using System.Linq;
using Factura.Domain;
using Factura.Repository.Context;

namespace Factura.Repository.Implementation
{
    public class CostoRepository : ICostoRepository
    {
        private ApplicationDbContext context;
        public CostoRepository(ApplicationDbContext context){
            this.context = context;
        }
        public bool DeleteById(int id)
        {
            try
            {
                Costo costo = GetById(id);
                context.Costos.Remove(costo);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Costo> GetAll()
        {
            var result = new List<Costo>();
            try
            {
                result = context.Costos
                .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public Costo GetById(int id)
        {
            var result = new Costo();
            try
            {
                result = context.Costos
                    .Single(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Save(Costo entity)
        {
            try
            { 
                context.Add(entity);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(Costo entity)
        {
            try
            {
                var originalCosto = context.Costos
                    .Single(x => x.Id == entity.Id);

                originalCosto.Motivo = entity.Motivo;
                originalCosto.Valor = entity.Valor;
                originalCosto.IsEfectivo = entity.IsEfectivo;
                originalCosto.IsInit = entity.IsInit;
                originalCosto.FacturaId = entity.FacturaId;

                context.Costos.Update(originalCosto);
                context.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}