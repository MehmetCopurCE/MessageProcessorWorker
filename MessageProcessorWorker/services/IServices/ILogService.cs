using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessorWorker.services.IServices
{
    public interface ILogService
    {
        void Log(string message);

        void LogError(string message, Exception ex);

        void LogWarning(string message);

    }
}
