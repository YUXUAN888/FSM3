using System.Collections.Generic;

namespace CurseForge.APIClient.Models.Mods
{
    public class FeaturedModsResponse
    {
        public List<Mod> Featured { get; set; } = new List<Mod>();
        public List<Mod> Popular { get; set; } = new List<Mod>();
        public List<Mod> RecentlyUpdated { get; set; } = new List<Mod>();
    }
}
