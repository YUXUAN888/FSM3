using CurseForge.APIClient.Models.Files;
using System.Collections.Generic;

namespace CurseForge.APIClient.Models.Fingerprints
{
    public class FingerprintFuzzyMatch
    {
        public uint Id { get; set; }
        public File File { get; set; }
        public List<File> LatestFiles { get; set; } = new List<File>();
        public List<long> Fingerprints { get; set; } = new List<long>();
    }
}
