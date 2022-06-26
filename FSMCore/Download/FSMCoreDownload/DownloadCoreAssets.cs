using SquareMinecraftLauncher.Minecraft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSM3.FSMCore.Download.FSMCoreDownload
{
    class DownProgressAssets
    {
        public volatile static int DownCount;

        public volatile static int FailedCount;
    }

    class DownloadCoreAssets
    {
        static DownloadCoreAssets()
        {
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            ServicePointManager.MaxServicePoints = int.MaxValue;
        }

        public async static void Download(MCDownload[] MCD, int retryCount = 0, int timeout = 10000)
        {
            DownProgressAssets.DownCount = 0;
            DownProgressAssets.FailedCount = 0;

            List<Task> asyncPool = new List<Task>();
            for(int i = 0;i< MCD.Length - 88;++i)
            {
                asyncPool.Add(DownloadFilesAsync(MCD[i].Url, MCD[i].path, retryCount, timeout));
            }
            await Task.WhenAll(asyncPool);
            async Task DownloadFilesAsync(string url, string filepath, int retry, int timeout1)
            {
                for (int i = 0; i < retry + 1; i++)
                {
                    try
                    {
                        string path = Path.GetDirectoryName(filepath);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        await new WebClient().DownloadFileTaskAsync(new Uri(url), filepath);
                        Console.WriteLine("创建");
                        DownProgressAssets.DownCount++;
                        goto SkipRetry;
                    }
                    catch { DownProgressAssets.FailedCount++; }
                }
                //DownProgressAssets.FailedCount++;
                SkipRetry:
                return;
            }
        }
    }
}
