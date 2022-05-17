using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FSM3.FSMCore.JavaFix
{
    class AutoJava
    {
        public static string GetAutoJava()
        {
            string java = null;
            MessageBox.Show(Var.IDVar);
            if (float.Parse(Var.IDVar) >= 1.17 && float.Parse(Var.IDVar) < 1.18)
            {
                for (int i = 0; i < Var.JavaB.Count; i++)
                {
                    float sum = float.Parse(Var.JavaB[i]);
                    if (sum >= 16)
                    {
                        java = Var.JavaL[i];
                    }
                }
            }
            else if (float.Parse(Var.IDVar) >= 1.18)
            {
                for (int i = 0; i < Var.JavaB.Count; i++)
                {
                    float sum = float.Parse(Var.JavaB[i]);
                    if (sum >= 17)
                    {
                        java = Var.JavaL[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < Var.JavaB.Count; i++)
                {
                    float sum = float.Parse(Var.JavaB[i]);
                    if (sum < 9)
                    {
                        java = Var.JavaL[i];
                    }
                }
            }
            return java;
        }
    }
}
