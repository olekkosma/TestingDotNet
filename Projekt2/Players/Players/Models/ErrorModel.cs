
namespace Players.Models
{
    public class ErrorModel
    {
        public string RequestId { get; set; }

        public string Response { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}