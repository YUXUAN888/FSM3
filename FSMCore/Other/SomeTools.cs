using mcbbs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SquareMinecraftLauncher.Minecraft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace FSM3.FSMCore.Other
{
    public sealed class Download
    {
        internal HttpWebResponse CreateGetHttpResponse(string url)
        {
            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url);
            request1.Timeout = 6000;
            request1.ContentType = "text/html;chartset=UTF-8";
            request1.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64; rv: 48.0) Gecko / 20100101 Firefox / 48.0";
            request1.Method = "GET";
            return (HttpWebResponse)request1.GetResponse();
        }

        public string getHtml(string url)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
            Stream stream;
            WebClient client = new WebClient();
            try
            {
                stream = client.OpenRead(url);
                stream.ReadTimeout = 1000;
            }
            catch (WebException exception1)
            {
                string message = exception1.Message;
                return null;
            }
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            try
            {
                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    public class MCVersionList
    {
        /// <summary>
        /// 版本
        /// </summary>
        public string id { get; internal set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; internal set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string releaseTime { get; internal set; }
    }
    class SomeTools
    {
        public static string MCVersionAnalysis(string type)
        {
            string[,] textArray1 = { { "snapshot", "快照版" }, { "release", "正式版" }, { "old_beta", "基岩版" }, { "old_alpha", "远古版" } };
            string[,] strArray = textArray1;
            for (int i = 0; i < (strArray.Length / 2); i++)
            {
                if (strArray[i, 0] == type)
                {
                    return strArray[i, 1];
                }
            }
            return null;
        }
        public static Download web = new Download();
        /// <summary>
        /// 取MC列表
        /// 默认官方源
        /// 0为官方
        /// 1为BMCLAPI
        /// 2为MCBBS
        /// </summary>
        /// <returns></returns>
        public static async Task<MCVersionList[]> GetMCVersionList(int DownFSM = 0)
        {
            string text = null;
            switch (DownFSM)
            {
                case 0:
                    await Task.Factory.StartNew(() =>
                    {
                        text = web.getHtml("https://launchermeta.mojang.com/mc/game/version_manifest.json");
                    });
                    break;
                case 1:
                    await Task.Factory.StartNew(() =>
                    {
                        text = web.getHtml("https://bmclapi2.bangbang93.com/mc/game/version_manifest.json");
                    });
                    break;
                case 2:
                    await Task.Factory.StartNew(() =>
                    {
                        text = web.getHtml("https://download.mcbbs.net/mc/game/version_manifest.json");
                    });
                    break;
            }
            if (text == null)
            {
                throw new ("请求失败");
            }
            List<MCVersionList> list = new List<MCVersionList>();
            foreach (JToken token in (JArray)JsonConvert.DeserializeObject(new mcbbsnews().TakeTheMiddle(text, "\"versions\":", "]}") + "]"))
            {
                string str2 = MCVersionAnalysis(token["type"].ToString());
                MCVersionList item = new MCVersionList
                {
                    type = str2,
                    id = token["id"].ToString(),
                    releaseTime = token["releaseTime"].ToString()
                };
                list.Add(item);
            }
            return list.ToArray();
        }
    }
}
