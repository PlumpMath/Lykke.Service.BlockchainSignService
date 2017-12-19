using System;
using Autofac;
using Common.Log;

namespace Lykke.Service.BlockchainSignService.Client
{
    public static class AutofacExtension
    {
        public static void RegisterBlockchainSignServiceClient(this ContainerBuilder builder, string serviceUrl, ILog log)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (serviceUrl == null) throw new ArgumentNullException(nameof(serviceUrl));
            if (log == null) throw new ArgumentNullException(nameof(log));
            if (string.IsNullOrWhiteSpace(serviceUrl))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(serviceUrl));

            builder.RegisterType<BlockchainSignServiceClient>()
                .WithParameter("serviceUrl", serviceUrl)
                .As<IBlockchainSignServiceClient>()
                .SingleInstance();
        }

        public static void RegisterBlockchainSignServiceClient(this ContainerBuilder builder, BlockchainSignServiceServiceClientSettings settings, ILog log)
        {
            builder.RegisterBlockchainSignServiceClient(settings?.ServiceUrl, log);
        }
    }
}
