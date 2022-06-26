using Downloader;
using SquareMinecraftLauncher.Minecraft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSM3.FSMCore.Download.FSMCoreDownload
{
    class DownloaderAssets
    {
        //private volatile static Dictionary<string, bool> isTimeoutPairs = new Dictionary<string, bool>();
        public volatile static int ProgressX = 0;
        public volatile static int FailedX = 0;
        public static async void DownloadCool(MCDownload[] MCD)
        {
            ParallelLoopResult result = Parallel.For(0, MCD.Length, (int i, ParallelLoopState pls) =>
            {
                Console.WriteLine("i: {0}, task : {1}", i, Task.CurrentId);
                if (!Directory.Exists(Path.GetDirectoryName(MCD[i].path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(MCD[i].path));
                }
                using (WebClient w = new WebClient())
                {
                    w.DownloadFileCompleted += W_DownloadFileCompleted;
                    w.DownloadFileAsync(new Uri(MCD[i].Url), MCD[i].path);
                }
                Thread.Sleep(9);
            });

        }

        private static void W_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                FailedX++;
            }
            else
            {
                ProgressX++;
            }
        }
    }
}
