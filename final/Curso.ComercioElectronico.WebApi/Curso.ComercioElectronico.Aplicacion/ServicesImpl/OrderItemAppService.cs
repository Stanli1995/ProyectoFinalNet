using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.CursoElectronico.Dominio.Entities;
using Curso.CursoElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.ServicesImpl
{
    public class OrderItemAppService : IOrderItemAppService
    {
        private readonly IOrderRepository context;

        private readonly IMapper mapper;
        public OrderItemAppService(IOrderRepository context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<OrderItemDto> AddProductAsync(CreateOrderItemDto createOrderItem)
        {

            OrderItem orderItem = mapper.Map<OrderItem>(createOrderItem);
            OrderItemDto orderItemDto = mapper.Map<OrderItemDto>(await context.AddProductAsync(orderItem));
            return orderItemDto;
        }


        public async Task<OrderDto> CancelAsync(Guid orderId)
        {
            IQueryable<Order> query = await context.CancelAsync(orderId);
            OrderDto orderDto = await GetResult(query);
            return orderDto;

        }

        public async Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            var query = context.GetByIdAsync(orderId);

            if (await query.SingleOrDefaultAsync() == null)
                throw new ArgumentException($"No existe la orden con id: {orderId}");

            return await GetResult(query);

        }
        public async Task<OrderDto> GetResult(IQueryable<Order> query)
        {
            decimal total = 0;
            OrderDto orderDto = await query.Select(x => new OrderDto()
            {
                Code = x.Id,
                
                
                DeliveryMethod = x.DeliveryMethod,
                DeliveryMethodId = x.DeliveryMethodId,

                orderItems = x.orderItems.Select(y => new OrderItemResultDto() { Product = y.Product.Nombre, 
                                                                                           Price = y.Product.Precio, 
                                                                                           QuantityProduct = y.QuantityProduct, 
                                                                                           Total = y.Total }).ToList(),
                
                Subtotal = x.Subtotal,
                TotalPrice = x.TotalPrice
            }).SingleOrDefaultAsync();
            foreach (var product in orderDto.orderItems)
            {
                total += product.Total;
            }
            orderDto.Subtotal = total - (total * (decimal)0.12);
            //orderDto.Iva = (total * (decimal)0.12);Metodo para calcular iva falta implmentar 
            orderDto.TotalPrice = total;
            return orderDto;
        }


        public async Task<OrderDto> PayAsync(Guid orderId)
        {
            IQueryable<Order> query = await context.PayAsync(orderId);
            OrderDto orderDto = await GetResult(query);
            await context.UpdateOrderAsync(orderId, orderDto.Subtotal, orderDto.TotalPrice);
            return orderDto;

        }

        public async Task<bool> RemoveProductAsync(Guid orderId, Guid productId)
        {
            return await context.RemoveProductAsync(orderId, productId);
        }

        public async Task<OrderItemDto> UpdateProductAsync(Guid orderId, UpdateOrderItemDto orderItemDto)
        {
            OrderItem orderItem = mapper.Map<OrderItem>(orderItemDto);
            OrderItemDto orderItemResultDto = mapper.Map<OrderItemDto>(await context.UpdateProductAsync(orderId, orderItem));
            return orderItemResultDto;
        }

    }
}
