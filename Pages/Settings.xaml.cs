using Microsoft.Win32;
using ModernWpf;
using ModernWpf.Controls;
using SquareMinecraftLauncher.Minecraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : System.Windows.Controls.Page
    {
        static String ZongX = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); //获取APPDATA
        public string UpdateD;
        static String ZongW = ZongX + @"\.fsm";
        String ZongSkin = ZongX + @"\.fsm\Skin";
        [DllImport("kernel32", CharSet = CharSet.Unicode)]

        public static extern long WritePrivateProfileString(string section, string key, string value, string filepath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]

        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public static string IniReadValue(string Section, string Key)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, File_);
            return temp.ToString();
        }
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public string IniReadValueW(string Section, string Key)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, ZongW + @"\ConsoleW.qwq");
            return temp.ToString();
        }
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public string IniReadValueS(string Section, string Key)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, ZongW + @"\Skin\SkinZ.Skin");
            return temp.ToString();
        }
        public string IniReadValueSS(string Section, string Key)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, ZongW + @"\Skin\SkinN.Skin");
            return temp.ToString();
        }
        public static int ssti;
        public static String Update;
        private int m_ProcessorCount = 0;
        public static long MemoryAvailable
        {
            get
            {
                long availablebytes = 0;
                //ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_PerfRawData_PerfOS_Memory"); 
                //foreach (ManagementObject mo in mos.Get()) 
                //{ 
                //    availablebytes = long.Parse(mo["Availablebytes"].ToString()); 
                //} 
                ManagementClass mos = new ManagementClass("Win32_OperatingSystem");
                foreach (ManagementObject mo in mos.GetInstances())
                {
                    if (mo["FreePhysicalMemory"] != null)
                    {
                        availablebytes = 1024 * long.Parse(mo["FreePhysicalMemory"].ToString());
                    }
                }
                return availablebytes;
            }
        }

        public static int GetRAM
        {
            get
            {
                int ram;
                int r1;
                r1 = (int)(MemoryAvailable / 1024 / 1024);

                if (r1 <= 1024)
                    ram = r1;
                else
                    ram = r1 - 1024;



                return ram;
            }


        }
        public Settings()
        {
            InitializeComponent();
            try
            {
                for (int i = 0; i < int.Parse(IniReadValue("Java", "JavaS")); ++i)
                {
                    java_list.Items.Add(IniReadValue("Java", i.ToString()));
                }
                JavaS = int.Parse(IniReadValue("Java", "JavaS"));
            }
            catch
            {

            }
            List<JavaVersion> aa = tools.Tools.GetJavaPath();
            for (int i = 0; i < aa.Count; i++)
            {
                java_list.Items.Add(aa[i].Version);
            }
            try
            {
                java_list.SelectedIndex = int.Parse(IniReadValue("Java", "List"));
            }
            catch
            {

            }
            RAMS.AddHandler(Slider.MouseUpEvent, new MouseButtonEventHandler(MouseUP), true);
            RAMS.Maximum = MemoryAvailable / 1024 / 1024;
            RAMS.Value = GetRAM;
            if (IniReadValue("RAM", "RAMW") != "")
            {
                RAMS.Value = double.Parse(IniReadValue("RAM", "RAMW"));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark)
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            }
            else
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            }
        }
        private void java_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WritePrivateProfileString("Java", "List", java_list.SelectedIndex.ToString(), FileS);
        }

        private void RAMS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RAMXS.Content = "当前配置内存:" + (int)RAMS.Value + "MB";
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {

        }

        private void Ch(object sender, RoutedEventArgs e)
        {

        }

        private void AZGX_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StackPanel panel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            panel.Children.Add(new TextBlock() { Text = "第一行" });
            TextBox box = new TextBox();
            box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "第二行");
            panel.Children.Add(box);

            ContentDialog dialog = new ContentDialog()
            {
                Title = "测试标题",
                PrimaryButtonText = "确定",
                CloseButtonText = "取消",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = panel,
            };
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                MessageBox.Show("点击了确定");
                MessageBox.Show(box.Text);
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "测试标题",
                PrimaryButtonText = "确定",
                CloseButtonText = "取消",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock()
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Text = "测试文本"
                },

            };
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                MessageBox.Show("点击了确定");
            }
            else if (result == ContentDialogResult.None)
            {
                MessageBox.Show("点击了取消");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.kancloud.cn/yu_xuan/fsm3/2628960");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        static int JavaS = 0;
        public static String FileS = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();//提示用户打开文件窗体
            fileDialog.Title = "选择Java路径";//文件对话框标题
            fileDialog.Filter = "Java路径|*javaw.exe";//文件格式筛选字符串
            if (fileDialog.ShowDialog() == true)//判断对话框返回值，点击打开
            {
                //fileDialog.FileName.ToString()
                //listBoxSS.DataContext = this;
                java_list.Items.Add(fileDialog.FileName.ToString());
                //MessageBox.Show(fileDialog.FileName.ToString());
                ++JavaS;
                WritePrivateProfileString("Java", "JavaS", JavaS.ToString(), FileS);
                WritePrivateProfileString("Java", JavaS.ToString(), fileDialog.FileName.ToString(), FileS);


            }
        }

        private void MouseUP(object sender, MouseButtonEventArgs e)
        {
            WritePrivateProfileString("RAM", "RAMW", RAMS.Value.ToString(), FileS);
            
        }

        private void AutoJava_c(object sender, RoutedEventArgs e)
        {
            if (AutoJava.IsChecked == true)
            {
                java_list.IsEnabled = false;
            }
            else
            {
                java_list.IsEnabled = true;
            }
        }

        private void GSXT_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void GSXT_Click(object sender, RoutedEventArgs e)
        {
            if(GSXT.IsChecked == true)
            {
                QHZTS.IsEnabled = false;
            }
            else
            {
                QHZTS.IsEnabled = true;
            }
        }

        private async void Button_Click_JD(object sender, RoutedEventArgs e)
        {
            StackPanel panel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            panel.Children.Add(new TextBlock() { Text = "\n第一行" });
            System.Windows.Controls.ProgressBar box = new System.Windows.Controls.ProgressBar();
            box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "第二行");
            panel.Children.Add(box);

            ContentDialog dialog = new ContentDialog()
            {
                Title = "测试标题",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = panel,
            };
            var result = await dialog.ShowAsync();
            
        }
    }
}
