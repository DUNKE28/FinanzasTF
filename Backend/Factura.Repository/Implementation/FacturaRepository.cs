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
                Factu factura = GetById(id);
                context.Facturas.Remove(factura);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Factu> GetAll()
        {
            var result = new List<Factu>();
            try
            {
                result = context.Facturas
                .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public Factu GetById(int id)
        {
            var result = new Factu();
            try
            {
                result = context.Facturas
                    .Single(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Save(Factu entity)
        {
            try
            { 
                entity.DiasTranscurridos = (entity.FechaPago - entity.FechaDescuento).Days;
                if (entity.IsNominal){
                    entity.m = entity.PlazoDeTasa / entity.PeriodoCapital;
                    entity.n = entity.DiasPorAño / entity.PeriodoCapital;
                    entity.TEA = Math.Pow((1+(entity.TasaNominal/entity.m)),entity.n) - 1;
                    entity.TEPeriodo = Math.Pow((1+(entity.TasaNominal/entity.m)),(entity.DiasTranscurridos/entity.PeriodoCapital)) - 1;
                }
                else {
                    entity.TEA = Math.Pow((1+entity.TasaEfectiva),(entity.DiasPorAño/entity.PlazoDeTasa)) - 1;
                    entity.TEPeriodo = Math.Pow((1+entity.TasaEfectiva),(entity.DiasTranscurridos/entity.PlazoDeTasa)) - 1;
                }
                entity.dPeriodo = entity.TEPeriodo/(1+entity.TEPeriodo);
                entity.DescuentoPeriodo = entity.TotalFacturado*entity.dPeriodo;
                entity.DescuentoPeriodo = Math.Round(entity.DescuentoPeriodo,2);
                entity.CostesIniciales = 0;
                entity.CostesFinales = 0;
                for (int i = 0; i < entity.Costos.Count(); i++)
                {
                    Costo aux = entity.Costos.ElementAt(i);
                    if (aux.IsInit){
                        if (aux.IsEfectivo){
                            entity.CostesIniciales += aux.Valor;
                        } else {
                            entity.CostesIniciales += aux.Valor * entity.TotalFacturado;
                        }
                    } else {
                        if (aux.IsEfectivo){
                            entity.CostesFinales += aux.Valor;
                        } else {
                            entity.CostesFinales += aux.Valor * entity.TotalFacturado;
                        }
                    }
                }
                entity.CostesIniciales = Math.Round(entity.CostesIniciales,2);
                entity.CostesFinales = Math.Round(entity.CostesFinales,2);
                entity.ValorNeto = entity.TotalFacturado - entity.DescuentoPeriodo;
                entity.ValorNeto = Math.Round(entity.ValorNeto,2);

                entity.ValorRecibido = entity.ValorNeto - entity.CostesIniciales;
                entity.ValorRecibido = Math.Round(entity.ValorRecibido,2);

                
                entity.ValorEntregado = entity.TotalFacturado + entity.CostesFinales;
                entity.ValorEntregado = Math.Round(entity.ValorEntregado,2);

                entity.TCEA = Math.Pow((entity.ValorEntregado/entity.ValorRecibido),(entity.DiasPorAño/entity.DiasTranscurridos)) - 1;
                
                context.Add(entity);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(Factu entity)
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
                
                originalUser.DiasTranscurridos = (originalUser.FechaPago - originalUser.FechaDescuento).Days;
                if (originalUser.IsNominal){
                    originalUser.m = originalUser.PlazoDeTasa / originalUser.PeriodoCapital;
                    originalUser.n = originalUser.DiasPorAño / originalUser.PeriodoCapital;
                    originalUser.TEA = Math.Pow(1+(originalUser.TasaNominal/originalUser.m),originalUser.n) - 1;
                    originalUser.TEPeriodo = Math.Pow(1+(originalUser.TasaNominal/originalUser.m),originalUser.DiasTranscurridos/originalUser.PeriodoCapital) - 1;
                }
                else {
                    originalUser.TEA = Math.Pow(1+originalUser.TasaEfectiva,originalUser.DiasPorAño/originalUser.PlazoDeTasa) - 1;
                    originalUser.TEPeriodo = Math.Pow(1+originalUser.TasaEfectiva,originalUser.DiasTranscurridos/originalUser.PlazoDeTasa) - 1;
                }
                originalUser.dPeriodo = originalUser.TEPeriodo/(1+originalUser.TEPeriodo);
                originalUser.DescuentoPeriodo = originalUser.TotalFacturado*originalUser.dPeriodo;
                originalUser.CostesIniciales = 0;
                originalUser.ValorNeto = originalUser.TotalFacturado - originalUser.DescuentoPeriodo;
                originalUser.ValorRecibido = originalUser.ValorNeto-originalUser.CostesIniciales;
                originalUser.CostesFinales = 0;
                originalUser.ValorEntregado = originalUser.TotalFacturado + originalUser.CostesFinales;
                originalUser.TCEA = Math.Pow(originalUser.ValorEntregado/originalUser.ValorRecibido,originalUser.DiasPorAño/originalUser.PlazoDeTasa) - 1;

                originalUser.UsuarioId = entity.UsuarioId;

                context.Facturas.Update(originalUser);
                context.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public Factu GetLastResult()
        {
            var result = new List<Factu>();
            var ilast = new Factu();
            try
            {
                result = context.Facturas.ToList();
                ilast = result.Last();

            }
            catch (Exception)
            {
                throw;
            }
            return ilast;
        }

        public IEnumerable<Factu> GetByIdUsuario(int idUser)
        {
            var result = new List<Factu>();
            try
            {
                result = context.Facturas.Where(x => x.UsuarioId == idUser).ToList();
            }
            catch(Exception)
            {
                throw;
            }
            return result;
        }
    }
}