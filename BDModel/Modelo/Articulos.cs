using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;

namespace BDModel
{
    [MetadataType(typeof(ArticulosValidation))]
    public partial class Articulos 
    {
        public string defaultImage
        {
            get
            {
                string filepath = HostingEnvironment.MapPath("~/ImagenesArticulos/Articulo_" + this.Id);
                DirectoryInfo d = new DirectoryInfo(filepath);
                FileInfo file = d.GetFiles().OrderBy(f => f.CreationTime).FirstOrDefault();

                //DateTime olderFile = d.GetFiles().Min(f => f.CreationTime);
                //foreach (FileInfo file in d.GetFiles())
                //{

                //}


                if (file != null)
                    return ("/ImagenesArticulos/Articulo_" + this.Id + "/" + file.Name);
                else
                    return string.Empty;
               

            }
        }
    }

    public class ArticulosValidation
    {
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public string Codigo { get; set; }

        [Display(Name = "Subcategoria")]
        public Nullable<int> SubcategoriaId { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public string Descripcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio Compra")]
        public Nullable<double> PrecioCompra { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio Venta")]
        public Nullable<double> PrecioVenta { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Introducir solo numeros Naturales")]
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public double Existencia { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Alta")]
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public System.DateTime FechaAlta { get; set; }

        [Display(Name = "Estatus")]
        [Required(ErrorMessage = "Favor de introducir {0}")]
        public bool Activo { get; set; }

        [Display(Name = "Unidad")]
        public Nullable<int> UnidadId { get; set; }

        [Display(Name = "Proveedor")]
        public Nullable<int> ProveedorId { get; set; }


    }
}
