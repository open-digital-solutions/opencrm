using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using OpenCRM.Core.Extensions;
using System.Text.Json;

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
        public async Task PostFileAsync(MediaModel model)
        {
            try
            {
                string extension=Path.GetExtension(model.FileData.FileName).ToLower();
                var fileDetails = new MediaEntity()
                {
                    ID = Guid.NewGuid(),
                    FileName = model.FileName,
                    FileType = extension==".pdf"?MediaType.PDF: extension == ".docx" ? MediaType.DOCX : MediaType.GENERIC,
                    IsPublic = model.IsPublic
                };

                using (var stream = new MemoryStream())
                {
                    model.FileData.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }
                if (model.IsPublic)
                {
                    StoreMediaToPublicFile(fileDetails);
                }

                var result = dbContextClass.Medias.Add(fileDetails);
                await dbContextClass.SaveChangesAsync();

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
                foreach (MediaUploadModel file in fileData)
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
                var file = await dbContextClass.Medias.Where(x => x.ID == uuid).FirstOrDefaultAsync();
                if (file == null) return null;
                if (file.FileName == null) return null;
                if (file.FileData == null) return null;

                if (file.IsPublic)
                {
                    StoreMediaToPublicFile(file);
                }

                var content = new System.IO.MemoryStream(file.FileData);

                var webRootPath = OpenCRMEnv.GetWebRoot();
                if (!string.IsNullOrEmpty(webRootPath))
                {
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
        public List<MediaEntity> GetMedias()
        {
            var result = dbContextClass.Medias.ToList() ?? new List<MediaEntity>();
            return result;
        }
        public MediaEntity GetMedia(Guid Id)
        {
            return dbContextClass.Medias.FirstOrDefault(s => s.ID == Id);

        }
        public async Task RemoveMedia(Guid Id)
        {
            var media = await dbContextClass.Medias.FindAsync(Id);
            if (media == null) return;
            dbContextClass.Medias.Remove(media);
            dbContextClass.SaveChanges();
        }
        public async Task<MediaEntity> EditFileAsync(Guid Id,MediaEntity media)
        {
            try
            {
                var entity = await dbContextClass.Medias.FindAsync(Id);
                
                if (entity == null) return null;
                
                    entity.FileName = media.FileName;
                    entity.FileType= media.FileData != null ? media.FileType:entity.FileType;
                    entity.IsPublic = media.IsPublic;
                    entity.FileData =media.FileData!=null ?media.FileData:entity.FileData;
                    entity.UpdatedAt = DateTime.UtcNow;
                    
                    if (media.IsPublic)
                    {
                        StoreMediaToPublicFile(entity);
                    }
                    await dbContextClass.SaveChangesAsync();
                    return entity;
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


    }
}
