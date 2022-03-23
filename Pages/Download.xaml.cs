using FSM3.About_List;
using FSMLauncher_3;
using Gac;
using ModernWpf.Controls;
using SquareMinecraftLauncher;
using SquareMinecraftLauncher.Core.fabricmc;
using SquareMinecraftLauncher.Minecraft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FSM3.MainWindow;
using static FSMLauncher_3.McVersionList;

namespace FSM3.Pages
{
    /// <summary>
    /// Download.xaml 的交互逻辑
    /// </summary>
    public partial class Download : System.Windows.Controls.Page
    {
        public Download()
        {
            InitializeComponent();
            ContentDialog dialog = new ContentDialog()
            {
                Title = "提示",
                PrimaryButtonText = "好哒!",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock()
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Text = "FSM3的下载有一点小问题，将会在未来的迭代更新中修复，目前尽量别使用哦，谢谢您的支持!"
                },

            };
            var result = dialog.ShowAsync();
            if (IfDown is true)
            {
                SFYXZJC.Content = "正在下载中...";
                if (inforge == 1)
                {
                    user.FG.Visibility = Visibility.Visible;
                }
                else if (inopt == 1)
                {
                    user.OF.Visibility = Visibility.Visible;
                }
                else if (infab == 1)
                {
                    user.FB.Visibility = Visibility.Visible;
                }
                else
                {
                    user.MC.Visibility = Visibility.Visible;
                }
                user.AZBZ.Content = DownloadBZ;
                user.Visibility = Visibility.Visible;
            }
        }

