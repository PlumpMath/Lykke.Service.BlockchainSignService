using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Models.Responses
{
    [DataContract]
    public class WalletResponse
    {
        [DataMember(Name = "publicAddress")]
        public string PublicAddress { get; set; }
    }

    [DataContract]
    public class WalletsResponse
    {
        [DataMember(Name = "wallets")]
        public IEnumerable<WalletResponse> Wallets { get; set; }
    }
}
