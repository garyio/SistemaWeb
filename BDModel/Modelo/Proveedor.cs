using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDModel
{
    [MetadataType(typeof(ProveedorValidation))]
    public partial class Proveedor
    {
    }

    public class ProveedorValidation
    {

        [Display(Name = "Nombre Fiscal")]
        public string NombreFiscal { get; set; }

        [Display(Name = "Nombre Comercial")]
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public string NombreComercial { get; set; }

        public string Email { get; set; }

        [Display(Name = "Estatus")]
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public Nullable<bool> Activo { get; set; }

        [RegularExpression(@"/^ ([A - ZÑ &]{3, 4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$/", ErrorMessage = "Introducir solo numeros Naturales")]
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public string RFC { get; set; }
    }
}
