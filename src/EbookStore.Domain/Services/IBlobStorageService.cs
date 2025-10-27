namespace EbookStore.Domain.Services;

public interface IBlobStorageService
{
	Task<string?> UploadAsync(Stream fileStream, string contentType, string fileName);
	Task<Stream?> DownloadAsync(string fileName);
	Task<bool> DeleteAsync(string fileName);
}
