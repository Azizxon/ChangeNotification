using ChangeNotification.Service.Authentication;
using ChangeNotification.Service.Authentication.Settings;
using ChangeNotification.Service.Certificate;
using ChangeNotification.Service.Queue;
using Microsoft.Graph;

namespace ChangeNotification.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddHttpClient(nameof(AuthenticationClient));
            services.AddScoped<IAuthenticationClient, AuthenticationClient>();
            services.AddScoped<AuthenticationProvider>();

            services.AddScoped(sp =>
            {
                var delegatedAuthentication = sp.GetRequiredService<IAuthenticationClient>();

                var httpClient = GraphClientFactory.Create(new DelegatingHandler[]
                {
                    new AuthenticationProvider(delegatedAuthentication)
                });
                return new GraphServiceClient(httpClient);
            });

            services.AddSingleton<SubscriptionStore>();
            services.AddSingleton<CertificateService>();
            services.AddScoped<ServiceBusSender>();

            return services;
        }

        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AdSettings>(configuration.GetSection("AdSettings"));
            services.Configure<UserCredentials>(configuration.GetSection("UserCredentials"));
            services.AddSingleton<TokenSettings>();
            services.Configure<ServiceBusSettings>(configuration.GetSection("ServiceBus"));
            
            return services;
        }
    }
}
