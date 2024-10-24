using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Reflection;
using TicketingSystem.ApiService.Endpoints;
using TicketingSystem.Common.Model.DTOs.Output;
using TicketingSystem.ApiService.Services.OrderService;
using FluentAssertions;
using TicketingSystem.Common.Model.DTOs.Input;

namespace TicketingSystem.Tests.EndpointTests
{
    [TestClass]
    public class OrderEndpointsTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrderEndpoints _endpoints;

        public OrderEndpointsTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _endpoints = new OrderEndpoints();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockOrderService.Verify();
        }

        [TestMethod]
        public async Task GetTicketsInCartTest()
        {
            // Arrange
            var method = _endpoints.GetType().GetMethod("GetTicketsInCart", BindingFlags.NonPublic | BindingFlags.Instance);

            Guid cartId = Guid.Empty;
            var ticketDtos = new List<TicketDto>
            {
                new() 
                {
                    CartId = cartId,
                    EventId = 1,
                    PersonId = 1,
                    PriceCategoryId = 1,
                    SeatId = 1,
                    Status = Common.Model.Database.Enums.TicketStatus.Free,
                    TicketId = 1
                }
            };

            var expectedResult = TypedResults.Ok(ticketDtos);

            _mockOrderService.Setup(x => x.GetTicketsInCartAsync(cartId)).Returns(Task.FromResult(ticketDtos));

            // Act
            var result = await (Task<Ok<List<TicketDto>>>)method!.Invoke(_endpoints, [cartId, _mockOrderService.Object])!;

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public async Task AddTicketToCartTest(bool resultDtoExists, bool expectedResultIsOk)
        {
            // Arrange
            var method = _endpoints.GetType().GetMethod("AddTicketToCart", BindingFlags.NonPublic | BindingFlags.Instance);

            var inputDto = new AddTicketToCartDto
            {
                EventId = 1,
                SeatId = 1
            };

            Guid cartId = Guid.Empty;

            CartDto? cartDto = null;
            string? errorMsg = "Cart not found";
            if (resultDtoExists)
            {
                cartDto = new CartDto
                {
                    CartId = cartId,
                    CartStatus = Common.Model.Database.Enums.CartStatus.NotPaid,
                    PaymentId = 1,
                };
                errorMsg = null;
            }

            Results<Ok<CartDto>, NotFound<string>> expectedResult = expectedResultIsOk
                ? TypedResults.Ok(cartDto!.Value)
                : TypedResults.NotFound(errorMsg);

            _mockOrderService.Setup(x => x.AddTicketToCartAsync(cartId, inputDto.EventId, inputDto.SeatId)).Returns(Task.FromResult(((CartDto?)cartDto, errorMsg)));

            // Act
            var result = await (Task<Results<Ok<CartDto>, NotFound<string>>>)method!.Invoke(_endpoints, [cartId, inputDto, _mockOrderService.Object])!;

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public async Task RemoveTicketFromCartTest(bool resultDtoExists, bool expectedResultIsOk)
        {
            // Arrange
            var method = _endpoints.GetType().GetMethod("RemoveTicketFromCart", BindingFlags.NonPublic | BindingFlags.Instance);
            var eventId = 1;
            var seatId = 1;

            Guid cartId = Guid.Empty;

            CartDto? cartDto = null;
            string? errorMsg = "Cart not found";
            if (resultDtoExists)
            {
                cartDto = new CartDto
                {
                    CartId = cartId,
                    CartStatus = Common.Model.Database.Enums.CartStatus.NotPaid,
                    PaymentId = 1,
                };
                errorMsg = null;
            }

            Results<Ok, NotFound<string>> expectedResult = expectedResultIsOk
                ? TypedResults.Ok()
                : TypedResults.NotFound(errorMsg);

            _mockOrderService.Setup(x => x.RemoveTicketFromCartAsync(cartId, eventId, seatId)).Returns(Task.FromResult(errorMsg));

            // Act
            var result = await (Task<Results<Ok, NotFound<string>>>)method!.Invoke(_endpoints, [cartId, eventId, seatId, _mockOrderService.Object])!;

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public async Task BookTicketsInCart(bool resultDtoExists, bool expectedResultIsOk)
        {
            // Arrange
            var method = _endpoints.GetType().GetMethod("BookTicketsInCart", BindingFlags.NonPublic | BindingFlags.Instance);

            Guid cartId = Guid.Empty;

            PaymentDto? paymentDto = null;
            if (resultDtoExists)
            {
                paymentDto = new()
                {
                    PaymentId = 1,
                    PaymentStatus = Common.Model.Database.Enums.PaymentStatus.Pending,
                    PaymentTime = new DateTime(2024, 1, 1)
                };
            }

            Results<Ok<PaymentDto>, NotFound> expectedResult = expectedResultIsOk
                ? TypedResults.Ok(paymentDto!.Value)
                : TypedResults.NotFound();

            _mockOrderService.Setup(x => x.BookTicketsInCart(cartId)).Returns(Task.FromResult(paymentDto));

            // Act
            var result = await (Task<Results<Ok<PaymentDto>, NotFound>>)method!.Invoke(_endpoints, [cartId, _mockOrderService.Object])!;

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
