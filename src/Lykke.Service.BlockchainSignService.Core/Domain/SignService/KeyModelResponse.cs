using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Core.Domain.SignService
{
    public class KeyModelResponse
    {
        public string PublicAddress { get; set; }
        public string PrivateKey { get; set; }
    }
}
