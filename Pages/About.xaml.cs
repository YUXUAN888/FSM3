using FSM3.About_List;
using FSM3.DisHelper;
using FSMLauncher_3;
using Gac;
using ICSharpCode.SharpZipLib.Zip;
using ModernWpf.Controls;
using ModernWpf.Media.Animation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
using static FSM3.MainWindow;
using FSM3.FSMCore.Download.FSMCoreDownload;
using SquareMinecraftLauncher.Minecraft;
using Utils;

namespace FSM3.Pages
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : System.Windows.Controls.Page
    {
        public About()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://afdian.net/@YUXUAN233");
        }

        private void Button_Click_36(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://blog.qwq.one");
        }

        private void Tile_Click_16(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/YUXUAN888/FSM3");
        }

        private void Button_Click_37(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://blog.qwq.one");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/baibao132");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://support.qq.com/products/361169");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ZBt.zbt.Content="会飞的意大利面怪物";
        }
        private NavigationTransitionInfo _transitionInfo = null;
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {     
            _transitionInfo = new DrillInNavigationTransitionInfo();
            FSM3.framecontrol.frame.Navigate(dyuri("/Pages/FK.xaml"), null, _transitionInfo);
            //System.Diagnostics.Process.Start("https://support.qq.com/products/361169");
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
        /// <summary>  
        /// 解压缩文件(压缩文件中含有子目录)  
        /// </summary>  
        /// <param name="zipfilepath">待解压缩的文件路径</param>  
        /// <param name="unzippath">解压缩到指定目录</param>  
        /// <returns>解压后的文件列表</returns>  
        public List<string> UnZipw(string zipfilepath, string unzippath)
        {
            //解压出来的文件列表  
            List<string> unzipFiles = new List<string>();

            //检查输出目录是否以“\\”结尾  
            if (unzippath.EndsWith("\\") == false || unzippath.EndsWith(":\\") == false)
            {
                unzippath += "\\";
            }

            ZipInputStream s = new ZipInputStream(File.OpenRead(zipfilepath));
            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = System.IO.Path.GetDirectoryName(unzippath);
                string fileName = System.IO.Path.GetFileName(theEntry.Name);

                //生成解压目录【用户解压到硬盘根目录时，不需要创建】  
                if (!string.IsNullOrEmpty(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }

                if (fileName != String.Empty)
                {
                    //如果文件的压缩后大小为0那么说明这个文件是空的,因此不需要进行读出写入  
                    if (theEntry.CompressedSize == 0)
                        continue;
                    //解压文件到指定的目录  
                    directoryName = System.IO.Path.GetDirectoryName(unzippath + theEntry.Name);
                    //建立下面的目录和子目录  
                    Directory.CreateDirectory(directoryName);

                    //记录导出的文件  
                    unzipFiles.Add(unzippath + theEntry.Name);

                    FileStream streamWriter = File.Create(unzippath + theEntry.Name);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                }
            }
            s.Close();
            GC.Collect();
            return unzipFiles;
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ZBt.zbt.Content = "准  备  就  绪";
            dlf.doSendMsg += new DownLoadFile.dlgSendMsg(SendMsgHander);
            ZWindoww.Zwindow.IsEnabled = false;
            Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\？？？？？？");
            Game.did3 = Download(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\？？？？？？\bgm.wav", "", "http://fsm.ft2.club/bailan/bgm.wav");
            Game.did2 = Download(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\？？？？？？\bailan.zip", "", "http://fsm.ft2.club/bailan/bailan.zip");
            Game.did1 = Download(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\？？？？？？\CD.exe", "", "http://fsm.ft2.club/bailan/CD.exe");
            Game.ONLINEW = Core5.timer(OnQ, 2333);
            Game.ONLINEW.Start();
        }

        private async void OnQ(object ob, EventArgs a)
        {
            if (DIYvar.xzItems[Game.did3].xzwz == "完成"&& DIYvar.xzItems[Game.did2].xzwz == "完成"&& DIYvar.xzItems[Game.did1].xzwz == "完成")
            {
                await Task.Run(() => Thread.Sleep(888));
                Game.ONLINEW.Stop();
                ZBt.zbt.Content = "新年快乐!!!";
                ZWindoww.Zwindow.IsEnabled = true;
                ZBt.zbt.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                if(UnZip(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\？？？？？？\bailan.zip", System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\？？？？？？"))
                {
                    System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\？？？？？？\CD.exe");
                }
                
                
               // var player = new SoundPlayer(System.AppDomain.CurrentDomain.BaseDirectory + @"FSM\？？？？？？\bgm.wav");
               // player.Play();
            }
        }
        public class UnZipClass
        {
            /// <summary>
            /// 解压功能(解压压缩文件到指定目录)
            /// </summary>
            /// <param name="FileToUpZip">待解压的文件</param>
            /// <param name="ZipedFolder">指定解压目标目录</param>
            public static void UnZip(string FileToUpZip, string ZipedFolder, string Password)
            {
                if (!File.Exists(FileToUpZip))
                {
                    return;
                }

                if (!Directory.Exists(ZipedFolder))
                {
                    Directory.CreateDirectory(ZipedFolder);
                }

                ZipInputStream s = null;
                ZipEntry theEntry = null;

                string fileName;
                FileStream streamWriter = null;
                try
                {
                    s = new ZipInputStream(File.OpenRead(FileToUpZip));
                    s.Password = Password;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        if (theEntry.Name != String.Empty)
                        {
                            fileName = System.IO.Path.Combine(ZipedFolder, theEntry.Name);
                            ///判断文件路径是否是文件夹
                            if (fileName.EndsWith("/") || fileName.EndsWith("//"))
                            {
                                Directory.CreateDirectory(fileName);
                                continue;
                            }
                            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fileName));
                            streamWriter = File.Create(fileName);
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    if (streamWriter != null)
                    {
                        streamWriter.Close();
                        streamWriter = null;
                    }
                    if (theEntry != null)
                    {
                        theEntry = null;
                    }
                    if (s != null)
                    {
                        s.Close();
                        s = null;
                    }
                    GC.Collect();
                    GC.Collect(1);
                }
            }
        }
        /// <summary>
        /// 解压功能(解压压缩文件到指定目录)
        /// </summary>
        /// <param name="fileToUnZip">待解压的文件</param>
        /// <param name="zipedFolder">指定解压目标目录</param>
        /// <param name="password">密码</param>
        /// <returns>解压结果</returns>
        public static bool UnZip(string fileToUnZip, string zipedFolder, string password)
        {
            bool result = true;
            FileStream fs = null;
            ZipInputStream zipStream = null;
            ZipEntry ent = null;
            string fileName;

            if (!File.Exists(fileToUnZip))
                return false;

            if (!Directory.Exists(zipedFolder))
                Directory.CreateDirectory(zipedFolder);

            try
            {
                zipStream = new ZipInputStream(File.OpenRead(fileToUnZip));
                if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                while ((ent = zipStream.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrEmpty(ent.Name))
                    {
                        fileName = System.IO.Path.Combine(zipedFolder, ent.Name);
                        fileName = fileName.Replace('/', '\\');//change by Mr.HopeGi

                        int index = ent.Name.LastIndexOf('/');
                        if (index != -1 || fileName.EndsWith("\\"))
                        {
                            string tmpDir = (index != -1 ? fileName.Substring(0, fileName.LastIndexOf('\\')) : fileName) + "\\";
                            if (!Directory.Exists(tmpDir))
                            {
                                Directory.CreateDirectory(tmpDir);
                            }
                            if (tmpDir == fileName)
                            {
                                continue;
                            }
                        }

                        fs = File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[size];
                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);
                            if (size > 0)
                                fs.Write(data, 0, data.Length);
                            else
                                break;
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (zipStream != null)
                {
                    zipStream.Close();
                    zipStream.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return result;
        }
        /// <summary>
        /// 解压功能(解压压缩文件到指定目录)
        /// </summary>
        /// <param name="fileToUnZip">待解压的文件</param>
        /// <param name="zipedFolder">指定解压目标目录</param>
        /// <returns>解压结果</returns>
        public static bool UnZip(string fileToUnZip, string zipedFolder)
        {
            bool result = UnZip(fileToUnZip, zipedFolder, null);
            return result;
        }
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        public Gac.DownLoadFile dlf = new DownLoadFile();
        internal int Download(string path, string ly, string url)
        {

            dlf.AddDown(url, path.Replace(System.IO.Path.GetFileName(path), ""), System.IO.Path.GetFileName(path), id);//增加下载
            dlf.StartDown(3);//开始下载
            id++;
            Core.xzItem xzItem = new Core.xzItem(System.IO.Path.GetFileName(path), 0, ly, "等待中", url, path);
            DIYvar.xzItems.Add(xzItem);


            return id - 1;
        }

        private async void Button_Click_sbm(object sender, RoutedEventArgs e)
        {
            string code = null;
            SelectQuery query = new SelectQuery("select * from Win32_ComputerSystemProduct");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (var item in searcher.Get())
                {
                    using (item) code = item["UUID"].ToString();
                }
            }
            Clipboard.SetDataObject(code);
            ContentDialog dialog = new ContentDialog()
            {
                Title = "已复制识别码",
                PrimaryButtonText = "好哒!",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock()
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Text = "请到内群解锁"
                },

            };
            await dialog.ShowAsync();
        }
        public static string jiema(string s)
        {
            System.Text.RegularExpressions.CaptureCollection cs =
               System.Text.RegularExpressions.Regex.Match(s, @"([01]{8})+").Groups[1].Captures;
            byte[] data = new byte[cs.Count];
            for (int i = 0; i < cs.Count; i++)
            {
                data[i] = Convert.ToByte(cs[i].Value, 2);
            }
            return Encoding.Unicode.GetString(data, 0, data.Length);
        }
        public static string bianma(string s)
        {
            byte[] data = Encoding.Unicode.GetBytes(s);
            StringBuilder result = new StringBuilder(data.Length * 8);

            foreach (byte b in data)
            {
                result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return result.ToString();
        }
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串 </returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));//转换为字节
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();//实例化数据加密标准
                MemoryStream mStream = new MemoryStream();//实例化内存流
                //将数据流链接到加密转换的流
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        public static bool YJS;
        public static uint SND_ASYNC = 0x0001;
        public static uint SND_FILENAME = 0x00020000;

        class PlayMusic
        {
            [DllImport("winmm.dll")]
            public static extern bool PlaySound(String Filename, int Mod, int Flags);

            public void Main()
            {
                PlaySound(@"D:\TestMusic.wav", 0, 1); //第3个形参，把1换为9，连续播放
            }
        }
        private void FixJS_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String JS = JS1.Text;
                string code = null;
                SelectQuery query = new SelectQuery("select * from Win32_ComputerSystemProduct");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (var item in searcher.Get())
                    {
                        using (item) code = item["UUID"].ToString();
                    }
                }
                string zz = DecryptDES(JS,"87654321");
                if (zz == code + "SHILI")
                {
                    Game.WritePrivateProfileString("JSM", "JSM", EncryptDES(code, "87654321"), Game.FileS);
                    SoundPlayer player = new SoundPlayer();
                    player.SoundLocation = ZongW + @"\Fix.wav";
                    player.Play();
                    YJS = true;
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "解锁成功",
                        PrimaryButtonText = "好哒!",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "感谢您使用FSM启动器,付费功能已解锁"
                        },

                    };
                    dialog.ShowAsync();
                }
                else
                {
                    ContentDialog dialog = new ContentDialog()
                    {
                        Title = "解锁失败",
                        PrimaryButtonText = "好吧",
                        IsPrimaryButtonEnabled = true,
                        DefaultButton = ContentDialogButton.Primary,
                        Content = new TextBlock()
                        {
                            TextWrapping = TextWrapping.WrapWithOverflow,
                            Text = "可能是解锁码错误了"
                        },

                    };
                    dialog.ShowAsync();
                }
            }
            catch
            {

            }
        }

        private void Button_Click_Help(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://support.qq.com/products/361169/faqs-more/");
        }

        private async void Button_Click_Download(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "啊不能点嘛",
                PrimaryButtonText = "好吧",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock()
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Text = "超级下载还在施工中..."
                },

            };
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //3.自定义文件夹框
            var dialog = new FolderSelectDialog
            {
                Title = "选择文件夹"
            };
            if (dialog.Show())
            {
                var selectFolder = dialog.FileName;
                Path.Text = selectFolder;
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(FSMCore.JavaFix.AutoJava.GetAutoJava(www.Text));
        }

        private async void Button_Click_9(object sender, RoutedEventArgs e)
        {
            var b = await FSMCore.CurseForgeAPI.V1.Get.GetModsV1();
            for(int i = 0; i < b.Data.Popular.Count; ++i)
            {
                Console.WriteLine(b.Data.Popular[i].Name);
            }
        }

        private void Button_Click_C(object sender, RoutedEventArgs e)
        {
            _transitionInfo = new DrillInNavigationTransitionInfo();
            //FSM3.framecontrol.frame.Navigate(dyuri("/Pages/Channel.xaml"), null, _transitionInfo);
            //Console.WriteLine(Get("http://124.221.215.96:321/matomo.php?idsite=1&amp;rec=1"));
            ContentDialog dialog = new ContentDialog()
            {
                Title = "啊不能点嘛",
                PrimaryButtonText = "好吧",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock()
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Text = "频道目前还在施工中..."
                },

            };
            dialog.ShowAsync();
        }

        private void Button_Click_Note(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "啊不能点嘛",
                PrimaryButtonText = "好吧",
                IsPrimaryButtonEnabled = true,
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBlock()
                {
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    Text = "笔记目前还在施工中..."
                },

            };
            dialog.ShowAsync();
        }
    }
}
