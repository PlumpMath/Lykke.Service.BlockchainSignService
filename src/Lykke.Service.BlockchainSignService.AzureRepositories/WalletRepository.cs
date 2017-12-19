using AzureStorage;
using Lykke.Service.BlockchainSignService.Core.Domain;
using Lykke.Service.BlockchainSignService.Core.Repositories;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.AzureRepositories
{
    public class WalletEntity : TableEntity, IWallet
    {
        public Guid WalletId { get; set; }
        public string PublicAddress { get; set; }
        public string EncryptedPrivateKey { get; set; }

        public static string GeneratePartition()
        {
            return "Wallet";
        }

        public static string GenerateRowKey(Guid walletId)
        {
            return walletId.ToString();
        }

        public static WalletEntity Create(IWallet wallet)
        {
            return new WalletEntity
            {
                PartitionKey = GeneratePartition(),
                RowKey = GenerateRowKey(wallet.WalletId),
                WalletId = wallet.WalletId,
                PublicAddress = wallet.PublicAddress,
                EncryptedPrivateKey = wallet.EncryptedPrivateKey
            };
        }
    }

    public class WalletRepository : IWalletRepository
    {
        private readonly INoSQLTableStorage<WalletEntity> _tableStorage;

        public WalletRepository(INoSQLTableStorage<WalletEntity> tableStorage)
        {
            _tableStorage = tableStorage;
        }

        public async Task<IWallet> GetWalletAsync(Guid walletId)
        {
            return await _tableStorage.GetDataAsync(WalletEntity.GeneratePartition(), WalletEntity.GenerateRowKey(walletId));
        }

        public async Task<IEnumerable<IWallet>> GetAllAsync()
        {
            return await _tableStorage.GetDataAsync(WalletEntity.GeneratePartition());
        }

        public async Task SaveAsync(IWallet wallet)
        {
            WalletEntity entity = WalletEntity.Create(wallet);

            await _tableStorage.InsertAsync(entity);
        }
    }
}
