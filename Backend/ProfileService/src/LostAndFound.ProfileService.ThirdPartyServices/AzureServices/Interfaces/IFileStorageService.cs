using LostAndFound.ProfileService.ThirdPartyServices.Models;

namespace LostAndFound.ProfileService.ThirdPartyServices.AzureServices.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(FileDto file);
        Task DeleteAsync(string blobName);
    }
}
