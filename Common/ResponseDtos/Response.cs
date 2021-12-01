using Common.Enums;

namespace Common.ResponseDtos
{
    public class Response
    {
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}