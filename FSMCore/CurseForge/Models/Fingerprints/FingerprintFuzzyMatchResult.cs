using System.Collections.Generic;

namespace CurseForge.APIClient.Models.Fingerprints
{
    public class FingerprintFuzzyMatchResult
    {
        public List<FingerprintFuzzyMatch> FuzzyMatches { get; set; } = new List<FingerprintFuzzyMatch>();
    }
}
