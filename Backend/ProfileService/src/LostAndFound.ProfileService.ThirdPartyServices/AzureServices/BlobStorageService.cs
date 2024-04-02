using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LostAndFound.ProfileService.ThirdPartyServices.AzureServices.Interfaces;
using LostAndFound.ProfileService.ThirdPartyServices.Models;
using LostAndFound.ProfileService.ThirdPartyServices.Settings;

namespace LostAndFound.ProfileService.ThirdPartyServices.AzureServices
{
    public class BlobStorageService : IFileStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobStorageSettings _blobStorageSettings;

        public BlobStorageService(BlobServiceClient blobServiceClient, BlobStorageSettings blobStorageSettings)
        {
            _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));
            _blobStorageSettings = blobStorageSettings ?? throw new ArgumentNullException(nameof(blobStorageSettings));
        }

        public async Task<string> UploadAsync(FileDto file)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(
                _blobStorageSettings.ProfilePicturesContainerName);
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            
            var blobClient = containerClient.GetBlobClient(file.GetPathWithFileName());
            await blobClient.UploadAsync(file.Content, new BlobHttpHeaders
            {
                ContentType = file.ContentType,
            });

            var builder = new UriBuilder(blobClient.Uri);
            if (_blobStorageSettings.ReplaceHostUri)
            {
                builder = new UriBuilder(_blobStorageSettings.NewUriHostValue + blobClient.Uri.AbsolutePath);
            }

            return builder.Uri.ToString();
        }

        public async Task DeleteAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(
                _blobStorageSettings.ProfilePicturesContainerName);

            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync();
        }
    }
}
