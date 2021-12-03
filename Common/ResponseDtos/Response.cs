using System.Text.Json.Serialization;
using Common.Enums;

namespace Common.ResponseDtos
{
    public class Response
    {
        [JsonPropertyName("statusCode")]
        public StatusCode StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }

        
    }
}