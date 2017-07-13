using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebRequestMonitor.MessageHandlers;

namespace WebRequestMonitor.SignalR
{
    public class SignalRLoggingDisplay : ILoggingDisplay
    {

        private static readonly IPersistentConnectionContext connectionContext = GlobalHost.ConnectionManager.GetConnectionContext<RequestMonitor>();


        public void Display(ApiLoggingInfo loggingInfo)
        {
            var message = new StringBuilder();
            message.AppendFormat(
                $"StartTime:{loggingInfo.StartTime},Method:{loggingInfo.HttpMethod},Url:{loggingInfo.UriAccessed},ResponseStatus:{loggingInfo.ResponseStatusCode},TotalTime:{loggingInfo.TotalTime}");
            message.AppendLine();
            message.AppendFormat($"Headers:{string.Join(",", loggingInfo.Headers)},Body:{loggingInfo.BodyContent}");
            connectionContext.Connection.Broadcast(message.ToString());
        }
    }
}