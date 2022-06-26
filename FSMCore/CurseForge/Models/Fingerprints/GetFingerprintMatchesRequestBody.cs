using System.Collections.Generic;

namespace CurseForge.APIClient.Models.Fingerprints
{
    public class GetFingerprintMatchesRequestBody
    {
        public List<long> Fingerprints {  get; set;} = new List<long>();
    }
}
