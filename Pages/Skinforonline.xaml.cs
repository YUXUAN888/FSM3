using FSM3.About_List;
using Microsoft.Win32;
using ModernWpf.Controls;
using MojangAPI;
using MojangAPI.Model;
using SquareMinecraftLauncher.Core.OAuth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using static FSM3.MainWindow;

namespace FSM3.Pages
{
    /// <summary>
    /// Skinforonline.xaml 的交互逻辑
    /// </summary>
    public partial class Skinforonline : System.Windows.Controls.Page
    {
        string[] after;
        public Skinforonline()
        {
            InitializeComponent();
            //if (Game.IniReadValue("Color", "ZT") is "Light")
            //{
            //    Skin.SetColor(SquareMinecraftLauncherSkin.WPFSkin.backgroundColor.white);
            //}
            //else
            //{
            //    Skin.SetColor(SquareMinecraftLauncherSkin.WPFSkin.backgroundColor.black);
            //}
            try
            {
                int TB1 = int.Parse(Game.IniReadValueSS("Skin", "List"));
                for (int i = 1; i < TB1 + 1; ++i)
                {
                    List<SkinList> item1 = new List<SkinList>();
                    SkinList item = new SkinList();
                    after = Game.IniReadValueSS("Skin", i.ToString()).Split(new char[] { '|' });
                    item.SkinNamew.Text = after[0];
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    BitmapImage bi = new BitmapImage();
                    //BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(Game.ZongSkin + @"\SkinN\" + after[0] + @"\SkinT.png", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    item.OffLineSkinImage.Source = bi;
                    item1.Add(item);
                    OnLineSkinList.Items.Add(item);
                    //OffLineSkinList.Items.Add(IniReadValueS("Skin", i.ToString()));
                }
            }
            catch
            {

            }
        }
        HttpClient httpClient = new HttpClient();
        Mojang mojangw = new Mojang();
        private async void OnlineSkinList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MicrosoftLogin microsoftLogin = new MicrosoftLogin();
            Xbox XboxLogin = new Xbox();
            string refresh_token = Game.IniReadValueW("wr", "Atoken");
            string Minecraft_Token = new MinecraftLogin().GetToken(XboxLogin.XSTSLogin(XboxLogin.GetToken(microsoftLogin.RefreshingTokens(refresh_token))));
            int a = OnLineSkinList.SelectedIndex + 1;
            string[] Wfter = Game.IniReadValueSS("Skin", a.ToString()).Split(new char[] { '|' });
            String Path = Wfter[1];
            ContentDialog dialog = new ContentDialog()
            {
                Title = "请选择皮肤类型",
                PrimaryButtonText = "普通",
                CloseButtonText = "纤细",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock()
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Text = "这将会更改您Minecraft皮肤的类型\n纤细会缩短皮肤手臂,模型为Alex；普通是史蒂夫的形态,模型为Steve"
                },

            };
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                MojangAPIResponse response = await mojangw.UploadSkin(Minecraft_Token, SkinType.Steve, Path);
                if (response.IsSuccess)
                {
                    ContentDialog dialogws = new ContentDialog()
                    {
                        Title = "更换成功！",
                        PrimaryButtonText = "好哒!",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "你可以使用新皮肤啦!"
                        },

                    };
                    await dialogws.ShowAsync();
                    System.Drawing.Point point = new System.Drawing.Point(8, 8);
                    System.Drawing.Size size = new System.Drawing.Size(8, 8);
                    Bitmap bitmap = new Bitmap(Path);
                    var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                    Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
                    System.Drawing.Image img = i;
                    BitmapImage bi = new BitmapImage();
                    //IM.Source = BitmapToBitmapImage(i);
                }
                else
                {
                    ContentDialog dialogw = new ContentDialog()
                    {
                        Title = "更换失败",
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "可能是网络错误\n错误信息:" + response.ErrorMessage
                        },

                    };
                    await dialogw.ShowAsync();
                }
            }
            else if (result == ContentDialogResult.None)
            {
                MojangAPIResponse response = await mojangw.UploadSkin(Minecraft_Token, SkinType.Alex, Path);
                if (response.IsSuccess)
                {
                    ContentDialog dialogws = new ContentDialog()
                    {
                        Title = "更换成功！",
                        PrimaryButtonText = "好哒!",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "你可以使用新皮肤啦!"
                        },

                    };
                    await dialogws.ShowAsync();
                    System.Drawing.Point point = new System.Drawing.Point(8, 8);
                    System.Drawing.Size size = new System.Drawing.Size(8, 8);
                    Bitmap bitmap = new Bitmap(Path);
                    var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                    Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
                    System.Drawing.Image img = i;
                    BitmapImage bi = new BitmapImage();
                    //IM.Source = BitmapToBitmapImage(i);
                }
                else
                {
                    ContentDialog dialogww = new ContentDialog()
                    {
                        Title = "更换失败",
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "可能是网络错误\n错误信息:" + response.ErrorMessage
                        },

                    };
                    await dialogww.ShowAsync();
                }
            }
        }

        private async void Add_Skin_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();//提示用户打开文件窗体
            fileDialog.Title = "选择皮肤文件";//文件对话框标题
            fileDialog.Filter = "皮肤文件|*.png";//文件格式筛选字符串
            if (fileDialog.ShowDialog() == true)//判断对话框返回值，点击打开
            {
                //fileDialog.FileName.ToString()
                /*
                MetroDialogSettings set = new MetroDialogSettings();
                set.AffirmativeButtonText = "普通";
                set.NegativeButtonText = "纤细";
                if(await this.ShowMessageAsync("请选择皮肤类型", "这将会更改您Minecraft皮肤的类型\n纤细会缩短皮肤手臂,模型为Alex；普通是史蒂夫的形态,模型为Steve", MessageDialogStyle.AffirmativeAndNegative, set) == MessageDialogResult.Affirmative)
                {
                    MojangAPIResponse response = await mojangw.UploadSkin("accessToken", SkinType.Steve, "skin_png_file_path");
                }
                else
                {
                    MojangAPIResponse response = await mojangw.UploadSkin("accessToken", SkinType.Alex, "skin_png_file_path");
                }
                */
                StackPanel panel = new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };
                panel.Children.Add(new TextBlock() { Text = "" });
                TextBox box = new TextBox();
                box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "这将显示在列表里");
                panel.Children.Add(box);

                ContentDialog dialog = new ContentDialog()
                {
                    Title = "请输入皮肤名",
                    PrimaryButtonText = "确定",
                    CloseButtonText = "取消",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = panel,
                };
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    string skinname = box.Text;
                    Directory.CreateDirectory(Settings.ZongX + @"\.fsm\Skin\SkinN\" + skinname);
                    string path = fileDialog.FileName;
                    string path2 = Settings.ZongX + @"\.fsm\Skin\SkinN\" + skinname + @"\SkinY.png";
                    FileInfo fi1 = new FileInfo(path);
                    FileInfo fi2 = new FileInfo(path2);
                    fi1.CopyTo(path2);
                    List<SkinList> item1 = new List<SkinList>();
                    SkinList item = new SkinList();
                    item.SkinNamew.Text = skinname;
                    System.Drawing.Point point = new System.Drawing.Point(8, 8);
                    System.Drawing.Size size = new System.Drawing.Size(8, 8);
                    Bitmap bitmap = new Bitmap(Settings.ZongX + @"\.fsm\Skin\SkinN\" + skinname + @"\SkinY.png");
                    var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                    Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
                    i.Save(Settings.ZongX + @"\.fsm\Skin\SkinN\" + skinname + @"\SkinT.png");
                    System.Drawing.Image img = i;
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    item.OffLineSkinImage.Source = BitmapToBitmapImage(i);
                    item1.Add(item);
                    OnLineSkinList.Items.Add(item);
                    //Skin.SkinFilePath = Settings.ZongX + @"\.fsm\Skin\SkinN\" + skinname + @"\SkinT.png";
                    //Skin.SetSkin();
                    Game.WritePrivateProfileString("Skin", "List", OnLineSkinList.Items.Count.ToString(), Settings.ZongSkin + @"\SkinN.Skin");
                    Game.WritePrivateProfileString("Skin", OnLineSkinList.Items.Count.ToString(), skinname + "|" + fileDialog.FileName, Settings.ZongSkin + @"\SkinN.Skin");
                }
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
    }
}
