using FSMLauncher_3;
using Gac;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
using static FSM3.MainWindow;

namespace FSM3.Pages
{
    /// <summary>
    /// Online.xaml 的交互逻辑
    /// </summary>
    public partial class Online : System.Windows.Controls.Page
    {
        private ContentDialog dialog;
        private Task<ModernWpf.Controls.ContentDialogResult> result;
        public Online()
        {
            InitializeComponent();
            ReadStdOutput1 += new DelReadStdOutput1(ReadStdOutputAction1);
            ReadStdOutputw += new DelReadStdOutputw(ReadStdOutputActionw);
            ReadStdOutputG += new DelReadStdOutputG(ReadStdOutputActionG);
            ReadStdOutputG1 += new DelReadStdOutputG1(ReadStdOutputActionG1);
            try
            {
                File.Delete(FileOnlineServer + @"\frpc.ini");
                File.Delete(FileOnlineKEHU + @"\frpc.ini");
            }
            catch
            {

            }
            String File_ = Game.ZongW + @"\Server\frpc.exe";
            if (!File.Exists(File_))
            {
                ZWindoww.Zwindow.IsEnabled = false;
                ZBt.zbt.Content = "FSM Launcher(初始化联机)";
                dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                Directory.CreateDirectory(Game.ZongW + @"\Server");
                Game.did1 = Download(File_, "OnLine", "http://www.baibaoblog.cn:81/frpc/Server/frpc.exe");
                Game.ONLINEW = Core5.timer(OnLineI, 2333);
                Game.ONLINEW.Start();
            }
        }
        private async void OnLineI(object ob, EventArgs a)
        {

            string aa = DIYvar.xzItems[Game.did1].xzwz;
            if (aa == "完成")
            {
                await Task.Run(() =>
                {
                    Thread.Sleep(2333);
                });
                ONLINEW.Stop();
                ONLINEW.Stop();
                ONLINEW.Stop();
                ONLINEW.Stop();
                ZWindoww.Zwindow.IsEnabled = true;
                ZBt.zbt.Content = "FSM Launcher";
                ONLINEW.Stop();
            }

            //JarTimerBool = false;


        }
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
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

