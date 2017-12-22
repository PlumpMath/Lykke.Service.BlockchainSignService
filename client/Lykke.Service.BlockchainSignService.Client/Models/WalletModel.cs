
namespace Lykke.Service.BlockchainSignService.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class WalletModel
    {
        public WalletModel(string publicAddress)
        {
            PublicAddress = publicAddress;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "publicAddress")]
        public string PublicAddress { get; set; }
    }
}
