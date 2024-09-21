using Imba.Utils;
using System.Runtime.InteropServices;

namespace CiFarm.Scripts.Services.BrowserService
{
    public class BrowserService : ManualSingletonMono<BrowserService>
    {
        [DllImport("__Internal")]
        private static extern void Quit();

        public override void Awake()
        {
            base.Awake();
        }

        public void HandleQuit()
        {
            #if UNITY_WEBGL
            Quit();
            #endif
        }
    }
}