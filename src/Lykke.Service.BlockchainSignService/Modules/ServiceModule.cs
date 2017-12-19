using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Log;
using Lykke.Service.BlockchainSignService.AzureRepositories;
using Lykke.Service.BlockchainSignService.Core.Repositories;
using Lykke.Service.BlockchainSignService.Core.Services;
using Lykke.Service.BlockchainSignService.Core.Settings.ServiceSettings;
using Lykke.Service.BlockchainSignService.Services;
using Lykke.SettingsReader;
using Microsoft.Extensions.DependencyInjection;

namespace Lykke.Service.BlockchainSignService.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<BlockchainSignServiceSettings> _settings;
        private readonly ILog _log;
        // NOTE: you can remove it if you don't need to use IServiceCollection extensions to register service specific dependencies
        private readonly IServiceCollection _services;

        public ServiceModule(IReloadingManager<BlockchainSignServiceSettings> settings, ILog log)
        {
            _settings = settings;
            _log = log;

            _services = new ServiceCollection();
        }

        protected override void Load(ContainerBuilder builder)
        {
            // TODO: Do not register entire settings in container, pass necessary settings to services which requires them
            // ex:
            //  builder.RegisterType<QuotesPublisher>()
            //      .As<IQuotesPublisher>()
            //      .WithParameter(TypedParameter.From(_settings.CurrentValue.QuotesPublication))

            builder.RegisterInstance(_log)
                .As<ILog>()
                .SingleInstance();

            builder.RegisterType<HealthService>()
                .As<IHealthService>()
                .SingleInstance();

            builder.RegisterType<StartupManager>()
                .As<IStartupManager>();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>();

            // TODO: Add your dependencies here
            #region Repos

            builder.RegisterType<WalletRepository>().
                As<IWalletRepository>().SingleInstance();

            #endregion Repos

            #region Services

            builder.RegisterType<WalletGeneratorService>().
                As<IWalletGeneratorService>()
                .WithParameter(TypedParameter.From(_settings.CurrentValue.PasswordBytes))
                .SingleInstance();

            builder.RegisterType<SignService>().
                As<ISignService>().SingleInstance();

            builder.RegisterType<EncryptionService>().
                As<IEncryptionService>().SingleInstance();

            builder.RegisterType<InternalSignServiceCaller>()
                .WithParameter(TypedParameter.From(_settings.CurrentValue.SignServiceUrl))
                .As<InternalSignServiceCaller>().SingleInstance();

            #endregion Services

            builder.Populate(_services);
        }
    }
}
