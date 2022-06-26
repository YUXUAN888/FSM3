using System;

namespace CurseForge.APIClient.Models.Games
{
    public class SortableGameVersion
    {
        public string GameVersionName { get; set; }
        public string GameVersionPadded { get; set; }
        public string GameVersion { get; set; }
        public DateTimeOffset GameVersionReleaseDate { get; set; }
        public uint? GameVersionTypeId { get; set; }
    }
}
