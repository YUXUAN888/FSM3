using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
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
using FSM3.Dialogs;
using FSM3.Pages;
using FSMLauncher_3;
using Gac;
using ModernWpf;
using ModernWpf.Controls;
using ModernWpf.Navigation;
using SquareMinecraftLauncher.Core.OAuth;
using SquareMinecraftLauncher.Minecraft;
using static FSM3.Pages.Settings;

namespace FSM3
{
    public class framecontrol
    {
        public static ModernWpf.Controls.Frame frame { get; set; }

    }

    
    public class tools
    {
        public static SquareMinecraftLauncher.Minecraft.Tools Tools { get; set; }

    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        SquareMinecraftLauncher.Minecraft.Tools Toolsw = new SquareMinecraftLauncher.Minecraft.Tools();
        public static string mojangyes;
        public static string wryes;
        public static string lxyes;
        public static string loginmode;
        public static int qd = 0;
        public static string wrtoken;
        public static string wruuid;
        public static string wrname;
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
        public static int dw;
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        static System.Windows.Threading.DispatcherTimer Jarw = new System.Windows.Threading.DispatcherTimer();
        public static System.Windows.Threading.DispatcherTimer ONLINEW = new System.Windows.Threading.DispatcherTimer();
        static System.Windows.Threading.DispatcherTimer UPDATEW = new System.Windows.Threading.DispatcherTimer();
        static int did1, did2, did3, did4;
        public static int id = 0;
        static String ZongX = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); //获取APPDATA
        public static String ZongW = ZongX + @"\.fsm";
        String ZongSkin = ZongX + @"\.fsm\Skin";
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
        public class ZWindoww
        {
            public static Window Zwindow { get; set; }

        }
        public class ZBt
        {
            public static Label zbt { get; set; }

        }
        public class NV
        {
            public static NavigationView NVW { get; set; }

        }
        public class StartGamew
        {
            public static StartGame SM { get; set; }
            
        }
        public MainWindow()
        {
            InitializeComponent();
            ServicePointManager.DefaultConnectionLimit = 512;
            Update = "Beta16";
            this.ResizeMode = ResizeMode.CanMinimize;
            framecontrol.frame = ZFrame;
            NV.NVW = nvSample;
            StartGamew.SM = G;
            (FindResource("hideMe") as System.Windows.Media.Animation.Storyboard).Begin(StartGamew.SM);
            tools.Tools = Toolsw;
            ZWindoww.Zwindow = ZWindow;
            ZBt.zbt = ZBT;
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM");
            try
            {
                if (IniReadValue("ONLINE", "TCPP2P") == "" || IniReadValue("ONLINE", "TCPP2P") == null)
                {
                    WritePrivateProfileString("ONLINE", "TCPP2P", "stcp", FileS);
                }
                if (IniReadValueW("Mojang", "Mail") == null || IniReadValueW("Mojang", "Mail") == "")
                {

                }
                else
                {
                    try
                    {
                        var login = tools.Tools.MinecraftLogin(IniReadValueW("Mojang", "Mail"), IniReadValueW("Mojang", "PassWord"));
                        Pages.Game.Mojangname = login.name;
                        Pages.Game.MojangUUID = login.uuid;
                        Pages.Game.MojangToken = login.token;
                        loginmode = "mojang";
                        mojangyes = "888";
                        Download(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png", "", tools.Tools.GetMinecraftSkin(Pages.Game.MojangUUID));
                        System.Drawing.Point point = new System.Drawing.Point(8, 8);
                        System.Drawing.Size size = new System.Drawing.Size(8, 8);
                        Bitmap bitmap = new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                        var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                        Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);

                        //i.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                        System.Drawing.Image img = i;



                    }
                    catch
                    {

                    }


                }
                if (IniReadValueW("wr", "Atoken") == null || IniReadValueW("wr", "Atoken") == "")
                {

                }
                else
                {
                    String Minecraft_Token;
                    try
                    {
                        MicrosoftLogin microsoftLogin = new MicrosoftLogin();
                        Xbox XboxLogin = new Xbox();
                        Minecraft_Token = new MinecraftLogin().GetToken(XboxLogin.XSTSLogin(XboxLogin.GetToken(microsoftLogin.RefreshingTokens(IniReadValueW("wr", "Atoken")))));
                        MinecraftLogin minecraftlogin = new MinecraftLogin();
                        var Minecraft = minecraftlogin.GetMincraftuuid(Minecraft_Token);
                        wruuid = Minecraft.uuid;
                        wrname = Minecraft.name;
                        wrtoken = Minecraft_Token;
                        loginmode = "wr";
                        wryes = "888";
                        System.Drawing.Point point = new System.Drawing.Point(8, 8);
                        System.Drawing.Size size = new System.Drawing.Size(8, 8);
                        Bitmap bitmap = new Bitmap(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\Skin.png");
                        var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                        Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
                        i.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png");
                        System.Drawing.Image img = i;
                        BitmapImage bi = new BitmapImage();
                        // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                        wryes = "888";

                    }
                    catch
                    {

                    }
                }
                try
                {
                    if (IniReadValueW("wz", "IDD") == null || IniReadValueW("wz", "IDD") == "")
                    {
                        ///////////////////////////////////////////////////
                    }
                    else
                    {
                        string yip = IniReadValueW("wz", "IP");
                        string yidd = IniReadValueW("wz", "IDD");
                        string yiddp = IniReadValueW("wz", "IDDPassWord");
                        Pages.Game.skin = tools.Tools.GetAuthlib_Injector(yip, yidd, yiddp);
                        //IDTab.SelectedIndex = 4;
                        Pages.Game.Yyes = "888";
                    }

                }
                catch
                {

                }
                if (FSM3.framecontrol.frame != null)
                {
                    Thread WRDF3 = new Thread(F3);
                    WRDF3.Start();
                }
            (FindResource("hideMe") as System.Windows.Media.Animation.Storyboard).Begin(FSMLogo);
            }
            catch
            {

            }
        }
        String[] after;
        public static string Get(string Url)
        {
            //System.GC.Collect();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();
            myResponseStream.Close();

            if (response != null)
            {
                response.Close();
            }
            if (request != null)
            {
                request.Abort();
            }

            return retString;
        }
        private void F1()
        {



            Application.Current.Dispatcher.Invoke(
        delegate
        {
            //Code
            FSM3.framecontrol.frame.Source = new Uri("/Pages/VerList.xaml", UriKind.RelativeOrAbsolute);
        });
        }
        private void F2()
        {



            Application.Current.Dispatcher.Invoke(
        delegate
        {
            //Code
            FSM3.framecontrol.frame.Source = new Uri("/Pages/Settings.xaml", UriKind.RelativeOrAbsolute);
        });
        }
        private void F3()
        {



            Application.Current.Dispatcher.Invoke(
        delegate
        {
            //Code
            FSM3.framecontrol.frame.Source = new Uri("/Pages/Game.xaml", UriKind.RelativeOrAbsolute);
        });
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Item(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                //这里是设置
                ZFrame.Source = new Uri("/Pages/Settings.xaml", UriKind.RelativeOrAbsolute);
            }
            else
            {
                //这里是上方菜单 为0开始
                ModernWpf.Controls.NavigationViewItem viewItem = nvSample.SelectedItem as ModernWpf.Controls.NavigationViewItem;
                switch (viewItem.Tag)
                {
                    case "Game":
                        ZFrame.Navigate(dyuri("/Pages/Game.xaml"));
                        break;
                    case "Download":
                        ZFrame.Navigate(dyuri("/Pages/Download.xaml"));
                        break;
                    case "Online":
                        ZFrame.Navigate(dyuri("/Pages/Online.xaml"));
                        break;
                    case "About":
                        ZFrame.Navigate(dyuri("/Pages/About.xaml"));
                        break;
                    case "Setting":
                        ZFrame.Navigate(dyuri("/Pages/Settings.xaml"));
                        break;
                }
            }
        }
        public static Uri dyuri(string a)
        {

            Uri w = new Uri(a, UriKind.RelativeOrAbsolute);
            return w;
        }
        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }
        public void Verww()
        {
            ZFrame.Source = new Uri("/Pages/VerList.xaml", UriKind.Relative);
        }

