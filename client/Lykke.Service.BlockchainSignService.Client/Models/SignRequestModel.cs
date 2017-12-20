namespace Lykke.Service.BlockchainSignService.Client.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class SignRequestModel
    {
      
        public SignRequestModel(IEnumerable<System.Guid> walletIds, string transactionHex = default(string))
        {
            WalletIds = walletIds;
            TransactionHex = transactionHex;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "WalletIds")]
        public IEnumerable<System.Guid> WalletIds { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "TransactionHex")]
        public string TransactionHex { get; set; }
    }
}
