using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM3.FSMCore.Modrinth
{
    class APICBD
    {
        public class License
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string slug { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string project_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string team { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string description { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string body { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string body_url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string published { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string updated { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string moderator_message { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public License license { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string client_side { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string server_side { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int downloads { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int followers { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> categories { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> versions { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string icon_url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string issues_url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string source_url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string wiki_url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string discord_url { get; set; }
        }

    }
}
