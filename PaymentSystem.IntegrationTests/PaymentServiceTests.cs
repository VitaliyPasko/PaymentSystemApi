using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Enums;
using Common.ResponseDtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces;
using PaymentSystem.IntegrationTests.Fakes.FakeServices;
using PaymentSystemApi;
using Xunit;

namespace PaymentSystem.IntegrationTests
{
    public class PaymentServiceTests
    {
        [Fact]
        public async Task CheckStatus_SendRequest_ShouldReturnOk()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                
            });

            HttpClient httpClient = webHost.CreateClient();

            // Act
            PaymentDto payment = new ()
            {
                Amount = 145,
                Phone = "7055779783",
                ExternalNumber = "a0c4a3fd-e23b-41b5-a18d-48d75c2fe730"
            };
            var content = new StringContent(JsonSerializer.Serialize(payment), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/payments", content);

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task AddPayment_Returns_UnableError()
        {
            // Arrange

            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var providerDeterminant =
                        services.SingleOrDefault(s => s.ServiceType == typeof(IProviderDeterminantService));
                    services.Remove(providerDeterminant);
                    services
                        .AddTransient<IProviderDeterminantService, FakeProviderDeterminantServiceGeneratingDeclining>();
                });
            });

            HttpClient httpClient = webHost.CreateClient();

            // Act
            PaymentDto payment = new ()
            {
                Amount = 145,
                Phone = "7055779783",
                ExternalNumber = "a0c4a3fd-e23b-41b5-a18d-48d75c2fe730"
            };
            var content = new StringContent(JsonSerializer.Serialize(payment), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/payments", content);

            // Assert

            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonString);

            result.Message.Should().BeEquivalentTo("Платеж отклонен.");
            result.StatusCode.Should().BeEquivalentTo(StatusCode.UnableError);
        }
    }
}