        public Gac.DownLoadFile dlf = new DownLoadFile();
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
                    //this.ShowMessageAsync("下载错误", msg.ErrMessage + "\n请尝试更换下载源");

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
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public string onlinename;
        public string onlineduankou;
        public string onlinezijiqq;
        public string onlineduifangqq;
        public string MFLJ;
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CJFJ.Content.ToString() is "关闭房间")
            {
                try
                {
                    CmdProcess1.Kill();
                    CmdProcess1.CancelOutputRead();
                    //ReadStdOutput = null;
                    CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                    CJFJ.Content = "创建并开启房间";
                }
                catch
                {

                }
            }
            else
            {
                StackPanel panel = new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };
                panel.Children.Add(new TextBlock() { Text = "请输入你的一些信息，以便进行联机" });
                TextBox box = new TextBox();
                TextBox box2 = new TextBox();
                TextBox box3 = new TextBox();
                box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "房间名:");
                box2.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "你的QQ:");
                box3.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "游戏端口:");
                panel.Children.Add(box);
                panel.Children.Add(box2);
                panel.Children.Add(box3);
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "联机信息采集",
                    PrimaryButtonText = "开始！",
                    CloseButtonText = "取消",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = panel,
                };
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    onlinename = box.Text;
                    onlinezijiqq = box2.Text;
                    onlineduankou = box3.Text;
                    if(onlinename != "" && onlinezijiqq != "" && onlineduankou != "")
                    {
                        try
                        {
                            if (Game.IniReadValue("ONLINE", "Server") == "GZ")
                            {
                                WebClient MyWebClient = new WebClient();
                                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                                StringBuilder sb = new StringBuilder();
                                String pageData = MyWebClient.DownloadString("http://119.29.66.223/"); //从指定网站下载数据
                                pageData = Encoding.UTF8.GetString(MyWebClient.DownloadData("http://119.29.66.223/"));
                                byte[] buff = Convert.FromBase64String(pageData);
                                string decStr = System.Text.Encoding.Default.GetString(buff);
                                Game.WritePrivateProfileString("common", "server_addr", "gz1.qwq.one", FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString("common", "server_port", "7000", FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString("common", "dns", "223.5.5.5", FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString("common", "token", decStr, FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString(onlinezijiqq, "type", Game.IniReadValue("ONLINE", "TCPP2P"), FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString(onlinezijiqq, "sk", "12345678", FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString(onlinezijiqq, "local_port", onlineduankou, FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString(onlinezijiqq, "remote_port", "25565", FileOnlineServer + @"\frpc.ini");
                                CmdProcess1.StartInfo.FileName = FileOnlineServer + @"\frpc.exe";
                                //CmdProcess.StartInfo.FileName = StartFileName;
                                CmdProcess1.StartInfo.Arguments = "-c " + FileOnlineServer + @"\frpc.ini";
                                CmdProcess1.StartInfo.CreateNoWindow = true;
                                CmdProcess1.StartInfo.UseShellExecute = false;
                                CmdProcess1.StartInfo.RedirectStandardInput = true;
                                CmdProcess1.StartInfo.RedirectStandardOutput = true;
                                CJFJ.Content = "关闭房间";
                                CmdProcess1.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived1);
                                CmdProcess1.Start();
                                CmdProcess1.BeginOutputReadLine();
                            }
                            else
                            {
                                WebClient MyWebClient = new WebClient();
                                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                                StringBuilder sb = new StringBuilder();
                                String pageData = MyWebClient.DownloadString("http://1.116.201.220/"); //从指定网站下载数据
                                pageData = Encoding.UTF8.GetString(MyWebClient.DownloadData("http://1.116.201.220/"));
                                byte[] buff = Convert.FromBase64String(pageData);
                                string decStr = System.Text.Encoding.Default.GetString(buff);
                                Game.WritePrivateProfileString("common", "server_addr", "sh.qwq.one", FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString("common", "server_port", "7000", FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString("common", "dns", "223.5.5.5", FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString("common", "token", decStr, FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString(onlinezijiqq, "type", Game.IniReadValue("ONLINE", "TCPP2P"), FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString(onlinezijiqq, "sk", "12345678", FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString(onlinezijiqq, "local_port", onlineduankou, FileOnlineServer + @"\frpc.ini");
                                Game.WritePrivateProfileString(onlinezijiqq, "remote_port", "25565", FileOnlineServer + @"\frpc.ini");
                                CmdProcess1.StartInfo.FileName = FileOnlineServer + @"\frpc.exe";
                                //CmdProcess.StartInfo.FileName = StartFileName;
                                CmdProcess1.StartInfo.Arguments = "-c " + FileOnlineServer + @"\frpc.ini";
                                CmdProcess1.StartInfo.CreateNoWindow = true;
                                CmdProcess1.StartInfo.UseShellExecute = false;
                                CmdProcess1.StartInfo.RedirectStandardInput = true;
                                CmdProcess1.StartInfo.RedirectStandardOutput = true;
                                CJFJ.Content = "关闭房间";
                                CmdProcess1.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived1);
                                CmdProcess1.Start();
                                CmdProcess1.BeginOutputReadLine();
                            }
                        }
                        catch (Exception ex)
                        {
                            ContentDialog dialogw = new ContentDialog()
                            {
                                Title = "灾难性故障",
                                PrimaryButtonText = "好吧",
                                IsPrimaryButtonEnabled = true,
                                DefaultButton = ContentDialogButton.Primary,
                                Content = new TextBlock()
                                {
                                    TextWrapping = TextWrapping.WrapWithOverflow,
                                    Text = ex.Message
                                },

                            };
                            var resultw = await dialogw.ShowAsync();
                            if (resultw == ContentDialogResult.Primary)
                            {

                            }
                        }
                    }
                    else
                    {
                        ContentDialog dialogwx = new ContentDialog()
                        {
                            Title = "不填写怎么联机呢",
                            PrimaryButtonText = "我知道啦!",
                            IsPrimaryButtonEnabled = true,
                            DefaultButton = ContentDialogButton.Primary,
                            Content = new TextBlock()
                            {
                                TextWrapping = TextWrapping.WrapWithOverflow,
                                Text = "填写准确的信息,你才能跟好友愉快的联机哦!",
                            },

                        };
                        var resultwx = await dialogwx.ShowAsync();
                        if (resultwx == ContentDialogResult.Primary)
                        {

                        }
                    }
                }
            }
        }
        public static String FileOnlineServer = Game.ZongW + @"\Server";
        public static String FileOnlineKEHU = Game.ZongW + @"\Client";
        private void p_OutputDataReceivedw(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Dispatcher.Invoke(ReadStdOutputw, new object[] { e.Data });
            }
        }
        public void p_OutputDataReceivedG(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Dispatcher.Invoke(ReadStdOutputG, new object[] { e.Data });
            }
        }
        public void p_OutputDataReceived1(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Dispatcher.Invoke(ReadStdOutput1, new object[] { e.Data });
            }
        }
        public void p_OutputDataReceivedG1(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Dispatcher.Invoke(ReadStdOutputG1, new object[] { e.Data });
            }
        }
        private async void ReadStdOutputAction1(string msg)
        {
            SC.Text = SC.Text + msg;
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs");
            //File.Create(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs\Online.log");
            using (StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs\Online.log"))
            {
                //开始写入
                sw.Write(SC.Text);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
            }
            if (msg.IndexOf("login") + 1 != 0)
            {
                if (msg.IndexOf("success") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n登录服务器成功！\n";
                }

            }
            if (msg.IndexOf("start") + 1 != 0)
            {
                if (msg.IndexOf("success") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n内网映射桥接成功！\n";
                    CJFJ.Content = "关闭房间";
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "开启成功",
                        PrimaryButtonText = "复制邀请码",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "您已开房，请复制邀请码给小伙伴~"
                        },

                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        byte[] b = System.Text.Encoding.Default.GetBytes(onlinezijiqq + "|" + onlinename);
                        MFLJ = Convert.ToBase64String(b);
                        Clipboard.SetDataObject(Convert.ToBase64String(b));
                    }
                }
                if (msg.IndexOf("error") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n内网映射桥接失败！\n";
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "桥接失败",
                        PrimaryButtonText = "打开日志",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "呜呜失败了，如果不能自己解决找作者哦"
                        },

                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs");
                    }
                    try
                    {
                        CmdProcess1.Kill();
                        CmdProcess1.CancelOutputRead();
                        //ReadStdOutput = null;
                        CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                        CJFJ.Content = "创建并开启房间";
                    }
                    catch
                    {
                        try
                        {
                            CmdProcess1.CancelOutputRead();
                            //ReadStdOutput = null;
                            CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);
                            CJFJ.Content = "创建并开启房间";
                        }
                        catch
                        {
                            CJFJ.Content = "创建并开启房间";
                        }
                    }
                    if (msg.IndexOf("port already used") + 1 != 0)
                    {
                        SC.Text = SC.Text + "\n远程端口被占用，请重新配置！\n";
                    }
                    if (msg.IndexOf("proxy name") + 1 != 0)
                    {
                        if (msg.IndexOf("already in use") + 1 != 0)
                        {
                            SC.Text = SC.Text + "\n此QQ号已被占用！请重启电脑再试或联系作者！\n";
                        }
                    }
                    //frpcOutlog.Text = frpcOutlog.Text + "\n端口被占用！请检查是否有进程占用端口或重新配置内网映射！\n";
                }

            }
            SC.ScrollToEnd();
        }
        public delegate void DelReadStdOutput1(string result);
        public delegate void DelReadStdOutputw(string result);
        public delegate void DelReadStdOutputG(string result);
        public delegate void DelReadStdOutputG1(string result);
        //frpc -c frpc.ini
        public static Process CmdProcess1 = new Process();
        public event DelReadStdOutputG ReadStdOutputG;
        public event DelReadStdOutput1 ReadStdOutput1;
        public event DelReadStdOutputG1 ReadStdOutputG1;
        public event DelReadStdOutputw ReadStdOutputw;
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
        public static string[] after;
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Game.IniReadValue("ONLINE", "Server") == "GZ")
            {
                try
                {
                    WebClient MyWebClient = new WebClient();
                    MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                    StringBuilder sb = new StringBuilder();
                    String pageData = MyWebClient.DownloadString("http://119.29.66.223/"); //从指定网站下载数据
                    pageData = Encoding.UTF8.GetString(MyWebClient.DownloadData("http://119.29.66.223/"));
                    byte[] buff = Convert.FromBase64String(pageData);
                    string decStr = System.Text.Encoding.Default.GetString(buff);
                    StackPanel panel = new StackPanel()
                    {
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                    };
                    panel.Children.Add(new TextBlock() { Text = "请输入你的一些信息，以便进行联机" });
                    TextBox box = new TextBox();
                    TextBox box2 = new TextBox();
                    box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "邀请码:");
                    box2.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "你的QQ:");
                    panel.Children.Add(box);
                    panel.Children.Add(box2);
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "联机信息采集",
                        PrimaryButtonText = "开始！",
                        CloseButtonText = "取消",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = panel,
                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        String yqm = box.Text;
                        if (yqm == MFLJ)
                        {
                            StackPanel panelw = new StackPanel()
                            {
                                VerticalAlignment = VerticalAlignment.Stretch,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                            };
                            panelw.Children.Add(new TextBlock() { Text = "获得成就:我连我自己\n奥利给!!!!!!" });
                            ContentDialog dialogw = new ContentDialog()
                            {
                                Title = "真好玩",
                                PrimaryButtonText = "好玩!",
                                IsPrimaryButtonEnabled = true,
                                DefaultButton = ContentDialogButton.Primary,
                                Content = panelw,
                            };
                            var resultw = await dialogw.ShowAsync();
                            if (resultw == ContentDialogResult.Primary)
                            {

                            }
                        }
                        else
                        {
                            String xcqq = box2.Text;
                            byte[] c = Convert.FromBase64String(yqm);
                            String ww = System.Text.Encoding.Default.GetString(c);
                            after = ww.Split(new char[] { '|' });
                            onlineduifangqq = after[0];
                            Game.WritePrivateProfileString("common", "server_addr", "gz1.qwq.one", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString("common", "server_port", "7000", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString("common", "token", decStr, FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString("common", "dns", "223.5.5.5", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "server_name", onlineduifangqq, FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "type", Game.IniReadValue("ONLINE", "TCPP2P"), FileOnlineKEHU + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "bind_addr", "127.0.0.1", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "bind_port", "25565", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "role", "visitor", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "sk", "12345678", FileOnlineServer + @"\frpc.ini");
                            CmdProcess1.StartInfo.FileName = FileOnlineServer + @"\frpc.exe";
                            //CmdProcess.StartInfo.FileName = StartFileName;
                            CmdProcess1.StartInfo.Arguments = "-c " + FileOnlineServer + @"\frpc.ini";
                            CmdProcess1.StartInfo.CreateNoWindow = true;
                            CmdProcess1.StartInfo.UseShellExecute = false;
                            CmdProcess1.StartInfo.RedirectStandardInput = true;
                            CmdProcess1.StartInfo.RedirectStandardOutput = true;
                            CmdProcess1.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceivedw);
                            CmdProcess1.Start();
                            CmdProcess1.BeginOutputReadLine();
                        }
                    }
                }
                catch(Exception ex)
                {
                    StackPanel panel = new StackPanel()
                    {
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                    };
                    panel.Children.Add(new TextBlock() { Text = "怎么会失败!?可能是邀请码错误 打开日志看看吧！\n"+ex.Message });
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "失..失败了？",
                        PrimaryButtonText = "打开日志",
                        CloseButtonText = "取消",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = panel,
                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs");
                    }
                }
            }
            else
            {
                try
                {
                    WebClient MyWebClient = new WebClient();
                    MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                    StringBuilder sb = new StringBuilder();
                    String pageData = MyWebClient.DownloadString("http://1.116.201.220/"); //从指定网站下载数据
                    pageData = Encoding.UTF8.GetString(MyWebClient.DownloadData("http://1.116.201.220/"));
                    byte[] buff = Convert.FromBase64String(pageData);
                    string decStr = System.Text.Encoding.Default.GetString(buff);
                    StackPanel panel = new StackPanel()
                    {
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                    };
                    panel.Children.Add(new TextBlock() { Text = "请输入你的一些信息，以便进行联机" });
                    TextBox box = new TextBox();
                    TextBox box2 = new TextBox();
                    box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "邀请码:");
                    box2.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "你的QQ:");
                    panel.Children.Add(box);
                    panel.Children.Add(box2);
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "联机信息采集",
                        PrimaryButtonText = "开始！",
                        CloseButtonText = "取消",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = panel,
                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        String yqm = box.Text;
                        if (yqm == MFLJ)
                        {
                            StackPanel panelw = new StackPanel()
                            {
                                VerticalAlignment = VerticalAlignment.Stretch,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                            };
                            panelw.Children.Add(new TextBlock() { Text = "获得成就:我连我自己\n奥利给!!!!!!" });
                            ContentDialog dialogw = new ContentDialog()
                            {
                                Title = "真好玩",
                                PrimaryButtonText = "好玩!",
                                IsPrimaryButtonEnabled = true,
                                DefaultButton = ContentDialogButton.Primary,
                                Content = panelw,
                            };
                            var resultw = await dialogw.ShowAsync();
                            if (resultw == ContentDialogResult.Primary)
                            {
                                
                            }
                        }
                        else
                        {
                            String xcqq = box2.Text;
                            byte[] c = Convert.FromBase64String(yqm);
                            String ww = System.Text.Encoding.Default.GetString(c);
                            after = ww.Split(new char[] { '|' });
                            onlineduifangqq = after[0];
                            Game.WritePrivateProfileString("common", "server_addr", "sh.qwq.one", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString("common", "server_port", "7000", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString("common", "token", decStr, FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString("common", "dns", "223.5.5.5", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "server_name", onlineduifangqq, FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "type", Game.IniReadValue("ONLINE", "TCPP2P"), FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "bind_addr", "127.0.0.1", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "bind_port", "25565", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "role", "visitor", FileOnlineServer + @"\frpc.ini");
                            Game.WritePrivateProfileString(xcqq, "sk", "12345678", FileOnlineServer + @"\frpc.ini");
                            CmdProcess1.StartInfo.FileName = FileOnlineServer + @"\frpc.exe";
                            //CmdProcess.StartInfo.FileName = StartFileName;
                            CmdProcess1.StartInfo.Arguments = "-c " + FileOnlineServer + @"\frpc.ini";
                            CmdProcess1.StartInfo.CreateNoWindow = true;
                            CmdProcess1.StartInfo.UseShellExecute = false;
                            CmdProcess1.StartInfo.RedirectStandardInput = true;
                            CmdProcess1.StartInfo.RedirectStandardOutput = true;
                            CmdProcess1.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceivedw);
                            CmdProcess1.Start();
                            CmdProcess1.BeginOutputReadLine();
                        }
                    }
                }
                catch
                {
                    StackPanel panel = new StackPanel()
                    {
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                    };
                    panel.Children.Add(new TextBlock() { Text = "怎么会失败!?可能是邀请码错误 打开日志看看吧！" });
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "失..失败了？",
                        PrimaryButtonText = "打开日志",
                        CloseButtonText = "取消",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = panel,
                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs");
                    }
                }
            }
        }
        private async void ReadStdOutputActionw(string msg)
        {
            SC.Text = SC.Text + msg;
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs");
            //File.Create(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs\Online.log");
            using (StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs\Online.log"))
            {
                //开始写入
                sw.Write(SC.Text);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
            }
            if (msg.IndexOf("login") + 1 != 0)
            {
                if (msg.IndexOf("success") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n登录服务器成功！\n";
                }

            }
            if (msg.IndexOf("start") + 1 != 0)
            {
                if (msg.IndexOf("success") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n内网映射桥接成功！\n";
                    JRFJ.IsEnabled = false;
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "连接成功!",
                        PrimaryButtonText = "好哒!",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "你连接上啦!在Minecraft多人游戏中输入127.0.0.1:25565即可连接！"
                        },

                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {

                    }
                }
                if (msg.IndexOf("error") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n内网映射桥接失败！\n";
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "桥接失败",
                        PrimaryButtonText = "打开日志",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "呜呜失败了，如果不能自己解决找作者哦"
                        },

                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs");
                    }
                    try
                    {
                        CmdProcess1.Kill();
                        CmdProcess1.CancelOutputRead();
                        //ReadStdOutput = null;
                        CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceivedw);
                        
                    }
                    catch
                    {
                        try
                        {
                            CmdProcess1.CancelOutputRead();
                            //ReadStdOutput = null;
                            CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceivedw);
                            
                        }
                        catch
                        {
                            
                        }
                    }
                    if (msg.IndexOf("port already used") + 1 != 0)
                    {
                        SC.Text = SC.Text + "\n远程端口被占用，请重新配置！\n";
                    }
                    if (msg.IndexOf("proxy name") + 1 != 0)
                    {
                        if (msg.IndexOf("already in use") + 1 != 0)
                        {
                            SC.Text = SC.Text + "\n此QQ号已被占用！请重启电脑再试或联系作者！\n";
                        }
                    }
                    //frpcOutlog.Text = frpcOutlog.Text + "\n端口被占用！请检查是否有进程占用端口或重新配置内网映射！\n";
                }

            }
            SC.ScrollToEnd();
        }
        private void Button_Click_GXNC(object sender, RoutedEventArgs e)
        {

        }
        private async void ReadStdOutputActionG(string msg)
        {
            SC.Text = SC.Text + msg;
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs");
            //File.Create(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs\Online.log");
            using (StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs\Online.log"))
            {
                //开始写入
                sw.Write(SC.Text);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
            }
            if (msg.IndexOf("login") + 1 != 0)
            {
                if (msg.IndexOf("success") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n登录服务器成功！\n";
                }

            }
            if (msg.IndexOf("start") + 1 != 0)
            {
                if (msg.IndexOf("success") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n内网映射桥接成功！\n";
                    JRFJ.IsEnabled = false;
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "创建成功!",
                        PrimaryButtonText = "复制邀请码",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "快让小伙伴来玩 复制邀请码哦!"
                        },

                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        byte[] b = System.Text.Encoding.Default.GetBytes(aw);
                        Clipboard.SetDataObject(Convert.ToBase64String(b));
                        GXNCJ.Content = "关闭房间";
                    }
                }
                if (msg.IndexOf("error") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n内网映射桥接失败！\n";
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "桥接失败",
                        PrimaryButtonText = "打开日志",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "呜呜失败了，如果不能自己解决找作者哦"
                        },

                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs");
                    }
                    try
                    {
                        CmdProcess1.Kill();
                        CmdProcess1.CancelOutputRead();
                        //ReadStdOutput = null;
                        CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceivedG);

                    }
                    catch
                    {
                        try
                        {
                            CmdProcess1.CancelOutputRead();
                            //ReadStdOutput = null;
                            CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceivedG);
                            GXNCJ.Content = "创建并开启房间";
                        }
                        catch
                        {
                            GXNCJ.Content = "创建并开启房间";
                        }
                    }
                    if (msg.IndexOf("port already used") + 1 != 0)
                    {
                        SC.Text = SC.Text + "\n远程端口被占用，请重新配置！\n";
                    }
                    if (msg.IndexOf("proxy name") + 1 != 0)
                    {
                        if (msg.IndexOf("already in use") + 1 != 0)
                        {
                            SC.Text = SC.Text + "\n此QQ号已被占用！请重启电脑再试或联系作者！\n";
                        }
                    }
                    //frpcOutlog.Text = frpcOutlog.Text + "\n端口被占用！请检查是否有进程占用端口或重新配置内网映射！\n";
                }

            }
            SC.ScrollToEnd();
        }
        private async void Button_Click_GXNK(object sender, RoutedEventArgs e)
        {
            if (GXNCJ.Content.ToString() is "关闭房间")
            {
                try
                {
                    CmdProcess1.Kill();
                    CmdProcess1.CancelOutputRead();
                    //ReadStdOutput = null;
                    CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceivedG);
                    GXNCJ.Content = "创建并开启房间";
                }
                catch
                {

                }
            }
            else {
                String JD;
                if (HZ.IsChecked == true)
                {
                    JD = "hz.qwq.one";
                }
                else
                {
                    JD = "gz2.qwq.one";
                }
                StackPanel panel = new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };
                panel.Children.Add(new TextBlock() { Text = "请输入你的一些信息，以便进行高性能联机" });
                TextBox box = new TextBox();
                TextBox box2 = new TextBox();
                TextBox box3 = new TextBox();
                box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "密码(请勿泄露):");
                box2.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "你的QQ:");
                box3.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "游戏端口:");
                panel.Children.Add(box);
                panel.Children.Add(box2);
                panel.Children.Add(box3);
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "联机信息采集",
                    PrimaryButtonText = "开始！",
                    CloseButtonText = "取消",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = panel,
                };
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    String Password = box.Text;
                    String QQ = box2.Text;
                    String DK = box3.Text;
                    Game.WritePrivateProfileString("common", "server_addr", JD, FileOnlineServer + @"\frpc.ini");
                    Game.WritePrivateProfileString("common", "server_port", "7000", FileOnlineServer + @"\frpc.ini");
                    Game.WritePrivateProfileString("common", "user", QQ, FileOnlineServer + @"\frpc.ini");
                    Game.WritePrivateProfileString("common", "meta_token", Password, FileOnlineServer + @"\frpc.ini");
                    Game.WritePrivateProfileString(QQ, "type", "stcp", FileOnlineServer + @"\frpc.ini");
                    Game.WritePrivateProfileString(QQ, "sk", "test", FileOnlineServer + @"\frpc.ini");
                    Game.WritePrivateProfileString(QQ, "local_port", DK, FileOnlineServer + @"\frpc.ini");
                    Game.WritePrivateProfileString(QQ, "remote_port", "32423", FileOnlineServer + @"\frpc.ini");
                    CmdProcess1.StartInfo.FileName = FileOnlineServer + @"\frpc.exe";
                    //CmdProcess.StartInfo.FileName = StartFileName;
                    CmdProcess1.StartInfo.Arguments = "-c " + FileOnlineServer + @"\frpc.ini";
                    CmdProcess1.StartInfo.CreateNoWindow = true;
                    CmdProcess1.StartInfo.UseShellExecute = false;
                    CmdProcess1.StartInfo.RedirectStandardInput = true;
                    CmdProcess1.StartInfo.RedirectStandardOutput = true;
                    CmdProcess1.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceivedG);
                    CmdProcess1.Start();
                    CmdProcess1.BeginOutputReadLine();
                    aw = EncryptDES(QQ + "|" + JD + "|" + Password, "1234567w");
                }
            }
        }
        String aw;
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串 </returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));//转换为字节
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();//实例化数据加密标准
                MemoryStream mStream = new MemoryStream();//实例化内存流
                //将数据流链接到加密转换的流
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        private void Button_Click_GXNFZ(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click_GXNJ(object sender, RoutedEventArgs e)
        {
            StackPanel panel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            panel.Children.Add(new TextBlock() { Text = "请输入你的一些信息，以便进行高性能联机" });
            TextBox box = new TextBox();
            TextBox box2 = new TextBox();
            box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "邀请码:");
            box2.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "你的QQ:");
            panel.Children.Add(box);
            panel.Children.Add(box2);
            ContentDialog dialog = new ContentDialog()
            {
                Title = "联机信息采集",
                PrimaryButtonText = "开始！",
                CloseButtonText = "取消",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = panel,
            };
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                String yqm = box.Text;
                String WWQ = box2.Text;
                byte[] c = Convert.FromBase64String(yqm);
                String ww = System.Text.Encoding.Default.GetString(c);
                String JM = DecryptDES(ww, "1234567w");
                after = JM.Split(new char[] { '|' });
                Game.WritePrivateProfileString("common", "server_addr", after[1], FileOnlineKEHU + @"\frpc.ini");
                Game.WritePrivateProfileString("common", "server_port", "7000", FileOnlineKEHU + @"\frpc.ini");
                Game.WritePrivateProfileString("common", "user", after[0], FileOnlineKEHU + @"\frpc.ini");
                Game.WritePrivateProfileString("common", "meta_token", after[2], FileOnlineKEHU + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "server_name", after[0], FileOnlineKEHU + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "type", "stcp", FileOnlineKEHU + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "bind_addr", "127.0.0.1", FileOnlineKEHU + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "bind_port", "32423", FileOnlineKEHU + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "role", "visitor", FileOnlineKEHU + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "sk", "test", FileOnlineKEHU + @"\frpc.ini");
                CmdProcess1.StartInfo.FileName = FileOnlineServer + @"\frpc.exe";
                //CmdProcess.StartInfo.FileName = StartFileName;
                CmdProcess1.StartInfo.Arguments = "-c " + FileOnlineServer + @"\frpc.ini";
                CmdProcess1.StartInfo.CreateNoWindow = true;
                CmdProcess1.StartInfo.UseShellExecute = false;
                CmdProcess1.StartInfo.RedirectStandardInput = true;
                CmdProcess1.StartInfo.RedirectStandardOutput = true;
                CmdProcess1.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceivedG1);
                CmdProcess1.Start();
                CmdProcess1.BeginOutputReadLine();
                File.Delete(FileOnlineServer + @"\frpc.ini");
            }
        }
        private async void ReadStdOutputActionG1(string msg)
        {
            SC.Text = SC.Text + msg;
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs");
            //File.Create(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs\Online.log");
            using (StreamWriter sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs\Online.log"))
            {
                //开始写入
                sw.Write(SC.Text);
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
            }
            if (msg.IndexOf("login") + 1 != 0)
            {
                if (msg.IndexOf("success") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n登录服务器成功！\n";
                }

            }
            if (msg.IndexOf("start") + 1 != 0)
            {
                if (msg.IndexOf("success") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n内网映射桥接成功！\n";
                    JRFJ.IsEnabled = false;
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "加入成功!",
                        PrimaryButtonText = "好哒!",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "进游戏后打开多人游戏，输入127.0.0.1:32423以连接哦!"
                        },

                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        byte[] b = System.Text.Encoding.Default.GetBytes(aw);
                        Clipboard.SetDataObject(Convert.ToBase64String(b));
                    }
                }
                if (msg.IndexOf("error") + 1 != 0)
                {
                    SC.Text = SC.Text + "\n内网映射桥接失败！\n";
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "桥接失败",
                        PrimaryButtonText = "打开日志",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "呜呜失败了，如果不能自己解决找作者哦"
                        },

                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\Logs");
                    }
                    try
                    {
                        CmdProcess1.Kill();
                        CmdProcess1.CancelOutputRead();
                        //ReadStdOutput = null;
                        CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);

                    }
                    catch
                    {
                        try
                        {
                            CmdProcess1.CancelOutputRead();
                            //ReadStdOutput = null;
                            CmdProcess1.OutputDataReceived -= new DataReceivedEventHandler(p_OutputDataReceived1);

                        }
                        catch
                        {

                        }
                    }
                    if (msg.IndexOf("port already used") + 1 != 0)
                    {
                        SC.Text = SC.Text + "\n远程端口被占用，请重新配置！\n";
                    }
                    if (msg.IndexOf("proxy name") + 1 != 0)
                    {
                        if (msg.IndexOf("already in use") + 1 != 0)
                        {
                            SC.Text = SC.Text + "\n此QQ号已被占用！请重启电脑再试或联系作者！\n";
                        }
                    }
                    //frpcOutlog.Text = frpcOutlog.Text + "\n端口被占用！请检查是否有进程占用端口或重新配置内网映射！\n";
                }

            }
            SC.ScrollToEnd();
        }
    }
}
