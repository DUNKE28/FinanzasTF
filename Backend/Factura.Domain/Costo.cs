using System;

namespace Factura.Domain
{
    public class Costo
    {
        public int Id {get; set;}
        public string Motivo {get; set;}
        public double Valor {get; set;}
        public bool IsEfectivo {get; set;}
        public bool IsInit {get; set;}
        public int FacturaId {get; set;}
        public Factu Factura {get; set;}
    }
}