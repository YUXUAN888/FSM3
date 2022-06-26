using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurseForge.APIClient.Models.Mods;
using CurseForge;

namespace FSM3.FSMCore.CurseForgeAPI.V1
{
    class Get
    {
        public static async Task<List<CurseForge.APIClient.Models.Games.Game>> First()
        {
            var cfApiClient = new CurseForge.APIClient.ApiClient("$2a$10$/R2wMCh2My1cWvaQN96qmOuXLeu.L9R29cy6RwaXnoC3jyBbvLYNe", "2690380288@qq.com");
            var games = await cfApiClient.GetGamesAsync();
            return games.Data;
        }
        public static async Task<List<uint>> GetModsID()
        {
            List<uint> ListV1 = new List<uint>();
            var cfApiClient = new CurseForge.APIClient.ApiClient("$2a$10$/R2wMCh2My1cWvaQN96qmOuXLeu.L9R29cy6RwaXnoC3jyBbvLYNe", "2690380288@qq.com");
            var featuredMods = await cfApiClient.GetFeaturedModsAsync(new GetFeaturedModsRequestBody
            {
                GameId = 432,
                ExcludedModIds = ListV1,
                GameVersionTypeId = null
            });
            return ListV1;
        }
        public static async Task<CurseForge.APIClient.Models.GenericResponse<FeaturedModsResponse>> GetModsV1()
        {
            var cfApiClient = new CurseForge.APIClient.ApiClient("$2a$10$/R2wMCh2My1cWvaQN96qmOuXLeu.L9R29cy6RwaXnoC3jyBbvLYNe", "2690380288@qq.com");
            var featuredMods = await cfApiClient.GetFeaturedModsAsync(new GetFeaturedModsRequestBody
            {
                GameId = 432,
                ExcludedModIds = new List<uint>(),
                GameVersionTypeId = null
            });
            return featuredMods;
        }
        public static async Task<CurseForge.APIClient.Models.GenericListResponse<Mod>> GetModsS(string Name, string Ver = null, ModLoaderType modloadertype = 0)
        {
            var cfApiClient = new CurseForge.APIClient.ApiClient("$2a$10$/R2wMCh2My1cWvaQN96qmOuXLeu.L9R29cy6RwaXnoC3jyBbvLYNe", "2690380288@qq.com");
            var searchedMods = await cfApiClient.SearchModsAsync(432, null, null, Ver, Name, null, ModsSearchSortOrder.Descending, modloadertype);
            return searchedMods;
        }
        public static async Task<CurseForge.APIClient.Models.GenericResponse<Mod>> GetModsD(uint modId)
        {
            var cfApiClient = new CurseForge.APIClient.ApiClient("$2a$10$/R2wMCh2My1cWvaQN96qmOuXLeu.L9R29cy6RwaXnoC3jyBbvLYNe", "2690380288@qq.com");
            var mod = await cfApiClient.GetModAsync(modId);
            return mod;
        }
        public static async Task<CurseForge.APIClient.Models.GenericListResponse<CurseForge.APIClient.Models.Files.File>> GetModsFileList(uint modId)
        {
            var cfApiClient = new CurseForge.APIClient.ApiClient("$2a$10$/R2wMCh2My1cWvaQN96qmOuXLeu.L9R29cy6RwaXnoC3jyBbvLYNe", "2690380288@qq.com");
            var modFiles = await cfApiClient.GetModFilesAsync(modId);
            return modFiles;
        }
    }
}
