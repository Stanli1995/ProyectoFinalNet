using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class OrderItemDto
    {
        [Key]
        public Guid OrderId { get; set; }
        
        public Guid ProductId { get; set; }
        [Required]
        public string Product { get; set; }
        [Required]
        public int QuantityProduct { get; set; }
        public decimal Total { get; set; }

    }
}
