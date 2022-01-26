using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using FSM3.About_List;
using FSMLauncher_3.About_List;
using SquareMinecraftLauncher.Minecraft;
using static FSMLauncher_3.Core;

namespace FSMLauncher_3
{

    internal class DIYvar
    {
        public static List<xzItem> xzItems = new List<xzItem>();
        internal DispatcherTimer timer(EventHandler tick, int Interval)
        {
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(Interval);
            timer1.Tick += tick;
            return timer1;
        }
        internal static UnifiedPass Pass = new UnifiedPass();
        internal static Skin Skin = new Skin();

        internal static SkinName Name = new SkinName();
        /// <summary>
        /// 正式版
        /// </summary>
        internal static List<McVersionList> minecraft1 = new List<McVersionList>();
        /// <summary>
        /// 快照版
        /// </summary>
        internal static List<McVersionList> minecraft2 = new List<McVersionList>();
        /// <summary>
        /// 早期测试
        /// </summary>
        internal static List<McVersionList> minecraft3 = new List<McVersionList>();
        /// <summary>
        /// 远古版
        /// </summary>
        internal static List<McVersionList> minecraft4 = new List<McVersionList>();
        /// <summary>
        /// 愚人节版本
        /// </summary>
        internal static List<McVersionList> minecraft5 = new List<McVersionList>();

        internal static SquareMinecraftLauncher.Minecraft.AllTheExistingVersion[] ForgeGameVersion = new AllTheExistingVersion[0];
        internal static List<Item> l = new List<Item>();
        internal static List<DoItem> lw = new List<DoItem>();





    }
    internal class McVersionList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string _version, _time;
        public string version { get { return _version; } set { _version = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("version")); } }
        public string time { get { return _time; } set { _time = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("time")); } }
        internal McVersionList(string version, string time)
        {
            this._version = version;
            this._time = time;
        }

    }
}