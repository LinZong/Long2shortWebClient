using System;

namespace Long2shortWebClient.Models
{
    
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string ErrorReason { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    }
}
