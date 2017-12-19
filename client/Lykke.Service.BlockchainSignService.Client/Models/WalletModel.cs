
namespace Lykke.Service.BlockchainSignService.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class WalletModel
    {
        public WalletModel(System.Guid walletId, string publicAddress = default(string))
        {
            PublicAddress = publicAddress;
            WalletId = walletId;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PublicAddress")]
        public string PublicAddress { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "WalletId")]
        public System.Guid WalletId { get; set; }
    }
}
