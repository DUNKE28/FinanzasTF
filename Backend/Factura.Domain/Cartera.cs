using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Factura.Domain
{
    public class Cartera
    {
        public int Id { get; set; }
        public float TCEA { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}