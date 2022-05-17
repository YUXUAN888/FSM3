using FSM3.About_List;
using ModernWpf.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace FSM3.Pages
{
    /// <summary>
    /// VerSettings.xaml 的交互逻辑
    /// </summary>
    public partial class VerSettings : System.Windows.Controls.Page
    {
        string path;
        public VerSettings()
        {
            InitializeComponent();
            VersionName.Content = Game.NowVw.NowV.Content;
            QWQ.DverV.Content = Game.NowVw.NowV.Content;
            int a = int.Parse(Game.IniReadValue("Vlist", "Path"));
            String b = (a + 1).ToString();
            if (b is "1")
            {
                path = Game.Dminecraft;
            }
            else
            {
                path = Game.IniReadValue("VPath", b);
            }
            if (VerList.IniReadValueKK("config", "icon", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif") is "Forge")
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"\Image\ForgeN.png", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                QWQ.Dimage.Source = bi;
                Com.SelectedIndex = 3;
            }
            else if (VerList.IniReadValueKK("config", "icon", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif") is "0E867560")
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"\Image\0E867560.PNG", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                QWQ.Dimage.Source = bi;
                Com.SelectedIndex = 2;
            }
            else if (VerList.IniReadValueKK("config", "icon", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif") is "Fabric")
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"\Image\Fabric.PNG", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                QWQ.Dimage.Source = bi;
                Com.SelectedIndex = 4;
            }
            else if (VerList.IniReadValueKK("config", "icon", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif") is "YB")
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"\Image\" + "0E885BB0.PNG", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                QWQ.Dimage.Source = bi;
                Com.SelectedIndex = 1;
            }
            else if(VerList.IniReadValueKK("config", "icon", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif") is "0E830D70")
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"\Image\" + "0E830D70.PNG", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                QWQ.Dimage.Source = bi;
                Com.SelectedIndex = 5;
            }
            else
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"\Image\" + VerList.IniReadValueKK("config", "icon", path + @"\version\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif") + ".png", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                QWQ.Dimage.Source = bi;
                Com.SelectedIndex = 0;
            }
            QWQ.BBLX.Content = VerList.IniReadValueKK("config", "bio", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif");
            QWQ.Booooood.Width = (double)VerList.GetLength(VerList.IniReadValueKK("config", "bio", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif")) * 5.20;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QWQ_Loaded(object sender, RoutedEventArgs e)
        {

        }
        string forge, of, fab;
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Com.SelectedIndex)
            {
                case 0:
                    Game.WritePrivateProfileString("config", "icon", "", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif");
                    if (tools.Tools.ForgeExist(Game.NowVw.NowV.Content.ToString(), ref forge))
                    {
                        BitmapImage bix3 = new BitmapImage();
                        bix3.BeginInit();
                        bix3.UriSource = new Uri(@"\Image\ForgeN.png", UriKind.RelativeOrAbsolute);
                        bix3.EndInit();
                        QWQ.Dimage.Source = bix3;
                    }
                    else if (tools.Tools.OptifineExist(Game.NowVw.NowV.Content.ToString(), ref forge))
                    {
                        BitmapImage bi42 = new BitmapImage();
                        bi42.BeginInit();
                        bi42.UriSource = new Uri(@"\Image\0E867560.png", UriKind.RelativeOrAbsolute);
                        bi42.EndInit();
                        QWQ.Dimage.Source = bi42;
                    }
                    else if (tools.Tools.FabricExist(Game.NowVw.NowV.Content.ToString(), ref forge))
                    {
                        BitmapImage bi2x = new BitmapImage();
                        bi2x.BeginInit();
                        bi2x.UriSource = new Uri(@"\Image\Fabric.png", UriKind.RelativeOrAbsolute);
                        bi2x.EndInit();
                        QWQ.Dimage.Source = bi2x;
                    }
                    else
                    {
                        BitmapImage bis = new BitmapImage();
                        bis.BeginInit();
                        bis.UriSource = new Uri(@"\Image\0E885BB0.png", UriKind.RelativeOrAbsolute);
                        bis.EndInit();
                        QWQ.Dimage.Source = bis;
                    }
                    break;
                case 1:
                    Game.WritePrivateProfileString("config", "icon", "YB", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif");
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(@"\Image\0E885BB0.png", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    QWQ.Dimage.Source = bi;
                    break;
                case 2:
                    Game.WritePrivateProfileString("config", "icon", "0E867560", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif");
                    BitmapImage bi4 = new BitmapImage();
                    bi4.BeginInit();
                    bi4.UriSource = new Uri(@"\Image\0E867560.png", UriKind.RelativeOrAbsolute);
                    bi4.EndInit();
                    QWQ.Dimage.Source = bi4;
                    break;
                case 3:
                    Game.WritePrivateProfileString("config", "icon", "Forge", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif");
                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(@"\Image\ForgeN.png", UriKind.RelativeOrAbsolute);
                    bi3.EndInit();
                    QWQ.Dimage.Source = bi3;
                    break;
                case 4:
                    Game.WritePrivateProfileString("config", "icon", "Fabric", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif");
                    BitmapImage bi2 = new BitmapImage();
                    bi2.BeginInit();
                    bi2.UriSource = new Uri(@"\Image\Fabric.png", UriKind.RelativeOrAbsolute);
                    bi2.EndInit();
                    QWQ.Dimage.Source = bi2;
                    break;
                case 5:
                    Game.WritePrivateProfileString("config", "icon", "0E830D70", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif");
                    BitmapImage bi1 = new BitmapImage();
                    bi1.BeginInit();
                    bi1.UriSource = new Uri(@"\Image\0E830D70.png", UriKind.RelativeOrAbsolute);
                    bi1.EndInit();
                    QWQ.Dimage.Source = bi1;
                    break;
            }
        }
        [DllImport("kernel32", CharSet = CharSet.Unicode)]

        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public static string IniReadValue(string Section, string Key)
        {
            String File_ = System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\FSM.slx";
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, File_);
            return temp.ToString();
        }
        private void Load(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(path + @"\versions\" + Game.NowVw.NowV.Content);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MODLIST.Items.Clear();
            GetFiles file = new GetFiles();
            var a = file.Readlist(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods", "jar");
            for(int i = 0; i < a.Length; i++)
            {
                string g = a[i].Replace(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods\","");
                MODLISTP mp = new MODLISTP();
                mp.Name.Content = g.Replace(".jar", "");
                BitmapImage bi = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                bi.BeginInit();
                bi.UriSource = new Uri(@"\Image\ModON.PNG", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                mp.Imagew.Source = bi;
                MODLIST.Items.Add(mp);
            }
            var b = file.Readlist(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods", "MODOFF");
            for (int i = 0; i < b.Length; i++)
            {
                string g = b[i].Replace(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods\", "");
                MODLISTP mp = new MODLISTP();
                mp.Name.Content = g.Replace(".MODOFF", "") + "(已禁用)";
                BitmapImage bi = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                bi.BeginInit();
                bi.UriSource = new Uri(@"\Image\ModOFF.PNG", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                mp.Imagew.Source = bi;
                MODLIST.Items.Add(mp);
            }
        }
        // 获得指定文件夹下指定后缀的所有文件
        class GetFiles
        {
            ArrayList alst;
            // 获得文件夹中指定后缀的文件
            // dir是文件夹，extension是后缀
            public void GetFile(string dir, string extension)
            {
                try
                {
                    string[] files = Directory.GetFiles(dir);
                    foreach (string file in files)
                    {
                        string exname = file.Substring(file.LastIndexOf(".") + 1);
                        if (extension.IndexOf(file.Substring(file.LastIndexOf(".") + 1)) > -1)
                        {
                            FileInfo fi = new FileInfo(file);
                            alst.Add(fi.FullName);
                        }
                    }
                }
                catch
                {
                }
            }
            // 获得文件夹中的文件及其子文件夹中的文件
            public void GetDirs(string path, string extension)
            {
                GetFile(path, extension);
                try
                {
                    string[] dirs = Directory.GetDirectories(path);
                    foreach (string dir in dirs)
                    {
                        GetDirs(dir, extension);
                    }
                }
                catch
                {
                }
            }
            // 获得指定文件夹下指定后缀的所有文件
            public string[] Readlist(string path, string extension)
            {
                alst = new ArrayList();  // 清空alst
                GetDirs(path, extension);
                return (string[])alst.ToArray(typeof(string));
            }
        }

        private void MODLIST_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((MODLIST.SelectedItem as MODLISTP).Name.Content.ToString().EndsWith("(已禁用)"))
                {
                    string str = (MODLIST.SelectedItem as MODLISTP).Name.Content.ToString().Replace("(已禁用)", "");
                    FileInfo f = new FileInfo(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods" + @"\" + str + ".MODOFF");
                    f.MoveTo(System.IO.Path.ChangeExtension(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods" + @"\" + str + ".MODOFF", ".jar"));
                }
                else
                {
                    FileInfo f = new FileInfo(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods" + @"\" + (MODLIST.SelectedItem as MODLISTP).Name.Content.ToString() + ".jar");
                    //MessageBox.Show(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods\" + (MODLIST.SelectedItem as MODLISTP).Name.Content.ToString());
                    f.MoveTo(System.IO.Path.ChangeExtension(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods\" + (MODLIST.SelectedItem as MODLISTP).Name.Content.ToString() + ".jar", ".MODOFF"));
                }
                MODLIST.Items.Clear();
                GetFiles file = new GetFiles();
                var a = file.Readlist(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods", "jar");
                for (int i = 0; i < a.Length; i++)
                {
                    string g = a[i].Replace(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods\", "");
                    MODLISTP mp = new MODLISTP();
                    mp.Name.Content = g.Replace(".jar", "");
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(@"\Image\ModON.PNG", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    mp.Imagew.Source = bi;
                    MODLIST.Items.Add(mp);
                }
                var b = file.Readlist(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods", "MODOFF");
                for (int i = 0; i < b.Length; i++)
                {
                    string g = b[i].Replace(path + @"\versions\" + Game.NowVw.NowV.Content + @"\mods\", "");
                    MODLISTP mp = new MODLISTP();
                    mp.Name.Content = g.Replace(".MODOFF", "") + "(已禁用)";
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.  
                    bi.BeginInit();
                    bi.UriSource = new Uri(@"\Image\ModOFF.PNG", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    mp.Imagew.Source = bi;
                    MODLIST.Items.Add(mp);
                }
            }
            catch
            {

            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StackPanel panel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };
            panel.Children.Add(new TextBlock() { Text = "更改新的描述" });
            TextBox box = new TextBox();
            box.SetCurrentValue(ModernWpf.Controls.Primitives.ControlHelper.HeaderProperty, "若不填写则不改变");
            panel.Children.Add(box);

            ContentDialog dialog = new ContentDialog()
            {
                Title = "请输入新描述",
                PrimaryButtonText = "好",
                CloseButtonText = "取消",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = panel,
            };
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if(box.Text is "") { } else
                {
                    Game.WritePrivateProfileString("config", "bio",box.Text, path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif");
                    QWQ.BBLX.Content = box.Text;
                }
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "确 定 吗 ?",
                PrimaryButtonText = "确定!",
                CloseButtonText = "取消",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock()
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Text = "如果此版本是有模组加载器，则模组和存档会消失很久，真的很久；如果没有加载器，则不会丢失您的数据"
                },

            };
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                Directory.Delete(path + @"\versions\" + Game.NowVw.NowV.Content, true);
                ContentDialog dialogs = new ContentDialog()
                {
                    Title = "已删除此版本",
                    PrimaryButtonText = "好哒!",
                    IsPrimaryButtonEnabled = true,
                    DefaultButton = ContentDialogButton.Primary,
                    Content = new TextBlock()
                    {
                        TextWrapping = TextWrapping.WrapWithOverflow,
                        Text = "没有后悔药啦~"
                    },

                };
                var results = await dialogs.ShowAsync();
            }
            else if (result == ContentDialogResult.None)
            {
                
            }
        }
    }
}
