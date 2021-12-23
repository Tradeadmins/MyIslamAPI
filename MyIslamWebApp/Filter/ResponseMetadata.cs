using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MyIslamWebApp.Filter
{
    public class ResponseMetadata
    {
        public string Version { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object Content { get; set; }
        public string ErrorMessage { get; set; }
        public object Result { get; set; }
        public DateTime Timestamp { get; set; }
        public long? Size { get; set; }
    }
}