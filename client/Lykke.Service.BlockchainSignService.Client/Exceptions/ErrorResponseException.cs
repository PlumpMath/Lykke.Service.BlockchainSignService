using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Client.Exceptions
{
    public class ErrorResponseException : Exception
    {
        public ErrorResponseException(string message, IDictionary<string, IList<string>> modelErrors) : base(message)
        {
            ModelErrors = modelErrors;
        }

        public IDictionary<string, IList<string>> ModelErrors { get; private set; }
    }
}
