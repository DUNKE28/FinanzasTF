using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Factura.Domain
{
    public class Usuario
    {
        public int Id { get; set; }
        [DisplayName("Nombre de la Razon Social")]
        [Required(ErrorMessage="Introduzca su Razon Social")]
        public string RazonSocial { get; set; }
        [DisplayName("Numero de RUC")]
        [Required(ErrorMessage="Introduzca su RUC")]
        [MaxLength(11)]
        public long RUC { get; set; }
        [DisplayName("Correo electronico")]
        [Required(ErrorMessage="Introduzca su correo")]
        public string Email { get; set; }
        [DisplayName("Contraseña")]
        [Required(ErrorMessage="Introduzca su contraseña")]
        public string Contraseña { get; set; }
        public IEnumerable<Cartera> Carteras { get; set; }
    }
}