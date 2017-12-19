using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.BlockchainSignService.Client.AutorestClient;
using Lykke.Service.BlockchainSignService.Client.AutorestClient.Models;
using Lykke.Service.BlockchainSignService.Client.Models;

namespace Lykke.Service.BlockchainSignService.Client
{
    public class BlockchainSignServiceClient : IBlockchainSignServiceClient, IDisposable
    {
        private readonly ILog _log;
        private readonly IBlockchainSignServiceAPI _api;

        public BlockchainSignServiceClient(string serviceUrl, ILog log)
        {
            _log = log;
            _api = new BlockchainSignServiceAPI(new Uri(serviceUrl));
        }

        public async Task<WalletModel> CreateWalletAsync()
        {
            var response = await _api.CreateWalletAsync();
            WalletCreationResponse wallet = response as WalletCreationResponse;

            return new WalletModel(wallet.WalletId, wallet.PublicAddress);
        }

        public async Task<SignedTransactionModel> SignTransactionAsync(SignRequestModel requestModel)
        {
            var request = new SignTransactionRequest(requestModel.WalletId, requestModel.TransactionHex);
            var response = await _api.SignTransactionAsync(request);
            SignTransactionResponse signTransactionResponse = response as SignTransactionResponse;

            return new SignedTransactionModel(signTransactionResponse.SignedTransaction);
        }

        public void Dispose()
        {
            if (_api== null)
                return;
            _api.Dispose();
        }
    }
}
