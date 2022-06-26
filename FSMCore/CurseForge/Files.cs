using CurseForge.APIClient.Models;
using CurseForge.APIClient.Models.Files;
using System.Threading.Tasks;

namespace CurseForge.APIClient
{
    public partial class ApiClient
    {
        public async Task<GenericResponse<File>> GetModFileAsync(uint modId, uint fileId)
        {
            return await GET<GenericResponse<File>>($"/v1/mods/{modId}/files/{fileId}");
        }

        public async Task<GenericListResponse<File>> GetModFilesAsync(uint modId, string gameVersionFlavor = null, uint? index = null, uint? pageSize = 900)
        {
            return await GET<GenericListResponse<File>>($"/v1/mods/{modId}/files",
                ("gameVersionFlavor", gameVersionFlavor), ("index", index), ("pageSize", pageSize)
            );
        }

        public async Task<GenericListResponse<File>> GetFilesAsync(GetModFilesRequestBody body)
        {
            return await POST<GenericListResponse<File>>("/v1/mods/files", body);
        }

        public async Task<GenericResponse<string>> GetModFileChangelogAsync(uint modId, uint fileId)
        {
            return await GET<GenericResponse<string>>($"/v1/mods/{modId}/files/{fileId}/changelog");
        }

        public async Task<GenericResponse<string>> GetModFileDownloadUrlAsync(uint modId, uint fileId)
        {
            return await GET<GenericResponse<string>>($"/v1/mods/{modId}/files/{fileId}/download-url");
        }
    }
}
