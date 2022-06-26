using ModernWpf;
using ModernWpf.Controls;
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
using ModernWpf.Media.Animation;
using static FSM3.MainWindow;
using System.IO;
using FSMLauncher_3;
using Gac;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using SquareMinecraftLauncher.Core.OAuth;
using System.Windows.Media.Animation;
using System.Threading;
using SquareMinecraftLauncher.Minecraft;
using System.Management;

namespace FSM3.Pages
{
    /// <summary>
    /// Game.xaml 的交互逻辑
    /// </summary>
    public partial class Game : System.Windows.Controls.Page
    {
        public class framecontrolW
        {
            public static ModernWpf.Controls.Frame frame { get; set; }

        }
        public class NowVw
        {
            public static Label NowV { get; set; }

        }
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
        public static string IniReadValueW(string Section, string Key)
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
        public static string IniReadValueS(string Section, string Key)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, ZongW + @"\Skin\SkinZ.Skin");
            return temp.ToString();
        }
        public static string Dminecraft = System.AppDomain.CurrentDomain.BaseDirectory + @".minecraft";
        public static string IniReadValueSS(string Section, string Key)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, ZongW + @"\Skin\SkinN.Skin");
            return temp.ToString();
        }
        ComboBox java_list = new ComboBox();
        int RAMW;
        string[] after;
        public Game()
        {
            InitializeComponent();
            NowVw.NowV = NowV;
            try
            {
                string aaw = About.DecryptDES(IniReadValue("JSM", "JSM"), "87654321");
                // MessageBox.Show(DecryptDES("ODliMzdkNWNmOTBhOTViNQ==", "8765432w"));
                string code = null;
                SelectQuery query = new SelectQuery("select * from Win32_ComputerSystemProduct");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (var item in searcher.Get())
                    {
                        using (item) code = item["UUID"].ToString();
                    }

                }
                if (aaw == code)
                {
                    About.YJS = true;
                    switch (int.Parse(IniReadValue("JSM", "Color")))
                    {
                        case 0:
                            DisHelper.DisHelper.RunOnMainThread(() =>
                            {
                                ThemeManager.Current.AccentColor = System.Windows.Media.Color.FromRgb(255, 140, 0);  //橙色
                                WritePrivateProfileString("JSM", "Color", "0", FileS);
                            });
                            break;
                        case 1:
                            DisHelper.DisHelper.RunOnMainThread(() =>
                            {
                                ThemeManager.Current.AccentColor = System.Windows.Media.Color.FromRgb(255, 67, 67);  //鲜艳红
                                WritePrivateProfileString("JSM", "Color", "1", FileS);
                            });
                            break;
                        case 2:
                            DisHelper.DisHelper.RunOnMainThread(() =>
                            {
                                ThemeManager.Current.AccentColor = System.Windows.Media.Color.FromRgb(232, 17, 35);  //中国红
                                WritePrivateProfileString("JSM", "Color", "2", FileS);
                            });
                            break;
                        case 3:
                            DisHelper.DisHelper.RunOnMainThread(() =>
                            {
                                ThemeManager.Current.AccentColor = System.Windows.Media.Color.FromRgb(234, 0, 94);  //小马宝莉
                                WritePrivateProfileString("JSM", "Color", "3", FileS);
                            });
                            break;
                        case 4:
                            DisHelper.DisHelper.RunOnMainThread(() =>
                            {
                                ThemeManager.Current.AccentColor = System.Windows.Media.Color.FromRgb(142, 140, 216);  //淡紫色
                                WritePrivateProfileString("JSM", "Color", "4", FileS);
                            });
                            break;
                        case 5:
                            DisHelper.DisHelper.RunOnMainThread(() =>
                            {
                                ThemeManager.Current.AccentColor = System.Windows.Media.Color.FromRgb(45, 125, 154);  //青色
                                WritePrivateProfileString("JSM", "Color", "5", FileS);
                            });
                            break;
                        case 6:
                            DisHelper.DisHelper.RunOnMainThread(() =>
                            {
                                ThemeManager.Current.AccentColor = System.Windows.Media.Color.FromRgb(16, 124, 16);  //原谅绿
                                WritePrivateProfileString("JSM", "Color", "6", FileS);
                            });
                            break;
                        case 7:
                            DisHelper.DisHelper.RunOnMainThread(() =>
                            {
                                ThemeManager.Current.AccentColor = System.Windows.Media.Color.FromRgb(46, 47, 42);  //高端灰
                                WritePrivateProfileString("JSM", "Color", "7", FileS);
                            });
                            break;
                        case 8:
                            DisHelper.DisHelper.RunOnMainThread(() =>
                            {
                                ThemeManager.Current.AccentColor = System.Windows.Media.Color.FromRgb(0, 204, 106);  //青草绿
                                WritePrivateProfileString("JSM", "Color", "8", FileS);
                            });
                            break;
                        case 9:
                            DisHelper.DisHelper.RunOnMainThread(() =>
                            {
                                ThemeManager.Current.AccentColor = System.Windows.Media.Color.FromRgb(202, 80, 16);  //深度橘
                                WritePrivateProfileString("JSM", "Color", "9", FileS);
                            });
                            break;
                    }
                }
            }
            catch
            {

            }
            if (Game.IniReadValueW("OffLine", "Skin") == "" || Game.IniReadValueW("OffLine", "Skin") == null)
            {

            }
            else
            {
                try
                {
                    after = Game.IniReadValueW("OffLine", "Skin").Split(new char[] { '|' });
                    Game.SkinUUIDMC = after[1];
                    BitmapImage bi = new BitmapImage();
                    //BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(Game.ZongSkin + @"\" + after[0] + @"\SkinT.png", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    OFFLINEI.Source = bi;
                }
                catch
                {

                }
            }
            try { if(IniReadValue("XX","XX") != "") { NowV.Content = IniReadValue("XX", "XX"); } } catch { }
            NZDM[0] = "FSM的第一个内部版本发布于2021年7月3日!";
            NZDM[1] = "FSM3的联机由Baibao提供";
            NZDM[2] = "FSM的联机与nat无关!";
            NZDM[3] = "把鼠标停留在一些按钮上，你可以看到它的提示";
            NZDM[4] = "作者在启动器里藏有3+个彩蛋！";
            NZDM[5] = "FSM的部分配置文件保存于系统AppData";
            NZDM[6] = "FSM最早于2020年2月发布！";
            try
            {
                RAMW = Settings.GetRAM;
                if (IniReadValue("RAM", "RAMW") != "")
                {
                    RAMW = int.Parse(IniReadValue("RAM", "RAMW"));
                }
            }
            catch
            {

            }
            if (VerList.NowVS != null)
            {
                NowV.Content = VerList.NowVS;
            }
            try {
                if(Yyes!="888")
                {

                }
                else
                {
                    for (int i = 0; i < skin.NameItem.Length; i++)
                    {
                        Ylist.Items.Add(skin.NameItem[i]);
                    }
                    Ylist.SelectedIndex = int.Parse(IniReadValue("Y", "Ylist"));
                    JS = skin.NameItem[Ylist.SelectedIndex].Name;
                    Yuuid = skin.NameItem[Ylist.SelectedIndex].uuid;
                    Yname = skin.NameItem[Ylist.SelectedIndex].Name;
                    Ytoken = skin.accessToken;
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                for (int i = 0; i < int.Parse(IniReadValue("Java", "JavaS")); ++i)
                {
                    java_list.Items.Add(IniReadValue("Java", i.ToString()));
                }
            }
            catch
            {

            }
            List<JavaVersion> aa = tools.Tools.GetJavaPath();
            for (int i = 0; i < aa.Count; i++)
            {
                java_list.Items.Add(aa[i].Path);
            }
            try
            {
                java_list.SelectedIndex = int.Parse(IniReadValue("JavaList", "Path"));
            }
            catch
            {

            }
            if (IniReadValue("Login", "LoginM") != "")
            {
                IDTab.SelectedIndex = int.Parse(IniReadValue("Login", "LoginM"));
            }
            if (IniReadValueW("OffLine", "ID") == null || IniReadValueW("OffLine", "ID") == "")
            {

            }
            else
            {
                OfflineName.Text = IniReadValueW("OffLine", "ID");
            }
            try
            {
                switch (IniReadValue("Login", "LoginM"))
                {
                    case "1":
                        if (wryes == "888")
                        {
                            loginmode = "wr";
                            WritePrivateProfileString("Login", "LoginM", "1", FileS);
                            WrD.SelectedIndex = 1;
                            NNW.Content = wrname;
                            BitmapImage bi = new BitmapImage();
                            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                            bi.BeginInit();
                            bi.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png", UriKind.RelativeOrAbsolute);
                            bi.EndInit();
                            IMW.Source = bi;
                        }
                        else
                        {
                            WrD.SelectedIndex = 0;
                            loginmode = "";
                        }
                        break;
                    case "3":
                        if (Yyes == "888")
                        {
                            loginmode = "y";
                            WzD.SelectedIndex = 1;
                            WritePrivateProfileString("Login", "LoginM", "3", FileS);
                            Yyes = "888";
                        }
                        else
                        {
                            WzD.SelectedIndex = 0;
                            loginmode = "";
                        }
                        break;
                }
                if (IniReadValue("Vlist", "Path") != "" && IniReadValue("Vlist", "Path") != null && IniReadValue("Vlist", "V") != "-1")
                {
                    AllTheExistingVersion[] t = new AllTheExistingVersion[0];
                    int a = int.Parse(IniReadValue("Vlist", "Path"));
                    String b = (a + 1).ToString();
                    if(b is "1")
                    {
                        tools.Tools.SetMinecraftFilesPath(Dminecraft);

                        t = tools.Tools.GetAllTheExistingVersion();

                        if(IniReadValue("XX","XX") is null || IniReadValue("XX", "XX") is "")
                        {
                            NowV.Content = t[0].version;
                        }
                        else
                        {
                            NowV.Content = IniReadValue("XX", "XX");
                        }
                    }
                    else
                    {
                        tools.Tools.SetMinecraftFilesPath(IniReadValue("VPath", b));

                        t = tools.Tools.GetAllTheExistingVersion();


                        if (IniReadValue("XX", "XX") is null || IniReadValue("XX", "XX") is "")
                        {
                            NowV.Content = t[0].version;
                        }
                        else
                        {
                            NowV.Content = IniReadValue("XX", "XX");
                        }
                    }
                }
            }
            catch
            {

            }
            
        }
        public static int dw;
        public static string Mojangname;
        public static string MojangUUID;
        public static string MojangToken;
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        public static System.Windows.Threading.DispatcherTimer Jarw = new System.Windows.Threading.DispatcherTimer();
        public static System.Windows.Threading.DispatcherTimer ONLINEW = new System.Windows.Threading.DispatcherTimer();
        static System.Windows.Threading.DispatcherTimer UPDATEW = new System.Windows.Threading.DispatcherTimer();
        public static int did1, did2, did3, did4;
        public static int id = 0;
        static String ZongX = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); //获取APPDATA
        public string UpdateD;
        public static String ZongW = ZongX + @"\.fsm";
        public static String ZongSkin = ZongX + @"\.fsm\Skin";
        public Gac.DownLoadFile dlf = new DownLoadFile();
        internal int Download(string path, string ly, string url)
        {

            dlf.AddDown(url, path.Replace(System.IO.Path.GetFileName(path), ""), System.IO.Path.GetFileName(path), id);//增加下载
            dlf.StartDown(3);//开始下载
            id++;
            Core.xzItem xzItem = new Core.xzItem(System.IO.Path.GetFileName(path), 0, ly, "等待中", url, path);
            DIYvar.xzItems.Add(xzItem);


            return id - 1;
        }
        [DllImport("kernel32", CharSet = CharSet.Unicode)]

        public static extern long WritePrivateProfileString(string section, string key, string value, string filepath);
        

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var login = tools.Tools.MinecraftLogin(Mojang1.Text, Mojang2.Password);
                Mojangname = login.name;
                MojangUUID = login.uuid;
                MojangToken = login.token;
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin");
                //Download(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png", "", tools.GetMinecraftSkin(MojangUUID));
                dw = Download(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png", "", tools.Tools.GetMinecraftSkin(MojangUUID));
                ONLINEW = Core5.timer(OnQ, 2333);
                ONLINEW.Start();
                System.Drawing.Point point = new System.Drawing.Point(8, 8);
                System.Drawing.Size size = new System.Drawing.Size(8, 8);
                loginmode = "mojang";
                NNN.Content = Mojangname;
                String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
                StringBuilder temp = new StringBuilder(500);
                mojangyes = "888";
                WritePrivateProfileString("Mojang", "Mail", Mojang1.Text, ZongW + @"\ConsoleW.qwq");
                WritePrivateProfileString("Mojang", "PassWord", Mojang2.Password, ZongW + @"\ConsoleW.qwq");
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "登录失败",
                    PrimaryButtonText = "好吧",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = ex.Message
                    },

                };
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                }
            }
        }
        private void OnQ(object ob, EventArgs a)
        {

            if (DIYvar.xzItems[dw].xzwz == "完成")
            {
                Bitmap bitmap = new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
                i.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                System.Drawing.Image img = i;
                BitmapImage bi = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                IMM.Source = BitmapToBitmapImage(i);
                MojangD.SelectedIndex = 1;
            }


        }
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
        private Bitmap crop(Bitmap src, System.Drawing.Point point, System.Drawing.Size size)
        {
            System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(point, size);
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new System.Drawing.Rectangle(0, 0, target.Width, target.Height),
                      cropRect,
                      GraphicsUnit.Pixel);
            }
            return target;
        }
        public enum ZoomType { NearestNeighborInterpolation, BilinearInterpolation }
        /// <summary>
        /// 图像缩放
        /// </summary>
        /// <param name="srcBmp">原始图像</param>
        /// <param name="width">目标图像宽度</param>
        /// <param name="height">目标图像高度</param>
        /// <param name="dstBmp">目标图像</param>
        /// <param name="GetNearOrBil">缩放选用的算法</param>
        /// <returns>处理成功 true 失败 false</returns>
        public static bool Zoom(Bitmap srcBmp, double ratioW, double ratioH, out Bitmap dstBmp, ZoomType zoomType)
        {//ZoomType为自定义的枚举类型
            if (srcBmp == null)
            {
                dstBmp = null;
                return false;
            }
            //若缩放大小与原图一样，则返回原图不做处理
            if ((ratioW == 1.0) && ratioH == 1.0)
            {
                dstBmp = new Bitmap(srcBmp);
                return true;
            }
            //计算缩放高宽
            double height = ratioH * (double)srcBmp.Height;
            double width = ratioW * (double)srcBmp.Width;
            dstBmp = new Bitmap((int)width, (int)height);

            BitmapData srcBmpData = srcBmp.LockBits(new System.Drawing.Rectangle(0, 0, srcBmp.Width, srcBmp.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData dstBmpData = dstBmp.LockBits(new System.Drawing.Rectangle(0, 0, dstBmp.Width, dstBmp.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* srcPtr = null;
                byte* dstPtr = null;
                int srcI = 0;
                int srcJ = 0;
                double srcdI = 0;
                double srcdJ = 0;
                double a = 0;
                double b = 0;
                double F1 = 0;//横向插值所得数值
                double F2 = 0;//纵向插值所得数值
                if (zoomType == ZoomType.NearestNeighborInterpolation)
                {//邻近插值法

                    for (int i = 0; i < dstBmp.Height; i++)
                    {
                        srcI = (int)(i / ratioH);//srcI是此时的i对应的原图像的高
                        srcPtr = (byte*)srcBmpData.Scan0 + srcI * srcBmpData.Stride;
                        dstPtr = (byte*)dstBmpData.Scan0 + i * dstBmpData.Stride;
                        for (int j = 0; j < dstBmp.Width; j++)
                        {
                            dstPtr[j * 3] = srcPtr[(int)(j / ratioW) * 3];//j / ratioW求出此时j对应的原图像的宽
                            dstPtr[j * 3 + 1] = srcPtr[(int)(j / ratioW) * 3 + 1];
                            dstPtr[j * 3 + 2] = srcPtr[(int)(j / ratioW) * 3 + 2];
                        }
                    }
                }
                else if (zoomType == ZoomType.BilinearInterpolation)
                {//双线性插值法
                    byte* srcPtrNext = null;
                    for (int i = 0; i < dstBmp.Height; i++)
                    {
                        srcdI = i / ratioH;
                        srcI = (int)srcdI;//当前行对应原始图像的行数
                        srcPtr = (byte*)srcBmpData.Scan0 + srcI * srcBmpData.Stride;//指原始图像的当前行
                        srcPtrNext = (byte*)srcBmpData.Scan0 + (srcI + 1) * srcBmpData.Stride;//指向原始图像的下一行
                        dstPtr = (byte*)dstBmpData.Scan0 + i * dstBmpData.Stride;//指向当前图像的当前行
                        for (int j = 0; j < dstBmp.Width; j++)
                        {
                            srcdJ = j / ratioW;
                            srcJ = (int)srcdJ;//指向原始图像的列
                            if (srcdJ < 1 || srcdJ > srcBmp.Width - 1 || srcdI < 1 || srcdI > srcBmp.Height - 1)
                            {//避免溢出（也可使用循环延拓）
                                dstPtr[j * 3] = 255;
                                dstPtr[j * 3 + 1] = 255;
                                dstPtr[j * 3 + 2] = 255;
                                continue;
                            }
                            a = srcdI - srcI;//计算插入的像素与原始像素距离（决定相邻像素的灰度所占的比例）
                            b = srcdJ - srcJ;
                            for (int k = 0; k < 3; k++)
                            {//插值    公式：f(i+p,j+q)=(1-p)(1-q)f(i,j)+(1-p)qf(i,j+1)+p(1-q)f(i+1,j)+pqf(i+1, j + 1)
                                F1 = (1 - b) * srcPtr[srcJ * 3 + k] + b * srcPtr[(srcJ + 1) * 3 + k];
                                F2 = (1 - b) * srcPtrNext[srcJ * 3 + k] + b * srcPtrNext[(srcJ + 1) * 3 + k];
                                dstPtr[j * 3 + k] = (byte)((1 - a) * F1 + a * F2);
                            }
                        }
                    }
                }
            }
            srcBmp.UnlockBits(srcBmpData);
            dstBmp.UnlockBits(dstBmpData);
            return true;
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

        private async void Button_Click_WR(object sender, RoutedEventArgs e)
        {
            //免密登录
            try
            {
                MicrosoftLogin microsoftLogin = new MicrosoftLogin();
                MinecraftLogin minecraftlogin = new MinecraftLogin();
                (FindResource("hideMe") as System.Windows.Media.Animation.Storyboard).Begin(XZWRZH);
                (FindResource("showMe") as System.Windows.Media.Animation.Storyboard).Begin(TX);
                try
                {

                    Xbox XboxLogin = new Xbox();

                    var token = microsoftLogin.GetToken(await microsoftLogin.Login(true));
                    wrtoken = new MinecraftLogin().GetToken(XboxLogin.XSTSLogin(XboxLogin.GetToken(token.access_token)));
                    string refresh_token = token.refresh_token;
                    ///WR.SetValue("Atoken", refresh_token);
                    WritePrivateProfileString("wr", "Atoken", refresh_token, ZongW + @"\ConsoleW.qwq");

                    var Minecraft = minecraftlogin.GetMincraftuuid(wrtoken);
                    dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                    wruuid = Minecraft.uuid;
                    wrname = Minecraft.name;
                    WrD.SelectedIndex = 1;
                    NNW.Content = wrname;
                    loginmode = "wr";
                    String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";

                    wryes = "888";

                    dw = Download(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png", "", tools.Tools.GetMinecraftSkin(wruuid));
                    ONLINEW = Core5.timer(OnQw, 2333);
                    ONLINEW.Start();

                    wryes = "888";

                }
                catch
                {

                };




            }
            catch (Exception ex)
            {

            }
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
       
        private void OnQw(object ob, EventArgs a)
        {
            try
            {
                if (DIYvar.xzItems[dw].xzwz == "完成")
                {
                    System.Drawing.Point point = new System.Drawing.Point(8, 8);
                    System.Drawing.Size size = new System.Drawing.Size(8, 8);
                    Bitmap bitmap = new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                    var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                    Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
                    i.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                    System.Drawing.Image img = i;
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    IMW.Source = BitmapToBitmapImage(i);
                    ///WritePrivateProfileString("wr", "Atoken", refresh_token, File_);
                    (FindResource("hideMe") as System.Windows.Media.Animation.Storyboard).Begin(TX);
                    (FindResource("showMe") as System.Windows.Media.Animation.Storyboard).Begin(XZWRZH);
                }
            }
            catch
            {

            }
        }

        public static String FileS = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
        public static System.Windows.Media.Animation.DoubleAnimation OSMessage(int From, double To, double Time)
        {
            var widthAnimation = new DoubleAnimation()
            {
                From = From,
                To = To,
                Duration = TimeSpan.FromSeconds(Time),
                EasingFunction = new QuinticEase()
                {
                    //Dispatcher = 0.43,
                    EasingMode = EasingMode.EaseInOut,
                },
            };
            return widthAnimation;
        }
        private void MojangUP(object sender, MouseButtonEventArgs e)
        {
            if (mojangyes == "888")
            {
                MojangD.SelectedIndex = 1;
                NNN.Content = Mojangname;
                loginmode = "mojang";
                WritePrivateProfileString("Login", "LoginM", "0", FileS);
            }
            else
            {
                MojangD.SelectedIndex = 0;
                loginmode = "";
            }
        }

        private void WrUP(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (wryes == "888")
                {
                    loginmode = "wr";
                    WritePrivateProfileString("Login", "LoginM", "1", FileS);
                    WrD.SelectedIndex = 1;
                    NNW.Content = wrname;
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    IMW.Source = bi;
                }
                else
                {
                    WrD.SelectedIndex = 0;
                    loginmode = "";
                }
            }
            catch
            {

            }
        }

        private void LXUP(object sender, MouseButtonEventArgs e)
        {
            WritePrivateProfileString("Login", "LoginM", "2", FileS);
            loginmode = "offline";
        }

        private void WZUP(object sender, MouseButtonEventArgs e)
        {
            if (Yyes == "888")
            {
                WzD.SelectedIndex = 1;
                JS = Yname;
                loginmode = "y";
                WritePrivateProfileString("Login", "LoginM", "3", FileS);
            }
            else
            {
                WzD.SelectedIndex = 0;
                loginmode = "";
            }
        }
        public static string offlinename;
        private void OfflineName_TextChanged(object sender, TextChangedEventArgs e)
        {
            offlinename = OfflineName.Text;
            loginmode = "offline";
            WritePrivateProfileString("OffLine", "ID", OfflineName.Text, ZongW + @"\ConsoleW.qwq");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            loginmode = "";
            WritePrivateProfileString("wr", "Atoken", "", ZongW + @"\ConsoleW.qwq");
            ///WR.DeleteValue("Atoken");
            WrD.SelectedIndex = 0;
            wryes = "";
            wrname = "";
            wruuid = "";
            wrtoken = "";
        }
        public static Skin skin = new Skin();
        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {

                skin = tools.Tools.GetAuthlib_Injector(IP.Text, IDD.Text, IDDPassWord.Password);
                Ylist.Items.Clear();
                for(int i = 0; i < skin.NameItem.Length; ++i)
                {
                    Ylist.Items.Add(skin.NameItem[i]);
                }
                WzD.SelectedIndex = 1;
                WritePrivateProfileString("wz", "IP", IP.Text, ZongW + @"\ConsoleW.qwq");
                WritePrivateProfileString("wz", "IDD", IDD.Text, ZongW + @"\ConsoleW.qwq");
                WritePrivateProfileString("wz", "IDDPassWord", IDDPassWord.Password, ZongW + @"\ConsoleW.qwq");
                Yyes = "888";
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "登录失败",
                    PrimaryButtonText = "好吧",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = ex.Message
                    },

                };
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                }
            }
        }
        public static string Ytoken;
        public static string Yname;
        public static string Yuuid;
        public static string Yyes;

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            WritePrivateProfileString("Y", "Ylist", "", FileS);
            WritePrivateProfileString("wz", "IDD", "", ZongW + @"\ConsoleW.qwq");
            WzD.SelectedIndex = 0;
            Yyes = "";
        }
        public static string JS;
        private void Ylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                JS = skin.NameItem[Ylist.SelectedIndex].Name;
                WritePrivateProfileString("Y", "Ylist", Ylist.SelectedIndex.ToString(), FileS);
                Yyes = "888";
            }

            catch
            {

            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            _transitionInfo = new DrillInNavigationTransitionInfo();
            FSM3.framecontrol.frame.Navigate(dyuri("/Pages/Skinforonline.xaml"), null, _transitionInfo);
            YS = YS + 1;
        }
        Storyboard storyboard = new Storyboard();//实例化故事板
        Storyboard storyboard1 = new Storyboard();//实例化故事板
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            _transitionInfo = new DrillInNavigationTransitionInfo();
            FSM3.framecontrol.frame.Navigate(dyuri("/Pages/Skinforoffline.xaml"), null, _transitionInfo);
        }

        private void Left(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if(NowV.Content is "未选择版本" || NowV.Content is "当前版本")
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "未选择版本",
                    PrimaryButtonText = "好吧",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = "选择一个版本再点击吧!",
                    },

                };
                var result = dialog.ShowAsync();
            }
            else
            {
                _transitionInfo = new DrillInNavigationTransitionInfo();
                FSM3.framecontrol.frame.Navigate(dyuri("/Pages/VerSettings.xaml"), null, _transitionInfo);
            }
        }
        public static string[] NZDM = new string[8];
        public static String Update;
        int aa;
        int kk;
        private async void BQQ(object ob, EventArgs a)
        {
            string cc = DIYvar.xzItems[kk].xzwz;
            SquareMinecraftLauncher.Minecraft.Game game = new SquareMinecraftLauncher.Minecraft.Game();//声明对象
            game.LogEvent += new SquareMinecraftLauncher.Minecraft.Game.LogDel(log);
            game.ErrorEvent += new SquareMinecraftLauncher.Minecraft.Game.ErrorDel(error);
            if (cc == "完成")
            {
                ONLINEW.Stop();
                Thread StartGameW = new Thread(StartGameTW);
                StartGameW.Start();
            }
        }
            private async void BQ(object ob, EventArgs a)
        {

            string cc = DIYvar.xzItems[aa].xzwz;
            if (cc == "完成")
            {
                ONLINEW.Stop();
                Thread StartGameW = new Thread(StartGameTW);
                StartGameW.Start();
            }

            //JarTimerBool = false;


        }
        public async void libraries(string version)
        {
            try
            {
                //  tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);
                dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                //libraries1 = mcVersionLists[MCV.SelectedIndex].version;
                MCDownload[] File = tools.Tools.GetMissingFile(version);
                if (File.Length != 0)
                {
                    Game.ONLINEW = Core5.timer(BQ, 2333);
                    Game.ONLINEW.Start();
                    for (int i = 0; i < File.Length; i++)
                    {
                        aa = Download(File[i].path, "补全", File[i].Url);
                    }
                    //libraries2 = sz.id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public double inta;
        public double intb;
        private const int INTERNET_CONNECTION_MODEM = 1;
        private const int INTERNET_CONNECTION_LAN = 2;
        [DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);
        private void StartGameTW()
        {
            Application.Current.Dispatcher.Invoke(
        async delegate
        {
            //Code
            Random ra = new Random();
            tools.Tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);
            SquareMinecraftLauncher.Minecraft.Game game = new SquareMinecraftLauncher.Minecraft.Game();//声明对象
            game.LogEvent += new SquareMinecraftLauncher.Minecraft.Game.LogDel(log);
            game.ErrorEvent += new SquareMinecraftLauncher.Minecraft.Game.ErrorDel(error);
            StartGamew.SM.NZDM.Content = NZDM[ra.Next(0, 7)];
                switch (loginmode)
                {
                    case "mojang":
                            try
                            {
                                //var loading = await this.ShowProgressAsync("启动游戏", "正在启动游戏,请稍后\n"+"你知道吗？"+NZDM[ra.Next(0, 6)]);
                                game.Launcher = "FSM3";
                                SquareMinecraftLauncher.Minecraft.Game.Online = false;
                                string java = "";
                                if (java_list.SelectedIndex == -1) java = java_list.Items[0].ToString();
                                else java = java_list.SelectedItem.ToString();
                                if (IniReadValue("AutoJava", "AutoJava") is "Yes")
                                {
                                    java = FSMCore.JavaFix.AutoJava.GetAutoJava(IniReadValue("XX","IDVar"));
                                }
                                await game.StartGame(NowV.Content.ToString(), java, RAMW, Mojangname, MojangUUID, MojangToken, IniReadValue("JVM", "JVMW") + " -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True -Dlog4j2.formatMsgNoLookups=true", IniReadValue("EY", "EYW"));
                                START.Visibility = Visibility.Hidden;
                                game.Launcher = "FSM3";
                            }
                            catch (Exception ex)
                            {
                                START.Visibility = Visibility.Hidden;
                                ContentDialog dialog = new ContentDialog()
                                {
                                    Title = "启动失败(一般性)",
                                    PrimaryButtonText = "好吧",
                                    IsPrimaryButtonEnabled = true,
                                    DefaultButton = ContentDialogButton.Primary,
                                    Content = new TextBlock()
                                    {
                                        TextWrapping = TextWrapping.WrapWithOverflow,
                                        Text = "Java路径:" + java_list.Text + "\n分配内存:" + RAMW + "\nJVM:" + IniReadValue("JVM", "JVMW") + " -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True -Dlog4j2.formatMsgNoLookups=true" + "\n额外参数:" + IniReadValue("EY", "EYW") + "\n启动器版本:" + Update + "\n异常抛出信息:" + ex.Message,
                                    },

                                };
                                var result = await dialog.ShowAsync();
                            }
                        
                        break;
                    case "wr":
                        try
                        {
                                SquareMinecraftLauncher.Minecraft.Game.Online = false;
                                //var loading = await this.ShowProgressAsync("启动游戏", "正在启动游戏,请稍后\n" + "你知道吗？" + NZDM[ra.Next(0, 6)]);
                                game.Launcher = "FSM3";
                                string java = "";
                                if (java_list.SelectedIndex == -1) java = java_list.Items[0].ToString();
                                else java = java_list.SelectedItem.ToString();
                        if (IniReadValue("AutoJava", "AutoJava") is "Yes")
                        {
                            java = FSMCore.JavaFix.AutoJava.GetAutoJava(IniReadValue("XX", "IDVar"));
                        }
                        await game.StartGame(NowV.Content.ToString(), java, RAMW, wrname, wruuid, wrtoken, IniReadValue("JVM", "JVMW") + " -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True -Dlog4j2.formatMsgNoLookups=true", IniReadValue("EY", "EYW"));
                                game.Launcher = "FSM3";
                                START.Visibility = Visibility.Hidden;
                            
                        }
                        catch (Exception ex)
                        {
                            START.Visibility = Visibility.Hidden;
                        ContentDialog dialog = new ContentDialog()
                        {
                            Title = "启动失败(一般性)",
                            PrimaryButtonText = "好吧",
                            IsPrimaryButtonEnabled = true,
                            DefaultButton = ContentDialogButton.Primary,
                            Content = new TextBlock()
                            {
                                TextWrapping = TextWrapping.WrapWithOverflow,
                                Text = "Java路径:" + java_list.Text + "\n分配内存:" + RAMW + "\nJVM:" + IniReadValue("JVM", "JVMW") + " -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True -Dlog4j2.formatMsgNoLookups=true" + "\n额外参数:" + IniReadValue("EY", "EYW") + "\n启动器版本:" + Update + "\n异常抛出信息:" + ex.Message,
                            },

                        };
                        var result = await dialog.ShowAsync();
                    }
                        break;
                    case "offline":
                        try
                        {

                                SquareMinecraftLauncher.Minecraft.Game.Online = false;
                                //var loading = await this.ShowProgressAsync("启动游戏", "正在启动游戏,请稍后\n" + "你知道吗？" + NZDM[ra.Next(0, 6)]);
                                if (SkinUUIDMC == "" || SkinUUIDMC == null)
                                {
                                    game.Launcher = "FSM3";
                                    string java = "";
                                    if (java_list.SelectedIndex == -1) java = java_list.Items[0].ToString();
                                    else java = java_list.SelectedItem.ToString();
                            if (IniReadValue("AutoJava", "AutoJava") is "Yes")
                            {
                                java = FSMCore.JavaFix.AutoJava.GetAutoJava(IniReadValue("XX", "IDVar"));
                            }
                            await game.StartGame(NowV.Content.ToString(), java, RAMW, OfflineName.Text, "SkinUUID", "YUXUANSHILI", IniReadValue("JVM", "JVMW") + " -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True -Dlog4j2.formatMsgNoLookups=true", IniReadValue("EY", "EYW"));
                                    game.Launcher = "FSM3";
                                }
                                else
                                {
                                    game.Launcher = "FSM3";
                                    string java = "";
                                    if (java_list.SelectedIndex == -1) java = java_list.Items[0].ToString();
                                    else java = java_list.SelectedItem.ToString();
                            if (IniReadValue("AutoJava", "AutoJava") is "Yes")
                            {
                                java = FSMCore.JavaFix.AutoJava.GetAutoJava(IniReadValue("XX", "IDVar"));
                            }
                            await game.StartGame(NowV.Content.ToString(), java, RAMW, OfflineName.Text, SkinUUIDMC, "YUXUANSHILI", IniReadValue("JVM", "JVMW") + " -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True -Dlog4j2.formatMsgNoLookups=true", IniReadValue("EY", "EYW"));
                                    game.Launcher = "FSM3";
                                }
                                //game.ErrorEvent += new Game.ErrorDel(error);//错误事件
                                START.Visibility = Visibility.Hidden;
                            }
                            catch (Exception ex)
                            {
                                START.Visibility = Visibility.Hidden;
                                ContentDialog dialog = new ContentDialog()
                                {
                                    Title = "启动失败(一般性)",
                                    PrimaryButtonText = "好吧",
                                    IsPrimaryButtonEnabled = true,
                                    DefaultButton = ContentDialogButton.Primary,
                                    Content = new TextBlock()
                                    {
                                        TextWrapping = TextWrapping.WrapWithOverflow,
                                        Text = "Java路径:" + java_list.Text + "\n分配内存:" + RAMW + "\nJVM:" + IniReadValue("JVM", "JVMW") + " -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True -Dlog4j2.formatMsgNoLookups=true" + "\n额外参数:" + IniReadValue("EY", "EYW") + "\n启动器版本:" + Update + "\n异常抛出信息:" + ex.Message,
                                    },

                                };
                                var result = await dialog.ShowAsync();
                            }
                        break;
                    case "y":
                        try
                        {
                                //var loading = await this.ShowProgressAsync("启动游戏", "正在启动游戏,请稍后\n" + "你知道吗？" + NZDM[ra.Next(0, 6)]);
                                //MessageBox.Show(Y.GetValue("IP").ToString());
                                SquareMinecraftLauncher.Minecraft.Game.Online = false;
                                game.Launcher = "FSM3";
                                string java = "";
                                if (java_list.SelectedIndex == -1) java = java_list.Items[0].ToString();
                                else java = java_list.SelectedItem.ToString();
                        if (IniReadValue("AutoJava", "AutoJava") is "Yes")
                        {
                            java = FSMCore.JavaFix.AutoJava.GetAutoJava(IniReadValue("XX", "IDVar"));
                        }
                        await game.StartGame(NowV.Content.ToString(), java, RAMW, Yname, Yuuid, Ytoken, IniReadValueW("wz", "IP"), IniReadValue("JVM", "JVMW") + " -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True -Dlog4j2.formatMsgNoLookups=true", IniReadValue("EY", "EYW"), AuthenticationServerMode.yggdrasil);
                                game.Launcher = "FSM3";
                                START.Visibility = Visibility.Hidden;
                            
                        }
                        catch (Exception ex)
                        {
                            START.Visibility = Visibility.Hidden;
                        ContentDialog dialog = new ContentDialog()
                        {
                            Title = "启动失败(一般性)",
                            PrimaryButtonText = "好吧",
                            IsPrimaryButtonEnabled = true,
                            DefaultButton = ContentDialogButton.Primary,
                            Content = new TextBlock()
                            {
                                TextWrapping = TextWrapping.WrapWithOverflow,
                                Text = "Java路径:" + java_list.Text + "\n分配内存:" + RAMW + "\nJVM:" + IniReadValue("JVM", "JVMW") + " -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True -Dlog4j2.formatMsgNoLookups=true" + "\n额外参数:" + IniReadValue("EY", "EYW") + "\n启动器版本:" + Update + "\n异常抛出信息:" + ex.Message,
                            },

                        };
                        var result = await dialog.ShowAsync();
                    }
                        break;
                    default:
                        START.Visibility = Visibility.Hidden;
                    break;
                }
                //game.ErrorEvent += new Game.ErrorDel(error);//错误事件
        });
        }
        DownloadFile dlfs = new DownloadFile();
        internal void Downloadw(string path, string url)
        {
            dlfs.Start(path, url);//增加下载
        }
        private async void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            //(FindResource("showMe") as System.Windows.Media.Animation.Storyboard).Begin(StartGamew.SM);
            DoubleAnimation yd5 = new DoubleAnimation(-100, 0, new Duration(TimeSpan.FromSeconds(0.55)));//浮点动画定义了开始值和起始值
            START.RenderTransform = new TranslateTransform();//在二维x-y坐标系统内平移(移动)对象
            yd5.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            // yd5.RepeatBehavior = RepeatBehavior.Forever;//设置循环播放
            Storyboard.SetTarget(yd5, START);//绑定动画为这个按钮执行的浮点动画
            Storyboard.SetTargetProperty(yd5, new PropertyPath("RenderTransform.Y"));//依赖的属性
            storyboard.Children.Add(yd5);//向故事板中加入此浮点动画
            storyboard.Begin();//播放此动画
            START.Visibility = Visibility.Visible;
            bool results = false;
            bool resultsx = false;
            await Task.Run(() => { Thread.Sleep(328); });
            try
            {
                if (IniReadValue("QDBQ", "QDBQ") is "Yes")
                {
                    Thread StartGameW = new Thread(StartGameTW);
                    StartGameW.Start();
                }
                else
                {
                    tools.Tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);
                    if (tools.Tools.GetMissingFile(NowV.Content.ToString()).Length != 0)
                    {
                        //Lib补全
                        MCDownload[] File = tools.Tools.GetMissingFile(NowV.Content.ToString());
                        foreach (var i in File)
                        {
                            Downloadw(i.path, i.Url);
                        }
                        await Task.Run(() =>
                        {
                            while (true)
                            {
                                this.Dispatcher.Invoke(new Action(delegate
                                {
                                    StartGamew.SM.NZDM.Content = "正在补全中：" + File.Length.ToString() + "|" + (File.Length - dlfs.Left).ToString();
                                }));
                                if (dlfs.EndDownload())
                                {
                                    results = true;
                                    break;
                                }
                            }
                        });
                    }
                    else
                    {
                        results = true;
                    }
                    if (results)
                    {
                        if (tools.Tools.GetMissingAsset(NowV.Content.ToString()).Length != 0)
                        {
                            //assets补全
                            AssetDownload assetDownload = new AssetDownload();//asset下载类
                            assetDownload.DownloadProgressChanged += AssetDownload_DownloadProgressChanged;//事件                                    AssetDownload assetDownload = new AssetDownload();//asset下载类
                            await assetDownload.BuildAssetDownload(1000, NowV.Content.ToString());//构建下载
                        }
                        else { resultsx = true; }
                    }
                    if (resultsx)
                    {
                        Thread StartGameW = new Thread(StartGameTW);
                        StartGameW.Start();
                    }
                }
            }
            catch
            {

            }
        }
        //(FindResource("showMew") as System.Windows.Media.Animation.Storyboard).Begin(StartGamew.SM);
        //StartGame.IsEnabled = false;
        internal static string logs = null;
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="Log"></param>
        public void AssetDownload_DownloadProgressChanged(AssetDownload.DownloadIntermation Log)
        {
            // MessageBox.Show("2");
            Console.WriteLine(Log.Progress + "  " + Log.Speed);
            if (Log.Progress == 100)
            {
                this.Dispatcher.Invoke(new Action(delegate { Thread StartGameW = new Thread(StartGameTW); StartGameW.Start(); }));
            }
        }
        public static String SkinUUIDMC;
        public void error(SquareMinecraftLauncher.Minecraft.Game.Error error)
        {
            try
            {
                Dispatcher.Invoke((Action) delegate ()
                {
                    if (error.SeriousError != null)
                    {
                        ContentDialog dialog = new ContentDialog()
                        {
                            Title = "游戏发生错误",
                            PrimaryButtonText = "好吧",
                            IsPrimaryButtonEnabled = true,
                            DefaultButton = ContentDialogButton.Primary,
                            Content = new TextBlock()
                            {
                                TextWrapping = TextWrapping.WrapWithOverflow,
                                Text = error.SeriousError,
                            },

                        };
                        var result = dialog.ShowAsync();
                    }
                });
            }
            catch
            {

            }
        }
        private void log(SquareMinecraftLauncher.Minecraft.Game.Log log)
        {
            logs += log.Message + "\n";
            Console.WriteLine(log.Message);
        }
        private NavigationTransitionInfo _transitionInfo = null;
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (FSM3.framecontrol.frame != null)
            {
                _transitionInfo = new DrillInNavigationTransitionInfo();
                FSM3.framecontrol.frame.Navigate(dyuri("/Pages/VerList.xaml"), null, _transitionInfo);
            }
        }
        public static Uri dyuri(string a)
        {

            Uri w = new Uri(a, UriKind.RelativeOrAbsolute);
            return w;
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Load(object sender, RoutedEventArgs e)
        {
            NV.NVW.IsEnabled = true;
            HomePage.Source = new Uri("/Main/HomePage.xaml", UriKind.RelativeOrAbsolute);
            try
            {
                if (IniReadValue("XX", "XX") is null || IniReadValue("XX", "XX") is "")
                {
                    tools.Tools.SetMinecraftFilesPath(Dminecraft);

                    AllTheExistingVersion[] t = tools.Tools.GetAllTheExistingVersion();
                    NowV.Content = t[0].version;
                }
                else
                {
                    NowV.Content = IniReadValue("XX", "XX");
                }
            }
            catch
            {

            }
        }
    }
}
