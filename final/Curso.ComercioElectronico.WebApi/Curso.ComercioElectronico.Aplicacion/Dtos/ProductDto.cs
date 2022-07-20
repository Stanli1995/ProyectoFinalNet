using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public  class ProductDto
    {
        [Key]
        public Guid Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string TypeProduct { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public int Stock { get; set; }


    }
}
