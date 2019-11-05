using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Factura.Domain
{
    public class Factu
    {
        public int Id { get; set; }
        [DisplayName("Fecha de consulta")]
        [Required(ErrorMessage="Introduzca la fecha de consulta")]
        public DateTime FechaConsulta { get; set; }
        [DisplayName("Fecha de emision")]
        [Required(ErrorMessage="Introduzca la fecha de emision")]
        public DateTime FechaEmision { get; set; }
        [DisplayName("Fecha de pago")]
        [Required(ErrorMessage="Introduzca la fecha de pago")]
        public DateTime FechaPago { get; set; }
        [DisplayName("Total Facturado")]
        [Required(ErrorMessage="Introduzca el total facturado")]
        public double TotalFacturado { get; set; } 
        [DisplayName("Retencion")]
        [Required(ErrorMessage="Introduzca la retencion")]
        public double Retencion { get; set; } 
        [DisplayName("Dias por año")]
        [Required(ErrorMessage="Introduzca los dias por año")]
        public double DiasPorAño { get; set; }
        [DisplayName("Plazo de Tasa")]
        [Required(ErrorMessage="Introduzca el plazo de la Tasa")]
        public double PlazoDeTasa { get; set; }
        public bool IsNominal { get; set; }
        [DisplayName("Tasa Efectiva")]
        public double TasaEfectiva { get; set; }
        [DisplayName("Tasa Nominal")]
        public double TasaNominal { get; set; }
        [DisplayName("Período de Capitalizacion")]
        public double PeriodoCapital { get; set; }
        public double m { get; set; }
        public double n { get; set; }
        [DisplayName("Fecha de descuento")]
        [Required(ErrorMessage="Introduzca la fecha de descuento")]
        public DateTime FechaDescuento { get; set; }
        public double TEA { get; set; }
        public double DiasTranscurridos { get; set; }
        public double TEPeriodo { get; set; }
        public double dPeriodo { get; set; }
        public double DescuentoPeriodo { get; set; }
        public double CostesIniciales { get; set; }
        public double ValorNeto { get; set; }
        public double ValorRecibido { get; set; }
        public double CostesFinales { get; set; }
        public double ValorEntregado { get; set; }
        public double TCEA { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public IEnumerable<Costo> Costos { get; set; }
    }
}