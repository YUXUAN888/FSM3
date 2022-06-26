using CurseForge.APIClient.Models.Games;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CurseForge.APIClient.Models.Files
{
    public class File
    {
        [JsonProperty("id")]
        public uint Id { get; set; }
        public uint GameId { get; set; }
        public uint ModId { get; set; }
        public bool IsAvailable { get; set; }
        public string DisplayName { get; set; }
        public string FileName { get; set; }
        public FileReleaseType ReleaseType { get; set; }
        public FileStatus FileStatus { get; set; }
        public List<FileHash> Hashes { get; set; } = new List<FileHash>();
        public DateTimeOffset FileDate { get; set; }
        public ulong FileLength { get; set; }
        public ulong DownloadCount { get; set; }
        public string DownloadUrl { get; set; }
        public List<string> GameVersions { get; set; } = new List<string>();
        public List<SortableGameVersion> SortableGameVersions { get; set; } = new List<SortableGameVersion>();
        public List<FileDependency> Dependencies { get; set; } = new List<FileDependency>();
        public bool? ExposeAsAlternative { get; set; }
        public uint? ParentProjectFileId { get; set; }
        public uint? AlternateFileId { get; set; }
        public bool? IsServerPack { get; set; }
        public uint? ServerPackFileId { get; set; }
        public long FileFingerprint { get; set; }
        public List<FileModule> Modules { get; set; } = new List<FileModule>();
    }
}
