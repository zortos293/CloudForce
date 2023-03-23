using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CloudForce
{
    internal class AppsJSON
    {
        public static string getAPPjson()
        {
            WebClient web = new WebClient();
            return web.DownloadString("https://raw.githubusercontent.com/zortos293/Cloudforce-Revamped-Resources/Dev/Apps.json");
        }
        public interface JsonContainer
        {
            List<App> Apps { get; set; }
        }

        public static Root APPJson = JsonConvert.DeserializeObject<AppsJSON.Root>(getAPPjson());

        public class App
        {
            public string AppName { get; set; }
            public string AppImage { get; set; }
            public string AppDescription { get; set; }
            public string AppGFNIssues { get; set; }
            public string AppGFNStatus { get; set; }
            public string AppDownloadUrl { get; set; }
            public string AppExe { get; set; }
            public string AppArguments { get; set; }
            public string AppUpdateLog { get; set; }
        }

        public class Root : JsonContainer
        {
            public List<App> Apps { get; set; }
        }
    }
}
