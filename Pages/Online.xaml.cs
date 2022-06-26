using FSM3.About_List;
using FSMLauncher_3;
using Gac;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
            //if (!File.Exists(File_))
            //{
            //    ZWindoww.Zwindow.IsEnabled = false;
            //    ZBt.zbt.Content = "FSM Launcher(初始化联机)";
            //    dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
            //    Directory.CreateDirectory(Game.ZongW + @"\Server");
            //    Game.did1 = Download(File_, "OnLine", "http://www.baibaoblog.cn:81/frpc/Server/frpc.exe");
            //    Game.ONLINEW = Core5.timer(OnLineI, 2333);
            //    Game.ONLINEW.Start();
            //}
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
                ZBt.zbt.Content = "FSM Launcher 3";
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
            StackPanel panel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            panel.Children.Add(new TextBlock() { Text = "请输入你的游戏端口，以便进行联机" });
            TextBox box3 = new TextBox();
            TextBox box4 = new TextBox();
            TextBox box5 = new TextBox();
            ComboBox box6 = new ComboBox();
            box3.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "游戏端口:");
            panel.Children.Add(box3);
            box4.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "你的昵称:");
            panel.Children.Add(box4);
            box5.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "游戏版本:");
            panel.Children.Add(box5);
            box6.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "加载器:");
            box6.Items.Add("无");
            box6.Items.Add("Forge");
            box6.Items.Add("Fabric");
            box6.SelectedIndex = 0;
            panel.Children.Add(box6);
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
                try
                {
                    SquareMinecraftLauncher.Online.Server s = new SquareMinecraftLauncher.Online.Server();
                    SquareMinecraftLauncher.Online.Server.Type tb = new SquareMinecraftLauncher.Online.Server.Type();
                    if (box6.SelectedIndex is 0)
                    {
                        tb = SquareMinecraftLauncher.Online.Server.Type.NoFound;
                    }
                    if (box6.SelectedIndex is 1)
                    {
                        tb = SquareMinecraftLauncher.Online.Server.Type.Forge;
                    }
                    if (box6.SelectedIndex is 2)
                    {
                        tb = SquareMinecraftLauncher.Online.Server.Type.Fabric;
                    }
                    string yqm = GetRnd(32, true, true, true, false, "@#$%&");
                    await s.ServerConnect("SDEVF3G28RGFEIQ3UFGR4389YRH3IR32G988GEIF328", box4.Text, box5.Text, SquareMinecraftLauncher.Online.Server.Type.Forge, int.Parse(box3.Text), SquareMinecraftLauncher.Online.Server.P2PType.Forward, yqm, "119.45.103.147");
                    s.Start();
                    ContentDialog dialogw = new ContentDialog()
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
                    var resultw = await dialogw.ShowAsync();
                    if (resultw == ContentDialogResult.Primary)
                    {
                        Clipboard.SetDataObject("我正在使用FSM启动器进行 Minecraft" + box5.Text + " 联机！ mod加载器:" + box6.Text + "  快来 FSM 联机方式B 输入邀请码" + yqm + "来找我玩吧!");
                    }
                }
                catch (Exception ex)
                {
                    ContentDialog dialogw = new ContentDialog()
                    {
                        Title = "遇到错误了",
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = ex.Message + "\n可能是你的游戏昵称没有填写，或者游戏版本没有填写",
                        },

                    };
                    var resultw = await dialogw.ShowAsync();
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
            StackPanel panel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            panel.Children.Add(new TextBlock() { Text = "请输入你的邀请码，以便进行联机" });
            TextBox box = new TextBox();
            TextBox box1 = new TextBox();
            box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "邀请码:");
            panel.Children.Add(box);
            box1.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "你的昵称:");
            panel.Children.Add(box1);
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
                try
                {
                    SquareMinecraftLauncher.Online.Client c = new SquareMinecraftLauncher.Online.Client();
                    await c.ClientConnect(box.Text, SquareMinecraftLauncher.Online.Client.P2PType.Forward, "SDEVF3G28RGFEIQ3UFGR4389YRH3IR32G988GEIF328", box1.Text);
                    string s = c.Start();
                    ContentDialog dialogw = new ContentDialog()
                    {
                        Title = "连接成功!",
                        PrimaryButtonText = "复制地址",
                        IsPrimaryButtonEnabled = true,
                        IsSecondaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "进入游戏后点击多人游戏，添加服务器地址为:" + s + " 以连接",
                        },

                    };
                    var resultw = await dialogw.ShowAsync();
                    if (resultw == ContentDialogResult.Primary)
                    {
                        Clipboard.SetDataObject(s);
                    }
                }
                catch (Exception ex)
                {
                    ContentDialog dialogw = new ContentDialog()
                    {
                        Title = "失..失败了?",
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = ex.Message + "\n可能是你的游戏昵称没有填写",
                        },

                    };
                    var resultw = await dialogw.ShowAsync();
                    if (resultw == ContentDialogResult.Primary)
                    {

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
                //File.Create(FileOnlineServer + @"\frpc.ini");
                Game.WritePrivateProfileString("common", "server_addr", after[1], FileOnlineServer + @"\frpc.ini");
                Game.WritePrivateProfileString("common", "server_port", "7000", FileOnlineServer + @"\frpc.ini");
                Game.WritePrivateProfileString("common", "user", after[0], FileOnlineServer + @"\frpc.ini");
                Game.WritePrivateProfileString("common", "meta_token", after[2], FileOnlineServer + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "server_name", after[0], FileOnlineServer + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "type", "stcp", FileOnlineServer + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "bind_addr", "127.0.0.1", FileOnlineServer + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "bind_port", "32423", FileOnlineServer + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "role", "visitor", FileOnlineServer + @"\frpc.ini");
                Game.WritePrivateProfileString(WWQ, "sk", "test", FileOnlineServer + @"\frpc.ini");
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
                //File.Delete(FileOnlineServer + @"\frpc.ini");
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
                        try
                        {
                            byte[] b = System.Text.Encoding.Default.GetBytes(aw);
                            Clipboard.SetDataObject(Convert.ToBase64String(b));
                        }
                        catch
                        {

                        }
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
        ///<summary>
        ///生成随机字符串
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                   s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
        private async void Button_Click_66(object sender, RoutedEventArgs e)
        {
            SquareMinecraftLauncher.Online.Server s = new SquareMinecraftLauncher.Online.Server();
                StackPanel panel = new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };
                panel.Children.Add(new TextBlock() { Text = "请输入你的游戏端口，以便进行联机" });
                TextBox box3 = new TextBox();
                TextBox box4 = new TextBox();
                CheckBox box6 = new CheckBox();
                box3.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "游戏端口:");
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
                    try
                    {
                        //SquareMinecraftLauncher.Online.Server.Type tb = new SquareMinecraftLauncher.Online.Server.Type();
                        bool x;
                        if (box6.IsChecked is true) { x = true; } else { x = false; }
                        string yqm = GetRnd(32, true, true, true, false, "@#$%&");
                        await s.ServerConnect("SDEVF3G28RGFEIQ3UFGR4389YRH3IR32G988GEIF328", box4.Text, "baibao Yes", SquareMinecraftLauncher.Online.Server.Type.Forge, int.Parse(box3.Text), SquareMinecraftLauncher.Online.Server.P2PType.Boring, yqm, "124.221.215.96",false);
                        s.Start();
                        ContentDialog dialogw = new ContentDialog()
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
                        var resultw = await dialogw.ShowAsync();
                        if (resultw == ContentDialogResult.Primary)
                        {
                            Clipboard.SetDataObject("我正在使用 FSM3 启动器进行 Minecraft 联机! 快来 FSM3 输入邀请码" + yqm + "来找我玩吧!");
                        }
                    }
                    catch (Exception ex)
                    {
                        ContentDialog dialogw = new ContentDialog()
                        {
                            Title = "遇到错误了",
                            PrimaryButtonText = "好吧",
                            IsPrimaryButtonEnabled = true,
                            DefaultButton = ContentDialogButton.Primary,
                            Content = new TextBlock()
                            {
                                TextWrapping = TextWrapping.WrapWithOverflow,
                                Text = ex.Message + "\n可能是你的游戏昵称没有填写，或者游戏版本没有填写",
                            },

                        };
                        var resultw = await dialogw.ShowAsync();
                    }
                }
        }

        private async void Button_Click_77(object sender, RoutedEventArgs e)
        {
            StackPanel panel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            panel.Children.Add(new TextBlock() { Text = "请输入你的邀请码，以便进行联机" });
            TextBox box = new TextBox();
            TextBox box1 = new TextBox();
            box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "邀请码:");
            panel.Children.Add(box);
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
                try
                {
                    SquareMinecraftLauncher.Online.Client c = new SquareMinecraftLauncher.Online.Client();
                    await c.ClientConnect(box.Text, SquareMinecraftLauncher.Online.Client.P2PType.Boring, "SDEVF3G28RGFEIQ3UFGR4389YRH3IR32G988GEIF328", DateTime.Now.ToLongTimeString());
                    string s = c.Start();
                    ContentDialog dialogw = new ContentDialog()
                    {
                        Title = "连接成功!",
                        PrimaryButtonText = "复制地址",
                        IsPrimaryButtonEnabled = true,
                        IsSecondaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "进入游戏后点击多人游戏，添加服务器地址为:"+ s +" 以连接",
                        },

                    };
                    var resultw = await dialogw.ShowAsync();
                    if (resultw == ContentDialogResult.Primary)
                    {
                        Clipboard.SetDataObject(s);
                    }
                }
                catch(Exception ex)
                {
                    ContentDialog dialogw = new ContentDialog()
                    {
                        Title = "失..失败了?",
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = ex.Message+"\n可能是你的游戏昵称没有填写",
                        },

                    };
                    var resultw = await dialogw.ShowAsync();
                    if (resultw == ContentDialogResult.Primary)
                    {

                    }
                }
            }
        }
        String[] OnlineHome = new String[5555555];
        private static string CmdPing(string strIp)

        {

            Process p = new Process(); p.StartInfo.FileName = "cmd.exe";//设定程序名

            p.StartInfo.UseShellExecute = false; //关闭Shell的使用

            p.StartInfo.RedirectStandardInput = true;//重定向标准输入

            p.StartInfo.RedirectStandardOutput = true;//重定向标准输出

            p.StartInfo.RedirectStandardError = true;//重定向错误输出

            p.StartInfo.CreateNoWindow = true;//设置不显示窗口

            string pingrst; p.Start(); p.StandardInput.WriteLine("ping " + strIp);

            p.StandardInput.WriteLine("exit");

            string strRst = p.StandardOutput.ReadToEnd();

            if (strRst.IndexOf("(0% loss)") != -1)

            {

                pingrst = "连接";

            }

            else if (strRst.IndexOf("Destination host unreachable.") != -1)

            {

                pingrst = "无法到达目的主机";

            }

            else if (strRst.IndexOf("Request timed out.") != -1)

            {

                pingrst = "超时";

            }

            else if (strRst.IndexOf("Unknown host") != -1)

            {

                pingrst = "无法解析主机";

            }

            else

            {

                pingrst = strRst;

            }

            p.Close();

            return pingrst;

        }

        private async void Load(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                string ipstr = "124.221.215.96";
                //接着实例化一个Ping的实例
                Ping p = new Ping();
                //设置Ping选项设置
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                //准备测试数据
                string data = "FSMNB";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                //设置超时时间
                int timeout = 120;
                //调用同步的send方法发送数据，将返回结果保存至PingReply实例
                PingReply reply = p.Send(ipstr, timeout, buffer, options);
                long time = 0;
                if (reply.Status == IPStatus.Success)
                {
                    string rpAddress = reply.Address.ToString();//响应主机的地址IP
                    time = reply.RoundtripTime;//响应时间
                    long lvtime = reply.Options.Ttl;//生存时间
                    bool iscon = reply.Options.DontFragment;//是否包含数据分段
                    string len = reply.Status.ToString();//缓冲区大小
                }
                MS.Content = time.ToString();
                await Task.Run(() =>
                {
                    Thread.Sleep(888);
                });
            }
        }
    }
}
