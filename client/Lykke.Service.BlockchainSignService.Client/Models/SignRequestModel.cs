namespace Lykke.Service.BlockchainSignService.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class SignRequestModel
    {
      
        public SignRequestModel(System.Guid walletId, string transactionHex = default(string))
        {
            WalletId = walletId;
            TransactionHex = transactionHex;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "WalletId")]
        public System.Guid WalletId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "TransactionHex")]
        public string TransactionHex { get; set; }
    }
}
