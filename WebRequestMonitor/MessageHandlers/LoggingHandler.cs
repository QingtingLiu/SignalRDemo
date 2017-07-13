using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebRequestMonitor.MessageHandlers
{
    public class LoggingHandler : DelegatingHandler
    {
        private static readonly string _loggingInfoKey = "loggingInfo";
        private ILoggingDisplay _loggingDisplay;

        public LoggingHandler(ILoggingDisplay loggingDisplay)
        {
            _loggingDisplay = loggingDisplay;
        }

        public LoggingHandler(HttpMessageHandler innerHandler, ILoggingDisplay loggingDisplay) : base(innerHandler)
        {
            _loggingDisplay = loggingDisplay;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return null;
        }

        private void LogRequestLoggingInfo(HttpRequestMessage request)
        {
            var info = new ApiLoggingInfo();
            info.HttpMethod = request.Method.Method;
            info.UriAccessed = request.RequestUri.AbsoluteUri;
            info.IpAddress = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "0.0.0.0";
            info.StartTime = DateTime.Now;
            ExtractMessageHeadersIntoLoggingInfo(info, request.Headers.ToList());
            request.Properties.Add(_loggingInfoKey, info);
        }

        private void LogResponseLoggingInfo(HttpResponseMessage response)
        {
            object loggingInfoObject = null;
            if (!response.RequestMessage.Properties.TryGetValue(_loggingInfoKey, out loggingInfoObject))
            {
                return;
            }
            var info = loggingInfoObject as ApiLoggingInfo;
            if (info == null)
            {
                return;
            }
            info.HttpMethod = response.RequestMessage.Method.ToString();
            info.ResponseStatusCode = response.StatusCode;
            info.ResponseStatusMessage = response.ReasonPhrase;
            info.UriAccessed = response.RequestMessage.RequestUri.AbsoluteUri;
            info.IpAddress = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "0.0.0.0";
            info.EndTime = DateTime.Now;
            info.TotalTime = (info.EndTime - info.StartTime).TotalMilliseconds;
            _loggingDisplay.Display(info);
        }


        private void ExtractMessageHeadersIntoLoggingInfo(ApiLoggingInfo info, List<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            headers.ForEach(h =>
            {
                var headerValues = new StringBuilder();
                if (h.Value != null)
                {
                    foreach (var hv in h.Value)
                    {
                        if (headerValues.Length > 0)
                        {
                            headerValues.Append(",");
                        }
                        headerValues.Append(hv);
                    }
                }
                info.Headers.Add($"{h.Key}:{headerValues.ToString()}");
            });
        }
    }
}