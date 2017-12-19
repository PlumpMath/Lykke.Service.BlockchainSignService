using System;
using Common.Log;

namespace Lykke.Service.BlockchainSignService.Client
{
    public class BlockchainSignServiceClient : IBlockchainSignServiceClient, IDisposable
    {
        private readonly ILog _log;

        public BlockchainSignServiceClient(string serviceUrl, ILog log)
        {
            _log = log;
        }

        public void Dispose()
        {
            //if (_service == null)
            //    return;
            //_service.Dispose();
            //_service = null;
        }
    }
}
