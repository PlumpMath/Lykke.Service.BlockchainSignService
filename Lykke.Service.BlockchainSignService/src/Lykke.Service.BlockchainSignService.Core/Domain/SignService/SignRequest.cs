using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Core.Domain.SignService
{
    public class SignRequest
    {
        public string PrivateKey { get; set; }

        public string TransactionHex { get; set; }
    }
}
