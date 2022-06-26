using CurseForge.APIClient.Models.Enums;
using System;

namespace CurseForge.APIClient.Models.Minecraft
{
    public class MinecraftModloaderInfo
    {
        public uint Id { get; set; }
        public uint GameVersionId { get; set; }
        public uint MinecraftGameVersionId { get; set; }
        public string ForgeVersion { get; set; }
        public string Name { get; set; }
        public CoreModloaderType Type { get; set; }
        public string DownloadUrl { get; set; }
        public string Filename { get; set; }
        public CoreModloaderInstallMethod InstallMethod { get; set; }
        public bool Latest { get; set; }
        public bool Recommended { get; set; }
        public bool Approved { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string MavenVersionString { get; set; }
        public string VersionJson { get; set; }
        public string LibrariesInstallLocation { get; set; }
        public string MinecraftVersion { get; set; }
        public string AdditionalFilesJson { get; set; }
        public uint ModloaderGameVersionId { get; set; }
        public uint ModloaderGameVersionTypeId { get; set; }
        public CoreGameVersionStatus ModloaderGameVersionStatus { get; set; }
        public CoreGameVersionTypeStatus ModloaderGameVersionTypeStatus { get; set; }
        public uint MCGameVersionId { get; set; }
        public uint MCGameVersionTypeId { get; set; }
        public CoreGameVersionStatus MCGameVersionStatus { get; set; }
        public CoreGameVersionTypeStatus MCGameVersionTypeStatus { get; set; }
        public string InstallProfileJson { get; set; }
    }
}
