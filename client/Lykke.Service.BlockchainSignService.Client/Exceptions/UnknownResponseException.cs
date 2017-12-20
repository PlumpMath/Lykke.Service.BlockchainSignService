using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Client.Exceptions
{
    public class UnknownResponseException : Exception
    {
        public UnknownResponseException(string message) : base(message)
        {
        }
    }
}
