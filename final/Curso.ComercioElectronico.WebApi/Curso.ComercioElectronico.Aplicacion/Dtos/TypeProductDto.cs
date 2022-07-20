using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class TypeProductDto
    {
        [Key]
        public string Codigo { get; set; }
        [Required]
        public string Nombre { get; set; }
        
        public DateTime CreateDate { get; set; }
    }
}
