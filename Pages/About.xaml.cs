﻿using FSMLauncher_3;
using Gac;
using ICSharpCode.SharpZipLib.Zip;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
            System.Diagnostics.Process.Start("https://github.com/YUXUAN888/FSMLauncher3");
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
            System.Diagnostics.Process.Start("https://afdian.net/@BaiBaoStudio");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://support.qq.com/products/361169");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ZBt.zbt.Content="会飞的意大利面怪物";
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://support.qq.com/products/361169");
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
    }
}
