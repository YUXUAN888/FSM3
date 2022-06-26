using FSM3.About_List;
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

namespace FSM3.Pages
{
    /// <summary>
    /// Channel.xaml 的交互逻辑
    /// </summary>
    public partial class Channel : Page
    {
        public static String retString;
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
        public Channel()
        {
            InitializeComponent();
        }
        public class Root
        {
            public List<string> Title { get; set; }
        }
        public class RootB
        {
            public List<string> Master { get; set; }
        }
        public class RootC
        {
            public List<string> Bio { get; set; }
        }
        public class RootD
        {
            public List<string> Photo { get; set; }
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            List<CList> item1 = new List<CList>();
            try
            {
                Root root = JsonConvert.DeserializeObject<Root>(Get("http://124.221.215.96:666/channel/Title.json"));
                RootB root1 = JsonConvert.DeserializeObject<RootB>(Get("http://124.221.215.96:666/channel/Master.json"));
                RootC root2 = JsonConvert.DeserializeObject<RootC>(Get("http://124.221.215.96:666/channel/Bio.json"));
                RootD root3 = JsonConvert.DeserializeObject<RootD>(Get("http://124.221.215.96:666/channel/MasterPhoto.json"));
                for (int i = 0; i < root.Title.Count; ++i)
                {
                    CList item = new CList();
                    item.Title.Content = root.Title[i];
                    item.Master.Content = root1.Master[i];
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(root3.Photo[i], UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    item.MasterPhoto.Source = bi;
                    item.LB.Content = root2.Bio[i];
                    Zlist.Items.Add(item);
                }
            }
            catch
            {
            }
        }
    }
}
