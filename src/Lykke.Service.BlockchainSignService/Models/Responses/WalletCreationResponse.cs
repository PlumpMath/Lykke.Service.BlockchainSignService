using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Models.Responses
{
    public class WalletCreationResponse
    {
        public string PublicAddress { get; set; }
        public Guid WalletId { get; set; }
    }
}
