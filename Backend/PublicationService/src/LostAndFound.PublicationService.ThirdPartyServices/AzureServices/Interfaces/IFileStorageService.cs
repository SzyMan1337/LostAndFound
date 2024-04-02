using LostAndFound.PublicationService.ThirdPartyServices.Models;

namespace LostAndFound.PublicationService.ThirdPartyServices.AzureServices.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(FileDto file);
        Task DeleteAsync(string blobName);
    }
}
