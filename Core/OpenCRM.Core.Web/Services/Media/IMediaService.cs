using Microsoft.AspNetCore.Http;
using OpenCRM.Core.Models;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services
{
    public interface IMediaService
    {
        Task CopyStream(Stream stream, string downloadPath);
        Task<MediaEntity?> DownloadFileById(Guid uuid);
        Task<MediaEntity> EditFileAsync(Guid Id, MediaEntity media);
        List<MediaBlockModel> GetImageMedias();
        MediaEntity GetMedia(Guid Id);
        List<MediaEntity> GetMedias();
        MediaType GetMediaType(string extension);
        string GetMediaUrl(string mediaId);
        bool IsImage(string fileName);
        Task<MediaEntity> PostFileAsync(IFormFile fileData, bool isPublic = false);
        Task PostFileAsync(MediaModel model);
        Task PostMultiFileAsync(List<MediaUploadModel> fileData);
        Task RemoveMedia(Guid Id);
    }
}