using System;
using System.Collections.Generic;
using System.Linq;
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
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Page
    {
        public About()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://afdian.net/@YUXUAN233");
        }

        private void Button_Click_36(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://blog.qwq.one");
        }

        private void Tile_Click_16(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/YUXUAN888/FSMLauncher3");
        }

        private void Button_Click_37(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://blog.qwq.one");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://afdian.net/@BaiBaoStudio");
        }
    }
}
