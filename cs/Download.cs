using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gac;
using SquareMinecraftLauncher.Minecraft;

namespace FSM3
{
    internal class DownloadFile
    {
        //这里是baibao写的，感谢baibao
        Gac.DownLoadFile downLoadFile = new Gac.DownLoadFile();
        public int All = 0;
        public int Left = 0;
        public int error = 0;

        public DownloadFile()
        {
            downLoadFile.doSendMsg += DownLoadFile_doSendMsg;//增加下载事件
        }



        private void DownLoadFile_doSendMsg(DownMsg msg)
        {
            msg downMsg = new msg();
            if (msg.Tag == DownStatus.End)
            {
                Left--;
            }
            else if (msg.Tag == DownStatus.Error)
            {
                Console.WriteLine(msg.ErrMessage);
                Left--;
                error++;
            }
            downMsg.LeftFile = Left;
            downMsg.AllFile = All;
            if (MsgEvent != null)
                MsgEvent(downMsg);
        }

        public void Start(MCDownload[] Files)
        {

            foreach (var file in Files)
            {
                AddDownload(file.path, file.Url);
            }
            All = Files.Length;
            Left = Files.Length;
        }
        public void Thread(int threads)
        {
            downLoadFile.ThreadNum = threads;
        }
        public void Start(string path, string url)
        {
            AddDownload(path, url);
            All++;
            Left++;
        }

        public static int id = 0;
        int AddDownload(string path, string url)
        {
            downLoadFile.AddDown(url, path.Replace(System.IO.Path.GetFileName(path), ""), System.IO.Path.GetFileName(path), id);//增加下载
            downLoadFile.StartDown(3);//开始下载
            id++;
            return id - 1;
        }

        public event MsgDel MsgEvent;
        public delegate void MsgDel(msg Log);

        public bool EndDownload()
        {
            if (Left == 0) return true;
            return false;
        }

        public void Close()
        {
            All = 0;
            Left = 0;
            error = 0;
        }

    }

    public class msg
    {
        public int AllFile { get; set; }
        public int LeftFile { get; set; }
        public int Error { get; set; }
    }
}
