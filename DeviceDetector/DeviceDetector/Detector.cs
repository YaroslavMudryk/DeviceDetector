using DeviceDetector.Models;
using Microsoft.AspNetCore.Http;

namespace DeviceDetector
{
    public class Detector : IDetector
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _userAgent;
        public Detector(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
        }

        public Browser GetBrowser()
        {
            var dd = new DeviceDetectorNET.DeviceDetector(_userAgent);
            dd.SkipBotDetection();
            dd.DiscardBotInformation();
            dd.Parse();
            var browser = dd.GetBrowserClient();
            return browser.Success ? new Browser
            {
                Name = browser.Match.Name,
                Engine = browser.Match.Engine,
                Type = browser.Match.Type,
                Version = browser.Match.Version
            } : null;
        }

        public ClientInfo GetClientInfo()
        {
            return new ClientInfo
            {
                Browser = GetBrowser(),
                Device = GetDevice(),
                OS = GetOs()
            };
        }

        public Device GetDevice()
        {
            var dd = new DeviceDetectorNET.DeviceDetector(_userAgent);
            dd.SkipBotDetection();
            dd.DiscardBotInformation();
            dd.Parse();
            return new Device
            {
                Brand = GetStringOrNull(dd.GetBrandName()),
                Model = GetStringOrNull(dd.GetModel()),
                Type = GetStringOrNull(dd.GetDeviceName())
            };
        }

        public OS GetOs()
        {
            var dd = new DeviceDetectorNET.DeviceDetector(_userAgent);
            dd.SkipBotDetection();
            dd.DiscardBotInformation();
            dd.Parse();
            var os = dd.GetOs();
            return os.Success ? new OS
            {
                Name = os.Match.Name,
                Version = os.Match.Version,
                Platform = os.Match.Platform
            } : null;
        }

        private string GetStringOrNull(string q)
        {
            if (string.IsNullOrEmpty(q))
                return null;
            return q;
        }
    }

}