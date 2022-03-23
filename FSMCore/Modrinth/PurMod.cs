using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FSM3.Modrinth
{
    class PurMod
    {
        public static void GetPur()
        {
            WebClient wc = new WebClient();
            string str = wc.DownloadString("https://api.modrinth.com/api/v1/mod");
            Root rb = JsonConvert.DeserializeObject<Root>(str);
            for (int i = 0; i < rb.hits.Count; ++i)
            {
                Console.WriteLine(rb.hits[i].title + "  " + rb.hits[i].versions[0]);
            }
        }
    }
}
