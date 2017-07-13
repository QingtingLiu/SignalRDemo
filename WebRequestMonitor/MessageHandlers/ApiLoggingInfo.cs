using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebRequestMonitor.MessageHandlers
{

    [Serializable]
    public class ApiLoggingInfo
    {
        private List<string> _headers = new List<string>();
        public List<string> Headers
        {
            get { return _headers; }
        }
        public string HttpMethod { get; set; }
        public string UriAccessed { get; set; }
        public string BodyContent { get; set; }
        public HttpStatusCode ResponseStatusCode { get; set; }
        public string ResponseStatusMessage { get; set; }
        public string IpAddress { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double TotalTime { get; set; }
    }
}