using FSM3.About_List;
using FSMLauncher_3.About_List;
using SquareMinecraftLauncher;
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
using static FSM3.Pages.Game;

namespace FSM3.Pages
{
    /// <summary>
    /// VerList.xaml 的交互逻辑
    /// </summary>
    public partial class VerList : Page
    {
        public class Vlistw
        {
            public static ListBox Vlist { get; set; } 
        }
        public class PathList
        {
            public static ListBox pathlist { get; set; }
        }
        
        public VerList()
        {
            InitializeComponent();
            Vlistw.Vlist = vlist;
            PathList.pathlist = pathlist;
            List<PathItem> user1 = new List<PathItem>();
            PathItem user = new PathItem();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
            user.Path.Content = Dminecraft;
            user.WJJBT.Content = "启动器目录文件夹";
            pathlist.Items.Add(user);
            pathlist.SelectedIndex = pathlist.SelectedIndex;
            try
            {
                for (int i = 2; i <= int.Parse(IniReadValue("VPath1", "1")); i++)
                {
                    PathItem userw = new PathItem();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    userw.Path.Content = IniReadValue("VPath", i.ToString());
                    pathlist.Items.Add(userw);
                }
            }
            catch
            {

            }
            try
            {
                if (IniReadValue("Vlist", "V") == "" || IniReadValue("Vlist", "V") == null)
                {
                    NowVw.NowV.Content = "当前没有版本";
                }
                if (IniReadValue("Vlist", "Path") != "" || IniReadValue("Vlist", "Path") != null || IniReadValue("Vlist", "Path") != "0")
                {
                    AllTheExistingVersion[] t = new AllTheExistingVersion[0];
                    pathlist.SelectedIndex = int.Parse(IniReadValue("Vlist", "Path"));
                    //tools.SetMinecraftFilesPath(IniReadValue("VPath", IniReadValue("Vlist", "Path")));

                    //t = tools.GetAllTheExistingVersion();


                    vlist.SelectedIndex = int.Parse(IniReadValue("Vlist", "V"));

                    NowVw.NowV.Content = t[vlist.SelectedIndex].version;
                }
                else
                {
                    pathlist.SelectedIndex = 0;
                }
            }
            catch
            {

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void pathlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // vlist.Items.Clear();

            AllTheExistingVersion[] t = new AllTheExistingVersion[0];
            //tools.SetMinecraftFilesPath(dminecraft_text.Text);
            //AllTheExistingVersion[] t = tools.GetAllTheExistingVersion();

            try
            {
                if (pathlist.SelectedIndex == 0)
                {
                    Game.WritePrivateProfileString("Vlist", "Path", pathlist.SelectedIndex.ToString(), File_);
                    tools.Tools.SetMinecraftFilesPath(Dminecraft);
                    t = tools.Tools.GetAllTheExistingVersion();
                    //List<DoItem> item1 = new List<DoItem>();
                    //https://blog.qwq.one/pic-fsm.php

                    string forge = null, Lite = null, Op = null, fab = null;
                    //DIYvar.l = item1;

                    List<DoItem> user1 = new List<DoItem>();
                    for (int i = 0; i < t.Length; i++)
                    {
                        DoItem user = new DoItem();
                        if (tools.Tools.ForgeExist(t[i].version, ref forge))
                        {
                            BitmapImage bi = new BitmapImage();
                            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                            bi.BeginInit();
                            bi.UriSource = new Uri(@"\Image\ForgeN.png", UriKind.RelativeOrAbsolute);
                            bi.EndInit();
                            user.DverV.Content = t[i].version;
                            user.BBLX.Content = forge;
                            user.Dimage.Source = bi;
                        }
                        else if (tools.Tools.OptifineExist(t[i].version, ref Op))
                        {
                            BitmapImage bi = new BitmapImage();
                            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                            bi.BeginInit();
                            bi.UriSource = new Uri(@"\Image\OptifineLogo.png", UriKind.RelativeOrAbsolute);
                            bi.EndInit();
                            user.DverV.Content = t[i].version;
                            user.BBLX.Content = Op;
                            user.Dimage.Source = bi;
                        }
                        else if (tools.Tools.FabricExist(t[i].version, ref fab))
                        {
                            BitmapImage bi = new BitmapImage();
                            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                            bi.BeginInit();
                            bi.UriSource = new Uri(@"\Image\Fabric.png", UriKind.RelativeOrAbsolute);
                            bi.EndInit();
                            user.DverV.Content = t[i].version;
                            Thickness thick = new Thickness();
                            thick.Left = -5;
                            user.Dimage.Margin = thick;
                            user.BBLX.Content = fab;
                            user.Dimage.Source = bi;
                        }
                        else
                        {
                            user.DverV.Content = t[i].version;
                            user.BBLX.Content = "原版 " + t[i].IdVersion;
                        }
                        user1.Add(user);
                    }
                    // DIYvar.lw = user1;
                    vlist.ItemsSource = user1.ToArray();


                }
                else
                {
                    Game.WritePrivateProfileString("Vlist", "Path", pathlist.SelectedIndex.ToString(), File_);
                    string mcPath = (pathlist.SelectedItem as PathItem).Path.Content.ToString();
                    if (mcPath == "")
                    {
                        vlist.ItemsSource = "找不到任何版本";
                    }
                    else
                    {
                        tools.Tools.SetMinecraftFilesPath(mcPath);
                        t = tools.Tools.GetAllTheExistingVersion();
                        List<DoItem> user1 = new List<DoItem>();
                        string forge = null, Lite = null, Op = null, fab = null;
                        for (int i = 0; i < t.Length; i++)
                        {

                            DoItem user = new DoItem();
                            if (tools.Tools.ForgeExist(t[i].version, ref forge))
                            {
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\ForgeN.png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.DverV.Content = t[i].version;
                                user.Dimage.Source = bi;
                                user.BBLX.Content = forge;
                            }
                            else if (tools.Tools.OptifineExist(t[i].version, ref Op))
                            {
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\OptifineLogo.png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.DverV.Content = t[i].version;
                                user.BBLX.Content = Op;
                                user.Dimage.Source = bi;
                            }
                            else if (tools.Tools.FabricExist(t[i].version, ref fab))
                            {
                                BitmapImage bi = new BitmapImage();
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\Fabric.png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.DverV.Content = t[i].version;
                                user.BBLX.Content = fab;
                                user.Dimage.Source = bi;
                            }
                            else
                            {
                                user.DverV.Content = t[i].version;
                                user.BBLX.Content = "原版 " + t[i].IdVersion;
                            }
                            user1.Add(user);
                        }
                        // DIYvar.lw = user1;
                        vlist.ItemsSource = user1.ToArray();
                    }

                }
                // string mcPath = (sender as ListBox).SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                vlist.ItemsSource = "";
            }
        }
        String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();  //选择文件夹
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();//提示用户打开文件窗体
            dialog.Description = "请选择.minecraft文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //pathlist.Items.Add(dialog.SelectedPath);
                List<PathItem> user1 = new List<PathItem>();

                PathItem user = new PathItem();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                user.Path.Content = dialog.SelectedPath;
                pathlist.Items.Add(user);
                //pathlist.ItemsSource = user1.ToArray();
            }
            try
            {

                Game.WritePrivateProfileString("VPath1", "1", pathlist.Items.Count.ToString(), File_);
                Game.WritePrivateProfileString("VPath", pathlist.Items.Count.ToString(), dialog.SelectedPath, File_);

            }
            catch
            {

            }
        }
        public static string NowVS;
        private void vlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (pathlist.SelectedIndex == 0)
                {
                    MinecraftDownload minecraft = new MinecraftDownload();
                    AllTheExistingVersion[] t = new AllTheExistingVersion[0];
                    string mcPath = (pathlist.SelectedItem as PathItem).Path.Content.ToString();
                    tools.Tools.SetMinecraftFilesPath(mcPath);
                    t = tools.Tools.GetAllTheExistingVersion();
                    String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
                    Game.WritePrivateProfileString("Vlist", "V", vlist.SelectedIndex.ToString(), File_);

                    NowVS = t[vlist.SelectedIndex].version;
                    NowVw.NowV.Content = t[vlist.SelectedIndex].version;
                }
                else
                {
                    MinecraftDownload minecraft = new MinecraftDownload();
                    AllTheExistingVersion[] t = new AllTheExistingVersion[0];
                    string mcPath = (pathlist.SelectedItem as PathItem).Path.Content.ToString();
                    tools.Tools.SetMinecraftFilesPath(mcPath);
                    t = tools.Tools.GetAllTheExistingVersion();
                    String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
                    Game.WritePrivateProfileString("Vlist", "V", vlist.SelectedIndex.ToString(), File_);

                    NowVS = t[vlist.SelectedIndex].version;
                    NowVw.NowV.Content = t[vlist.SelectedIndex].version;
                }
            }
            catch
            {

            }
        }
    }
}
