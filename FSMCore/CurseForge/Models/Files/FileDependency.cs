namespace CurseForge.APIClient.Models.Files
{
    public class FileDependency
    {
        public uint ModId { get; set; }
        public uint FileId { get; set; }
        public FileRelationType RelationType { get; set; }
    }
}
