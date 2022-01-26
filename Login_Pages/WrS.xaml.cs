using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace FSM3.Login_Pages
{
    class NNNWR : Page
    {
        public static Label NNNwr { get; set; }
    }
    class IMMWR : Page
    {
        public static Image IMMwr { get; set; }
    }
    
    /// <summary>
    /// WrS.xaml 的交互逻辑
    /// </summary>
    public partial class WrS : Page
    {
        public static string NameSJ;
        public static BitmapImage TxSJ;
        public WrS()
        {
            InitializeComponent();
            NNNWR.NNNwr = NNN;
            IMMWR.IMMwr = IMM;
            if(NameSJ != null)
            {
                NNN.Content = NameSJ;
                IMM.Source = TxSJ;
            }
        }
    }
}
