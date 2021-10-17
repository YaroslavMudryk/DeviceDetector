using Microsoft.Extensions.DependencyInjection;

namespace DeviceDetector.Extensions
{
    public static class DetectorServiceCollectionExtension
    {
        public static void AddDeviceDetector(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IDetector, Detector>();
        }
    }
}