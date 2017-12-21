using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Core.Exceptions
{
    public class ClientSideException : Exception
    {
        public enum ClientSideExceptionType
        {
            ValidationError,
            SignError,
            EntityDoesNotExist,
            SignServiceError
        }

        public ClientSideExceptionType Type { get; }

        public ClientSideException(string message, ClientSideExceptionType type) : base(message)
        {
            Type = type;
        }
    }
}
