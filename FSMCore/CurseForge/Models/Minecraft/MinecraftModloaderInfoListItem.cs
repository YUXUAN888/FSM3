using CurseForge.APIClient.Models.Enums;
using System;

namespace CurseForge.APIClient.Models.Minecraft
{
    public class MinecraftModloaderInfoListItem
    {
        public string Name { get; set; }
        public string GameVersion { get; set; }
        public bool Latest { get; set; }
        public bool Recommended { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public CoreModloaderType Type { get; set; }
    }
}
