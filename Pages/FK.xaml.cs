using FSM3.About_List;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    /// FK.xaml 的交互逻辑
    /// </summary>
    public partial class FK : Page
    {
        public FK()
        {
            InitializeComponent();
            WebClient wc = new WebClient();
            string str = wc.DownloadString("https://txc.qq.com/api/v1/361169/posts");
            Root rb = JsonConvert.DeserializeObject<Root>(str);
            List<FKList> item1 = new List<FKList>();
            for (int i = 0; i < rb.data.Count; ++i)
            {
                if (rb.data[i].is_admin is "true")
                {

                }
                else
                {
                    FKList item = new FKList();
                    item.FBR.Content = rb.data[i].nick_name + "的反馈";
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(rb.data[i].avatar_url, UriKind.RelativeOrAbsolute);
                    item.NR.Content = "反馈内容:" + rb.data[i].content;
                    if (rb.data[i].is_todo is "false")
                    {
                        item.SFJJ.Content = "已解决";
                    }
                    else
                    {
                        item.SFJJ.Content = "未解决";
                    }
                    bi.EndInit();
                    item.TX.Source = bi;
                    item1.Add(item);
                }
            }
            FKSJ.ItemsSource = item1.ToArray();
        }

        private void FKSJ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://support.qq.com/products/361169");
        }
    }
}
