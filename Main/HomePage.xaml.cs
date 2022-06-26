using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FSM3.Main
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        public static string retString;
        public static string Get(string Url)
        {
            //System.GC.Collect();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();

                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            catch
            {

            }

            return retString;
        }
        public HomePage()
        {
            InitializeComponent();
        }
        public class Root
        {
            public List<string> FSM { get; set; }
        }
        private Random ra = new Random();
        private WebClient wc = new WebClient();
        private void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                HomePage.Root root = JsonConvert.DeserializeObject<HomePage.Root>(Get("http://124.221.215.96:666/FSM.json"));
                int index = this.ra.Next(0, root.FSM.Count);
                this.RP.Content = (object)root.FSM[index];
            }
            catch
            {
            }
        }
    }
}
