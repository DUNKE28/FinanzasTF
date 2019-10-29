using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Factura.Domain;
using Factura.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Factura.Repository.Implementation
{
    public class FacturaRepository : IFacturaRepository
    {
        private ApplicationDbContext context;

        public FacturaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool DeleteById(int id)
        {
            try
            {
                Factura factura = GetById(id);
                context.Facturas.Remove(factura);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Factura> GetAll()
        {
            var result = new List<Factura>();
            try
            {
                result = context.Facturas
                .Include(factura => factura.Usuario)
                .Include(factura => factura.Costos)
                .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public Factura GetById(int id)
        {
            var result = new Factura();
            try
            {
                result = context.Facturas
                    .Include(factura => factura.Usuario)
                    .Include(factura => factura.Costos)
                    .Single(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Save(Factura entity)
        {
            try
            { 
                entity.DiasTranscurridos = entity.FechaPago - entity.FechaDescuento;
                if (entity.IsNominal){
                    entity.m = entity.PlazoDeTasa / entity.PeriodoCapital;
                    entity.n = entity.DiasPorAño / entity.PeriodoCapital;
                    entity.TEA = pow(1+(entity.TasaNominal/entity.m),entity.n) - 1;
                    entity.TEPeriodo = pow(1+(entity.TasaNominal/entity.m),entity.DiasTranscurridos/entity.PeriodoCapital) - 1;
                }
                else {
                    entity.TEA = pow(1+entity.TasaEfectiva,entity.DiasPorAño/entity.PlazoDeTasa) - 1;
                    entity.TEPeriodo = pow(1+entity.TasaEfectiva,entity.DiasTranscurridos/entity.PlazoDeTasa) - 1;
                }
                entity.dPeriodo = entity.TEPeriodo/(1+entity.TEPeriodo);
                entity.DescuentoPeriodo = entity.TotalFacturado*entity.dPeriodo;
                entity.CostesIniciales = 0;
                entity.ValorNeto = entity.TotalFacturado - entity.DescuentoPeriodo;
                entity.ValorRecibido = entity.ValorNeto-entity.CostesIniciales;
                entity.CostesFinales = 0;
                entity.ValorEntregado = entity.TotalFacturado + entity.CostesFinales;
                entity.TCEA = pow(entity.ValorEntregado/entity.ValorRecibido,entity.DiasPorAño/entity.PlazoDeTasa) - 1;

                context.Add(entity);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(Factura entity)
        {
            try
            {
                var originalUser = context.Facturas
                    .Single(factura => factura.Id == entity.Id);

                originalUser.FechaConsulta = entity.FechaConsulta;
                originalUser.FechaEmision = entity.FechaEmision;
                originalUser.FechaPago = entity.FechaPago;
                originalUser.TotalFacturado = entity.TotalFacturado;
                originalUser.Retencion = entity.Retencion;
                originalUser.DiasPorAño = entity.DiasPorAño;
                originalUser.PlazoDeTasa = entity.PlazoDeTasa;
                originalUser.IsNominal = entity.IsNominal;
                originalUser.TasaEfectiva = entity.TasaEfectiva;
                originalUser.TasaNominal = entity.TasaNominal;
                originalUser.PeriodoCapital = entity.PeriodoCapital;
                originalUser.FechaDescuento = entity.FechaDescuento;
                
                originalUser.DiasTranscurridos = originalUser.FechaPago - originalUser.FechaDescuento;
                if (originalUser.IsNominal){
                    originalUser.m = originalUser.PlazoDeTasa / originalUser.PeriodoCapital;
                    originalUser.n = originalUser.DiasPorAño / originalUser.PeriodoCapital;
                    originalUser.TEA = pow(1+(originalUser.TasaNominal/originalUser.m),originalUser.n) - 1;
                    originalUser.TEPeriodo = pow(1+(originalUser.TasaNominal/originalUser.m),originalUser.DiasTranscurridos/originalUser.PeriodoCapital) - 1;
                }
                else {
                    originalUser.TEA = pow(1+originalUser.TasaEfectiva,originalUser.DiasPorAño/originalUser.PlazoDeTasa) - 1;
                    originalUser.TEPeriodo = pow(1+originalUser.TasaEfectiva,originalUser.DiasTranscurridos/originalUser.PlazoDeTasa) - 1;
                }
                originalUser.dPeriodo = originalUser.TEPeriodo/(1+originalUser.TEPeriodo);
                originalUser.DescuentoPeriodo = originalUser.TotalFacturado*originalUser.dPeriodo;
                originalUser.CostesIniciales = 0;
                originalUser.ValorNeto = originalUser.TotalFacturado - originalUser.DescuentoPeriodo;
                originalUser.ValorRecibido = originalUser.ValorNeto-originalUser.CostesIniciales;
                originalUser.CostesFinales = 0;
                originalUser.ValorEntregado = originalUser.TotalFacturado + originalUser.CostesFinales;
                originalUser.TCEA = pow(originalUser.ValorEntregado/originalUser.ValorRecibido,originalUser.DiasPorAño/originalUser.PlazoDeTasa) - 1;

                originalUser.UsuarioId = entity.UsuarioId;

                context.Usuarios.Update(originalUser);
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