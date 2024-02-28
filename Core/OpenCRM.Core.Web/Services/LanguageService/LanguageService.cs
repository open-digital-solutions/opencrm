using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.Data;

namespace OpenCRM.Core.Web.Services.LanguageService
{
    public class LanguageService<TDBContext> : ILanguageService where TDBContext : DataContext
    {
        private readonly TDBContext _dbContext;

        public LanguageService(TDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<LanguageModel?> AddLanguage(LanguageModel model)
        {
            try
            {
                //TODO: Handle errors and exceptions
                var entity = Activator.CreateInstance<LanguageEntity>();
                entity.Code = model.Code;
                entity.Name = model.Name;
                _dbContext.Languagess.Add(entity);
                await _dbContext.SaveChangesAsync();

                return new LanguageModel()
                {
                    ID = entity.ID,
                    Code = entity.Code,
                    Name = entity.Name
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<LanguageModel?> EditLanguage(LanguageModel model)
        {
            try
            {
                //TODO: Handle errors and exceptions
                var entity = await _dbContext.Languagess.FindAsync(model.ID);
                if (entity == null) return null;

                entity.Code = model.Code;
                entity.Name = model.Name;
                _dbContext.Languagess.Update(entity);
                await _dbContext.SaveChangesAsync();
                return new LanguageModel()
                {
                    ID = entity.ID,
                    Code = entity.Code,
                    Name = entity.Name
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task DeleteLanguage(Guid Id)
        {
            try
            {
                var entity = await _dbContext.Languagess.FindAsync(Id);
                if (entity != null)
                {
                    _dbContext.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<LanguageModel?> GetLanguage(Guid id)
        {
            try
            {
                var language = await _dbContext.Languagess.FindAsync(id);
                if (language == null)
                {
                    //TODO: Handle this error
                    return null;
                }

                return new LanguageModel
                {
                    ID = id,
                    Code = language.Code,
                    Name = language.Name,
                };
            }
            catch (Exception ex)
            {
                //TODO: Handle this error
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<LanguageEntity?> GetCurrentLanguage()
        {
            //TODO:
            // 1 - If logged user search on logged user 
            // 2 - If Coockies Session check on cookies session
            // 3 - Search on request headers
            // 4 - Use english as default language
            var currentLanguage = await _dbContext.Languagess.FirstOrDefaultAsync(l => l.Code.Contains("EN"));
            if (currentLanguage == null) return null;
            return currentLanguage;
        }

        public List<LanguageModel>? GetLanguageListAsync()
        {
            try
            {
                var languages = _dbContext.Languagess.OrderBy(lang => lang.AddedAt).ToList();
                if (languages == null || languages.Count == 0) return null;

                var result = new List<LanguageModel>();

                foreach (var language in languages)
                {
                    if (language != null && !string.IsNullOrWhiteSpace(language.ID.ToString()))
                    {
                        var dataModel = new LanguageModel()
                        {
                            ID = language.ID,
                            Code = language.Code,
                            Name = language.Name
                        };

                        if (dataModel != null)
                        {
                            result.Add(dataModel);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                //TODO Handle this error
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task SeedAsync()
        {
            var existingEnglish = await _dbContext.Languagess.FirstOrDefaultAsync(l => l.Code == "EN-gb");
            if (existingEnglish == null)
            {
                var english = new LanguageModel()
                {
                    ID = Guid.Empty,
                    Code = "EN-gb",
                    Name = "English"
                };

                await AddLanguage(english);
            }

            var existingSpanish = await _dbContext.Languagess.FirstOrDefaultAsync(l => l.Code == "ES-es");
            if (existingSpanish == null)
            {
                var espanol = new LanguageModel()
                {
                    ID = Guid.Empty,
                    Code = "ES-es",
                    Name = "Espa�ol"
                };

                await AddLanguage(espanol);
            }
        }
    }
}