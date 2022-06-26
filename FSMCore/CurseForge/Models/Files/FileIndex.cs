using CurseForge.APIClient.Models.Mods;

namespace CurseForge.APIClient.Models.Files
{
    public class FileIndex
    {
        public string GameVersion { get; set; }
        public uint FileId { get; set; }
        public string Filename { get; set; }
        public FileReleaseType ReleaseType { get; set; }
        public uint? GameVersionTypeId { get; set; }
        public ModLoaderType? ModLoader { get; set; }
    }
}
