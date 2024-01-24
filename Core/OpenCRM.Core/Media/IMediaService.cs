﻿using Microsoft.AspNetCore.Http;

namespace OpenCRM.Core
{
    public interface IMediaService
    {
        public Task<MediaEntity> PostFileAsync(IFormFile fileData, bool isPublic = false);

        public Task PostMultiFileAsync(List<MediaUploadModel> fileData);

        public Task<MediaEntity> DownloadFileById(Guid uuid);
    }
}
