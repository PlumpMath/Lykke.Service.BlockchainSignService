using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Models.Responses
{
    public class WalletResponse
    {
        public string PublicAddress { get; set; }
        public Guid WalletId { get; set; }
    }

    public class WalletsResponse
    {
        public IEnumerable<WalletResponse> Wallets { get; set; }
    }
}
