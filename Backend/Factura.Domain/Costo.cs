using System;

namespace Factura.Domain
{
    public class Costo
    {
        public int Id {get; set;}
        public string Motivo {get; set;}
        public double Valor {get; set;}
        public string Tipo {get; set;}
        public string PorcEfect {get; set;}
        //public int FacturaId {get; set;}
        //public Factura Factura {get; set;}
    }
}