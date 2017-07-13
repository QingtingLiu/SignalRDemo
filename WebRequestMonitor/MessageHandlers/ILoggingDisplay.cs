using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRequestMonitor.MessageHandlers
{
    public interface ILoggingDisplay
    {
        void Display(ApiLoggingInfo apiLoggingInfo);
    }
}
