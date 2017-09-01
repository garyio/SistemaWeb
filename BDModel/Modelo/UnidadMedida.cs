using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDModel.Modelo
{
    [MetadataType(typeof(CategoriasValidation))]
    public partial class UnidadMedida
    {
    }

    public class UnidadMedidaValidation
    {
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public string Nombre { get; set; }

        [Display(Name = "Estatus")]
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public bool Activo { get; set; }
    }
}
