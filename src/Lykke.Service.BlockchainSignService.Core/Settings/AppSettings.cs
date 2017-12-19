using Lykke.Service.BlockchainSignService.Core.Settings.ServiceSettings;
using Lykke.Service.BlockchainSignService.Core.Settings.SlackNotifications;

namespace Lykke.Service.BlockchainSignService.Core.Settings
{
    public class AppSettings
    {
        public BlockchainSignServiceSettings BlockchainSignService { get; set; }
        public SlackNotificationsSettings SlackNotifications { get; set; }
    }
}
