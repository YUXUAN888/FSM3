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
using System.Windows.Threading;
using static FSM3.MainWindow;

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
            if (IniReadValue("ONLINE", "Server") != "" && IniReadValue("ONLINE", "Server") == "GZ")
            {
                GZJDXZ.IsChecked = true;
            }
            STCP.IsChecked = false;
            XTCP.IsChecked = false;
            if (IniReadValue("ONLINE", "TCPP2P") == "" || IniReadValue("ONLINE", "TCPP2P") == null)
            {
                STCP.IsChecked = true;
                WritePrivateProfileString("ONLINE", "TCPP2P", "stcp", FileS);
            }
            else
            {
                if (IniReadValue("ONLINE", "TCPP2P") == "stcp")
                {
                    STCP.IsChecked = true;
                }
                else if (IniReadValue("ONLINE", "TCPP2P") == "xtcp")
                {
                    XTCP.IsChecked = true;
                }
            }
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
            WritePrivateProfileString("ONLINE", "TCPP2P", "xtcp", FileS);
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("ONLINE", "TCPP2P", "stcp", FileS);
        }

        private void Ch(object sender, RoutedEventArgs e)
        {
            if (IniReadValue("ONLINE", "Server") == "GZ")
            {
                WritePrivateProfileString("ONLINE", "Server", "SH", FileS);
            }
            else
            {
                WritePrivateProfileString("ONLINE", "Server", "GZ", FileS);
            }
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

        private async void Button_Click_3(object sender, RoutedEventArgs e)
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
            DisHelper.DisHelper.RunOnMainThread(() =>
            {
                ThemeManager.Current.AccentColor = Color.FromRgb(255,140,0) ;  //橙色
            });
            DisHelper.DisHelper.RunOnMainThread(() =>
            {
                ThemeManager.Current.AccentColor = Color.FromRgb(255, 67, 67);  //鲜艳红
            });
            DisHelper.DisHelper.RunOnMainThread(() =>
            {
                ThemeManager.Current.AccentColor = Color.FromRgb(232, 17, 35);  //中国红
            });
            DisHelper.DisHelper.RunOnMainThread(() =>
            {
                ThemeManager.Current.AccentColor = Color.FromRgb(234, 0, 94);  //小马宝莉
            });
            DisHelper.DisHelper.RunOnMainThread(() =>
            {
                ThemeManager.Current.AccentColor = Color.FromRgb(142, 140, 216);  //淡紫色
            });
            DisHelper.DisHelper.RunOnMainThread(() =>
            {
                ThemeManager.Current.AccentColor = Color.FromRgb(45, 125, 154);  //青色
            });
            DisHelper.DisHelper.RunOnMainThread(() =>
            {
                ThemeManager.Current.AccentColor = Color.FromRgb(16, 124, 16);  //原谅绿
            });
            DisHelper.DisHelper.RunOnMainThread(() =>
            {
                ThemeManager.Current.AccentColor = Color.FromRgb(46, 47, 42);  //高端灰
            });
            DisHelper.DisHelper.RunOnMainThread(() =>
            {
                ThemeManager.Current.AccentColor = Color.FromRgb(0, 178, 148);  //实力青
            });
            DisHelper.DisHelper.RunOnMainThread(() =>
            {
                ThemeManager.Current.AccentColor = Color.FromRgb(0, 204, 106);  //青草绿
            });
            DisHelper.DisHelper.RunOnMainThread(() =>
            {
                ThemeManager.Current.AccentColor = Color.FromRgb(202, 80, 16);  //深度橘
            });
        }
        public class AccentColors : List<AccentColor>
        {
            public AccentColors()
            {
                Add("#FFB900", "Yellow gold");
                Add("#FF8C00", "Gold");
                Add("#F7630C", "Orange bright");
                Add("#CA5010", "Orange dark");
                Add("#DA3B01", "Rust");
                Add("#EF6950", "Pale rust");
                Add("#D13438", "Brick red");
                Add("#FF4343", "Mod red");
                Add("#E74856", "Pale red");
                Add("#E81123", "Red");
                Add("#EA005E", "Rose bright");
                Add("#C30052", "Rose");
                Add("#E3008C", "Plum light");
                Add("#BF0077", "Plum");
                Add("#C239B3", "Orchid light");
                Add("#9A0089", "Orchid");
                Add("#0078D7", "Default blue");
                Add("#0063B1", "Navy blue");
                Add("#8E8CD8", "Purple shadow");
                Add("#6B69D6", "Purple shadow Dark");
                Add("#8764B8", "Iris pastel");
                Add("#744DA9", "Iris spring");
                Add("#B146C2", "Violet red light");
                Add("#881798", "Violet red");
                Add("#0099BC", "Cool blue bright");
                Add("#2D7D9A", "Cool blue");
                Add("#00B7C3", "Seafoam");
                Add("#038387", "Seafoam team");
                Add("#00B294", "Mint light");
                Add("#018574", "Mint dark");
                Add("#00CC6A", "Turf green");
                Add("#10893E", "Sport green");
                Add("#7A7574", "Gray");
                Add("#5D5A58", "Gray brown");
                Add("#68768A", "Steel blue");
                Add("#515C6B", "Metal blue");
                Add("#567C73", "Pale moss");
                Add("#486860", "Moss");
                Add("#498205", "Meadow green");
                Add("#107C10", "Green");
                Add("#767676", "Overcast");
                Add("#4C4A48", "Storm");
                Add("#69797E", "Blue gray");
                Add("#4A5459", "Gray dark");
                Add("#647C64", "Liddy green");
                Add("#525E54", "Sage");
                Add("#847545", "Camouflage desert");
                Add("#7E735F", "Camouflage");
            }

            private void Add(string color, string name)
            {
                Add(new AccentColor((Color)ColorConverter.ConvertFromString(color), name));
            }
        }
        public class AccentColor
        {
            public AccentColor(Color color, string name)
            {
                Color = color;
                Name = name;
                Brush = new SolidColorBrush(color);
            }

            public Color Color { get; }
            public string Name { get; }
            public SolidColorBrush Brush { get; }

            public override string ToString()
            {
                return Name;
            }
        }
        private void GSXT_Click(object sender, RoutedEventArgs e)
        {
            if (GSXT.IsChecked == true)
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

        bool IsLeftMouseDown = false;
        byte cc = 83;
        byte cd = 21;
        byte ca = 24;
        byte cb = 66;
        private async void Caidan(object sender, MouseButtonEventArgs e)
        {
            IsLeftMouseDown = true;
            Random ra = new Random();

            Thread.Sleep(1500);

            if (IsLeftMouseDown)
            {
                while (true)
                {
                    await Task.Run(() => Thread.Sleep(123));
                    ca = (byte)(ca + ra.Next(8, 88));
                    cb = (byte)(cb + ra.Next(9, 99));
                    cc = (byte)(cc + ra.Next(11, 200));
                    cd = (byte)(cd + ra.Next(8, 66));
                    if (ca > (byte)255)
                    {
                        ca = 110;
                    }
                    ZBt.zbt.Foreground = new SolidColorBrush(Color.FromArgb(ca, cb, cc, cd));
                }
            }
            else
            {

            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ColorCombobox.SelectedIndex)
            {
                case 0:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(255, 140, 0);  //橙色
                    });
                    break;
                case 1:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(255, 67, 67);  //鲜艳红
                    });
                    break;
                case 2:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(232, 17, 35);  //中国红
                    });
                    break;
                case 3:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(234, 0, 94);  //小马宝莉
                    });
                    break;
                case 4:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(142, 140, 216);  //淡紫色
                    });
                    break;
                case 5:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(45, 125, 154);  //青色
                    });
                    break;
                case 6:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(16, 124, 16);  //原谅绿
                    });
                    break;
                case 7:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(46, 47, 42);  //高端灰
                    });
                    break;
                case 8:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(0, 204, 106);  //青草绿
                    });
                    break;
                case 9:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(202, 80, 16);  //深度橘
                    });
                    break;
            }
        }
    }
}
