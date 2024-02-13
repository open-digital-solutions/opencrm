using Microsoft.AspNetCore.Http;

namespace OpenCRM.Core.Web.Services
{
    public interface IMediaService
    {
        Task CopyStream(Stream stream, string downloadPath);
        Task<MediaEntity?> DownloadFileById(Guid uuid);
        Task<MediaEntity> EditFileAsync(Guid Id, MediaEntity media);
        MediaEntity GetMedia(Guid Id);
        List<MediaEntity> GetMedias();
        string GetMediaUrl(string mediaId);
        Task<MediaEntity> PostFileAsync(IFormFile fileData, bool isPublic = false);
        Task PostFileAsync(MediaModel model);
        Task PostMultiFileAsync(List<MediaUploadModel> fileData);
        Task RemoveMedia(Guid Id);
    }
}