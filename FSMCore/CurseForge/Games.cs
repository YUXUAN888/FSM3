using CurseForge.APIClient.Models;
using CurseForge.APIClient.Models.Games;
using System.Threading.Tasks;

namespace CurseForge.APIClient
{
    public partial class ApiClient
    {
        public async Task<GenericListResponse<Game>> GetGamesAsync(uint? index = null, uint? pageSize = null)
        {
            return await GET<GenericListResponse<Game>>(
                "/v1/games",
                ("index", index),
                ("pageSize", pageSize)
            );
        }

        public async Task<GenericResponse<Game>> GetGameAsync(uint gameId)
        {
            return await GET<GenericResponse<Game>>($"/v1/games/{gameId}");
        }

        public async Task<GenericListResponse<GameVersionsByType>> GetGameVersionsAsync(uint gameId)
        {
            return await GET<GenericListResponse<GameVersionsByType>>($"/v1/games/{gameId}/versions");
        }

        public async Task<GenericListResponse<GameVersionType>> GetGameVersionTypesAsync(uint gameId)
        {
            return await GET<GenericListResponse<GameVersionType>>($"/v1/games/{gameId}/version-types");
        }
    }
}
