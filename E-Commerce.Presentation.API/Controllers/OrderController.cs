using AutoMapper;
using E_commerce.Domain.Entites.Orders;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Service;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Specifications.Orders;
using E_Commerce.Shared.Dtos.Auth;
using E_Commerce.Shared.Dtos.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Presentation.API.Controllers
{
    public class OrderController(IServiceManager manger) : APIBaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder(OrderRequestDto input)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await manger.OrderService.CreateOrderAsync(input, email.Value);

            return Ok(result);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResponseDto>>> GetAllDeliveryMethods()
        {
            var result = await manger.OrderService.GetAllDeliveryMethodsAsync();
            return Ok(result);
        }
        [HttpGet("UserOrders")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAllUserOrders()
        {
            var email = User.FindFirst(ClaimTypes.Email);

            var result = await manger.OrderService.GetAllOrdersForSpecificUserAsync(email.Value);
            return Ok(result);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderResponseDto>> GetOrderById(Guid Id)
        {
            var email = User.FindFirst(ClaimTypes.Email);
            var result = await manger.OrderService.GetOrderForSpecificUserByIdAsync(Id,email.Value);
            return Ok(result);
        }
    }
}
