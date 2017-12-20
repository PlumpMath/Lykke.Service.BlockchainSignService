using Lykke.Service.BlockchainSignService.Core.Domain;
using Lykke.Service.BlockchainSignService.Core.Domain.SignService;
using Lykke.Service.BlockchainSignService.Core.Repositories;
using Lykke.Service.BlockchainSignService.Core.Services;
using Lykke.Service.BlockchainSignService.Core.Settings;
using Lykke.Service.BlockchainSignService.Core.Settings.ServiceSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IInternalSignServiceCaller _internalSignServiceCaller;
        private readonly IEncryptionService _encryptionService;
        private readonly byte[] _passwordBytes;

        public WalletService(
            IWalletRepository walletRepository,
            IInternalSignServiceCaller internalSignServiceCaller,
            IEncryptionService encryptionService,
            BlockchainSignServiceSettings settings 
            )
        {
            _walletRepository = walletRepository;
            _internalSignServiceCaller = internalSignServiceCaller;
            _encryptionService = encryptionService;
            _passwordBytes = settings.PasswordBytes;
        }

        public async Task<WalletCreationResult> CreateWalletAsync()
        {
            Guid walletId = Guid.NewGuid();

            KeyModelResponse response = await _internalSignServiceCaller.CreateWalletAsync();
            string encryptedPrivateKey = 
                _encryptionService.EncryptAesString(response.PrivateKey, _passwordBytes);
            await _walletRepository.SaveAsync(new Wallet()
            {
                EncryptedPrivateKey = encryptedPrivateKey,
                PublicAddress = response.PublicAddress,
                WalletId = walletId,
            });

            return new WalletCreationResult()
            {
                PublicAddress = response.PublicAddress,
                WalletId = walletId
            };
        }

        public async Task<IEnumerable<WalletCreationResult>> GetAllWalletsAsync()
        {
            var allWallets = await _walletRepository.GetAllAsync();
            var results = allWallets.Select(x => new WalletCreationResult()
            {
                PublicAddress = x.PublicAddress,
                WalletId = x.WalletId
            });

            return results;
        }

        public async Task<WalletCreationResult> GetByPublicAddressAsync(string publicAddress)
        {
            IWallet wallet = await _walletRepository.GetWalletByPublicAddressAsync(publicAddress);

            if (wallet == null)
            {
                return null;
            }

            return new WalletCreationResult()
            {
                PublicAddress = wallet.PublicAddress,
                WalletId = wallet.WalletId
            };
        }

        public async Task<WalletCreationResult> GetByWalletIdAsync(Guid walletId)
        {
            IWallet wallet = await _walletRepository.GetWalletAsync(walletId);

            if (wallet == null)
            {
                return null;
            }

            return new WalletCreationResult()
            {
                PublicAddress = wallet.PublicAddress,
                WalletId = wallet.WalletId
            };
        }
    }
}
