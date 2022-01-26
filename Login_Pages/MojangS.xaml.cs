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

namespace FSM3.Login_Pages
{
    class NNNW : Page
    {
        public static Label NNNw { get; set; }
    }
    class IMMW : Page
    {
        public static Image IMMw { get; set; }
    }
    /// <summary>
    /// MojangS.xaml 的交互逻辑
    /// </summary>
    public partial class MojangS : Page
    {
        public MojangS()
        {
            InitializeComponent();
            NNNW.NNNw = NNN;
            IMMW.IMMw = IMM;
        }
    }
}
