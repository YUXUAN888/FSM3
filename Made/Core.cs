using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Gac;
using SquareMinecraftLauncher;
using SquareMinecraftLauncher.Minecraft;

namespace FSMLauncher_3
{

    internal class Core
    {
        internal DispatcherTimer timer(EventHandler tick, int Interval)
        {
            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(Interval);
            timer1.Tick += tick;
            return timer1;
        }

        public class xzItem : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            string _File, _ly, _xzwz;
            double _jd;
            public string File { get { return _File; } set { _File = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("File")); } }
            public double Template
            {
                get
                {
                    return _jd;
                }
                set
                {
                    _jd = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Template"));
                }
            }
            public string ly { get { return _ly; } set { _ly = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ly")); } }
            public string xzwz { get { return _xzwz; } set { _xzwz = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("xzwz")); } }
            public string url { get; set; }
            public string Path { get; set; }
            internal xzItem(string File, double jd, string ly, string xzwz, string url, string Path)
            {
                this._File = File;
                this._jd = jd;
                this._ly = ly;
                this._xzwz = xzwz;
                this.url = url;
                this.Path = Path;
            }



        }




    }
}