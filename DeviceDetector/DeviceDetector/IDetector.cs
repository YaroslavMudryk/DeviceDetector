using DeviceDetector.Models;

namespace DeviceDetector
{
    public interface IDetector
    {
        Browser GetBrowser();
        Device GetDevice();
        OS GetOs();
        ClientInfo GetClientInfo();
    }
}