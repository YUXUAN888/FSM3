using System;
using System.Collections.Generic;
using System.Text;

namespace MojangAPI.Model
{
    public enum SkinType 
    {
        Steve, 
        Alex
    }

    static class Extension
    {
        public static string GetModelType(this SkinType skinType)
        {
            switch (skinType)
            {
                case SkinType.Alex:
                    return "slim";
                case SkinType.Steve:
                    return "classic";
                default:
                    return "";
            }
        }
    }
}
