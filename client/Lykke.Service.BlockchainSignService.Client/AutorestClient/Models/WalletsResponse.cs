// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.BlockchainSignService.Client.AutorestClient.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class WalletsResponse
    {
        /// <summary>
        /// Initializes a new instance of the WalletsResponse class.
        /// </summary>
        public WalletsResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the WalletsResponse class.
        /// </summary>
        public WalletsResponse(IList<WalletResponse> wallets = default(IList<WalletResponse>))
        {
            Wallets = wallets;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Wallets")]
        public IList<WalletResponse> Wallets { get; set; }

    }
}