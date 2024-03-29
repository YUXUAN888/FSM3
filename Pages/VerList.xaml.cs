﻿using FSM3.About_List;
using FSMLauncher_3.About_List;
using ModernWpf.Controls;
using SquareMinecraftLauncher;
using SquareMinecraftLauncher.Minecraft;
using System;
using System.Collections.Generic;
using System.IO;
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
using Utils;
using static FSM3.Pages.Game;

namespace FSM3.Pages
{
    /// <summary>
    /// VerList.xaml 的交互逻辑
    /// </summary>
    public partial class VerList : System.Windows.Controls.Page
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
            if (IniReadValue("Vlist", "Path") == null)
            {
                pathlist.SelectedIndex = 0;
            }
            if (IniReadValue("Vlist", "Path") == "")
            {
                pathlist.SelectedIndex = 0;
            }
            if (IniReadValue("Vlist", "Path") == "0")
            {
                pathlist.SelectedIndex = 0;
            }
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
                    tools.Tools.SetMinecraftFilesPath(IniReadValue("VPath", IniReadValue("Vlist", "Path")));

                    t = tools.Tools.GetAllTheExistingVersion();


                    vlist.SelectedIndex = int.Parse(IniReadValue("Vlist", "V"));

                    NowVw.NowV.Content = t[vlist.SelectedIndex].version;
                }
                else
                {
                    pathlist.SelectedIndex = 0;
                }
                if (IniReadValue("Vlist", "Path") == null)
                {
                    pathlist.SelectedIndex = 0;
                }
                if (IniReadValue("Vlist", "Path") == "")
                {
                    pathlist.SelectedIndex = 0;
                }
                if (IniReadValue("Vlist", "Path") == "0")
                {
                    pathlist.SelectedIndex = 0;
                }
            }
            catch
            {

            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pathlist.SelectedIndex is 0)
                {
                    ContentDialog dialogx = new ContentDialog()
                    {
                        Title = "不能删除文件夹",
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "此项为启动器目录,无法删除",
                        },
                    };
                    await dialogx.ShowAsync();
                }
                else
                {
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "删除提示",
                        PrimaryButtonText = "好哒!",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "请注意，删除文件夹是指删除游戏文件夹在启动器中列表的项，并不是删除你的游戏文件夹，所以不会丢失数据",
                        },

                    };
                    var result = await dialog.ShowAsync();
                    pathlist.Items.Remove(pathlist.SelectedItem);
                    for (int i = 2; i <= int.Parse(IniReadValue("VPath1", "1")); i++)
                    {
                        Game.WritePrivateProfileString("VPath", i.ToString(), "", File_);
                    }
                    Game.WritePrivateProfileString("VPath1", "1", (pathlist.Items.Count - 1).ToString(), File_);
                    for (int i = 1; i < pathlist.Items.Count; ++i)
                    {
                        Game.WritePrivateProfileString("VPath", i.ToString(), (pathlist.Items[i] as PathItem).Path.Content.ToString(), File_);
                    }
                }
            }
            catch
            {

            }
        }
        public static string IniReadValueKK(string Section, string Key,string File)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, File);
            return temp.ToString();
        }
        public static int GetLength(string str)
        {
            if (str.Length == 0)
                return 0;
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
            }
            return tempLen;
        }
        private void pathlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // vlist.Items.Clear();
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
                            Directory.CreateDirectory(t[i].path + @"\FSM");
                            if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "Forge")
                            {
                                WritePrivateProfileString("config", "icon", "Forge", t[i].path + @"\FSM\Banner.inif");
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\ForgeN.png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            else
                            {
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == forge)
                            {
                                WritePrivateProfileString("config", "bio", forge, t[i].path + @"\FSM\Banner.inif");
                                user.BBLX.Content = forge;
                            }
                            else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                            user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                            user.DverV.Content = t[i].version;
                        }
                        else if (tools.Tools.OptifineExist(t[i].version, ref Op))
                        {
                            BitmapImage bi = new BitmapImage();
                            Directory.CreateDirectory(t[i].path + @"\FSM");
                            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                            user.DverV.Content = t[i].version;
                            if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "Of")
                            {
                                WritePrivateProfileString("config", "icon", "Of", t[i].path + @"\FSM\Banner.inif");
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E867560.PNG", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            else
                            {
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == Op)
                            {
                                WritePrivateProfileString("config", "bio", Op, t[i].path + @"\FSM\Banner.inif");
                                user.BBLX.Content = Op;
                            }
                            else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                            user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                        }
                        else if (tools.Tools.FabricExist(t[i].version, ref fab))
                        {
                            BitmapImage bi = new BitmapImage();
                            Directory.CreateDirectory(t[i].path + @"\FSM");
                            user.DverV.Content = t[i].version;
                            if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "Fabric")
                            {
                                WritePrivateProfileString("config", "icon", "Fabric", t[i].path + @"\FSM\Banner.inif");
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\Fabric.png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            else
                            {
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == fab)
                            {
                                WritePrivateProfileString("config", "bio", fab, t[i].path + @"\FSM\Banner.inif");
                                user.BBLX.Content = fab;
                            }
                            else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                            user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                        }
                        else
                        {
                            BitmapImage bi = new BitmapImage();
                            user.DverV.Content = t[i].version;
                            Directory.CreateDirectory(t[i].path + @"\FSM");
                            if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "YB")
                            {
                                WritePrivateProfileString("config", "icon", "YB", t[i].path + @"\FSM\Banner.inif");
                            }
                            else
                            {
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "")
                            {
                                user.BBLX.Content = "原版 " + t[i].IdVersion;
                                WritePrivateProfileString("config", "bio", "原版 " + t[i].IdVersion, t[i].path + @"\FSM\Banner.inif");
                            }
                            else
                            {
                                user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif");
                            }
                            user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
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
                                Directory.CreateDirectory(t[i].path + @"\FSM");
                                if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "Forge")
                                {
                                    WritePrivateProfileString("config", "icon", "Forge", t[i].path + @"\FSM\Banner.inif");
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\ForgeN.png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                else
                                {
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == forge)
                                {
                                    WritePrivateProfileString("config", "bio", forge, t[i].path + @"\FSM\Banner.inif");
                                    user.BBLX.Content = forge;
                                }
                                else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                                user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                user.DverV.Content = t[i].version;
                            }
                            else if (tools.Tools.OptifineExist(t[i].version, ref Op))
                            {
                                BitmapImage bi = new BitmapImage();
                                Directory.CreateDirectory(t[i].path + @"\FSM");
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                user.DverV.Content = t[i].version;
                                if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "0E867560")
                                {
                                    WritePrivateProfileString("config", "icon", "0E867560", t[i].path + @"\FSM\Banner.inif");
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\0E867560.PNG", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                else
                                {
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == Op)
                                {
                                    WritePrivateProfileString("config", "bio", Op, t[i].path + @"\FSM\Banner.inif");
                                    user.BBLX.Content = Op;
                                }
                                else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                                user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                            }
                            else if (tools.Tools.FabricExist(t[i].version, ref fab))
                            {
                                BitmapImage bi = new BitmapImage();
                                Directory.CreateDirectory(t[i].path + @"\FSM");
                                user.DverV.Content = t[i].version;
                                if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "Fabric")
                                {
                                    WritePrivateProfileString("config", "icon", "Fabric", t[i].path + @"\FSM\Banner.inif");
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\Fabric.png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                else
                                {
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == fab)
                                {
                                    WritePrivateProfileString("config", "bio", fab, t[i].path + @"\FSM\Banner.inif");
                                    user.BBLX.Content = fab;
                                }
                                else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                                user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                            }
                            else
                            {
                                BitmapImage bi = new BitmapImage();
                                user.DverV.Content = t[i].version;
                                Directory.CreateDirectory(t[i].path + @"\FSM");
                                if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "YB")
                                {
                                    WritePrivateProfileString("config", "icon", "YB", t[i].path + @"\FSM\Banner.inif");
                                }
                                else
                                {
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "")
                                {
                                    user.BBLX.Content = "原版 " + t[i].IdVersion;
                                    WritePrivateProfileString("config", "bio", "原版 " + t[i].IdVersion, t[i].path + @"\FSM\Banner.inif");
                                }
                                else
                                {
                                    user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif");
                                }
                                user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                            }
                            user1.Add(user);
                        }
                        // DIYvar.lw = user1;
                        vlist.ItemsSource = user1.ToArray();
                    }
                    
                }
                // string mcPath = (sender as ListBox).SelectedItem.ToString();
                vlist.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                
            }
        }
        String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            var dialogx = new FolderSelectDialog
            {
                Title = "请选择.minecraft文件夹"
            };
            if (dialogx.Show())
            {
                var selectFolder = dialogx.FileName;
                //pathlist.Items.Add(dialog.SelectedPath);
                List<PathItem> user1 = new List<PathItem>();

                PathItem user = new PathItem();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                user.Path.Content = selectFolder;
                pathlist.Items.Add(user);
                //pathlist.ItemsSource = user1.ToArray();
            }
            try
            {

                Game.WritePrivateProfileString("VPath1", "1", pathlist.Items.Count.ToString(), File_);
                Game.WritePrivateProfileString("VPath", pathlist.Items.Count.ToString(), dialogx.FileName, File_);

            }
            catch
            {

            }
        }
        public static string NowVS;
        private void vlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pathlist.SelectedIndex == -1) return;
                if (pathlist.SelectedIndex == 0)
                {
                    MinecraftDownload minecraft = new MinecraftDownload();
                    AllTheExistingVersion[] t = new AllTheExistingVersion[0];
                    string mcPath = (pathlist.SelectedItem as PathItem).Path.Content.ToString();
                    tools.Tools.SetMinecraftFilesPath(mcPath);
                    t = tools.Tools.GetAllTheExistingVersion();
                    String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
                    Game.WritePrivateProfileString("Vlist", "V", vlist.SelectedIndex.ToString(), File_);
                Game.WritePrivateProfileString("XX", "XX", t[vlist.SelectedIndex].version, File_);
                Game.WritePrivateProfileString("XX", "IDVar", t[vlist.SelectedIndex].IdVersion, File_);
                NowVS = t[vlist.SelectedIndex].version;
                    NowVw.NowV.Content = t[vlist.SelectedIndex].version;
                Var.IDVar = t[vlist.SelectedIndex].IdVersion;
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
                    Game.WritePrivateProfileString("XX", "XX", t[vlist.SelectedIndex].version, File_);
                    NowVS = t[vlist.SelectedIndex].version;
                Var.IDVar = t[vlist.SelectedIndex].IdVersion;
                Game.WritePrivateProfileString("XX", "IDVar", t[vlist.SelectedIndex].IdVersion, File_);
                NowVw.NowV.Content = t[vlist.SelectedIndex].version;
                }
        }

        private void MouseUp(object sender, MouseButtonEventArgs e)
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
                            Directory.CreateDirectory(t[i].path + @"\FSM");
                            if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "Forge")
                            {
                                WritePrivateProfileString("config", "icon", "Forge", t[i].path + @"\FSM\Banner.inif");
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\ForgeN.png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            else
                            {
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == forge)
                            {
                                WritePrivateProfileString("config", "bio", forge, t[i].path + @"\FSM\Banner.inif");
                                user.BBLX.Content = forge;
                            }
                            else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                            user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                            user.DverV.Content = t[i].version;
                        }
                        else if (tools.Tools.OptifineExist(t[i].version, ref Op))
                        {
                            BitmapImage bi = new BitmapImage();
                            Directory.CreateDirectory(t[i].path + @"\FSM");
                            // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                            user.DverV.Content = t[i].version;
                            if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "Of")
                            {
                                WritePrivateProfileString("config", "icon", "Of", t[i].path + @"\FSM\Banner.inif");
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\0E867560.PNG", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            else
                            {
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == Op)
                            {
                                WritePrivateProfileString("config", "bio", Op, t[i].path + @"\FSM\Banner.inif");
                                user.BBLX.Content = Op;
                            }
                            else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                            user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                        }
                        else if (tools.Tools.FabricExist(t[i].version, ref fab))
                        {
                            BitmapImage bi = new BitmapImage();
                            Directory.CreateDirectory(t[i].path + @"\FSM");
                            user.DverV.Content = t[i].version;
                            if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "Fabric")
                            {
                                WritePrivateProfileString("config", "icon", "Fabric", t[i].path + @"\FSM\Banner.inif");
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\Fabric.png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            else
                            {
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == fab)
                            {
                                WritePrivateProfileString("config", "bio", fab, t[i].path + @"\FSM\Banner.inif");
                                user.BBLX.Content = fab;
                            }
                            else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                            user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                        }
                        else
                        {
                            BitmapImage bi = new BitmapImage();
                            user.DverV.Content = t[i].version;
                            Directory.CreateDirectory(t[i].path + @"\FSM");
                            if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "YB")
                            {
                                WritePrivateProfileString("config", "icon", "YB", t[i].path + @"\FSM\Banner.inif");
                            }
                            else
                            {
                                bi.BeginInit();
                                bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                bi.EndInit();
                                user.Dimage.Source = bi;
                            }
                            if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "")
                            {
                                user.BBLX.Content = "原版 " + t[i].IdVersion;
                                WritePrivateProfileString("config", "bio", "原版 " + t[i].IdVersion, t[i].path + @"\FSM\Banner.inif");
                            }
                            else
                            {
                                user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif");
                            }
                            user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
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
                                Directory.CreateDirectory(t[i].path + @"\FSM");
                                if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "Forge")
                                {
                                    WritePrivateProfileString("config", "icon", "Forge", t[i].path + @"\FSM\Banner.inif");
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\ForgeN.png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                else
                                {
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == forge)
                                {
                                    WritePrivateProfileString("config", "bio", forge, t[i].path + @"\FSM\Banner.inif");
                                    user.BBLX.Content = forge;
                                }
                                else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                                user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                user.DverV.Content = t[i].version;
                            }
                            else if (tools.Tools.OptifineExist(t[i].version, ref Op))
                            {
                                BitmapImage bi = new BitmapImage();
                                Directory.CreateDirectory(t[i].path + @"\FSM");
                                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                                user.DverV.Content = t[i].version;
                                if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "0E867560")
                                {
                                    WritePrivateProfileString("config", "icon", "0E867560", t[i].path + @"\FSM\Banner.inif");
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\0E867560.PNG", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                else
                                {
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == Op)
                                {
                                    WritePrivateProfileString("config", "bio", Op, t[i].path + @"\FSM\Banner.inif");
                                    user.BBLX.Content = Op;
                                }
                                else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                                user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                            }
                            else if (tools.Tools.FabricExist(t[i].version, ref fab))
                            {
                                BitmapImage bi = new BitmapImage();
                                Directory.CreateDirectory(t[i].path + @"\FSM");
                                user.DverV.Content = t[i].version;
                                if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "Fabric")
                                {
                                    WritePrivateProfileString("config", "icon", "Fabric", t[i].path + @"\FSM\Banner.inif");
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\Fabric.png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                else
                                {
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") == fab)
                                {
                                    WritePrivateProfileString("config", "bio", fab, t[i].path + @"\FSM\Banner.inif");
                                    user.BBLX.Content = fab;
                                }
                                else { user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif"); }
                                user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                            }
                            else
                            {
                                BitmapImage bi = new BitmapImage();
                                user.DverV.Content = t[i].version;
                                Directory.CreateDirectory(t[i].path + @"\FSM");
                                if (IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "" || IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") is "YB")
                                {
                                    WritePrivateProfileString("config", "icon", "YB", t[i].path + @"\FSM\Banner.inif");
                                }
                                else
                                {
                                    bi.BeginInit();
                                    bi.UriSource = new Uri(@"\Image\" + IniReadValueKK("config", "icon", t[i].path + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                                    bi.EndInit();
                                    user.Dimage.Source = bi;
                                }
                                if (IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is null || IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif") is "")
                                {
                                    user.BBLX.Content = "原版 " + t[i].IdVersion;
                                    WritePrivateProfileString("config", "bio", "原版 " + t[i].IdVersion, t[i].path + @"\FSM\Banner.inif");
                                }
                                else
                                {
                                    user.BBLX.Content = IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif");
                                }
                                user.Booooood.Width = (double)GetLength(IniReadValueKK("config", "bio", t[i].path + @"\FSM\Banner.inif")) * 5.20;
                            }
                            user1.Add(user);
                        }
                        // DIYvar.lw = user1;
                        vlist.ItemsSource = user1.ToArray();
                    }

                }
                // string mcPath = (sender as ListBox).SelectedItem.ToString();
                vlist.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            
        }


        private void vlist_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(vlist.SelectedIndex != -1) Var.Frame.Navigate(dyuri("/Pages/Game.xaml"));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "提示",
                PrimaryButtonText = "好吧",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock()
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Text = "整合包安装功能将会在7月上线",
                },

            };
            var result = dialog.ShowAsync();
        }
    }
}
