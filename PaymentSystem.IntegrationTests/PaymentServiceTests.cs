using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;
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
        public async Task CheckStatus_SendRequest_ShouldReturnOk2()
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
    }
}