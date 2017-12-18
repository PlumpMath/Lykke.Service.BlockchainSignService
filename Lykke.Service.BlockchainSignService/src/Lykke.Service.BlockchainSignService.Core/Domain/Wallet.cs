using System;
using System.Collections.Generic;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Core.Domain
{
    public interface IWallet
    {
        Guid WalletId { get; set; }
        string PublicAddress { get; set; }
        string EncryptedPrivateKey { get; set; }
    }

    public class Wallet : IWallet
    {
        public Guid WalletId { get; set; }
        public string PublicAddress { get; set; }
        public string EncryptedPrivateKey { get; set; }
    }
}
