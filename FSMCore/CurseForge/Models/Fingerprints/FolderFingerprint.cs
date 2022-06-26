using System.Collections.Generic;

namespace CurseForge.APIClient.Models.Fingerprints
{
    public class FolderFingerprint
    {
        public string Foldername { get; set; }
        public List<long> Fingerprints { get; set; } = new List<long>();
    }
}
