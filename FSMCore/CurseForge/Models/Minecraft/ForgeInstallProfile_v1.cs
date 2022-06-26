using System;
using System.Collections.Generic;

namespace CurseForge.APIClient.Models.Minecraft
{
    public class ForgeInstallProfile_v1
    {
        public ForgeInstallProfileInstall Install { get; set; }
        public ForgeInstallProfileVersionInfo VersionInfo { get; set; }

        public class ForgeInstallProfileInstall
        {
            public string ProfileName { get; set; }
            public string Target { get; set; }
            public string Path { get; set; }
            public string Version { get; set; }
            public string FilePath { get; set; }
            public string Welcome { get; set; }
            public string Minecraft { get; set; }
            public string MirrorList { get; set; }
            public string Logo { get; set; }
            public string ModList { get; set; }
        }

        public class ForgeInstallProfileVersionInfo
        {
            public string Id { get; set; }
            public DateTimeOffset Time { get; set; }
            public string ReleaseTime { get; set; }
            public string Type { get; set; }
            public string MinecraftArguments { get; set; }
            public List<ModloaderLibrary> Libraries { get; set; } = new List<ModloaderLibrary>();
            public string MainClass { get; set; }
            public int MinimumLauncherVersion { get; set; }
        }

        public class ModloaderLibrary
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public bool ServerReq { get; set; }
            public List<string> Checksums { get; set; } = new List<string>();
            public string Comment { get; set; }
            public bool ClientReq { get; set; }
            public Dictionary<string, string> Natives { get; set; } = new Dictionary<string, string>();
        }
    }
}
