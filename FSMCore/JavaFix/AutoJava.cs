using SquareMinecraftLauncher.Minecraft;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace FSM3.FSMCore.JavaFix
{
    class AutoJava
    {
        /// <summary>
        /// 获取指定文件内容
        /// 注：这里适用于读取文本类型文件
        /// </summary>
        public static string GetTxtFileContent(string fileName)
        {
            if (!File.Exists(fileName)) //文件不存在
                return null;
            return File.ReadAllText(fileName);
        }
        public static string GetAutoJava(string version)
        {
            string pc = null;
            ComboBox FH = new ComboBox();
            string x = null;
            ComboBox ZList = new ComboBox();
            List<JavaVersion> aa = tools.Tools.GetJavaPath();
            string sPath = Environment.GetEnvironmentVariable("JAVA_HOME");
            for (int i = 0; i < aa.Count; i++)
            {
                ZList.Items.Add(aa[i].Path);
            }
            if (sPath != null) { ZList.Items.Add(sPath); }
            int pcc = 0;
            for (int i = 0; i < ZList.Items.Count; i++)
            {
                string s = ZList.Items[i].ToString();
                FH.Items.Add(FileVersionInfo.GetVersionInfo(s).FileVersion);
            }
            for (int ix = 0; ix < FH.Items.Count; ++ix)
            {
                string[] c = version.Split('.');
                float a = float.Parse(c[1]);
                string[] b = FH.Items[ix].ToString().Split('.');
                if (a >= 18)
                {
                    if (float.Parse(b[0]) >= 17)
                    {
                        pcc = ix;
                        pc = (string)ZList.Items[pcc];
                        break;
                    }
                    else
                    {
                        pc = "无法找到对应Java，请手动选择";
                    }
                }
                else if (a >= 17 && a < 18)
                {
                    if (float.Parse(b[0]) >= 16)
                    {
                        pcc = ix;
                        pc = (string)ZList.Items[pcc];
                        break;
                    }
                    else
                    {
                        pc = "无法找到对应Java，请手动选择";
                    }
                }
                else
                {
                    if (float.Parse(b[0]) <= 8)
                    {
                        pcc = ix;
                        pc = (string)ZList.Items[pcc];
                        break;
                    }
                    else
                    {
                        pc = "无法找到对应Java，请手动选择";
                    }
                }
            }
            return pc;
        }
    }
}
