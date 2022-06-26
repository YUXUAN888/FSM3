using System.Collections.Generic;

namespace CurseForge.APIClient.Models.Mods
{
    public class GetFeaturedModsRequestBody
    {
        public uint GameId { get; set; }
        public List<uint> ExcludedModIds { get; set; } = new List<uint>();
        public uint? GameVersionTypeId { get; set; }
    }
}
