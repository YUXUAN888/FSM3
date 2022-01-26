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
        
        public Game()
        {
            InitializeComponent();
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
            switch (IniReadValue("Login", "LoginM"))
            {
                case "1":
                    if (wryes == "888")
                    {
                        WrD.SelectedIndex = 1;
                        NNW.Content = wrname;
                        BitmapImage bi = new BitmapImage();
                        // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                        bi.BeginInit();
                        bi.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png", UriKind.RelativeOrAbsolute);
                        bi.EndInit();
                        IMW.Source = bi;
                        loginmode = "wr";
                        WritePrivateProfileString("Login", "LoginM", "1", FileS);
                    }
                    else
                    {
                        WrD.SelectedIndex = 0;
                        loginmode = "";
                    }
                    break;
            }
            

        }
        public static int dw;
        public static string Mojangname;
        public static string MojangUUID;
        public static string MojangToken;
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        static System.Windows.Threading.DispatcherTimer Jarw = new System.Windows.Threading.DispatcherTimer();
        public static System.Windows.Threading.DispatcherTimer ONLINEW = new System.Windows.Threading.DispatcherTimer();
        static System.Windows.Threading.DispatcherTimer UPDATEW = new System.Windows.Threading.DispatcherTimer();
        static int did1, did2, did3, did4;
        public static int id = 0;
        static String ZongX = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); //获取APPDATA
        public string UpdateD;
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
        public static String FileOnlineServer = ZongW + @"\Server";
        public static String FileOnlineKEHU = ZongW + @"\Client";
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
            if (wryes == "888")
            {
                WrD.SelectedIndex = 1;
                NNW.Content = wrname;
                BitmapImage bi = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                bi.BeginInit();
                bi.UriSource = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Skin\steven.png", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                IMW.Source = bi;
                loginmode = "wr";
                WritePrivateProfileString("Login", "LoginM", "1", FileS);
            }
            else
            {
                WrD.SelectedIndex = 0;
                loginmode = "";
            }
        }

        private void LXUP(object sender, MouseButtonEventArgs e)
        {
            WritePrivateProfileString("Login", "LoginM", "2", FileS);
            loginmode = "offline";
        }

        private void WZUP(object sender, MouseButtonEventArgs e)
        {
            /*
            if (Yyes == "888")
            {
                IDTab.SelectedIndex = 4;
                JS.Text = Yname;
                loginmode = "y";
                WritePrivateProfileString("Login", "LoginM", "3", FileS);
            }
            else
            {
                IDTab.SelectedIndex = 5;
                loginmode = "";
            }
            */
        }
        public static string offlinename;
        private void OfflineName_TextChanged(object sender, TextChangedEventArgs e)
        {
            offlinename = OfflineName.Text;
            loginmode = "offline";
            WritePrivateProfileString("OffLine", "ID", OfflineName.Text, ZongW + @"\ConsoleW.qwq");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (FSM3.framecontrol.frame != null)
            {
                FSM3.framecontrol.frame.Source = new Uri("/Pages/VerList.xaml", UriKind.RelativeOrAbsolute);
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Load(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Test.Xaml"))
                {
                    try
                    {

                        HomePage.Source = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Test.Xaml", UriKind.RelativeOrAbsolute);
                    }
                    catch
                    {

                    }

                }
                else
                {
                    HomePage.Source = new Uri("/Main/HomePage.xaml", UriKind.RelativeOrAbsolute);
                }
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "加载自定义主页错误",
                    PrimaryButtonText = "重试",
                    CloseButtonText = "取消",
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
                    return;
                }
                else if (result == ContentDialogResult.None)
                {

                }
            }
        }
    }
}
