using Microsoft.AspNetCore.Http;

namespace OpenCRM.Core
{
    public interface IMediaService
    {
        public Task<MediaEntity> PostFileAsync(IFormFile fileData, bool isPublic = false);
        public Task PostFileAsync(MediaModel model);
        public Task PostMultiFileAsync(List<MediaUploadModel> fileData);
        public Task<MediaEntity> DownloadFileById(Guid uuid);
        public List<MediaEntity> GetMedias();
        public MediaEntity GetMedia(Guid Id);
        Task RemoveMedia(Guid Id);
        public Task<MediaEntity> EditFileAsync(Guid Id,MediaEntity media);

        public string GetImageUrl(Guid id);
    }
}
