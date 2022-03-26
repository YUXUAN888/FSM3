using FSM3.About_List;
using FSMLauncher_3;
using Gac;
using ModernWpf.Controls;
using MojangAPI;
using MojangAPI.Model;
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
    /// Skinforoffline.xaml 的交互逻辑
    /// </summary>
    public partial class Skinforoffline : System.Windows.Controls.Page
    {
        public Skinforoffline()
        {
            InitializeComponent();
            try
            {
                int TB1 = int.Parse(Game.IniReadValueS("Skin", "List"));
                for (int i = 1; i < TB1 + 1; ++i)
                {
                    List<SkinList> item1 = new List<SkinList>();
                    SkinList item = new SkinList();
                    after = Game.IniReadValueS("Skin", i.ToString()).Split(new char[] { '|' });
                    item.SkinNamew.Text = after[0] + "的皮肤";
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    BitmapImage bi = new BitmapImage();
                    //BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(Game.ZongSkin + @"\" + after[0] + @"\SkinT.png", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    item.OffLineSkinImage.Source = bi;
                    item1.Add(item);
                    OffLineSkinList.Items.Add(item);
                    //OffLineSkinList.Items.Add(IniReadValueS("Skin", i.ToString()));
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
                    //OFFLINEI.Source = bi;
                }
                catch
                {

                }
            }
        }
        String inputSkinName;
        int onlw;
        String SkinUUID;
        int id = 0;
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
        private async void Add_Skin_Click(object sender, RoutedEventArgs e)
        {
            StackPanel panel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            panel.Children.Add(new TextBlock() { Text = "" });
            TextBox box = new TextBox();
            box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "输入后,您可以使用ta的皮肤");
            panel.Children.Add(box);

            ContentDialog dialog = new ContentDialog()
            {
                Title = "请输入正版玩家名",
                PrimaryButtonText = "OK!",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = panel,
            };
            var result = await dialog.ShowAsync();
            inputSkinName = box.Text;
            try
            {
                //statistics = KMCCC.Pro.Modules.MojangAPI.MojangAPI.GetStatistics();
                //MessageBox.Show(statistics.getTotal().ToString());
                HttpClient httpClient = new HttpClient();
                Mojang mojang = new Mojang(httpClient);
                PlayerUUID uuid = await mojang.GetUUID(inputSkinName);
                SkinUUID = uuid.UUID;
                ///SkinUUID = KMCCC.Pro.Modules.MojangAPI.MojangAPI.NameToUUID(inputSkinName).ToString();
                Directory.CreateDirectory(Game.ZongSkin + @"\" + inputSkinName);
                if (System.IO.File.Exists(Game.ZongSkin + @"\SkinZ.Skin"))
                {
                    //存在文件
                }
                else
                {
                    //不存在文件
                    using (File.Create(Game.ZongSkin + @"\SkinZ.Skin")) ;
                }
                onlw = Download(Settings.ZongSkin + @"\" + inputSkinName + @"\SkinY.png", "", tools.Tools.GetMinecraftSkin(SkinUUID));
                dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
                Game.Jarw = Core5.timer(Skinw, 1000);
                Game.Jarw.Start();
            }
            catch (Exception ex)
            {
                ContentDialog dialogw = new ContentDialog()
                {
                    Title = "添加失败",
                    PrimaryButtonText = "好吧",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = "错误信息:" + ex.Message
                    },

                };
                var resultw = dialogw.ShowAsync();
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
        public void Skinw(object ob, EventArgs a)
        {
            if (DIYvar.xzItems[onlw].xzwz == "完成")
            {
                Game.Jarw.Stop();
                List<SkinList> item1 = new List<SkinList>();
                SkinList item = new SkinList();
                item.SkinNamew.Text = inputSkinName + "的皮肤";
                System.Drawing.Point point = new System.Drawing.Point(8, 8);
                System.Drawing.Size size = new System.Drawing.Size(8, 8);
                Bitmap bitmap = new Bitmap(Game.ZongSkin + @"\" + inputSkinName + @"\SkinY.png");
                var i = crop(bitmap, new System.Drawing.Point(8, 8), new System.Drawing.Size(8, 8));
                Zoom(i, 258, 258, out i, ZoomType.NearestNeighborInterpolation);
                i.Save(Game.ZongSkin + @"\" + inputSkinName + @"\SkinT.png");
                System.Drawing.Image img = i;
                BitmapImage bi = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                item.OffLineSkinImage.Source = BitmapToBitmapImage(i);
                item1.Add(item);
                OffLineSkinList.Items.Add(item);
                Game.WritePrivateProfileString("Skin", "List", OffLineSkinList.Items.Count.ToString(), Game.ZongSkin + @"\SkinZ.Skin");
                Game.WritePrivateProfileString("Skin", OffLineSkinList.Items.Count.ToString(), inputSkinName + "|" + SkinUUID, Game.ZongSkin + @"\SkinZ.Skin");
            }
        }
        string[] after;
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        private void OnlineSkinList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int a = OffLineSkinList.SelectedIndex + 1;
            after = Game.IniReadValueS("Skin", a.ToString()).Split(new char[] { '|' });
            Game.SkinUUIDMC = after[1];
            BitmapImage bi = new BitmapImage();
            //BitmapImage.UriSource must be in a BeginInit/EndInit block.  
            bi.BeginInit();
            bi.UriSource = new Uri(Game.ZongSkin + @"\" + after[0] + @"\SkinT.png", UriKind.RelativeOrAbsolute);
            bi.EndInit();
            ///OFFLINEI.Source = bi;
            Game.WritePrivateProfileString("OffLine", "Skin", after[0] + "|" + after[1], Game.ZongW + @"\ConsoleW.qwq");
            //Skin.SkinFilePath = Game.ZongSkin + @"\" + after[0] + @"\SkinT.png";
            //Skin.SetSkin();
            //Skin3D.SkinFilePath = ZongSkin + @"\" + after[0] + @"\SkinY.png";
            //Skin3D.SetSkin();
        }
    }
}
