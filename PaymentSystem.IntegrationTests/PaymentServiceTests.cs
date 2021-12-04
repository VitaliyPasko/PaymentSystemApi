using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Enums;
using Common.ResponseDtos;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.ApplicationLayer.Data;
using PaymentSystem.ApplicationLayer.Extensions;
using PaymentSystem.ApplicationLayer.Services.PaymentService.Dto;
using PaymentSystem.ApplicationLayer.Services.ProviderDeterminantService.Interfaces;
using PaymentSystem.IntegrationTests.Helpers;
using PaymentSystemApi;
using Xunit;

namespace PaymentSystem.IntegrationTests
{
    public class PaymentServiceTests
    {

        private readonly WebApplicationFactory<Startup> _webHost;
        private readonly HttpClient _httpClient;

        public PaymentServiceTests()
        {
            _webHost = Utilities.SubstituteDbOnTestDb();
            _httpClient = _webHost.CreateClient();
        }
        
        [Fact]
        public async Task AddPayment_Returns_Success()
        {
            // Act
            PaymentDto payment = Utilities.GetValidPaymentDto();
            var content = new StringContent(JsonSerializer.Serialize(payment), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("api/payments", content);

            // Assert
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonString);

            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
            result.Message.Should().BeEquivalentTo("Платеж пополнен успешно.");
            result.StatusCode.Should().BeEquivalentTo(StatusCode.Success);
        }
        
        [Fact]
        public async Task AddPayment_Returns_ProviderNotFound()
        {
            // Act
            PaymentDto payment = Utilities.GetPaymentWithInvalidValidProvider();
            var content = new StringContent(JsonSerializer.Serialize(payment), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("api/payments", content);

            // Assert
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonString);

            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
            result.Message.Should().BeEquivalentTo($"Провайдера с таким префиксом не найдено: {payment.Phone[..3]}");
            result.StatusCode.Should().BeEquivalentTo(StatusCode.ProviderNotFound);
        }
        
        [Fact]
        public async Task AddPayment_Returns_PhoneValidationProblem()
        {
            // Act
            PaymentDto payment = Utilities.GetPaymentWithInvalidValidPhone();
            var content = new StringContent(JsonSerializer.Serialize(payment), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("api/payments", content);

            // Assert
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonString);

            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
            result.Message.Should().BeEquivalentTo("Валидация провалилась.");
            result.StatusCode.Should().BeEquivalentTo(StatusCode.ValidationProblem);
        }
        
        [Fact]
        public async Task AddPayment_Returns_AmountValidationProblem()
        {
            // Act
            PaymentDto payment = Utilities.GetPaymentWithInvalidValidAmount();
            var content = new StringContent(JsonSerializer.Serialize(payment), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("api/payments", content);

            // Assert
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonString);

            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
            result.Message.Should().BeEquivalentTo("Валидация провалилась.");
            result.StatusCode.Should().BeEquivalentTo(StatusCode.ValidationProblem);
        }
        
        [Fact]
        public async Task AddPayment_Returns_DuplicateExternalNumber()
        {
            // Arrange
            ApplicationDbContext dbContext =
                _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()!;
            IProviderDeterminantService providerDeterminantService =
                _webHost.Services.CreateScope().ServiceProvider.GetService<IProviderDeterminantService>()!;

            // Act
            var paymentDto = Utilities.GetValidPaymentDto();
            var provider = providerDeterminantService.GetProvider(paymentDto.Phone);
            var payment = paymentDto.MapToPayment(provider.ProviderType);
            
            await dbContext.Payments.AddAsync(payment);
            await dbContext.SaveChangesAsync();
            var content = new StringContent(JsonSerializer.Serialize(paymentDto), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("api/payments", content);

            // Assert
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonString);

            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
            result.Message.Should().BeEquivalentTo("Платеж не удался. Повторяющийся ExternalNumber");
            result.StatusCode.Should().BeEquivalentTo(StatusCode.DuplicateExternalNumber);
        }
        
