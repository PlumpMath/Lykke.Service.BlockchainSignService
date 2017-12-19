using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.Service.BlockchainSignService.Core.Services
{
    public interface IEncryptionService
    {
        string EncryptAesString(string data, byte[] key);

        string DecryptAesString(string data, byte[] key);
    }
}
