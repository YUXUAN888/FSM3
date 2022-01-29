using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
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
using System.Xml.Serialization;
using MojangAPI.Cache;
using MojangAPI.Model;
using MojangAPI.SecurityQuestion;
using KMCCC.Pro.Modules.MojangAPI;

namespace FSM3.Home_API
{
    /// <summary>
    /// WeatherAPI.xaml 的交互逻辑
    /// </summary>
    public partial class MojangAPI : UserControl
    {
        public MojangAPI()
        {
            InitializeComponent();
        }
        private void W(object sender, RoutedEventArgs e)
        {
            var a = KMCCC.Pro.Modules.MojangAPI.MojangAPI.GetStatistics();
            xx.Content = "24小时内销量:" + a.getLast24h() + "\n" + "总销量:" + a.getTotal();
        }
    }
}
