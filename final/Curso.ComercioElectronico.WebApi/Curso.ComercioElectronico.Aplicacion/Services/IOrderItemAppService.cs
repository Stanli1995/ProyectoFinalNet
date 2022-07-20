using Curso.ComercioElectronico.Aplicacion.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IOrderItemAppService
    {
        public Task<OrderItemDto> AddProductAsync(CreateOrderItemDto createOrderProduct);
        public Task<OrderItemDto> UpdateProductAsync(Guid orderId, UpdateOrderItemDto orderProduct);
        public Task<bool> RemoveProductAsync(Guid orderId, Guid productId);
        public Task<OrderDto> GetByIdAsync(Guid orderId);
        public Task<OrderDto> PayAsync(Guid orderId);
        public Task<OrderDto> CancelAsync(Guid orderId); 

    }
}
