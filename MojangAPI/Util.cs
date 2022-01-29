using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MojangAPI
{
    class Util
    {
        internal static string ReplaceInvalidChars(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
