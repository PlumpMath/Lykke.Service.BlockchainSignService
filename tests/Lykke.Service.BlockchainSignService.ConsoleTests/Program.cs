using Lykke.Service.BlockchainSignService.Client;
using System;

namespace Lykke.Service.BlockchainSignService.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var client = new BlockchainSignServiceClient("", null);

            var result = client.CreateWalletAsync().Result;
            var result1 = client.GetWalletByIdAsync(result.WalletId).Result;
            var result2 = client.GetWalletByPublicAddressAsync(result.PublicAddress).Result;

            var signed = client.SignTransactionAsync(new Lykke.Service.BlockchainSignService.Client.Models.SignRequestModel(new[] { result.WalletId },
                "ed82ee25850ba43b7400827530949ee9509a41faf2e27fca5a3016031e3105c265d687b1a2bc2ec5000080808080")).Result;

            var allWallets = client.GetAllWalletsAsync().Result;
            client.Dispose();
        }
    }
}
