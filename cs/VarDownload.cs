using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FSM3.cs
{
    class VarDownload
    {
        FSMLauncher_3.Core Core5 = new FSMLauncher_3.Core();
        static System.Windows.Threading.DispatcherTimer Jarw = new System.Windows.Threading.DispatcherTimer();
        DownloadFile dlf = new DownloadFile();
        internal void Downloadw(string path, string url)
        {
            dlf.Start(path, url);//增加下载
        }
        public static async Task<bool> Optifine()
        {
            var j = tools.Tools.GetJavaPath();
            await tools.Tools.OptifineInstall(Var.ZVar, Var.optpatch, j[0].Path);
            return true;
        }
        public static async Task<bool> Forge()
        {
            var j = tools.Tools.GetJavaPath();
            await tools.Tools.ForgeInstallation(Var.ForgePath,Var.ZVar,j[0].Path);
            return true;
        }
        public static async Task<bool> Fabric()
        {
            SquareMinecraftLauncher.Core.fabricmcs.fabricmc fb = new SquareMinecraftLauncher.Core.fabricmcs.fabricmc();
            await fb.FabricmcVersionInstall(Var.ZVar, Var.FabVar);
            return true;
        }
        public static async Task<bool> FabricAndOptifine()
        {
            return true;
        }
        public static async Task<bool> ForgeAndOptifine()
        {
            return true;
        }
    }
}
