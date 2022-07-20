using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class DeliveryMethodDto
    {
        [Key]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, MaxLength(32)]
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
