using System;
using System.Net;

namespace GraphDotCms.Application.Exceptions
{
    public class RestException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Title { get; set; }
        public object Details { get; set; }
    }
}
