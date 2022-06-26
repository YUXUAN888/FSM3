namespace CurseForge.APIClient.Models.Files
{
    public enum FileRelationType
    {
        EmbeddedLibrary = 1,
        OptionalDependency = 2,
        RequiredDependency = 3,
        Tool = 4,
        Incompatible = 5,
        Include = 6
    }
}
