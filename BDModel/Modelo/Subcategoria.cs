using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDModel
{
    [MetadataType(typeof(SubcategoriaValidation))]
    public partial class Subcategoria
    {
    }

    public class SubcategoriaValidation
    {
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Favor de introducir {0}")]
        public string Nombre { get; set; }

        [Display(Name = "Estatus")]
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public bool Activo { get; set; }
    }
}
