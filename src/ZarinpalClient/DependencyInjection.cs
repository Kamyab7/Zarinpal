using Microsoft.Extensions.DependencyInjection;

namespace ZarinpalClient;

    public static class DependencyInjection
    {
        public static IServiceCollection AddZarinpalClient(this IServiceCollection services, ZarinpalClientSettings zarinpalClientSettings)
        {
            if (zarinpalClientSettings.MerchantId is null)
            {
                throw new ArgumentNullException($"{nameof(zarinpalClientSettings.MerchantId)} cannot be null");
            }

            if (zarinpalClientSettings.PaymentCallBackUrl is null)
            {
                throw new ArgumentNullException($"{nameof(zarinpalClientSettings.PaymentCallBackUrl)} cannot be null");
            }
            
            services.AddSingleton<ZarinpalClientService>();
            
            services.AddSingleton(zarinpalClientSettings);
            
            return services;
        }
    }

