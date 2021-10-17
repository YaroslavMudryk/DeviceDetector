using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceDetector.Extensions
{
    public static class DetectorServiceCollectionExtension
    {
        public static void AddDeviceDetector(this IServiceCollection services)
        {
            services.AddSingleton(ServiceDescriptor.Singleton<IHttpContextAccessor, HttpContextAccessor>());
            services.AddTransient<IDetector, Detector>();
        }
    }
}