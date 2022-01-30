using System.Windows;
using System.Windows.Controls;

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
