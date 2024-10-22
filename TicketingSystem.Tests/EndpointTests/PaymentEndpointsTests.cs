using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Reflection;
using TicketingSystem.ApiService.Endpoints;
using TicketingSystem.ApiService.Services.PaymentService;
using FluentAssertions;
using TicketingSystem.Common.Model.Database.Enums;

namespace TicketingSystem.Tests.EndpointTests
{
    [TestClass]
    public class PaymentEndpointsTests
    {
        private readonly Mock<IPaymentService> _mockPaymentService;
        private readonly PaymentEndpoints _endpoints;

        public PaymentEndpointsTests()
        {
            _mockPaymentService = new Mock<IPaymentService>();
            _endpoints = new PaymentEndpoints();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockPaymentService.Verify();
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public async Task GetPaymentTest(bool resultDtoExists, bool expectedResultIsOk)
        {
            // Arrange
            var method = _endpoints.GetType().GetMethod("GetPayment", BindingFlags.NonPublic | BindingFlags.Instance);

            var paymentId = 1;
            var paymentStatus = PaymentStatus.Pending;

            Results<Ok<PaymentStatus>, NotFound> expectedResult = expectedResultIsOk
                ? TypedResults.Ok(paymentStatus)
                : TypedResults.NotFound();

            _mockPaymentService.Setup(x => x.GetStatusByIdAsync(paymentId)).Returns(Task.FromResult((PaymentStatus?)paymentStatus));

            // Act
            var result = await (Task<Results<Ok<PaymentStatus>, NotFound>>)method!.Invoke(_endpoints, [paymentId, _mockPaymentService.Object])!;

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public async Task CompletePaymentTest(bool paymentCompleted, bool expectedResultIsOk)
        {
            // Arrange
            var method = _endpoints.GetType().GetMethod("CompletePayment", BindingFlags.NonPublic | BindingFlags.Instance);

            var paymentId = 1;

            Results<Ok, NotFound> expectedResult = expectedResultIsOk
                ? TypedResults.Ok()
                : TypedResults.NotFound();

            _mockPaymentService.Setup(x => x.CompletePayment(paymentId)).Returns(Task.FromResult(paymentCompleted));

            // Act
            var result = await (Task<Results<Ok, NotFound>>)method!.Invoke(_endpoints, [paymentId, _mockPaymentService.Object])!;

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [DataTestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public async Task FailPaymentTest(bool paymentCompleted, bool expectedResultIsOk)
        {
            // Arrange
            var method = _endpoints.GetType().GetMethod("FailPayment", BindingFlags.NonPublic | BindingFlags.Instance);

            var paymentId = 1;

            Results<Ok, NotFound> expectedResult = expectedResultIsOk
                ? TypedResults.Ok()
                : TypedResults.NotFound();

            _mockPaymentService.Setup(x => x.FailPayment(paymentId)).Returns(Task.FromResult(paymentCompleted));

            // Act
            var result = await (Task<Results<Ok, NotFound>>)method!.Invoke(_endpoints, [paymentId, _mockPaymentService.Object])!;

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
