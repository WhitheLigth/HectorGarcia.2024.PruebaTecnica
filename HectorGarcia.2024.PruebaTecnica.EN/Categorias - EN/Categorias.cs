#region REFERENCIAS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencias Necesarias Para El Correcto Funcionamiento
using System.ComponentModel.DataAnnotations;
using HectorGarcia._2024.PruebaTecnica.EN.Productos___EN;

#endregion

namespace HectorGarcia._2024.PruebaTecnica.EN.Categorias___EN
{
    public class Categorias
    {
        #region ATRIBUTOS DE LA ENTIDAD
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Nombre")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ/ ]+$", ErrorMessage = "El Nombre debe contener solo Letras")]
        public string Nombre { get; set; } = string.Empty;
        #endregion

        public List<Productos>? Productos { get; set; } // Propiedad de navegacion
    }
}
