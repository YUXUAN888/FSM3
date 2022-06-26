using CurseForge.APIClient.Models;
using CurseForge.APIClient.Models.Mods;
using System.Threading.Tasks;

namespace CurseForge.APIClient
{
    public partial class ApiClient
    {
        public async Task<GenericListResponse<Mod>> SearchModsAsync(uint gameId, uint? classId = null, uint? categoryId = null,
            string gameVersion = null, string searchFilter = null, ModsSearchSortField? sortField = null, ModsSearchSortOrder sortOrder = ModsSearchSortOrder.Descending,
            ModLoaderType? modLoaderType = null, string slug = null, uint? gameVersionTypeId = null, uint? index = null, uint? pageSize = null)
        {
            return await GET<GenericListResponse<Mod>>("/v1/mods/search",
                ("gameId", gameId), ("classId", classId), ("categoryId", categoryId), ("gameVersion", gameVersion), ("searchFilter", searchFilter),
                ("slug", slug), ("sortField", sortField), ("sortOrder", sortOrder == ModsSearchSortOrder.Descending ? "desc" : "asc"), ("modLoaderType", modLoaderType), ("gameVersionTypeId", gameVersionTypeId),
                ("index", index), ("pageSize", pageSize));
        }

        public async Task<GenericResponse<Mod>> GetModAsync(uint modId)
        {
            return await GET<GenericResponse<Mod>>($"/v1/mods/{modId}");
        }

        public async Task<GenericResponse<string>> GetModDescriptionAsync(uint modId)
        {
            return await GET<GenericResponse<string>>($"/v1/mods/{modId}/description");
        }

        public async Task<GenericListResponse<Mod>> GetModsByIdListAsync(GetModsByIdsListRequestBody body)
        {
            return await POST<GenericListResponse<Mod>>("/v1/mods", body);
        }

        public async Task<GenericResponse<FeaturedModsResponse>> GetFeaturedModsAsync(GetFeaturedModsRequestBody body)
        {
            return await POST<GenericResponse<FeaturedModsResponse>>("/v1/mods/featured", body);
        }
    }
}
