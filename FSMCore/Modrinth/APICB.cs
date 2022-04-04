using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FSM3.Modrinth
{
    public class HitsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string mod_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string slug { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string author { get; set; }
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
        public List<string> categories { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> versions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int downloads { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int follows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string page_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icon_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string author_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string date_created { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string date_modified { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string latest_version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string license { get; set; }
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
        public string host { get; set; }
        ///<summary>
        ///
        ///</summary>
        public string project_id { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public List<HitsItem> hits { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int limit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total_hits { get; set; }
    }
}
