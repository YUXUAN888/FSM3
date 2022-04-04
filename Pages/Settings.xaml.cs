using FSMLauncher_3;
using Gac;
using Microsoft.Win32;
using ModernWpf;
using ModernWpf.Controls;
using SquareMinecraftLauncher.Minecraft;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static String ZongX = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); //获取APPDATA
        public static string UpdateD;
        static String ZongW = ZongX + @"\.fsm";
        public static String ZongSkin = ZongX + @"\.fsm\Skin";
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
            if(IniReadValue("QDBQ","QDBQ") is "Yes")
            {
                BQQD.IsChecked = true;
            }
            STCP.IsChecked = false;
            if(About.YJS is true)
            {
                ColorCombobox.IsEnabled = true;
            }
            try { ColorCombobox.SelectedIndex = int.Parse(Game.IniReadValue("JSM", "Color")); } catch { }
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
                    Java_list.Items.Add(IniReadValue("Java", i.ToString()));
                }
                JavaS = int.Parse(IniReadValue("Java", "JavaS"));
            }
            catch
            {

            }
            List<JavaVersion> aa = tools.Tools.GetJavaPath();
            for (int i = 0; i < aa.Count; i++)
            {
                Java_list.Items.Add(aa[i].Path);
            }
            RAMS.AddHandler(Slider.MouseUpEvent, new MouseButtonEventHandler(MouseUP), true);
            RAMS.Maximum = MemoryAvailable / 1024 / 1024;
            RAMS.Value = GetRAM;
            if (IniReadValue("RAM", "RAMW") != "")
            {
                RAMS.Value = double.Parse(IniReadValue("RAM", "RAMW"));
            }
            if (IniReadValue("JVM", "JVMW") != "")
            {
                JVM.Text = IniReadValue("JVM", "JVMW");
            }
            if (IniReadValue("EY", "EYW") != "")
            {
                EY.Text = IniReadValue("EY", "EYW");
            }
            if(SFGX is true)
            {
                AZGX.Visibility = System.Windows.Visibility.Visible;
                UpdateLabel.Content = "检测到新版本! FSM" + xdbb;
                Update_Log.Content = "更新日志：" + GXNR;
            }
            else
            {
                AZGX.Visibility = System.Windows.Visibility.Hidden;
            }
            try
            {
                Java_list.SelectedIndex = int.Parse(Settings.IniReadValue("JavaList", "Path"));
            }
            catch
            {

            }
            if(Java_list.SelectedIndex is -1)
            {
                Java_list.SelectedIndex = 0;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark)
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                WritePrivateProfileString("Color","ZT","Light",FileS);
            }
            else
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                WritePrivateProfileString("Color", "ZT", "Dark", FileS);
            }
            
        }
        private void java_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WritePrivateProfileString("JavaList", "Path", Java_list.SelectedIndex.ToString(), FileS);
            Java_List = Java_list.Text;
        }
        public static string Java_List;
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
        int wa;
        public static Gac.DownLoadFile dlf = new DownLoadFile();
        public static int id = 0;
        internal int Download(string path, string ly, string url)
        {

            dlf.AddDown(url, path.Replace(System.IO.Path.GetFileName(path), ""), System.IO.Path.GetFileName(path), id);//增加下载
            dlf.StartDown(3);//开始下载
            id++;
            Core.xzItem xzItem = new Core.xzItem(System.IO.Path.GetFileName(path), 0, ly, "等待中", url, path);
            DIYvar.xzItems.Add(xzItem);


            return id - 1;
        }
        public void SendMsgHander(DownMsg msg)
        {

            Dispatcher.Invoke((Action)delegate ()
            {
                DownStatus tag = msg.Tag;

                if (tag == DownStatus.Start)
                {
                    DIYvar.xzItems[msg.Id].xzwz = "开始下载";

                    return;
                }
                if (tag == DownStatus.End)
                {
                    DIYvar.xzItems[msg.Id].xzwz = "完成";

                    DIYvar.xzItems[msg.Id].Template = 100;

                    return;
                }
                if (tag == DownStatus.Error)
                {
                    DIYvar.xzItems[msg.Id].xzwz = msg.ErrMessage;
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "安装遇到错误",
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = msg.ErrMessage
                        },

                    };
                    var result = dialog.ShowAsync();

                    return;
                }
                if (tag == DownStatus.DownLoad)
                {
                    DIYvar.xzItems[msg.Id].xzwz = "下载中";

                    DIYvar.xzItems[msg.Id].Template = msg.Progress;

                    Console.WriteLine("test");

                    return;
                }
            });
        }
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        static System.Windows.Threading.DispatcherTimer UPDATEW = new System.Windows.Threading.DispatcherTimer();
        private async void AZGX_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StackPanel panel = new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };
                panel.Children.Add(new TextBlock() { Text = "正在更新启动器..." });
                System.Windows.Controls.ProgressBar box = new System.Windows.Controls.ProgressBar();
                box.IsIndeterminate = true;
                panel.Children.Add(box);

                ContentDialog dialog = new ContentDialog()
                {
                    Title = "安装更新",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = panel,
                };
                var result = dialog.ShowAsync();
                String File_ = System.AppDomain.CurrentDomain.BaseDirectory + "[" + xdbb + "]FSM.exe";
                dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                wa = Download(File_, "", UpdateD);
                UPDATEW = Core5.timer(UPDATEWW, 2333);
                UPDATEW.Start();
                Online.CmdProcess1.Kill();
                Online.CmdProcess1.CancelOutputRead();
            }
            catch
            {

            }
        }
        private async void UPDATEWW(object ob, EventArgs a)
        {
            try
            {
                string aa = DIYvar.xzItems[wa].xzwz;
                //HttpDownloadFile(UpdateD, File_, 6, Tab1);
                String File_ = System.AppDomain.CurrentDomain.BaseDirectory + "[" + xdbb + "]FSM.exe";
                if (aa == "完成")
                {
                    UPDATEW.Stop();
                    System.Windows.Forms.NotifyIcon fyIcon = new System.Windows.Forms.NotifyIcon();
                    fyIcon.Icon = new System.Drawing.Icon(ZongW + @"\Creeper.ico");/*找一个ico图标将其拷贝到 debug 目录下*/
                    fyIcon.BalloonTipText = "开始安装更新";/*必填提示内容*/
                    fyIcon.BalloonTipTitle = "FSM Core";
                    //fyIcon.Icon = new Icon(@"D:\下载文件夹\PCL1-master\PCL1-master\Plain Craft Launcher\Images\icon.ico");/*找一个ico图标将其拷贝到 debug 目录下*/
                    fyIcon.Visible = true;/*必须设置显隐，因为默认值是 false 不显示通知*/
                    fyIcon.ShowBalloonTip(0);
                    (FindResource("hideMe") as System.Windows.Media.Animation.Storyboard).Begin(this);
                    await Task.Run(() =>
                    {
                        Thread.Sleep(1222);
                    });
                    OpenFile(File_);
                    System.Environment.Exit(0);
                }
            }
            catch
            {

            }
        }
        private void OpenFile(string NewFileName)
        {
            Process process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo(NewFileName);
            process.StartInfo = processStartInfo;
            #region 下面这段被注释掉代码（可以用来全屏打开代码）
            //建立新的系统进程
            //System.Diagnostics.Process process = new System.Diagnostics.Process();
            //设置文件名，此处为图片的真实路径 + 文件名（需要有后缀）    
            //process.StartInfo.FileName = NewFileName;
            //此为关键部分。设置进程运行参数，此时为最大化窗口显示图片。    
            //process.StartInfo.Arguments = "rundll32.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen";
            //此项为是否使用Shell执行程序，因系统默认为true，此项也可不设，但若设置必须为true
            //process.StartInfo.UseShellExecute = true;
            #endregion
            try
            {
                process.Start();
                try
                {
                    // process.WaitForExit();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (process != null)
                    {
                        process.Close();
                        process = null;
                    }
                }
                catch { }
            }


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
            WritePrivateProfileString("EY", "EYW", EY.Text, FileS);
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
                Java_list.Items.Add(fileDialog.FileName.ToString());
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
                Java_list.IsEnabled = false;
            }
            else
            {
                Java_list.IsEnabled = true;
            }
        }
        private void GSXT_Checked(object sender, RoutedEventArgs e)
        {
            
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

        private async void Button_Click_JD(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Get("https://www.mcmod.cn/modlist.html"));
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
                        Game.WritePrivateProfileString("JSM", "Color", "0", Game.FileS);
                    });
                    break;
                case 1:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(255, 67, 67);  //鲜艳红
                        Game.WritePrivateProfileString("JSM", "Color", "1", Game.FileS);
                    });
                    break;
                case 2:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(232, 17, 35);  //中国红
                        Game.WritePrivateProfileString("JSM", "Color", "2", Game.FileS);
                    });
                    break;
                case 3:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(234, 0, 94);  //小马宝莉
                        Game.WritePrivateProfileString("JSM", "Color", "3", Game.FileS);
                    });
                    break;
                case 4:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(142, 140, 216);  //淡紫色
                        Game.WritePrivateProfileString("JSM", "Color", "4", Game.FileS);
                    });
                    break;
                case 5:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(45, 125, 154);  //青色
                        Game.WritePrivateProfileString("JSM", "Color", "5", Game.FileS);
                    });
                    break;
                case 6:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(16, 124, 16);  //原谅绿
                        Game.WritePrivateProfileString("JSM", "Color", "6", Game.FileS);
                    });
                    break;
                case 7:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(46, 47, 42);  //高端灰
                        Game.WritePrivateProfileString("JSM", "Color", "7", Game.FileS);
                    });
                    break;
                case 8:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(0, 204, 106);  //青草绿
                        Game.WritePrivateProfileString("JSM", "Color", "8", Game.FileS);
                    });
                    break;
                case 9:
                    DisHelper.DisHelper.RunOnMainThread(() =>
                    {
                        ThemeManager.Current.AccentColor = Color.FromRgb(202, 80, 16);  //深度橘
                        Game.WritePrivateProfileString("JSM", "Color", "9", Game.FileS);
                    });
                    break;
            }
        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            WritePrivateProfileString("JVM", "JVMW", JVM.Text, FileS);
        }

        private void Load(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Check(object sender, RoutedEventArgs e)
        {
            if(BQQD.IsChecked is true)
            {
                WritePrivateProfileString("QDBQ", "QDBQ", "Yes", FileS);
            }
            else
            {
                WritePrivateProfileString("QDBQ", "QDBQ", "No", FileS);
            }
        }
    }
}
