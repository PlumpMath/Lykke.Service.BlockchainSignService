using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Core.Domain
{
    public class WalletCreationResult
    {
        public string PublicAddress { get; set; }
        public Guid WalletId { get; set; }
    }
}