        [Fact]
        public async Task AddPayment_Returns_UnableError()
        {
            // Arrange
            WebApplicationFactory<Startup> webHost =
                Utilities.SubstituteOnFakeDecliningRequestProvider();
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
        
        [Fact]
        public async Task AddPayment_Returns_UnavailableService()
        {
            // Arrange
            WebApplicationFactory<Startup> webHost =
                Utilities.SubstituteOnFakeUnavailableRequestProvider();
            HttpClient httpClient = webHost.CreateClient();

            // Act
            PaymentDto payment = Utilities.GetValidPaymentDto();
            var content = new StringContent(JsonSerializer.Serialize(payment), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/payments", content);

            // Assert
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Response>(jsonString);

            response.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
            result.Message.Should().BeEquivalentTo("Сервис недоступен.");
            result.StatusCode.Should().BeEquivalentTo(StatusCode.ServiceUnavailable);
        }
        
        [Fact]
        public async Task AddPaymentWith777Prefix_Returns_SuccessAndProviderTypeBeeline()
        {
            // Arrange
            ApplicationDbContext dbContext =
                _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()!;
            // Act
            PaymentDto paymentDto = Utilities.GetPaymentWith777Prefix();
            var content = new StringContent(JsonSerializer.Serialize(paymentDto), Encoding.UTF8, "application/json");
            HttpResponseMessage _ = await _httpClient.PostAsync("api/payments", content);

            // Assert
            var payment = await dbContext.Payments.FirstOrDefaultAsync(p => p.Phone == paymentDto.Phone && p.ExternalNumber == paymentDto.ExternalNumber);
            payment.ProviderType.Should().BeEquivalentTo(ProviderType.Beeline);
        }
        
        [Fact]
        public async Task AddPaymentWith705Prefix_Returns_SuccessAndProviderTypeBeeline()
        {
            // Arrange
            ApplicationDbContext dbContext =
                _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()!;
            HttpClient httpClient = _webHost.CreateClient();

            // Act
            PaymentDto paymentDto = Utilities.GetPaymentWith705Prefix();
            var content = new StringContent(JsonSerializer.Serialize(paymentDto), Encoding.UTF8, "application/json");
            HttpResponseMessage _ = await httpClient.PostAsync("api/payments", content);

            // Assert
            var payment = await dbContext.Payments.FirstOrDefaultAsync(p => p.Phone == paymentDto.Phone && p.ExternalNumber == paymentDto.ExternalNumber);
            payment.ProviderType.Should().BeEquivalentTo(ProviderType.Beeline);
        }
        
        [Fact]
        public async Task AddPaymentWith701Prefix_Returns_SuccessAndProviderTypeActiv()
        {
            // Arrange
            ApplicationDbContext dbContext =
                _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()!;

            // Act
            PaymentDto paymentDto = Utilities.GetPaymentWith701Prefix();
            var content = new StringContent(JsonSerializer.Serialize(paymentDto), Encoding.UTF8, "application/json");
            HttpResponseMessage _ = await _httpClient.PostAsync("api/payments", content);

            // Assert
            var payment = await dbContext.Payments.FirstOrDefaultAsync(p => p.Phone == paymentDto.Phone && p.ExternalNumber == paymentDto.ExternalNumber);
            payment.ProviderType.Should().BeEquivalentTo(ProviderType.Activ);
        }
        
        [Fact]
        public async Task AddPaymentWith708Prefix_Returns_SuccessAndProviderTypeAltel()
        {
            // Arrange
            ApplicationDbContext dbContext =
                _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()!;

            // Act
            PaymentDto paymentDto = Utilities.GetPaymentWith708Prefix();
            var content = new StringContent(JsonSerializer.Serialize(paymentDto), Encoding.UTF8, "application/json");
            HttpResponseMessage _ = await _httpClient.PostAsync("api/payments", content);

            // Assert
            var payment = await dbContext.Payments.FirstOrDefaultAsync(p => p.Phone == paymentDto.Phone && p.ExternalNumber == paymentDto.ExternalNumber);
            payment.ProviderType.Should().BeEquivalentTo(ProviderType.Altel);
        }
        
        [Fact]
        public async Task AddPaymentWith700Prefix_Returns_SuccessAndProviderTypeAltel()
        {
            // Arrange
            ApplicationDbContext dbContext =
                _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()!;

            // Act
            PaymentDto paymentDto = Utilities.GetPaymentWith700Prefix();
            var content = new StringContent(JsonSerializer.Serialize(paymentDto), Encoding.UTF8, "application/json");
            HttpResponseMessage _ = await _httpClient.PostAsync("api/payments", content);

            // Assert
            var payment = await dbContext.Payments.FirstOrDefaultAsync(p => p.Phone == paymentDto.Phone && p.ExternalNumber == paymentDto.ExternalNumber);
            payment.ProviderType.Should().BeEquivalentTo(ProviderType.Altel);
        }
        
        [Fact]
        public async Task AddPaymentWith707Prefix_Returns_SuccessAndProviderTypeTeleTwo()
        {
            // Arrange
            ApplicationDbContext dbContext =
                _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()!;

            // Act
            PaymentDto paymentDto = Utilities.GetPaymentWith707Prefix();
            var content = new StringContent(JsonSerializer.Serialize(paymentDto), Encoding.UTF8, "application/json");
            HttpResponseMessage _ = await _httpClient.PostAsync("api/payments", content);

            // Assert
            var payment = await dbContext.Payments.FirstOrDefaultAsync(p => p.Phone == paymentDto.Phone && p.ExternalNumber == paymentDto.ExternalNumber);
            payment.ProviderType.Should().BeEquivalentTo(ProviderType.TeleTwo);
        }
        
        [Fact]
        public async Task AddPaymentWith747Prefix_Returns_SuccessAndProviderTypeTeleTwo()
        {
            // Arrange
            ApplicationDbContext dbContext =
                _webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>()!;

            // Act
            PaymentDto paymentDto = Utilities.GetPaymentWith747Prefix();
            var content = new StringContent(JsonSerializer.Serialize(paymentDto), Encoding.UTF8, "application/json");
            HttpResponseMessage _ = await _httpClient.PostAsync("api/payments", content);

            // Assert
            var payment = await dbContext.Payments.FirstOrDefaultAsync(p => p.Phone == paymentDto.Phone && p.ExternalNumber == paymentDto.ExternalNumber);
            payment.ProviderType.Should().BeEquivalentTo(ProviderType.TeleTwo);
        }
    }
}