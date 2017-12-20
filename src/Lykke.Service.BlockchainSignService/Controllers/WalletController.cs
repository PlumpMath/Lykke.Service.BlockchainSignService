using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Lykke.Service.BlockchainSignService.Cache;
using Lykke.Service.BlockchainSignService.Core.Domain;
using Lykke.Service.BlockchainSignService.Core.Services;
using Lykke.Service.BlockchainSignService.Models;
using Lykke.Service.BlockchainSignService.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lykke.Service.BlockchainSignService.Controllers
{
    [Route("api/wallets")]
    public class WalletController : Controller
    {
        private readonly IWalletService _walletGeneratorService;
        private readonly ICache<WalletCreationResult> _cache;

        public WalletController(IWalletService walletGeneratorService, ICache<WalletCreationResult> cache)
        {
            _walletGeneratorService = walletGeneratorService;
            _cache = cache;
        }

        [HttpGet]
        [SwaggerOperation("GetAllWallets")]
        [ProducesResponseType(typeof(WalletsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllWalletsAsync()
        {
            IEnumerable<WalletCreationResult> wallets = await _cache.GetListAsync("allWallets", async () =>
            {
                return await _walletGeneratorService.GetAllWalletsAsync();
            });

            return Ok(new WalletsResponse()
            {
                Wallets = wallets.Select(walletCreationResult => new WalletResponse()
                {
                    PublicAddress = walletCreationResult.PublicAddress,
                    WalletId = walletCreationResult.WalletId
                })
            });
        }

        [HttpGet("by-id/{walletId}")]
        [SwaggerOperation("GetByWalletId")]
        [ProducesResponseType(typeof(WalletResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByWalletIdAsync(Guid walletId)
        {
            WalletCreationResult walletCreationResult = await _walletGeneratorService.GetByWalletIdAsync(walletId);

            if (walletCreationResult == null)
            {
                return NotFound();
            }

            return Ok(new WalletResponse()
            {
                PublicAddress = walletCreationResult.PublicAddress,
                WalletId = walletCreationResult.WalletId
            });
        }

        [HttpGet("by-public-address/{publicAddress}")]
        [SwaggerOperation("GetByPublicAddress")]
        [ProducesResponseType(typeof(WalletResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByPublicAddressAsync(string publicAddress)
        {
            WalletCreationResult walletCreationResult = await _walletGeneratorService.GetByPublicAddressAsync(publicAddress);

            if (walletCreationResult == null)
            {
                return NotFound();
            }

            return Ok(new WalletResponse()
            {
                PublicAddress = walletCreationResult.PublicAddress,
                WalletId = walletCreationResult.WalletId
            });
        }

        [HttpPost]
        [SwaggerOperation("CreateWallet")]
        [ProducesResponseType(typeof(WalletResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateWalletAsync()
        {
            WalletCreationResult walletCreationResult = await _walletGeneratorService.CreateWalletAsync();
            await _cache.InvalidateAsync();

            return Ok(new WalletResponse()
            {
                PublicAddress = walletCreationResult.PublicAddress,
                WalletId = walletCreationResult.WalletId
            });
        }
    }
}
