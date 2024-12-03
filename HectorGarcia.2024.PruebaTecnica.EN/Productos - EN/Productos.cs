#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencia Necesarias Para El Correcto Funcionamiento
using System.ComponentModel.DataAnnotations;
using HectorGarcia._2024.PruebaTecnica.EN.Categorias___EN;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace HectorGarcia._2024.PruebaTecnica.EN.Productos___EN
{
    public class Productos
    {
        #region ATRIBUTOS DE LA ENTIDAD
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Nombre")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ/ ]+$", ErrorMessage = "El Nombre debe contener solo Letras")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero")]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency, ErrorMessage = "El formato del precio no es válido")]
        public decimal Precio { get; set; } = 0.00m;

        [ForeignKey("Categorias")]
        [Required(ErrorMessage = "La categoria es requerido")]
        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        #endregion

        public Categorias? Categorias { get; set; } // Propiedad de navegacion
    }
}
