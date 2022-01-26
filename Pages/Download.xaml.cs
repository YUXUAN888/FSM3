using FSM3.About_List;
using FSMLauncher_3;
using ModernWpf.Controls;
using SquareMinecraftLauncher.Minecraft;
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
            
        }

        private void MCV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tile_Click_25(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_26(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_17(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_X(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {

        }

        private void yxlx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    return;
                }
                else if (result == ContentDialogResult.None)
                {
                    
                } 
            }
            foreach (var i in mc)
            {
                switch (i.type)
                {
                    case "正式版":
                        McVersionList a = new McVersionList(i.id, i.type);
                        FSMLauncher_3.DIYvar.minecraft1.Add(a);
                        break;
                    case "快照版":
                        McVersionList b = new McVersionList(i.id, i.type);
                        FSMLauncher_3.DIYvar.minecraft2.Add(b);
                        break;
                    case "基岩版":
                        McVersionList c = new McVersionList(i.id, "早期测试");
                        FSMLauncher_3.DIYvar.minecraft3.Add(c);
                        break;
                    case "远古版":
                        McVersionList d = new McVersionList(i.id, i.type);
                        FSMLauncher_3.DIYvar.minecraft4.Add(d);
                        break;
                }
                var DT = DateTime.Parse(i.releaseTime);
                if (DT.ToString("MM-dd") == "04-01")
                {
                    McVersionList s = new McVersionList(i.id, "愚人节版本");
                    FSMLauncher_3.DIYvar.minecraft5.Add(s);
                }

            }
            mcVersionLists = FSMLauncher_3.DIYvar.minecraft1.ToArray();
            List<Item> item1 = new List<Item>();


            for (int i = 0; i < mcVersionLists.Length; i++)
            {
                Item item = new Item();
                item.Dver.Text = mcVersionLists[i].version;
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
    }
}
