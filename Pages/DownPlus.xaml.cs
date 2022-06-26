using FSM3.cs;
using FSM3.FSMCore.Download.FSMCoreDownload;
using ModernWpf.Controls;
using SquareMinecraftLauncher;
using SquareMinecraftLauncher.Minecraft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    /// DownPlus.xaml 的交互逻辑
    /// </summary>
    public partial class DownPlus : System.Windows.Controls.Page
    {
        public DownPlus()
        {
            InitializeComponent();
            box.Text = Var.DownVar;
        }

        private void Tile_Click_X(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_26(object sender, RoutedEventArgs e)
        {

        }
        public static bool booljar,booljson;
        string DownloadMCName;
        static int JarID, JsonID;
        MinecraftDownload MinecraftDownload = new MinecraftDownload();
        DownloadFile dlf = new DownloadFile();
        internal void Downloadw(string path, string url)
        {
            dlf.Start(path, url);//增加下载
        }
        short pd;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DownloadMCName = box.Text; //设置版本名
            Var.ZVar = box.Text;
            AllTheExistingVersion[] a = new AllTheExistingVersion[0];
            a = tools.Tools.GetAllTheExistingVersion();
            if (box.Text != "")
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i].path.Contains(box.Text) || box.Text is "")
                    {
                        ContentDialog dialog = new ContentDialog()
                        {
                            Title = "不能安装 Minecraft",
                            PrimaryButtonText = "好吧",
                            IsPrimaryButtonEnabled = true,
                            DefaultButton = ContentDialogButton.Primary,
                            Content = new TextBlock()
                            {
                                TextWrapping = TextWrapping.WrapWithOverflow,
                                Text = "你的版本名字重复了或你没写版本名字哦",
                            },

                        };
                        var result = await dialog.ShowAsync();
                    }
                    else //开始安装
                    {
                        pd = 999;
                    }
                }
                if (pd == 999 || a.Length is 0)
                {
                    try
                    {
                        switch (Game.IniReadValue("DownloadSC", "type"))
                        {
                            case "MCBBS":
                                tools.Tools.DownloadSourceInitialization(DownloadSource.MCBBSSource); //设置下载源
                                break;
                            case "BMCLAPI":
                                tools.Tools.DownloadSourceInitialization(DownloadSource.bmclapiSource); //设置下载源
                                break;
                            case "Minecraft":
                                tools.Tools.DownloadSourceInitialization(DownloadSource.MinecraftSource); //设置下载源
                                break;
                            case null:
                                tools.Tools.DownloadSourceInitialization(DownloadSource.MCBBSSource); //设置下载源
                                break;
                        }
                        DownloadCore.Restart();
                        string VarID = Var.DownVar;
                        // 设置不能退出界面 >
                        BDD.PDD.Visibility = Visibility.Visible;
                        DownB.IsEnabled = false;
                        //到这里设置完成 <
                        AZJD.Content = "当前:安装游戏核心文件"; //设置过程
                        booljar = false; booljson = false;
                        ZJDw.IsIndeterminate = true;
                        ZJD.IsIndeterminate = true;
                        WebClient web = new WebClient();
                        MCDownload download = MinecraftDownload.MCjarDownload(VarID); //获取MCDownload数据
                                                                                      //下载Jar和Json(开始下载)
                        if (Game.IniReadValue("Vlist", "Path") == "" || Game.IniReadValue("Vlist", "Path") == "0")
                        {
                            Directory.CreateDirectory(Game.Dminecraft + @"\versions\" + DownloadMCName);
                            await web.DownloadFileTaskAsync(download.Url, Game.Dminecraft + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + ".jar");
                            tools.Tools.SetMinecraftFilesPath(Game.Dminecraft);
                            booljar = true;
                            download = MinecraftDownload.MCjsonDownload(VarID);
                            await web.DownloadFileTaskAsync(download.Url, Game.Dminecraft + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + ".json");
                            tools.Tools.SetMinecraftFilesPath(Game.Dminecraft);
                            booljson = true;
                        }
                        else
                        {
                            string ab = Game.IniReadValue("Vlist", "Path");
                            int s = int.Parse(ab);
                            int ss = s + 1;
                            Directory.CreateDirectory(Game.IniReadValue("VPath", ss.ToString()) + @"\versions\" + DownloadMCName);
                            await web.DownloadFileTaskAsync(download.Url,Game.IniReadValue("VPath", ss.ToString()) + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + ".jar");
                            booljar = true;
                            string ax = Game.IniReadValue("Vlist", "Path");
                            download = MinecraftDownload.MCjsonDownload(VarID);
                            await web.DownloadFileTaskAsync(download.Url, Game.IniReadValue("VPath", ss.ToString()) + @"\versions\" + DownloadMCName + @"\" + DownloadMCName + ".json");
                            booljson = true;
                        }
                        Jarw = Core5.timer(MCjarInstall, 500); //实时判断是否下载完成
                        Jarw.Start();
                    }
                    catch(Exception ex)
                    {
                        this.Dispatcher.Invoke(new Action(delegate { BDD.PDD.Visibility = Visibility.Hidden; }));
                        this.Dispatcher.Invoke(new Action(delegate
                        {
                            DownB.IsEnabled = false;
                        }));
                        ContentDialog dialog = new ContentDialog()
                        {
                            Title = "已停止下载",
                            PrimaryButtonText = "好吧",
                            IsPrimaryButtonEnabled = true,
                            DefaultButton = ContentDialogButton.Primary,
                            Content = new TextBlock()
                            {
                                TextWrapping = TextWrapping.WrapWithOverflow,
                                Text = "遇到错误了，报错信息如下\n" + ex.Message+ex.Data,
                            },
                        };
                        var result = await dialog.ShowAsync();
                    }
                }
            }
        }
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        static System.Windows.Threading.DispatcherTimer Jarw = new System.Windows.Threading.DispatcherTimer();
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
            this.Dispatcher.Invoke(new Action(delegate { ZJD.Value = Log.Progress; }));
            if (Log.Progress == 100)
            {
                this.Dispatcher.Invoke(async()=>
                {
                    await DG.ShowAsync();
                    BDD.PDD.Visibility = Visibility.Hidden;
                    DownB.IsEnabled = false;
                });
            }
        }
        public async Task<bool> libraries(string version)
        {
            try
            {
                //  tools.DownloadSourceInitialization(DownloadSource.MCBBSSource);
                //libraries1 = mcVersionLists[MCV.SelectedIndex].version;
                MCDownload[] File = tools.Tools.GetMissingFile(version);
                if (File.Length != 0)
                {
                    foreach (var i in File)
                    {
                        Downloadw(i.path, i.Url);
                    }
                    ZJDw.Maximum = File.Length;
                    bool result = false;
                    await Task.Run(() =>
                    {
                        while (true)
                        {
                            this.Dispatcher.Invoke(new Action(delegate
                            {
                                ZJDw.Value = File.Length - dlf.Left;
                            }));
                            if (dlf.EndDownload())
                            {
                                if (dlf.error > 0) result = false;
                                else result = true;
                                break;
                            }
                        }
                    });
                    //libraries2 = sz.id;
                    if (result)
                    {
                        MCDownload[] Filex = tools.Tools.GetAllTheAsset(version);
                        if (Filex.Length is 0)
                        {
                            this.Dispatcher.Invoke(async() => {
                                await DG.ShowAsync();BDD.
                                PDD.Visibility = Visibility.
                                Hidden;DownB.IsEnabled = false;
                            });

                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                if(tools.Tools.GetMissingAsset(DownloadMCName).Length != 0)
                {
                    AssetDownload assetDownload = new AssetDownload();//asset下载类
                    await assetDownload.BuildAssetDownload(1000, DownloadMCName);//构建下载
                }
                else
                {
                    this.Dispatcher.Invoke(new Action(delegate { BDD.PDD.Visibility = Visibility.Hidden; }));
                    this.Dispatcher.Invoke(new Action(delegate
                    {
                        DownB.IsEnabled = false;
                    }));
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "安装完成",
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "打开游戏吧\n"+ex.Message,
                        },
                    };
                    var result = await dialog.ShowAsync();
                }
            }
            return true;

        }
        List<string> op = new List<string>(); 
        SquareMinecraftLauncher.Core.fabricmc.fabricmc fab = new SquareMinecraftLauncher.Core.fabricmc.fabricmc();
        private async void Load(object sender, RoutedEventArgs e)
        {
            dlf.Thread(4);
            BarForge.IsActive = true;
            BarFabric.IsActive = true;
            BarOptifine.IsActive = true;
            try
            {
                var a = await tools.Tools.GetForgeList(Var.DownVar);
                // sort forge versions
                var b = await tools.Tools.GetMaxForge(Var.DownVar);
                ForgeList.Items.Add(b.ForgeVersion);
                foreach (var i in a)
                {
                    ForgeList.Items.Add(i.ForgeVersion);
                }
                BarForge.IsActive = false;
            }
            catch
            {
                BarForge.IsActive = false;
            }
            try
            {
                var c = await fab.FabricmcList(Var.DownVar);
                foreach (var i in c)
                {
                    FabricList.Items.Add(i);
                }
                BarFabric.IsActive = false;
            }
            catch
            {
                BarFabric.IsActive = false;
            }
            try
            {
                var d = await tools.Tools.GetOptiFineList(Var.DownVar);
                foreach (var i in d)
                {
                    OptifineList.Items.Add(i.filename);
                    op.Add(i.patch);
                }
                BarOptifine.IsActive = false;
            }
            catch
            {
                BarOptifine.IsActive = false;
            }
        }

        private void OptifineList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Var.optpatch = op[OptifineList.SelectedIndex - 1];
            }
            catch
            {

            }
        }

        private void FabricList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Var.FabVar = FabricList.Text;
            try
            {

            }
            catch
            {

            }
        }

        private void ForgeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Var.ForgeVar = ForgeList.Text;
            try
            {

            }
            catch
            {

            }
        }
        MCDownload mcd = new MCDownload();
        SquareMinecraftLauncher.MinecraftDownload down = new MinecraftDownload();

        private void OptifineDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void FabricDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void ForgeDown(object sender, MouseButtonEventArgs e)
        {
        }

        public async void MCjarInstall(object ob, EventArgs a)
        {
            //下载Jar和Json(下载完成)
            Jarw.Stop();
            await Task.Run(() =>
            {
                while (true)
                {
                    if (booljar&&booljson)
                    {
                        break;
                    }
                }
            });
            try
                {
                    if (ForgeList.Text is "不安装" && FabricList.Text is "不安装" && OptifineList.Text is "不安装")
                    {
                        //不安装任何扩展
                        AZJD.Content = "当前:安装游戏运行库"; //设置过程
                        ZJDw.IsIndeterminate = false;
                        ZJD.IsIndeterminate = false;
                        MCDownload[] cc = tools.Tools.GetMissingFile(DownloadMCName);
                        ZJDw.Maximum = cc.Length;
                        FSMCore.Download.FSMCoreDownload.DownloadCore.Download(cc);
                        await Task.Run(() =>
                        {
                            while (true)
                            {
                                if (DownProgress.ProX + DownProgress.Error != cc.Length)
                                {
                                    this.Dispatcher.Invoke(new Action(delegate
                                    {
                                        ZJDw.Value = DownProgress.ProX+DownProgress.Error;
                                    }));
                                }
                                else
                                {
                                    break;
                                }
                            }
                        });
                    AZJD.Content = "当前:安装游戏资源文件"; //设置过程
                    ZJDw.IsIndeterminate = false;
                    ZJD.IsIndeterminate = false;
                    MCDownload[] ccx = tools.Tools.GetMissingAsset(DownloadMCName);
                    ZJD.Maximum = ccx.Length - 2;
                    if (ccx.Length is 0)
                    {
                        await DG.ShowAsync();
                        BDD.PDD.Visibility = Visibility.Hidden;
                        DownB.IsEnabled = false;
                        AZJD.Content = "当前已完成"; //设置过程
                        ZJD.Value = ccx.Length - 2;
                    }
                    else
                    {
                        await Task.Run(() => { DownloaderAssets.DownloadCool(ccx); });
                        await Task.Run(() =>
                        {
                            while (true)
                            {
                                if (DownloaderAssets.ProgressX + DownloaderAssets.FailedX < ccx.Length - 2
                                )
                                {
                                    this.Dispatcher.Invoke(new Action(delegate
                                    {
                                        ZJD.Value = DownloaderAssets.ProgressX + DownloaderAssets.FailedX;
                                    }));
                                }
                                else
                                {
                                    this.Dispatcher.Invoke(new Action(delegate
                                    {
                                        DG.ShowAsync();
                                        BDD.PDD.Visibility = Visibility.Hidden;
                                        DownB.IsEnabled = false;
                                        AZJD.Content = "当前已完成"; //设置过程
                                        ZJDw.Value = cc.Length;
                                    }));
                                    break;
                                }
                            }
                        });
                    }
                    DG.ShowAsync();
                        BDD.PDD.Visibility = Visibility.Hidden;
                        DownB.IsEnabled = false;
                        AZJD.Content = "当前已完成"; //设置过程
                        ZJDw.Value = cc.Length;
                    }
                    if (ForgeList.Text is "不安装" && OptifineList.Text != "不安装" && FabricList.Text is "不安装")
                    { 
                        //只安装Optifine
                        AZJD.Content = "当前:安装高清修复"; //设置过程
                        ZJDw.IsIndeterminate = true;
                        ZJD.IsIndeterminate = true;
                        if (await VarDownload.Optifine())
                        {
                        AZJD.Content = "当前:安装游戏运行库"; //设置过程
                        ZJDw.IsIndeterminate = false;
                        ZJD.IsIndeterminate = false;
                        MCDownload[] cc = tools.Tools.GetMissingFile(DownloadMCName);
                        ZJDw.Maximum = cc.Length;
                        FSMCore.Download.FSMCoreDownload.DownloadCore.Download(cc);
                        await Task.Run(() =>
                        {
                            while (true)
                            {
                                if (DownProgress.ProX + DownProgress.Error != cc.Length)
                                {
                                    this.Dispatcher.Invoke(new Action(delegate
                                    {
                                        ZJDw.Value = DownProgress.ProX + DownProgress.Error;
                                    }));
                                }
                                else
                                {
                                    break;
                                }
                            }
                        });
                        AZJD.Content = "当前:安装游戏资源文件"; //设置过程
                        ZJDw.IsIndeterminate = false;
                        ZJD.IsIndeterminate = false;
                        MCDownload[] ccx = tools.Tools.GetMissingAsset(DownloadMCName);
                        ZJD.Maximum = ccx.Length - 2;
                        if (ccx.Length is 0)
                        {
                            await DG.ShowAsync();
                            BDD.PDD.Visibility = Visibility.Hidden;
                            DownB.IsEnabled = false;
                            AZJD.Content = "当前已完成"; //设置过程
                            ZJD.Value = ccx.Length - 2;
                        }
                        else
                        {
                            await Task.Run(() => { DownloaderAssets.DownloadCool(ccx); });
                            await Task.Run(() =>
                            {
                                while (true)
                                {
                                    if (DownloaderAssets.ProgressX + DownloaderAssets.FailedX < ccx.Length - 2
                                    )
                                    {
                                        this.Dispatcher.Invoke(new Action(delegate
                                        {
                                            ZJD.Value = DownloaderAssets.ProgressX + DownloaderAssets.FailedX;
                                        }));
                                    }
                                    else
                                    {
                                        this.Dispatcher.Invoke(new Action(delegate
                                        {
                                            DG.ShowAsync();
                                            BDD.PDD.Visibility = Visibility.Hidden;
                                            DownB.IsEnabled = false;
                                            AZJD.Content = "当前已完成"; //设置过程
                                            ZJDw.Value = cc.Length;
                                        }));
                                        break;
                                    }
                                }
                            });
                        }
                        DG.ShowAsync();
                        BDD.PDD.Visibility = Visibility.Hidden;
                        DownB.IsEnabled = false;
                        AZJD.Content = "当前已完成"; //设置过程
                        ZJDw.Value = cc.Length;
                    }
                    }
                    if (ForgeList.Text != "不安装" && OptifineList.Text != "不安装" && FabricList.Text is "不安装")
                    {
                        //安装Optifine和Forge
                        AZJD.Content = "当前:下载 Forge（过程可能较慢,请耐心等待）"; //设置过程
                        ZJDw.IsIndeterminate = true;
                        ZJD.IsIndeterminate = true;
                        mcd = down.ForgeDownload(Var.ZVar, ForgeList.Text); //获取Forge下载地址
                        Var.ForgePath = mcd.path;
                        Downloadw(mcd.path, mcd.Url); //下载Forge
                        bool results = false;
                        await Task.Run(() =>
                        {
                            while (true)
                            {
                                if (dlf.EndDownload())
                                {
                                    if (dlf.error > 0) results = false;
                                    else results = true;
                                    break;
                                }
                            }
                        });
                        if (results)
                        {
                            AZJD.Content = "当前:执行 Forge 安装"; //设置过程
                            ZJDw.IsIndeterminate = true;
                            ZJD.IsIndeterminate = true;
                            if (await VarDownload.Forge())
                            {
                                //下载Optifine
                                AZJD.Content = "当前:下载高清修复"; //设置过程
                                ZJDw.IsIndeterminate = true;
                                ZJD.IsIndeterminate = true;
                                mcd = down.DownloadOptifine(Var.ZVar, OptifineList.Text); //获取Forge下载地址
                                if (Game.IniReadValue("Vlist", "Path") == "" || Game.IniReadValue("Vlist", "Path") == "0")
                                {
                                    Downloadw(Game.Dminecraft + @"\versions\" + DownloadMCName + @"\mods\" + OptifineList.Text, mcd.Url);
                                }
                                else
                                {
                                    string ab = Game.IniReadValue("Vlist", "Path");
                                    int s = int.Parse(ab);
                                    int ss = s + 1;
                                    Downloadw(Game.IniReadValue("VPath", ss.ToString()) + @"\versions\" + DownloadMCName + @"\mods\" + OptifineList.Text, mcd.Url);
                                }
                                bool resultsx = false;
                                await Task.Run(() =>
                                {
                                    while (true)
                                    {
                                        if (dlf.EndDownload())
                                        {
                                            if (dlf.error > 0) resultsx = false;
                                            else resultsx = true;
                                            break;
                                        }
                                    }
                                });
                                if (resultsx)
                                {
                                AZJD.Content = "当前:安装游戏运行库"; //设置过程
                                ZJDw.IsIndeterminate = false;
                                ZJD.IsIndeterminate = false;
                                MCDownload[] cc = tools.Tools.GetMissingFile(DownloadMCName);
                                ZJDw.Maximum = cc.Length;
                                FSMCore.Download.FSMCoreDownload.DownloadCore.Download(cc);
                                await Task.Run(() =>
                                {
                                    while (true)
                                    {
                                        if (DownProgress.ProX + DownProgress.Error != cc.Length)
                                        {
                                            this.Dispatcher.Invoke(new Action(delegate
                                            {
                                                ZJDw.Value = DownProgress.ProX + DownProgress.Error;
                                            }));
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                });
                                AZJD.Content = "当前:安装游戏资源文件"; //设置过程
                                ZJDw.IsIndeterminate = false;
                                ZJD.IsIndeterminate = false;
                                MCDownload[] ccx = tools.Tools.GetMissingAsset(DownloadMCName);
                                ZJD.Maximum = ccx.Length - 2;
                                if (ccx.Length is 0)
                                {
                                    await DG.ShowAsync();
                                    BDD.PDD.Visibility = Visibility.Hidden;
                                    DownB.IsEnabled = false;
                                    AZJD.Content = "当前已完成"; //设置过程
                                    ZJD.Value = ccx.Length - 2;
                                }
                                else
                                {
                                    await Task.Run(() => { DownloaderAssets.DownloadCool(ccx); });
                                    await Task.Run(() =>
                                    {
                                        while (true)
                                        {
                                            if (DownloaderAssets.ProgressX + DownloaderAssets.FailedX < ccx.Length - 2
                                            )
                                            {
                                                this.Dispatcher.Invoke(new Action(delegate
                                                {
                                                    ZJD.Value = DownloaderAssets.ProgressX + DownloaderAssets.FailedX;
                                                }));
                                            }
                                            else
                                            {
                                                this.Dispatcher.Invoke(new Action(delegate
                                                {
                                                    DG.ShowAsync();
                                                    BDD.PDD.Visibility = Visibility.Hidden;
                                                    DownB.IsEnabled = false;
                                                    AZJD.Content = "当前已完成"; //设置过程
                                                    ZJDw.Value = cc.Length;
                                                }));
                                                break;
                                            }
                                        }
                                    });
                                }
                                DG.ShowAsync();
                                BDD.PDD.Visibility = Visibility.Hidden;
                                DownB.IsEnabled = false;
                                AZJD.Content = "当前已完成"; //设置过程
                                ZJDw.Value = cc.Length;
                            }
                            }
                        }
                    }
                    if (ForgeList.Text is "不安装" && OptifineList.Text is "不安装" && FabricList.Text != "不安装")
                    {
                        //只安装Fabric
                        Var.FabVar = FabricList.Text;
                        AZJD.Content = "当前:安装 Fabric"; //设置过程
                        ZJDw.IsIndeterminate = true;
                        ZJD.IsIndeterminate = true;
                        if (await VarDownload.Fabric())
                        {
                        AZJD.Content = "当前:安装游戏运行库"; //设置过程
                        ZJDw.IsIndeterminate = false;
                        ZJD.IsIndeterminate = false;
                        MCDownload[] cc = tools.Tools.GetMissingFile(DownloadMCName);
                        ZJDw.Maximum = cc.Length;
                        FSMCore.Download.FSMCoreDownload.DownloadCore.Download(cc);
                        await Task.Run(() =>
                        {
                            while (true)
                            {
                                if (DownProgress.ProX + DownProgress.Error != cc.Length)
                                {
                                    this.Dispatcher.Invoke(new Action(delegate
                                    {
                                        ZJDw.Value = DownProgress.ProX + DownProgress.Error;
                                    }));
                                }
                                else
                                {
                                    break;
                                }
                            }
                        });
                        AZJD.Content = "当前:安装游戏资源文件"; //设置过程
                        ZJDw.IsIndeterminate = false;
                        ZJD.IsIndeterminate = false;
                        MCDownload[] ccx = tools.Tools.GetMissingAsset(DownloadMCName);
                        ZJD.Maximum = ccx.Length - 2;
                        if (ccx.Length is 0)
                        {
                            await DG.ShowAsync();
                            BDD.PDD.Visibility = Visibility.Hidden;
                            DownB.IsEnabled = false;
                            AZJD.Content = "当前已完成"; //设置过程
                            ZJD.Value = ccx.Length - 2;
                        }
                        else
                        {
                            await Task.Run(() => { DownloaderAssets.DownloadCool(ccx); });
                            await Task.Run(() =>
                            {
                                while (true)
                                {
                                    if (DownloaderAssets.ProgressX + DownloaderAssets.FailedX < ccx.Length - 2
                                    )
                                    {
                                        this.Dispatcher.Invoke(new Action(delegate
                                        {
                                            ZJD.Value = DownloaderAssets.ProgressX + DownloaderAssets.FailedX;
                                        }));
                                    }
                                    else
                                    {
                                        this.Dispatcher.Invoke(new Action(delegate
                                        {
                                            DG.ShowAsync();
                                            BDD.PDD.Visibility = Visibility.Hidden;
                                            DownB.IsEnabled = false;
                                            AZJD.Content = "当前已完成"; //设置过程
                                            ZJDw.Value = cc.Length;
                                        }));
                                        break;
                                    }
                                }
                            });
                        }
                        DG.ShowAsync();
                        BDD.PDD.Visibility = Visibility.Hidden;
                        DownB.IsEnabled = false;
                        AZJD.Content = "当前已完成"; //设置过程
                        ZJDw.Value = cc.Length;
                    }
                    }
                    if (ForgeList.Text is "不安装" && OptifineList.Text != "不安装" && FabricList.Text != "不安装")
                    {
                        //安装Fabric和Optifine
                        ContentDialog dialog = new ContentDialog()
                        {
                            Title = "尚未支持Optifine和Fabric一起安装",
                            PrimaryButtonText = "好吧",
                            IsPrimaryButtonEnabled = true,
                            DefaultButton = ContentDialogButton.Primary,
                            Content = new TextBlock()
                            {
                                TextWrapping = TextWrapping.WrapWithOverflow,
                                Text = "都装Fabric了，就有更好的加载光影MOD了",
                            },

                        };
                        var result = await dialog.ShowAsync();
                    }
                    if (ForgeList.Text != "不安装" && OptifineList.Text != "不安装" && FabricList.Text != "不安装")
                    {
                        //不可安装
                        ContentDialog dialog = new ContentDialog()
                        {
                            Title = "三个扩展不可一起安装",
                            PrimaryButtonText = "好吧",
                            IsPrimaryButtonEnabled = true,
                            DefaultButton = ContentDialogButton.Primary,
                            Content = new TextBlock()
                            {
                                TextWrapping = TextWrapping.WrapWithOverflow,
                                Text = "啦啦啦啦啦",
                            },

                        };
                        var result = await dialog.ShowAsync();
                    }
                    if (ForgeList.Text != "不安装" && OptifineList.Text is "不安装" && FabricList.Text is "不安装")
                    {
                        //安装Forge
                        AZJD.Content = "当前:下载 Forge（过程可能较慢,请耐心等待）"; //设置过程
                        ZJDw.IsIndeterminate = true;
                        ZJD.IsIndeterminate = true;
                        mcd = down.ForgeDownload(Var.ZVar, ForgeList.Text); //获取Forge下载地址
                        Var.ForgePath = mcd.path;
                        Downloadw(mcd.path, mcd.Url); //下载Forge
                        bool results = false;
                        await Task.Run(() =>
                        {
                            while (true)
                            {
                                if (dlf.EndDownload())
                                {
                                    if (dlf.error > 0) results = false;
                                    else results = true;
                                    break;
                                }
                            }
                        });
                        if (results)
                        {
                            AZJD.Content = "当前:执行 Forge 安装"; //设置过程
                            ZJDw.IsIndeterminate = true;
                            ZJD.IsIndeterminate = true;
                            if (await VarDownload.Forge())
                            {
                            AZJD.Content = "当前:安装游戏运行库"; //设置过程
                            ZJDw.IsIndeterminate = false;
                            ZJD.IsIndeterminate = false;
                            MCDownload[] cc = tools.Tools.GetMissingFile(DownloadMCName);
                            ZJDw.Maximum = cc.Length;
                            FSMCore.Download.FSMCoreDownload.DownloadCore.Download(cc);
                            await Task.Run(() =>
                            {
                                while (true)
                                {
                                    if (DownProgress.ProX + DownProgress.Error != cc.Length)
                                    {
                                        this.Dispatcher.Invoke(new Action(delegate
                                        {
                                            ZJDw.Value = DownProgress.ProX + DownProgress.Error;
                                        }));
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            });
                            AZJD.Content = "当前:安装游戏资源文件"; //设置过程
                            ZJDw.IsIndeterminate = false;
                            ZJD.IsIndeterminate = false;
                            MCDownload[] ccx = tools.Tools.GetMissingAsset(DownloadMCName);
                            ZJD.Maximum = ccx.Length - 2;
                            if (ccx.Length is 0)
                            {
                                await DG.ShowAsync();
                                BDD.PDD.Visibility = Visibility.Hidden;
                                DownB.IsEnabled = false;
                                AZJD.Content = "当前已完成"; //设置过程
                                ZJD.Value = ccx.Length - 2;
                            }
                            else
                            {
                                await Task.Run(() => { DownloaderAssets.DownloadCool(ccx); });
                                await Task.Run(() =>
                                {
                                    while (true)
                                    {
                                        if (DownloaderAssets.ProgressX + DownloaderAssets.FailedX < ccx.Length - 2
                                        )
                                        {
                                            this.Dispatcher.Invoke(new Action(delegate
                                            {
                                                ZJD.Value = DownloaderAssets.ProgressX + DownloaderAssets.FailedX;
                                            }));
                                        }
                                        else
                                        {
                                            this.Dispatcher.Invoke(new Action(delegate
                                            {
                                                DG.ShowAsync();
                                                BDD.PDD.Visibility = Visibility.Hidden;
                                                DownB.IsEnabled = false;
                                                AZJD.Content = "当前已完成"; //设置过程
                                                ZJDw.Value = cc.Length;
                                            }));
                                            break;
                                        }
                                    }
                                });
                            }
                            DG.ShowAsync();
                            BDD.PDD.Visibility = Visibility.Hidden;
                            DownB.IsEnabled = false;
                            AZJD.Content = "当前已完成"; //设置过程
                            ZJDw.Value = cc.Length;
                        }
                        }
                    }
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(new Action(delegate { BDD.PDD.Visibility = Visibility.Hidden; }));
                    this.Dispatcher.Invoke(new Action(delegate
                    {
                        dialogb.ShowAsync();
                        DownB.IsEnabled = false;
                    }));
                Console.WriteLine(ex.Message);
                }
            }
        ContentDialog dialogb = new ContentDialog()
        {
            Title = "已停止下载",
            PrimaryButtonText = "好吧",
            IsPrimaryButtonEnabled = true,
            DefaultButton = ContentDialogButton.Primary,
            Content = new TextBlock()
            {
                TextWrapping = TextWrapping.WrapWithOverflow,
                Text = "遇到错误了",
            },
        };
    }
    }
