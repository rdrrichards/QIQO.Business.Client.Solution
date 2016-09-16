using Prism.Logging;
using L = QIQO.Common.Core.Logging;

namespace QIQO.Business.Client.Core
{
    public class LoggerAdapter : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            string formatted_msg = priority.ToString() + " - " + message;
            switch (category)
            {
                case Category.Debug:
                    L.Log.Debug(formatted_msg);
                    break;
                case Category.Exception:
                    L.Log.Error(formatted_msg);
                    break;
                case Category.Info:
                    L.Log.Info(formatted_msg);
                    break;
                case Category.Warn:
                    L.Log.Warn(formatted_msg);
                    break;
            }
        }
    }
}
