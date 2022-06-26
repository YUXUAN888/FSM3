using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM3.FSMCore.Other.YoudaoT
{
    class APIT
    {
        public class WebItem
        {
            /// <summary>
            /// 
            /// </summary>
            public List<string> value { get; set; }
            /// <summary>
            /// 钠
            /// </summary>
            public string key { get; set; }
        }

        public class Dict
        {
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
        }

        public class Webdict
        {
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
        }

        public class Basic
        {
            /// <summary>
            /// 
            /// </summary>
            public string phonetic { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> explains { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public List<string> returnPhrase { get; set; }
            /// <summary>
            /// 钠
            /// </summary>
            public string query { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string errorCode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string l { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tSpeakUrl { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<WebItem> web { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string requestId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> translation { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Dict dict { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Webdict webdict { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Basic basic { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string isWord { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string speakUrl { get; set; }
        }
    }
}
