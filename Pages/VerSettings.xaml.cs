using ModernWpf.Controls;
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
            }
            else if (VerList.IniReadValueKK("config", "icon", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif") is "Of")
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"\Image\0E867560.PNG", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                QWQ.Dimage.Source = bi;
            }
            else if (VerList.IniReadValueKK("config", "icon", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif") is "Fabric")
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"\Image\Fabric.PNG", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                QWQ.Dimage.Source = bi;
            }
            else if (VerList.IniReadValueKK("config", "icon", path + @"\versions\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif") is "YB")
            {

            }
            else
            {
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"\Image\" + VerList.IniReadValueKK("config", "icon", path + @"\version\" + Game.NowVw.NowV.Content + @"\FSM\Banner.inif"), UriKind.RelativeOrAbsolute);
                bi.EndInit();
                QWQ.Dimage.Source = bi;
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
