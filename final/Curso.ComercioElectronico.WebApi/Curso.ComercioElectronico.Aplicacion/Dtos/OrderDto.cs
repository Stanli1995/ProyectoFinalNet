using Curso.CursoElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class OrderDto
    {
        [Key]
        public Guid Code { get; set; }
        public virtual List<OrderItemResultDto> orderItems { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        [Required]
        public string DeliveryMethodId { get; set; }
        [Required]
        public decimal Subtotal { get; set; } = 0;
        [Required]
        public decimal TotalPrice { get; set; } = 0;

    }
}
