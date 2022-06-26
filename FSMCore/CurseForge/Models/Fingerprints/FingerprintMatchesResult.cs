using System.Collections.Generic;

namespace CurseForge.APIClient.Models.Fingerprints
{
    public class FingerprintMatchesResult
    {
        public bool IsCacheBuilt { get; set; }
        public List<FingerprintMatch> ExactMatches { get; set; } = new List<FingerprintMatch>();
        public List<long> ExactFingerprints { get; set; } = new List<long>();
        public List<FingerprintMatch> PartialMatches { get; set; } = new List<FingerprintMatch>();
        public object PartialMatchFingerprints { get; set; }
        public List<long> AdditionalProperties { get; set; } = new List<long>();
        public List<long> InstalledFingerprints { get; set; } = new List<long>();
        public List<long> UnmatchedFingerprints { get; set; } = new List<long>();
    }
}
