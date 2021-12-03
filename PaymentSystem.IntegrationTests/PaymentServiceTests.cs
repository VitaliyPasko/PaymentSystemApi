using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Enums;
using Common.ResponseDtos;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces;
using PaymentSystem.IntegrationTests.Fakes.FakeServices;
using PaymentSystem.IntegrationTests.Helpers;
using PaymentSystemApi;
using Xunit;

namespace PaymentSystem.IntegrationTests
{
    public class PaymentServiceTests
    {
        [Fact]
        public async Task AddPayment_Returns_UnableError()
        {
            // Arrange
            WebApplicationFactory<Startup> webHost =
                Utilities.SubstituteOnFakeProviderDeterminantGeneratingDecliningRequestProvider();

            HttpClient httpClient = webHost.CreateClient();

            // Act
            PaymentDto payment = Utilities.GetValidPaymentDto();
            var content = new StringContent(JsonSerializer.Serialize(payment), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/payments", content);

            // Assert
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonString);

            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
            result.Message.Should().BeEquivalentTo("Платеж отклонен.");
            result.StatusCode.Should().BeEquivalentTo(StatusCode.UnableError);
        }
    }
}