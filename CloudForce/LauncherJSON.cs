using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CloudForce
{
    internal class LauncherJSON
    {
        public static string getLauncherjson()
        {
            WebClient web = new WebClient();
            return web.DownloadString("https://raw.githubusercontent.com/zortos293/Cloudforce-Revamped-Resources/Dev/launchers.json");
        }

        public static Root LauncherJson = JsonConvert.DeserializeObject<LauncherJSON.Root>(getLauncherjson());

        public class App
        {
            public string AppName { get; set; }
            public string AppImage { get; set; }
            public string AppDownloadUrl { get; set; }
            public string AppExe { get; set; }
            public string AppArguments { get; set; }
        }

        public class Root
        {
            public List<App> Apps { get; set; }
        }
    }
}
