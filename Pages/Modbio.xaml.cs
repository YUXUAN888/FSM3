using ModernWpf.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.IO;

namespace FSM3.Pages
{
    /// <summary>
    /// Modbio.xaml 的交互逻辑
    /// </summary>
    public partial class Modbio : System.Windows.Controls.Page
    {
        SquareMinecraftLauncher.Core.wiki wiki = new SquareMinecraftLauncher.Core.wiki();
        public Modbio()
        {
            InitializeComponent();
        }
        DownloadFile dlf = new DownloadFile();
        internal void Downloadw(string path, string url)
        {
            dlf.Start(path, url);//增加下载
        }
        static System.Windows.Threading.DispatcherTimer UPDATEW = new System.Windows.Threading.DispatcherTimer();
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(ModL.SelectedItem.ToString());
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\DownloadMods"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\DownloadMods");
            }
            WebClient we = new WebClient();
            ContentDialog dialog = new ContentDialog()
            {
                Title = "开始下载!",
                PrimaryButtonText = "好哒!",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock()
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Text = "已经开始下载你选的 Mod 啦!\n下载完成后会提示,请到启动器目录的 FSM 文件夹里的'DownloadMods'查看!"
                },

            }; var result = dialog.ShowAsync();
            await we.DownloadFileTaskAsync(new Uri(xzlj[ModL.SelectedIndex]),System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\DownloadMods\" + xzlsj[ModL.SelectedIndex]);
            ContentDialog dialogx = new ContentDialog()
            {
                Title = "下载完成!",
                PrimaryButtonText = "好哒!",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock()
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Text = "快加上 Mod 玩吧！注意哦，FSM 在有 mod 的版本自动开启版本隔离!"
                },

            };
            var resultx = dialogx.ShowAsync();
            System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\DownloadMods");
        }
        public void Cree(object ob, EventArgs a)
        {
            if (dlf.EndDownload())
            {
                UPDATEW.Stop();
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "下载完成!",
                    PrimaryButtonText = "好哒!",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = "快加上 Mod 玩吧！注意哦，FSM 在有 mod 的版本自动开启版本隔离!"
                    },

                };
                var result = dialog.ShowAsync();
                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\DownloadMods");
            }
        }

        public static string[] xzlj = new string[666];
        public static string[] xzlsj = new string[666];
        private async void Load(object sender, RoutedEventArgs e)
        {
            if (Var.IsCurse)
            {
                Name.Content = FSMCore.Modrinth.Modpublic.modname;
                var a = await FSMCore.CurseForgeAPI.V1.Get.GetModsD(Var.MODCurseID);
                bio.Content = a.Data.Summary + "|下载量:" + a.Data.DownloadCount;
                ModLogo.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(a.Data.Logo.Url))
                };
                var b = await FSMCore.CurseForgeAPI.V1.Get.GetModsFileList(Var.MODCurseID);
                Time.Content = a.Data.DateCreated;
                try
                {
                    ImageYL.Background = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(a.Data.Screenshots[0].Url))
                    };
                }
                catch
                {

                }
                LY.Content = "此资源来源于 CurseForge\nFSM3 通过官方API进行访问";
                for (int i = 0; i < b.Data.Count; ++i)
                {
                    ModL.Items.Add(b.Data[i].FileName);
                    try
                    {
                        xzlsj[i] = b.Data[i].FileName;
                        xzlj[i] = b.Data[i].DownloadUrl;
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                LY.Content = "此资源来源于 Modrinth\nFSM3 通过官方API进行访问";
                Name.Content = FSMCore.Modrinth.Modpublic.modname;
                WebClient web = new WebClient();
                String c = FSMCore.Modrinth.Modpublic.modname;
                string[] after = c.Split(new char[] { '|' });
                string str = web.DownloadString("https://api.modrinth.com/v2/project/" + after[1]);
                FSMCore.Modrinth.APICBD.Root rb = JsonConvert.DeserializeObject<FSMCore.Modrinth.APICBD.Root>(str);
                Name.Content = after[0] + " | 下载量:" + rb.downloads;
                ModLogo.Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(rb.icon_url))
                };
                bio.Content = rb.description;
                Time.Content = rb.published;
                string str2 = web.DownloadString("https://api.modrinth.com/v2/project/" + after[1] + "/version");
                string xs = "{" + "\"l\":";
                try
                {
                    FSMCore.Modrinth.APICCC.Root rb2 = JsonConvert.DeserializeObject<FSMCore.Modrinth.APICCC.Root>(xs + str2 + "}");
                    for (int i = 0; i < rb2.l.Count; ++i)
                    {
                        Console.WriteLine(rb2.l[i].files[0].filename);
                        ModL.Items.Add(rb2.l[i].files[0].filename + " | 点击下载");
                        try
                        {
                            xzlsj[i] = rb2.l[i].files[0].filename;
                            xzlj[i] = rb2.l[i].files[0].url;
                        }
                        catch
                        {

                        }
                    }
                }
                catch
                {

                }
            }
        }
    }
}
