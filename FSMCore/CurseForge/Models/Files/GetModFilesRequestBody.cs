using System.Collections.Generic;

namespace CurseForge.APIClient.Models.Files
{
    public class GetModFilesRequestBody
    {
        public List<uint> FileIds { get; set; } = new List<uint>();
    }
}
