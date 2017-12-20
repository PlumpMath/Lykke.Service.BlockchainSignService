using AzureStorage;
using AzureStorage.Tables.Templates.Index;
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
        private readonly INoSQLTableStorage<AzureIndex> _indexTable;
        private const string _indexPartition = "PublicAddressIndex";

        public WalletRepository(INoSQLTableStorage<WalletEntity> tableStorage, INoSQLTableStorage<AzureIndex> indexTable)
        {
            _tableStorage = tableStorage;
            _indexTable = indexTable;
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
            AzureIndex index = AzureIndex.Create(_indexPartition, wallet.PublicAddress, entity);

            await _tableStorage.InsertAsync(entity);
            await _indexTable.InsertAsync(index);
        }

        public async Task<IWallet> GetWalletByPublicAddressAsync(string publicAddress)
        {
            AzureIndex index = await _indexTable.GetDataAsync(_indexPartition, publicAddress);

            if (index == null)
            {
                return null;
            }

            IWallet wallet = await _tableStorage.GetDataAsync(index);

            return wallet;
        }
    }
}
