using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.Extensions;

namespace OpenCRM.Core
{
    public class MediaService<TDBContext> : IMediaService where TDBContext : DataContext
    {
        private readonly TDBContext dbContextClass;

        public MediaService(TDBContext dbContext)
        {
            dbContextClass = dbContext;
        }

        public async Task<MediaEntity> PostFileAsync(IFormFile fileData, bool isPublic = false)
        {
            try
            {
                var fileDetails = new MediaEntity()
                {
                    ID = Guid.NewGuid(),
                    FileName = fileData.FileName,
                    FileType = MediaType.GENERIC,
                    IsPublic = isPublic
                };

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }

                StoreMediaToPublicFile(fileDetails);

                var result = dbContextClass.Medias.Add(fileDetails);
                await dbContextClass.SaveChangesAsync();

                return fileDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

       

        public async Task PostMultiFileAsync(List<MediaUploadModel> fileData)
        {
            try
            {
                foreach(MediaUploadModel file in fileData)
                {
                    var fileDetails = new MediaEntity()
                    {
                        ID = Guid.NewGuid(),
                        FileName = file.FileDetails?.FileName,
                        FileType = MediaType.GENERIC,
                        IsPublic = file.IsPublic
                    };

                    using (var stream = new MemoryStream())
                    {
                        file.FileDetails?.CopyTo(stream);
                        fileDetails.FileData = stream.ToArray();
                    }

                    if (file.IsPublic)
                    {
                        StoreMediaToPublicFile(fileDetails);
                    }

                    var result = dbContextClass.Medias.Add(fileDetails);
                }             
                await dbContextClass.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MediaEntity?> DownloadFileById(Guid uuid)
        {
            try
            {
                var file =  await dbContextClass.Medias.Where(x => x.ID == uuid).FirstOrDefaultAsync();
                if (file == null) return null;
                if (file.FileName == null) return null;
                if (file.FileData == null) return null;

                if (file.IsPublic) {
                    StoreMediaToPublicFile(file);
                }
               
                var content = new System.IO.MemoryStream(file.FileData);

                var webRootPath = OpenCRMEnv.GetWebRoot();
                if (!string.IsNullOrEmpty(webRootPath)) {
                    var path = Path.Combine(webRootPath, "media", file.FileName);
                    await CopyStream(content, path);
                }

                return file;
            }
            catch (Exception)
            {
                throw;
            }        
        }
 
        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
               await stream.CopyToAsync(fileStream);
            }
        }

        private static void StoreMediaToPublicFile(MediaEntity fileDetails)
        {
            var webRootPath = OpenCRMEnv.GetWebRoot();

            if (!string.IsNullOrEmpty(webRootPath))
            {
                var mediaPublicDir = "media";

                string mediaPublicDirPath = Path.Combine(webRootPath, mediaPublicDir);

                if (!Directory.Exists(mediaPublicDirPath))
                    Directory.CreateDirectory(mediaPublicDirPath);

                var extension = Path.GetExtension(fileDetails.FileName);
                var filePath = Path.Combine(mediaPublicDirPath, fileDetails.ID.ToString() + extension);
                if (fileDetails == null || fileDetails.FileData == null) return;
                File.WriteAllBytes(filePath, fileDetails.FileData);
            }
        }
    }
}
