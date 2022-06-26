using SquareMinecraftLauncher.Minecraft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSM3.FSMCore.Download.FSMCoreDownload
{
    class DownProgress
    {
        public volatile static int Pro;
        public static volatile int ProX;
        public static volatile int Error;
    }
    class DownloadCore
    {
        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion
        public async static void Download(MCDownload[] MCD)
        {
            //并发Libraries
            DownProgress.ProX = 0; DownProgress.Error = 0;
            //aysnc 池

            List<Task> asyncPool = new List<Task>();
            foreach (var item in MCD)
            {
                try
                {
                    asyncPool.Add(DownloadFilesAsync(item.Url, item.path));
                }
                catch
                {
                    DownProgress.Error++;
                }
            }
            await Task.WhenAll(asyncPool);
            //主下载func
            async Task DownloadFilesAsync(string url, string filepath)
            {
                Directory.CreateDirectory(filepath + "FSM");
                byte[] dl = await new System.Net.WebClient().DownloadDataTaskAsync(url);
                Console.WriteLine("创建");
                DownProgress.
                    ProX++;
                File.WriteAllBytes(filepath, dl);
                ClearMemory();
            }
        }
        public async static void PlusDownload(string path, string url)
        {
            //并发

            //aysnc 池
            List<Task> AsyncPool = new List<Task>();
            AsyncPool.Add(DownloadFilesAsync(url, path));
            await Task.WhenAll(AsyncPool);
            //主下载func
            async Task DownloadFilesAsync(string downUrl, string filepath)
            {
                //Directory.CreateDirectory(filepath + "FSM");
                byte[] dl = await new WebClient().DownloadDataTaskAsync(downUrl);
                //Console.WriteLine("创建");
                DownProgress.Pro++;
                File.WriteAllBytes(filepath, dl);

            }
        }
        public static void Restart()
        {
            DownProgress.Pro = 0;
        }
    }
}
