using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class OrderItemResultDto
    {
        [Required]
        public string Product { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int QuantityProduct { get; set; }
        [Required]
        public decimal Total { get; set; }

    }

}
