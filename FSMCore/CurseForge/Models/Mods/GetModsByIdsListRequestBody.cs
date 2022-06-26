using System.Collections.Generic;

namespace CurseForge.APIClient.Models.Mods
{
    public class GetModsByIdsListRequestBody
    {
        public List<uint> ModIds { get; set; } = new List<uint>();
    }
}
