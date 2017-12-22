namespace Lykke.Service.BlockchainSignService.Client.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class SignRequestModel
    {
      
        public SignRequestModel(IEnumerable<string> publicAddresses, string transactionHex = default(string))
        {
            PublicAddresses = publicAddresses;
            TransactionHex = transactionHex;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "walletIds")]
        public IEnumerable<string> PublicAddresses { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "transactionHex")]
        public string TransactionHex { get; set; }
    }
}