        private async void Load(object sender, RoutedEventArgs e)
        {
            if (IniReadValue("Color", "ZT") != "" || IniReadValue("Color", "ZT") != null)
            {
                if(IniReadValue("Color", "ZT") is "Dark")
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                }
                else
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                }
            }
            String gx1 = Get("http://2018k.cn/api/checkVersion?id=acdbe11aceff42a599113997cbb74103&version=" + Update);
            after = gx1.Split(new char[] { '|' });
            String gx2 = Get("http://2018k.cn/api/getExample?id=acdbe11aceff42a599113997cbb74103&data=force");
            String gx3 = Get("http://2018k.cn/api/getExample?id=acdbe11aceff42a599113997cbb74103&data=version");
            String gx4 = Get("http://2018k.cn/api/getExample?id=acdbe11aceff42a599113997cbb74103&data=remark|notice");
            String gx5 = Get("http://2018k.cn/api/getExample?id=acdbe11aceff42a599113997cbb74103&data=url");
            UpLog_and_GG = gx4.Split(new char[] { '|' });
            UpdateD = gx5;
            if (gx2 == "true" && gx3 != Update)
            {
                SFGX = true;
                xdbb = after[4];
                GXNR = UpLog_and_GG[0];
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "检测到启动器有新的版本!",
                    PrimaryButtonText = "去更新!",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = "新版本为:" + after[4] + "\n请到 设置-软件更新 进行更新!"
                    },

                };
                var result = await dialog.ShowAsync();
            }
            try
            {

                String ww = Get("http://api.2018k.cn/getExample?id=acdbe11aceff42a599113997cbb74103&data=notice");
                string ggxx = IniReadValue("GG", "DQGG");
                if (ggxx == ww)
                {

                }
                else
                {
                    ContentDialog dialogw = new ContentDialog()
                    {
                        Title = "新公告",
                        PrimaryButtonText = "好哒!",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "公告内容:" + ww,
                        },

                    };
                    var resultw = await dialogw.ShowAsync();
                    WritePrivateProfileString("GG", "DQGG", ww, FileS);
                }
            }
            catch
            {

            }
        }
        public string[] UpLog_and_GG;
        public static bool SFGX;
        public static string GXNR;
        public static string xdbb;
        private void Size(object sender, SizeChangedEventArgs e)
        {
            System.Windows.Rect r = new System.Windows.Rect(e.NewSize);
            RectangleGeometry gm = new RectangleGeometry(r, 6, 6);
            ((UIElement)sender).Clip = gm;
        }

        private void MouseWin(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Online.CmdProcess1.Kill();
                Online.CmdProcess1.CancelOutputRead();
                //ReadStdOutput = null;
            }
            catch
            {

            }
            (FindResource("hideMe") as System.Windows.Media.Animation.Storyboard).Begin(this);
            await Task.Run(() =>
            {
                Thread.Sleep(199);
            });
            System.Environment.Exit(0);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
