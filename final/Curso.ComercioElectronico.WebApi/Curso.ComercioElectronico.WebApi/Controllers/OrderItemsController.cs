using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase, IOrderItemAppService
    {
        private readonly IOrderItemAppService orderController;
        public OrderItemsController(IOrderItemAppService orderController)
        {
            this.orderController = orderController;
        }
        [HttpPost("CreateOrder")]
        public async Task<OrderItemDto> AddProductAsync(CreateOrderItemDto CreateOrderItem)
        {
            return await orderController.AddProductAsync(CreateOrderItem);
        }
        [HttpPut("{orderId}/Buscar")]
        public async Task<OrderDto> CancelAsync(Guid orderId)
        {
            return await orderController.CancelAsync(orderId);
        }

        [HttpGet("{orderId}")]
        public async Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            return await orderController.GetByIdAsync(orderId);
        }


        [HttpPut("{orderId}")]
        public async Task<OrderDto> PayAsync(Guid orderId)
        {
            return await orderController.PayAsync(orderId);
        }

        [HttpDelete("{orderId}/EliminarProducto/{productId}")]
        public async Task<bool> RemoveProductAsync(Guid orderId, Guid productId)
        {
            return await orderController.RemoveProductAsync(orderId, productId);
        }
        [HttpPut("{orderId}/ActualizarProducto")]
        public async Task<OrderItemDto> UpdateProductAsync(Guid orderId, UpdateOrderItemDto orderProduct)
        {
            return await orderController.UpdateProductAsync(orderId, orderProduct);
        }
    }
}
