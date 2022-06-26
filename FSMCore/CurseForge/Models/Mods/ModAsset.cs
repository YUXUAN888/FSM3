namespace CurseForge.APIClient.Models.Mods
{
    public class ModAsset
    {
        public uint Id { get; set; }
        public uint ModId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Url { get; set; }
    }
}
