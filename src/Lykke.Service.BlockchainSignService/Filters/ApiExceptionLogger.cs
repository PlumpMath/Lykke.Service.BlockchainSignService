using Common.Log;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Filters
{
    public class ApiExceptionLogger : IExceptionFilter
    {
        private readonly ILog _log;

        public ApiExceptionLogger(ILog log)
        {
            _log = log;
        }

        public void OnException(ExceptionContext context)
        {
            var tasks = new List<Task>();
            var aggrExc = context.Exception as AggregateException;
            if (aggrExc != null)
            {
                foreach (var exc in aggrExc.InnerExceptions)
                {
                    tasks.Add(_log.WriteErrorAsync("Web Api", context.HttpContext.Request.GetUri().AbsoluteUri, exc, DateTime.UtcNow));
                }
            }
            else
            {
                tasks.Add(_log.WriteErrorAsync("Web Api", context.HttpContext.Request.GetUri().AbsoluteUri, context.Exception, DateTime.UtcNow));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }

    public class ApiExceptionLoggerFactory : IFilterFactory
    {
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var log = (ILog)serviceProvider.GetService(typeof(ILog));
            return new ApiExceptionLogger(log);
        }
    }
}
