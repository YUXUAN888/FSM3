using FSM3.Pages;
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
        public HomePage()
        {
            InitializeComponent();
        }
        Random ra = new Random();
        private void Load(object sender, RoutedEventArgs e)
        {
            int YS = ra.Next(0, 100);
            if (Game.IniReadValue("YS", "YS") == YS.ToString())
            {
                int YSW = int.Parse(Game.IniReadValue("YS", "YS"));
                if (YSW > 95)
                {
                    RP.Content = YSW + "!" + "\n人品爆棚！快带上好基友出去玩";
                }
                else if (YSW > 60 && YSW < 95)
                {
                    RP.Content = YSW + "" + "\n今天一般般吧....";
                }
                else if (YSW > 30 && YSW < 60)
                {
                    RP.Content = YSW + "" + "\n倒霉透了！";
                }
                else if (YSW > 0 && YSW < 30)
                {
                    RP.Content = YSW + "" + "\n呜哇！";
                }
                else if (YSW is 0)
                {
                    RP.Content = YSW + "" + "\n你的运势！0！";
                }
            }
            else
            {
                Game.WritePrivateProfileString("YS", "YS", YS.ToString(), Game.FileS);
                if (YS > 95)
                {
                    RP.Content = YS + "!" + "\n人品爆棚！快带上好基友出去玩";
                }
                else if (YS > 60 && YS < 95)
                {
                    RP.Content = YS + "" + "\n今天一般般吧....";
                }
                else if (YS > 30 && YS < 60)
                {
                    RP.Content = YS + "" + "\n倒霉透了！";
                }
                else if (YS > 0 && YS < 30)
                {
                    RP.Content = YS + "" + "\n呜哇！";
                }
                else if (YS is 0)
                {
                    RP.Content = YS + "" + "\n你的运势！0！";
                }
            }
        }
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
    }
}
