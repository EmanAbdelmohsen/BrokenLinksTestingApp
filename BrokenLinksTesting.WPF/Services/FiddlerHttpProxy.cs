using BrokenLinksTesting.WPF.Common;
using Fiddler;
using System.Collections.Generic;

namespace BrokenLinksTesting.WPF
{
    /// <summary>
    /// HTTP proxy to capture HTTP requests 
    /// </summary>
    public static class FiddlerHttpProxy
    {
        public static List<Link> HttpLinks = new List<Link>();

        /// <summary>
        /// Initializes FiddlerApplication
        /// </summary>
        public static void Run()
        {
            System.Diagnostics.Debug.WriteLine("Starting FiddlerCore...");
            CONFIG.IgnoreServerCertErrors = false;
            try
            {
                //do not register as system proxy to avoid interfering with the machine proxy
                FiddlerApplication.Startup(8888, false, true);

                //assign fiddler to the current application only
                //SetProxyInProcess only impacts the WinINET proxy settings for the current process
                URLMonInterop.SetProxyInProcess("127.0.0.1:8888", null);
            }
            catch { }

            //assign AfterSessionComplete event handler to capture requests
            //AfterSessionComplete event is chosen to get all necessary data from both the request and its response
            FiddlerApplication.AfterSessionComplete += FiddlerAfterSessionCompleteEventHandler;
        }

        static void FiddlerAfterSessionCompleteEventHandler(Session s)
        {
            // Ignore HTTPS connect requests
            if (s.RequestMethod == "CONNECT")
                return;

            // Capture url and request info
            HttpLinks.Add(new Link
            {
                Id = RandomNumberGenerator.GenerateRandomNumber(),
                URL = s.fullUrl,
                RequestDate = s.Timers.ClientBeginRequest.ToShortDateString() + " " + s.Timers.ClientBeginRequest.ToShortTimeString(),
                RequestMethod = s.RequestMethod,
                ResponseCode = s.responseCode.ToString(),
            });
        }
    }
}
