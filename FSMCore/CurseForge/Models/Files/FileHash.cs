using CurseForge.APIClient.Models.Enums;

namespace CurseForge.APIClient.Models.Files
{
    public class FileHash
    {
        public string Value { get; set; }
        public HashAlgo Algo { get; set; }
    }
}