        private void MCV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public short inforge;
        public short inopt;
        public short infab;
        public short inlite;
        TextBlock AZForgeV = new TextBlock();
        Storyboard storyboard = new Storyboard();//实例化故事板
        Storyboard storyboard1 = new Storyboard();//实例化故事板
        Storyboard storyboard2 = new Storyboard();//实例化故事板
        Storyboard storyboard3 = new Storyboard();//实例化故事板
        Storyboard storyboard4 = new Storyboard();//实例化故事板
        public async Task<string> Huandong()
        {
            #region 缓动动画*进
            //await Task.Run(() => { Thread.Sleep(100); });
            DoubleAnimation yd5 = new DoubleAnimation(110, 0, new Duration(TimeSpan.FromSeconds(0.88)));//浮点动画定义了开始值和起始值
            ForgeList.RenderTransform = new TranslateTransform();//在二维x-y坐标系统内平移(移动)对象
            yd5.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            // yd5.RepeatBehavior = RepeatBehavior.Forever;//设置循环播放
            Storyboard.SetTarget(yd5, ForgeList);//绑定动画为这个按钮执行的浮点动画
            Storyboard.SetTargetProperty(yd5, new PropertyPath("RenderTransform.Y"));//依赖的属性
            storyboard.Children.Add(yd5);//向故事板中加入此浮点动画
            storyboard.Begin();//播放此动画
            return "";
            #endregion
        }
        private async void Tile_Click_25(object sender, RoutedEventArgs e)
        {
            if (infab == 1 || inlite == 1)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "不能进行Forge安装",
                    PrimaryButtonText = "好吧",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = "Forge与Fabric，LiteLoader不兼容"
                    },
                };
                var result = await dialog.ShowAsync();
            }
            else
            {
                try
                {
                    ForgeList.Items.Clear();
                    ForgeList.SelectedIndex = -1;
                    MCVersionList[] mcvv = new MCVersionList[0];
                    AZForgeV.Text = mcVersionLists[MCV.SelectedIndex].version;
                    var a = await tools.Tools.GetForgeList(AZForgeV.Text);
                    // sort forge versions
                    foreach (var i in a)
                    {
                        ForgeList.Items.Add("" + i.ForgeVersion);
                    }
                    var b = await tools.Tools.GetMaxForge(AZForgeV.Text);
                    MaxForge.Content = "建议的Forge版本:" + b.ForgeVersion + " (点击选择)";
                    DTB.SelectedIndex = 3;

                    TranslateTransform tt = new TranslateTransform();
                    TranslateTransform aa = new TranslateTransform();
                    //创建一个一个对象，对两个值在时间线上进行动画处理（移动距离，移动到的位置）
                    DoubleAnimation da = new DoubleAnimation();
                    //设定动画时间线
                    Duration duration = new Duration(TimeSpan.FromSeconds(0.3));
                    //btnFlash要进行动画操作的控件名
                    ForgeList.RenderTransform = tt;
                    MaxForge.RenderTransform = aa;
                    //开始动画控件的初始位置，一般控件所在的位置是0位置
                    tt.X = 60;
                    aa.Y = -60;
                    //设定移动动画的结束值，控件向下移动60个像素，向上移动则是-60
                    da.To = 0;
                    da.Duration = duration;
                    //开始进行动画处理
                    await Huandong();
                    aa.BeginAnimation(TranslateTransform.YProperty, da);
                }
                catch (Exception ex)
                {
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = ex.Message,
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "出现错误"
                        },
                    };
                    var result = await dialog.ShowAsync();
                }
            }
        }
        String ForgeVer;
        String OptVer;
        String FabVer;
        String LiteVer;
        private async void Tile_Click_26(object sender, RoutedEventArgs e)
        {
            if (inforge == 1 || inopt == 1 || inlite == 1)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "不能进行Fabric安装",
                    PrimaryButtonText = "好吧",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = "Fabric与Forge，Optifine，LiteLoader不兼容"
                    },
                };
                var result = await dialog.ShowAsync();
            }
            else
            {
                try
                {
                    FabList.Items.Clear();
                    FabList.SelectedIndex = -1;


                    string bb = mcVersionLists[MCV.SelectedIndex].version;

                    string[] a = await fabricmc.FabricmcList(bb);
                    // sort forge versions


                    for(int i = 0; i < a.Length; ++i)
                    {
                        FabList.Items.Add(a[i]);
                    }

                    //MessageBox.Show(a[i]);
                    DTB.SelectedIndex = 5;
                    TranslateTransform tt = new TranslateTransform();
                    TranslateTransform aa = new TranslateTransform();
                    //创建一个一个对象，对两个值在时间线上进行动画处理（移动距离，移动到的位置）
                    DoubleAnimation da = new DoubleAnimation();
                    //设定动画时间线
                    Duration duration = new Duration(TimeSpan.FromSeconds(0.3));
                    //btnFlash要进行动画操作的控件名
                    FabList.RenderTransform = tt;
                    //MaxForge.RenderTransform = aa;
                    //开始动画控件的初始位置，一般控件所在的位置是0位置
                    tt.Y = -60;
                    //设定移动动画的结束值，控件向下移动60个像素，向上移动则是-60
                    da.To = 0;
                    da.Duration = duration;
                    //开始进行动画处理
                    //tt.BeginAnimation(TranslateTransform.YProperty, da);
                    DoubleAnimation yd5 = new DoubleAnimation(110, 0, new Duration(TimeSpan.FromSeconds(0.88)));//浮点动画定义了开始值和起始值
                    FabList.RenderTransform = new TranslateTransform();//在二维x-y坐标系统内平移(移动)对象
                    yd5.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
                    // yd5.RepeatBehavior = RepeatBehavior.Forever;//设置循环播放
                    Storyboard.SetTarget(yd5, FabList);//绑定动画为这个按钮执行的浮点动画
                    Storyboard.SetTargetProperty(yd5, new PropertyPath("RenderTransform.Y"));//依赖的属性
                    storyboard.Children.Add(yd5);//向故事板中加入此浮点动画
                    storyboard.Begin();//播放此动画
                    //aa.BeginAnimation(TranslateTransform.YProperty, da);
                }
                catch (Exception ex)
                {
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = ex.Message,
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "出现错误"
                        },
                    };
                    var result = await dialog.ShowAsync();
                }
            }
        }
        fabricmc fabricmc = new fabricmc();
        private async void Tile_Click_17(object sender, RoutedEventArgs e)
        {
            if (inlite == 1 || infab == 1)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "不能进行Optifine安装",
                    PrimaryButtonText = "好吧",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = "Optifine与Fabric，LiteLoader不兼容"
                    },
                };
                var result = await dialog.ShowAsync();
            }
            else
            {
                try
                {
                    OptifineList.Items.Clear();
                    MCVersionList[] mcvv = new MCVersionList[0];
                    OptifineList.SelectedIndex = -1;

                    string aw = mcVersionLists[MCV.SelectedIndex].version;
                    opp = await tools.Tools.GetOptiFineList(aw);
                    // sort forge versions


                    foreach (var i in opp)
                    {
                        OptifineList.Items.Add(i.filename);
                    }

                    DTB.SelectedIndex = 4;
                    TranslateTransform tt = new TranslateTransform();
                    TranslateTransform aa = new TranslateTransform();
                    //创建一个一个对象，对两个值在时间线上进行动画处理（移动距离，移动到的位置）
                    DoubleAnimation da = new DoubleAnimation();
                    //设定动画时间线
                    Duration duration = new Duration(TimeSpan.FromSeconds(0.3));
                    //btnFlash要进行动画操作的控件名
                    OptifineList.RenderTransform = tt;
                    //MaxForge.RenderTransform = aa;
                    //开始动画控件的初始位置，一般控件所在的位置是0位置
                    tt.Y = -60;
                    //设定移动动画的结束值，控件向下移动60个像素，向上移动则是-60
                    da.To = 0;
                    da.Duration = duration;
                    //开始进行动画处理
                    //tt.BeginAnimation(TranslateTransform.YProperty, da);
                    //aa.BeginAnimation(TranslateTransform.YProperty, da);
                    DoubleAnimation yd5 = new DoubleAnimation(110, 0, new Duration(TimeSpan.FromSeconds(0.88)));//浮点动画定义了开始值和起始值
                    OptifineList.RenderTransform = new TranslateTransform();//在二维x-y坐标系统内平移(移动)对象
                    yd5.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
                    // yd5.RepeatBehavior = RepeatBehavior.Forever;//设置循环播放
                    Storyboard.SetTarget(yd5, OptifineList);//绑定动画为这个按钮执行的浮点动画
                    Storyboard.SetTargetProperty(yd5, new PropertyPath("RenderTransform.Y"));//依赖的属性
                    storyboard.Children.Add(yd5);//向故事板中加入此浮点动画
                    storyboard.Begin();//播放此动画
                }
                catch (Exception ex)
                {
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = ex.Message,
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "出现错误"
                        },
                    };
                    var result = await dialog.ShowAsync();
                }
            }
        }
        OptiFineList[] opp = new OptiFineList[0];

        private void Tile_Click_X(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {

        }

        private void yxlx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        MCDownload[] downloads = new MCDownload[0];
        DownloadSource QJDown;
        internal static bool JarTimerBool = false;
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        static System.Windows.Threading.DispatcherTimer Jarw = new System.Windows.Threading.DispatcherTimer();
        static int JarID, JsonID;
        MinecraftDownload MinecraftDownload = new MinecraftDownload();
        public static Gac.DownLoadFile dlf = new DownLoadFile();
        public static int id = 0;
        internal int Downloadw(string path, string ly, string url)
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
                    Console.WriteLine("-----------------\n"+msg.ErrMessage+ "\n-----------------");

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
        public double liba;
        public void SendMsgHanderS(DownMsg msg)
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
                    Console.WriteLine("-----------------\n" + msg.ErrMessage + "\n-----------------");

                    return;
                }
                if (tag == DownStatus.DownLoad)
                {
                    DIYvar.xzItems[msg.Id].xzwz = "下载中";
                    DIYvar.xzItems[msg.Id].Template = msg.Progress;
                    liba = msg.Progress;
                    Console.WriteLine(msg.Progress);
                    Console.WriteLine("test");

                    return;
                }
            });
        }
        public static bool IfDown;
        public static int DownloadInt = 0;
        public static String DownloadBZ;
        string DownloadMCName;
        public static int bl;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StackPanel panel = new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };
                panel.Children.Add(new TextBlock() { Text = "显示的版本名字" });
                TextBox box = new TextBox();
                box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "后期可以更改,但不能跟其他版本重复");
                panel.Children.Add(box);

                ContentDialog dialogs = new ContentDialog()
                {
                    Title = "请输入安装版本名",
                    PrimaryButtonText = "OK!",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = panel,
                };
                var results = await dialogs.ShowAsync();
                if (results == ContentDialogResult.Primary)
                {
                    DownloadMCName = box.Text;
                    AllTheExistingVersion[] a = new AllTheExistingVersion[0];
                    a = tools.Tools.GetAllTheExistingVersion();
                    if (box.Text != "")
                    {
                        for(int i = 0; i < a.Length; i++)
                        {
                            if (a[i].path.Contains(box.Text))
                            {
                                bl = 1;
                            }
                        }
                    }
                }
                if (bl is 1)
                {
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "不能安装Minecraft",
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "你的版本名字重复啦！更换一个名字再试试吧！"
                        },

                    };
                    var result = await dialog.ShowAsync();
                    bl = 0;
                }
                else
                {
                    ForgeB.Content = "Forge(主流加载器)\n" + "当前未选择";
                    OptifineB.Content = "Optifine(高清修复)\n" + "当前未选择";
                    FabricB.Content = "Fabric(轻量加载器)\n" + "当前未选择";
                    LiteB.Content = "LiteLoader(轻量加载器)\n" + "当前未选择";
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "开始下载",
                        PrimaryButtonText = "好哒!",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "已经开始下载Minecraft啦!\n仅需亿杯咖啡的时间,即可体验游戏快乐!"
                        },

                    };
                    var result = await dialog.ShowAsync();
                    IfDown = true;
                    SFYXZJC.Content = "正在下载中...";
                    user.AZBZ.Content = "当前步骤:安装Jar";
                    DownloadBZ = "当前步骤:安装Jar";
                    DoF.Visibility = Visibility.Visible;
                    BDD.PDD.Visibility = Visibility.Visible;
                    DoubleAnimation yd5 = new DoubleAnimation(55, 0, new Duration(TimeSpan.FromSeconds(0.58)));//浮点动画定义了开始值和起始值
                    DoF.RenderTransform = new TranslateTransform();//在二维x-y坐标系统内平移(移动)对象
                    yd5.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
                    // yd5.RepeatBehavior = RepeatBehavior.Forever;//设置循环播放
                    Storyboard.SetTarget(yd5, DoF);//绑定动画为这个按钮执行的浮点动画
                    Storyboard.SetTargetProperty(yd5, new PropertyPath("RenderTransform.Y"));//依赖的属性
                    storyboard.Children.Add(yd5);//向故事板中加入此浮点动画
                    storyboard.Begin();//播放此动画
                    tools.Tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);
                    MCDownload download = MinecraftDownload.MCjarDownload(mcVersionLists[MCV.SelectedIndex].version);
                    if(Game.IniReadValue("Vlist", "Path") is "0")
                    {
                        JarID = Downloadw(Game.Dminecraft + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + ".jar", "", download.Url);
                    }
                    else
                    {
                        JarID = Downloadw(Game.IniReadValue("VPath", int.Parse(Game.IniReadValue("Vlist", "Path") + 2).ToString()) + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + ".jar", "", download.Url);
                    }
                    download = MinecraftDownload.MCjsonDownload(mcVersionLists[MCV.SelectedIndex].version);
                    if (Game.IniReadValue("Vlist", "Path") is "0")
                    {
                        JsonID = Downloadw(Game.Dminecraft + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + ".json", "", download.Url);
                    }
                    else
                    {
                        JsonID = Downloadw(Game.IniReadValue("VPath", int.Parse(Game.IniReadValue("Vlist", "Path") + 2).ToString()) + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + ".json", "", download.Url);
                    }
                    dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                    Jarw = Core5.timer(MCjarInstall, 5555);
                    Jarw.Start();
                    JarTimerBool = true;
                }
                ///以上是Asset补全
                //var loading = await this.ShowProgressAsync("提示", "正在补全中...");
                //loading.SetIndeterminate();
                //await lib(loading);
                //await loading.CloseAsync();

            }
            catch
            {

            }
        }
        MinecraftDownload mcd = new MinecraftDownload();
        MCDownload ddd;
        public async void OptifineI(object ob, EventArgs a)
        {
            if (DIYvar.xzItems[bbb].xzwz == "完成")
            {
                UPDATEW.Stop();
                UPDATEW.Stop();
                var j = tools.Tools.GetJavaPath();
                if (await tools.Tools.OptifineInstall(mcVersionLists[MCV.SelectedIndex].version, optpatch, j[0].Path))
                {
                    AssetDownload assetDownload = new AssetDownload();//asset下载类
                    assetDownload.DownloadProgressChanged += AssetDownload_DownloadProgressChanged;//事件

                    await libraries(mcVersionLists[MCV.SelectedIndex].version);

                    await assetDownload.BuildAssetDownload(1000, mcVersionLists[MCV.SelectedIndex].version);//构建下载
                }

            }
        }
        static System.Windows.Threading.DispatcherTimer UPDATEW = new System.Windows.Threading.DispatcherTimer();
        public async void ForgeI(object ob, EventArgs a)
        {
            if (DIYvar.xzItems[bbb].xzwz == "完成")
            {
                UPDATEW.Stop();
                var j = tools.Tools.GetJavaPath();
                await tools.Tools.ForgeInstallation(ddd.path, DownloadMCName, j[0].Path);
                //   await Lod.CloseAsync();
                AssetDownload assetDownload = new AssetDownload();//asset下载类
                assetDownload.DownloadProgressChanged += AssetDownload_DownloadProgressChanged;//事件

                await libraries(DownloadMCName);

                await assetDownload.BuildAssetDownload(1000, DownloadMCName);//构建下载
            }
        }
        public async void OptifineandForgeI(object ob, EventArgs a)
        {
            if (DIYvar.xzItems[bbb].xzwz == "完成")
            {
                UPDATEW.Stop();
                user.AZBZ.Content = "当前步骤:安装Optifine";
                DownloadBZ = "当前步骤:安装Optifine";
                var j = tools.Tools.GetJavaPath();
                await tools.Tools.ForgeInstallation(ddd.path, DownloadMCName, j[0].Path);
                ddd = mcd.DownloadOptifine(DownloadMCName, OptVer);
                if (Game.IniReadValue("Vlist", "Path") is "0")
                {
                    Directory.CreateDirectory(Game.Dminecraft + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + @"\mods");
                    bbb = Downloadw(Game.Dminecraft + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + @"\mods\" + OptVer + "", "Optifine", ddd.Url);
                }
                else
                {
                    Directory.CreateDirectory(Game.IniReadValue("VPath", int.Parse(Game.IniReadValue("Vlist", "Path") + 2).ToString()) + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + @"\mods");
                    bbb = Downloadw(Game.IniReadValue("VPath", int.Parse(Game.IniReadValue("Vlist", "Path") + 2).ToString()) + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + @"\mods\" + OptVer + "", "Optifine", ddd.Url);
                }
                //bbb = Downloadw(Game.IniReadValue("VPath", int.Parse(Game.IniReadValue("Vlist", "Path") + 1).ToString()) + @"\mods\" + OptVer + "", "Optifine", ddd.Url);
                /// await Lod.CloseAsync();
                UPDATEW = Core5.timer(Cree, 5555);
                UPDATEW.Start();
            }
        }
        public async void Cree(object ob, EventArgs a)
        {
            if (DIYvar.xzItems[bbb].xzwz == "完成")
            {
                UPDATEW.Stop();
                AssetDownload assetDownload = new AssetDownload();//asset下载类
                assetDownload.DownloadProgressChanged += AssetDownload_DownloadProgressChanged;//事件

                await libraries(DownloadMCName);

                await assetDownload.BuildAssetDownload(1000, DownloadMCName);//构建下载
            }
        }
        private async void MCjarInstall(object ob, EventArgs a)
        {
            //MessageBox.Show(DIYvar.xzItems[JsonID].xzwz);
            string aa = DIYvar.xzItems[JarID].xzwz;
            string bb = DIYvar.xzItems[JsonID].xzwz;

            if (aa == "完成" && bb == "完成")
            {

                DoF.InstallJar.IsChecked = true;
                JarTimerBool = false;
                Jarw.Stop();
                try
                {
                    if (inforge == 1)
                    {
                        if (inopt == 1)
                        {
                            Jarw.Stop();
                            ///Lod.SetIndeterminate();
                            //安装optifine和Forge
                            user.AZBZ.Content = "当前步骤:安装Forge";
                            DownloadBZ = "当前步骤:安装Forge";
                            ddd = mcd.ForgeDownload(DownloadMCName, ForgeVer);

                            bbb = Downloadw(ddd.path, "Forge", ddd.Url);
                            UPDATEW = Core5.timer(OptifineandForgeI, 5555);
                            UPDATEW.Start();


                        }
                        else
                        {
                            Jarw.Stop();
                            //安装Forge
                            ///   Lod.SetIndeterminate();
                            ddd = mcd.ForgeDownload(DownloadMCName, ForgeVer);
                            user.AZBZ.Content = "当前步骤:安装Forge";
                            DownloadBZ = "当前步骤:安装Forge";
                            await tools.Tools.ForgeInstallation(ddd.path, DownloadMCName, Settings.Java_List);
                            bbb = Downloadw(ddd.path, "Forge", ddd.Url);
                            UPDATEW = Core5.timer(ForgeI, 5555);
                            UPDATEW.Start();

                        }
                    }
                    else if (infab == 1)
                    {
                        Jarw.Stop();
                        //安装Fabric
                        user.AZBZ.Content = "当前步骤:安装Fabric";
                        DownloadBZ = "当前步骤:安装Fabric";
                        await fabricmc.FabricmcVersionInstall(DownloadMCName, FabVer);
                        AssetDownload assetDownload = new AssetDownload();//asset下载类
                        assetDownload.DownloadProgressChanged += AssetDownload_DownloadProgressChanged;//事件

                        await libraries(DownloadMCName);

                        await assetDownload.BuildAssetDownload(1000, DownloadMCName);//构建下载
                    }
                    else if (inlite == 1)
                    {
                        Jarw.Stop();
                        //安装LiteLoader
                    }
                    else if (inopt == 1)
                    {
                        //安装Optifine
                        Jarw.Stop();

                        // dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                        //ddd = mcd.DownloadOptifine(mcVersionLists[MCV.SelectedIndex].version, OptVer);
                        //bbb = Download(ddd.path,"Optifine", ddd.Url);
                        //UPDATEW = Core5.timer(OptifineI, 5555);
                        //UPDATEW.Start();
                        //OptiFineList[] opt = new OptiFineList[0];

                        //ddd = mcd.DownloadOptifine(mcVersionLists[MCV.SelectedIndex].version,OptifineList.SelectedItem.ToString());
                        //bbb = Download(System.AppDomain.CurrentDomain.BaseDirectory + @"SquareMinecraftLauncher\OptiFine\"+ OptifineList.SelectedItem.ToString(), "Optifine", ddd.Url);
                        //UPDATEW = Core5.timer(OptifineI, 5555);
                        //UPDATEW.Start();
                        user.AZBZ.Content = "当前步骤:安装Optifine";
                        var j = tools.Tools.GetJavaPath();
                        if (await tools.Tools.OptifineInstall(DownloadMCName, optpatch, j[0].Path))
                        {
                            AssetDownload assetDownload = new AssetDownload();//asset下载类
                            assetDownload.DownloadProgressChanged += AssetDownload_DownloadProgressChanged;//事件

                            await libraries(DownloadMCName);

                            await assetDownload.BuildAssetDownload(1000, DownloadMCName);//构建下载
                        }
                    }
                    else
                    {
                        Jarw.Stop();
                        //MessageBox.Show("1");
                        AssetDownload assetDownload = new AssetDownload();//asset下载类
                        assetDownload.DownloadProgressChanged += AssetDownload_DownloadProgressChanged;//事件

                        await libraries(DownloadMCName);

                        await assetDownload.BuildAssetDownload(1000, DownloadMCName);//构建下载
                    }
                    //tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);


                }
                catch (Exception ex)
                {
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = ex.Message,
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "出现错误"
                        },
                    };
                    var result = await dialog.ShowAsync();
                }

            }


        }
        public async Task<bool> libraries(string version)
        {

            try
            {
                //  tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);
                dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHanderS);
                //libraries1 = mcVersionLists[MCV.SelectedIndex].version;
                MCDownload[] File = tools.Tools.GetMissingFile(version);
                if (File.Length != 0)
                {
                    foreach (var i in File)
                    {
                        int aa = Downloadw(i.path, "补全", i.Url);
                        user.AZBZ.Content = "当前步骤:补全游戏文件";
                        DownloadBZ = "当前步骤:补全游戏文件";
                    }
                    //libraries2 = sz.id;
                    return false;
                }


            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = ex.Message,
                    PrimaryButtonText = "好吧",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = "出现错误"
                    },
                };
                var result = await dialog.ShowAsync();
            }
            return true;

        }
        int bbb;
        ContentDialog DG = new ContentDialog()
        {
            Title = "下载完成",
            PrimaryButtonText = "好哒!",
            IsPrimaryButtonEnabled = true,
            DefaultButton = ContentDialogButton.Primary,
            Content = new TextBlock()
            {
                TextWrapping = TextWrapping.WrapWithOverflow,
                Text = "开始您的游戏吧!"
            },

        };
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="Log"></param>
        public void AssetDownload_DownloadProgressChanged(AssetDownload.DownloadIntermation Log)
        {
            // MessageBox.Show("2");
            Console.WriteLine(Log.Progress + "  " + Log.Speed);
            this.Dispatcher.Invoke(new Action(delegate {DoF.InstallLib.IsChecked = true; }));
            this.Dispatcher.Invoke(new Action(delegate { DoF.ZJD.Value = Log.Progress; }));
            if (Log.Progress == 100)
            { 
                this.Dispatcher.Invoke(new Action(delegate { DG.ShowAsync(); }));
                this.Dispatcher.Invoke(new Action(delegate { DoF.InstallAssets.IsChecked = true; }));
                this.Dispatcher.Invoke(new Action(delegate { WC(); }));
                this.Dispatcher.Invoke(new Action(delegate { BDD.PDD.Visibility = Visibility.Hidden; }));
            }
        }
        public void WC()
        {
            DoubleAnimation yd5 = new DoubleAnimation(0, 55, new Duration(TimeSpan.FromSeconds(0.58)));//浮点动画定义了开始值和起始值
            DoF.RenderTransform = new TranslateTransform();//在二维x-y坐标系统内平移(移动)对象
            yd5.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            // yd5.RepeatBehavior = RepeatBehavior.Forever;//设置循环播放
            Storyboard.SetTarget(yd5, DoF);//绑定动画为这个按钮执行的浮点动画
            Storyboard.SetTargetProperty(yd5, new PropertyPath("RenderTransform.Y"));//依赖的属性
            storyboard.Children.Add(yd5);//向故事板中加入此浮点动画
            storyboard.Begin();//播放此动画
            DoF.Visibility = Visibility.Hidden;
        }
        private async void Load(object sender, RoutedEventArgs e)
        {
            MCVersionList[] mc = new MCVersionList[0];
            try
            {
                mc = await tools.Tools.GetMCVersionList();
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "未获取到游戏下载列表，请重试",
                    PrimaryButtonText = "重试",
                    CloseButtonText = "取消",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = "请检查网络，或重试一遍\n" + ex.Message
                    },

                };
                await dialog.ShowAsync();
            }
            foreach (var i in mc)
            {
                switch (i.type)
                {
                    case "正式版":
                        McVersionList a = new McVersionList(i.id, i.type,i.releaseTime);
                        FSMLauncher_3.DIYvar.minecraft1.Add(a);
                        break;
                    case "快照版":
                        McVersionList b = new McVersionList(i.id, i.type, i.releaseTime);
                        FSMLauncher_3.DIYvar.minecraft2.Add(b);
                        break;
                    case "基岩版":
                        McVersionList c = new McVersionList(i.id, "早期测试", i.releaseTime);
                        FSMLauncher_3.DIYvar.minecraft3.Add(c);
                        break;
                    case "远古版":
                        McVersionList d = new McVersionList(i.id, i.type, i.releaseTime);
                        FSMLauncher_3.DIYvar.minecraft4.Add(d);
                        break;
                }
                var DT = DateTime.Parse(i.releaseTime);
                if (DT.ToString("MM-dd") == "04-01")
                {
                    McVersionList s = new McVersionList(i.id, "愚人节版本", i.releaseTime);
                    FSMLauncher_3.DIYvar.minecraft5.Add(s);
                }

            }
            mcVersionLists = FSMLauncher_3.DIYvar.minecraft1.ToArray();
            List<Item> item1 = new List<Item>();


            for (int i = 0; i < mcVersionLists.Length; i++)
            {
                Item item = new Item();
                item.Dver.Text = mcVersionLists[i].version;
                item.TIME.Text = mcVersionLists[i].tsw;
                item1.Add(item);
            }
            //DIYvar.l = item1;
            MCV.ItemsSource = item1.ToArray();
        }
        internal static McVersionList[] mcVersionLists = new McVersionList[0];

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                if (yxlx.SelectedIndex != -1)
                {
                    switch (yxlx.SelectedIndex)
                    {
                        case 0:

                            mcVersionLists = DIYvar.minecraft1.ToArray();
                            List<Item> item1 = new List<Item>();


                            for (int i = 0; i < mcVersionLists.Length; i++)
                            {
                                Item item = new Item();
                                item.Dver.Text = mcVersionLists[i].version;
                                item.TIME.Text = mcVersionLists[i].tsw;
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E885BB0.PNG", UriKind.RelativeOrAbsolute);
                                item.dbb.Text = "正式版";
                                bi.EndInit();
                                item.Dimage.Source = bi;
                                item1.Add(item);
                            }
                            DIYvar.l = item1;
                            MCV.ItemsSource = item1.ToArray();
                            break;
                        case 1:
                            mcVersionLists = DIYvar.minecraft2.ToArray();
                            List<Item> item11 = new List<Item>();


                            for (int i = 0; i < mcVersionLists.Length; i++)
                            {
                                Item item = new Item();
                                item.Dver.Text = mcVersionLists[i].version;
                                item.TIME.Text = mcVersionLists[i].tsw;
                                item.Dimage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/0E8348D8.PNG"));
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E8348D8.PNG", UriKind.RelativeOrAbsolute);
                                item.dbb.Text = "快照版";
                                bi.EndInit();
                                item.Dimage.Source = bi;
                                item11.Add(item);
                            }
                            DIYvar.l = item11;
                            MCV.ItemsSource = item11.ToArray();
                            break;
                        case 2:
                            mcVersionLists = DIYvar.minecraft3.ToArray();
                            List<Item> item111 = new List<Item>();


                            for (int i = 0; i < mcVersionLists.Length; i++)
                            {
                                Item item = new Item();
                                item.Dver.Text = mcVersionLists[i].version;
                                item.TIME.Text = mcVersionLists[i].tsw;
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E8DB058.PNG", UriKind.RelativeOrAbsolute);
                                item.dbb.Text = "早期测试";
                                bi.EndInit();
                                item.Dimage.Source = bi;
                                //item.Dimage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/0E8DB058.PNG"));
                                item111.Add(item);
                            }
                            DIYvar.l = item111;
                            MCV.ItemsSource = item111.ToArray();
                            break;
                        case 3:
                            mcVersionLists = DIYvar.minecraft4.ToArray();
                            List<Item> item1111 = new List<Item>();


                            for (int i = 0; i < mcVersionLists.Length; i++)
                            {
                                Item item = new Item();
                                item.Dver.Text = mcVersionLists[i].version;
                                item.TIME.Text = mcVersionLists[i].tsw;
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E830D70.PNG", UriKind.RelativeOrAbsolute);
                                item.dbb.Text = "远古版";
                                bi.EndInit();
                                item.Dimage.Source = bi;
                                //item.Dimage.Source = new BitmapImage(new Uri("pack://application:,,,/Image/0E830D70.PNG"));
                                item1111.Add(item);
                            }
                            DIYvar.l = item1111;
                            MCV.ItemsSource = item1111.ToArray();
                            break;
                        case 4:
                            mcVersionLists = DIYvar.minecraft5.ToArray();
                            List<Item> item11111 = new List<Item>();


                            for (int i = 0; i < mcVersionLists.Length; i++)
                            {
                                Item item = new Item();
                                item.Dver.Text = mcVersionLists[i].version;
                                item.TIME.Text = mcVersionLists[i].tsw;
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E826620.PNG", UriKind.RelativeOrAbsolute);
                                item.dbb.Text = "愚人节版";
                                bi.EndInit();
                                item.Dimage.Source = bi;
                                item11111.Add(item);
                            }
                            DIYvar.l = item11111;
                            MCV.ItemsSource = item11111.ToArray();
                            break;
                    }
                    if (MCV != null)
                    {
                        List<Item> item1 = new List<Item>();


                        for (int i = 0; i < mcVersionLists.Length; i++)
                        {
                            Item item = new Item();
                            item.Dver.Text = mcVersionLists[i].version;
                            item.TIME.Text = mcVersionLists[i].tsw;
                            BitmapImage bi = new BitmapImage();
                            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                            bi.BeginInit();
                            //bi.UriSource = new Uri(@"\Image\0E885BB0.PNG", UriKind.RelativeOrAbsolute);
                            bi.EndInit();
                            //item.Dimage.Source = bi;
                            item1.Add(item);
                        }
                        DIYvar.l = item1;
                        MCV.ItemsSource = item1.ToArray();
                        //MCV.ItemsSource = ItemAdd(mcVersionLists);
                    }


                }

            }
            catch
            {

            }
        }

        private async void MaxForge_Click(object sender, RoutedEventArgs e)
        {
            DTB.SelectedIndex = 0;
            var b = await tools.Tools.GetMaxForge(AZForgeV.Text);
            inforge = 1;
            ForgeVer = b.ForgeVersion;
            ForgeB.Content = "Forge\n" + "Forge - " + b.ForgeVersion;
        }

        private void ForgeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DTB.SelectedIndex = 0;
            ForgeB.Content = "Forge\n" + ForgeList.SelectedItem;
            ForgeVer = ForgeList.SelectedItem.ToString();
            inforge = 1;
        }
        String optpatch;
        private void OptifineList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DTB.SelectedIndex = 0;
            //OptiFineList[] op = new OptiFineList[0];
            OptifineB.Content = "Optifine\n" + OptifineList.SelectedItem;
            OptVer = OptifineList.SelectedItem.ToString();
            optpatch = opp[OptifineList.SelectedIndex].patch;
            inopt = 1;
        }

        private void XZJC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void FabList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DTB.SelectedIndex = 0;
            FabricB.Content = "Fabric\n" + FabList.SelectedItem;
            FabVer = FabList.SelectedItem.ToString();
            infab = 1;
        }
    }
}
