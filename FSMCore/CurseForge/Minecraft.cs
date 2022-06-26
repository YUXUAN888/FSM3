using CurseForge.APIClient.Models;
using CurseForge.APIClient.Models.Minecraft;
using System.Threading.Tasks;

namespace CurseForge.APIClient
{
    public partial class ApiClient
    {
        public async Task<GenericListResponse<MinecraftVersionInfo>> GetMinecraftVersions(bool sortDescending = false) =>
            await GET<GenericListResponse<MinecraftVersionInfo>>(
                "/v1/minecraft/version",
                ("sortDescending", sortDescending)
            );

        public async Task<GenericResponse<MinecraftVersionInfo>> GetSpecificMinecraftVersionInfo(string gameVersionString) =>
            await GET<GenericResponse<MinecraftVersionInfo>>(
                $"/v1/minecraft/version/{gameVersionString}"
            );

        public async Task<GenericListResponse<MinecraftModloaderInfoListItem>> GetMinecraftModloaders(string version = null, bool includeAll = false) =>
            await GET<GenericListResponse<MinecraftModloaderInfoListItem>>(
                "/v1/minecraft/modloader",
                ("version", version),
                ("includeAll", includeAll)
            );

        public async Task<GenericResponse<MinecraftModloaderInfo>> GetSpecificMinecraftModloaderInfo(string modloaderName) =>
            await GET<GenericResponse<MinecraftModloaderInfo>>(
                $"/v1/minecraft/modloader/{modloaderName}"
            );
    }
}
