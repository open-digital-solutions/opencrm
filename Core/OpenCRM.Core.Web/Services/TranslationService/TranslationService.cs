using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;

namespace OpenCRM.Core.Web.Services.TranslationService
{
    public class TranslationService<TDBContext> : ITranslationService where TDBContext : DataContext
    {
        private readonly TDBContext _dbContext;

        private ILanguageService _languageService;

        public TranslationService(TDBContext dBContext, ILanguageService languageService)
        {
            _dbContext = dBContext;
            _languageService = languageService;
        }

        private async Task<TranslationByLanguage?> AddTranslation(string key, string translation, Guid languageId)
        {
            try
            {
                var entity = Activator.CreateInstance<TranslationEntity>();
                entity.Key = key;
                entity.Translation = translation;
                entity.LanguageId = languageId;
                _dbContext.Translationss.Add(entity);
                await _dbContext.SaveChangesAsync();

                return new TranslationByLanguage()
                {
                    ID = entity.ID,
                    Key = key,
                    Translation = entity.Translation,
                    LanguageId = entity.LanguageId,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot add this translation: {key}, {translation})");
                return null;
            }
        }

        public async Task<TranslationModel?> AddTranslations(TranslationModel model)
        {
            try
            {
                var languages = _languageService.GetLanguageListAsync();

                if (languages != null)
                {
                    var allTranslations = new List<TranslationByLanguage>();

                    foreach (var language in languages)
                    {
                        var getTranslation = model.Translations?.Where(t => t.LanguageCode == language.Code).FirstOrDefault();
                        var translation = getTranslation != null ? await AddTranslation(model.Key, getTranslation.Translation, getTranslation.LanguageId) : await AddTranslation(model.Key, model.Key, language.ID);

                        if (translation != null)
                        {
                            allTranslations.Add(translation);
                        }
                    }

                    return new TranslationModel()
                    {
                        Key = model.Key,
                        Translations = allTranslations
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Cannot add these translations with code {model.Key}");
            }
            return null;
        }

        public async Task<TranslationModel?> EditTranslations(TranslationModel model)
        {
            if (model.Translations != null)
            {
                var editedTranslations = new List<TranslationByLanguage>();

                foreach (var translation in model.Translations)
                {
                    try
                    {
                        if (translation.Translation != null)
                        {
                            var entity = await _dbContext.Translationss.FindAsync(translation.ID);
                            if (entity == null) return null;
                            entity.Key = model.Key;
                            entity.Translation = translation.Translation;
                            _dbContext.Translationss.Update(entity);
                            await _dbContext.SaveChangesAsync();

                            editedTranslations.Add(new TranslationByLanguage()
                            {
                                ID = entity.ID,
                                Key = model.Key,
                                Translation = entity.Translation,
                                LanguageId = entity.LanguageId,
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Cannot edit these translations with code {model.Key}");
                    }
                }

                return new TranslationModel()
                {
                    Key = model.Key,
                    Translations = editedTranslations
                };
            }
            return null;
        }

        public async Task DeleteTranslation(string key)
        {
            var translations = GetTranslationsByKey(key);

            if (translations != null)
            {
                foreach (var translation in translations)
                {
                    try
                    {
                        var entity = await _dbContext.Translationss.FindAsync(translation.ID);
                        if (entity != null)
                        {
                            _dbContext.Remove(entity);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Cannot delete this translations with code {key}");
                    }
                }
            }
        }

        public async Task<string?> GetTranslationKey(Guid id)
        {
            try
            {
                var translation = await _dbContext.Translationss.FindAsync(id);

                if (translation == null || string.IsNullOrWhiteSpace(translation.Key.ToString()))
                {
                    Console.WriteLine("Translation is null or translation key is null or empty");
                    return null;
                }

                return translation.Key;
            }
            catch (Exception ex)
            {
                //TODO: Handle this error
                Console.WriteLine("Cannot get translation key");
                return null;
            }
        }

        public List<TranslationByLanguage>? GetTranslationsByKey(string key)
        {
            try
            {
                var translations = _dbContext.Translationss.Where(t => t.Key == key).ToList();
                var languages = _languageService.GetLanguageListAsync();

                if (translations != null && translations.Count != 0 && languages != null)
                {
                    var result = new List<TranslationByLanguage>();

                    foreach (var translation in translations)
                    {
                        if (translation != null && !string.IsNullOrWhiteSpace(translation.ID.ToString()))
                        {
                            var language = languages.Where(l => l.ID == translation.LanguageId).FirstOrDefault();

                            result.Add(new TranslationByLanguage()
                            {
                                ID = translation.ID,
                                Key = translation.Key,
                                Translation = translation.Translation,
                                LanguageId = translation.LanguageId,
                                LanguageCode = language?.Code
                            });
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                //TODO: Handle this error
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public List<TranslationByLanguage>? GetTranslationsList()
        {
            try
            {
                var translations = _dbContext.Translationss.ToList();
                var languages = _languageService.GetLanguageListAsync();

                if (translations != null && translations.Count != 0 && languages != null)
                {
                    var result = new List<TranslationByLanguage>();
                    foreach (var translation in translations)
                    {
                        if (translation != null && !string.IsNullOrWhiteSpace(translation.ID.ToString()))
                        {
                            var language = languages.Where(l => l.ID == translation.LanguageId).FirstOrDefault();

                            result.Add(new TranslationByLanguage()
                            {
                                ID = translation.ID,
                                Key = translation.Key,
                                Translation = translation.Translation,
                                LanguageId = translation.LanguageId,
                                LanguageCode = language?.Code
                            });
                        }
                    }
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                //TODO Handle this error
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public Dictionary<string, List<TranslationByLanguage>>? GetTranslationsToDictionary()
        {
            var translations = GetTranslationsList();

            var languages = _languageService.GetLanguageListAsync();

            if (translations != null && languages != null)
            {
                var keyTranslations = new Dictionary<string, List<TranslationByLanguage>>();

                foreach (var translation in translations)
                {
                    var language = languages.Find(l => l.ID == translation.LanguageId);

                    if (language != null)
                    {
                        if (!keyTranslations.ContainsKey(translation.Key))
                            keyTranslations.Add(translation.Key, new List<TranslationByLanguage>());

                        keyTranslations[translation.Key].Add(new TranslationByLanguage()
                        {
                            ID = translation.ID,
                            Key = translation.Key,
                            Translation = translation.Translation,
                            LanguageCode = language.Code,
                            LanguageId = translation.LanguageId
                        });
                    }
                }
                return keyTranslations;
            }
            return null;
        }

        public List<DataBlockModel<TranslationByLanguage>> ToListDataBlockModel(List<TranslationByLanguage> keyTranslations)
        {
            var response = new List<DataBlockModel<TranslationByLanguage>>();

            var languages = _languageService.GetLanguageListAsync();

            if (languages != null)
            {
                foreach (var translation in keyTranslations)
                {
                    var language = languages.Find(l => l.ID == translation.LanguageId);

                    if (language != null)
                    {
                        response.Add(new DataBlockModel<TranslationByLanguage>
                        {
                            ID = translation.ID,
                            Code = translation.Key,
                            Description = translation.Translation,
                            Type = typeof(TranslationByLanguage).ToString(),

                            Data = new TranslationByLanguage()
                            {
                                ID = translation.ID,
                                Key = translation.Key,
                                Translation = translation.Translation,
                                LanguageId = translation.LanguageId,
                                LanguageCode = translation.LanguageCode,
                            }
                        });
                    }
                }
            }

            return response;
        }

        public async Task<string?> GetTranslationValue(string key) //arreglar para que devuelva varias traducciones
        {
            var currentLanguage = await _languageService.GetCurrentLanguage();
            if (currentLanguage == null) { return key; }
            var translationValue = await _dbContext.Translationss.FirstOrDefaultAsync((t) => t.Key == key && t.LanguageId == currentLanguage.ID);
            if (translationValue == null) { return key; }
            return translationValue.Translation;
        }

        public virtual async Task Seed()
        {
            var languages = _dbContext.Languagess.ToList();

            if (languages != null && languages.Count > 0)
            {
                await AddTranslations(new TranslationModel()
                {
                    Key = "KEY_MANAGE_WELCOME"
                });
            }
        }
    }
}
