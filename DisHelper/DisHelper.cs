using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FSM3.DisHelper
{
    public static class DisHelper
    {
        public static void RunOnMainThread(Action action)
        {
            RunOnUIThread(Application.Current, action);
        }
        public static void RunOnUIThread(this DispatcherObject d, Action action)
        {
            var dispatcher = d.Dispatcher;
            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.BeginInvoke(action);
            }
        }
    }
}
