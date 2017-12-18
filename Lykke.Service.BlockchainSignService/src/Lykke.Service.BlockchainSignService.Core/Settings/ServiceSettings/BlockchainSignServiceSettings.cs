using System.Security.Cryptography;
using System.Text;

namespace Lykke.Service.BlockchainSignService.Core.Settings.ServiceSettings
{
    public class BlockchainSignServiceSettings
    {
        private byte[] _passwordBytes;

        public DbSettings Db { get; set; }
        public string SignServiceUrl { get; set; }
        public string Password { get; set; }

        public byte[] PasswordBytes
        {
            get
            {
                if (_passwordBytes != null)
                    return _passwordBytes;

                if (Password == null)
                    return null;

                var sha = SHA256.Create();

                return _passwordBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(Password));
            }
        }
    }
}
