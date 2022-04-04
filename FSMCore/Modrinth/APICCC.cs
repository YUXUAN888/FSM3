using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM3.FSMCore.Modrinth
{
    class APICCC
    {
        public class Hashes
        {
            /// <summary>
            /// 
            /// </summary>
            public string sha512 { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sha1 { get; set; }
        }

        public class FilesItem
        {
            /// <summary>
            /// 
            /// </summary>
            public Hashes hashes { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string filename { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string primary { get; set; }
        }

        public class LItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string project_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string author_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string featured { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string version_number { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string changelog { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string changelog_url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string date_published { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int downloads { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string version_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<FilesItem> files { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> dependencies { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> game_versions { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> loaders { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public List<LItem> l { get; set; }
        }

    }
}
