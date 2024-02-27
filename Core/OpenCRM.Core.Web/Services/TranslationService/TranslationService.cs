using Newtonsoft.Json;
using OpenCRM.Core.Data;
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
      
        public async Task<TranslationModel<TDataModel>?> AddTranslation<TDataModel>(TranslationModel<TDataModel> model)
        {
            try
            {
                //TODO: Handle errors and exceptions
                var entity = Activator.CreateInstance<TranslationEntity>();
                entity.Key = model.Key;
                entity.Translation = model.Translation;
                entity.LanguageId = model.LanguageId;
                _dbContext.Translationss.Add(entity);

                await _dbContext.SaveChangesAsync();
                return entity.ToDataModel<TDataModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<TranslationModel<TDataModel>?> EditTranslation<TDataModel>(TranslationModel<TDataModel> model)
        {
            try
            {
                //TODO: Handle errors and exceptions
                var entity = await _dbContext.Translationss.FindAsync(model.ID);
                if (entity == null) return null;
                entity.Key = model.Key;
                entity.Translation = model.Translation;
                _dbContext.Translationss.Update(entity);

                await _dbContext.SaveChangesAsync();
                return entity.ToDataModel<TDataModel>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task DeleteTranslation<TDataModel>(Guid Id)
        {
            try
            {
                var entity = await _dbContext.Translationss.FindAsync(Id);
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

        public async Task<TranslationModel<TranslationEntity>?> GetTranslationAsync<TranslationEntity>(Guid id)
        {
            try
            {
                var translation = await _dbContext.Translationss.FindAsync(id);

                if (translation == null || string.IsNullOrWhiteSpace(translation.Key.ToString()))
                {
                    //TODO: Handle this error
                    return null;
                }
                return translation.ToDataModel<TranslationEntity>();
            }
            catch (Exception ex)
            {
                //TODO: Handle this error
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<TranslationModel<TDataModel>>? GetTranslationListAsync<TDataModel>()
        {
            try
            {
                var translations = _dbContext.Translationss.ToList();

                if (translations != null && translations.Count != 0)
                {
                    var result = new List<TranslationModel<TDataModel>>();
                    foreach (var translation in translations)
                    {
                        if (translation != null && !string.IsNullOrWhiteSpace(translation.ID.ToString()))
                        {
                            var dataModel = translation.ToDataModel<TDataModel>();

                            if (dataModel != null)
                            {
                                result.Add(dataModel);
                            }
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

        public async Task<Dictionary<string, List<TranslationByLanguage>>?> GetKeyTranslationsByLanguage<TDataModel>()
        {
            var translations = GetTranslationListAsync<TDataModel>();

            if (translations != null)
            {
                var keyTranslations = new Dictionary<string, List<TranslationByLanguage>>();

                foreach (var translation in translations)
                {
                    var language = await _languageService.GetLanguage(translation.LanguageId);

                    if (language != null)
                    {
                        if (!keyTranslations.ContainsKey(translation.Key))
                            keyTranslations.Add(translation.Key, new List<TranslationByLanguage>());

                        keyTranslations[translation.Key].Add(new TranslationByLanguage()
                        {
                            LanguageCode = language.Code,
                            Translation = translation.Translation
                        });
                    }
                }
                return keyTranslations;
            }
            return null;
        }

        public string? GetTranslationValue(string key)
        {
            var currentLangCode = "En"; //TODO debe de venir del language service que lo saca del usersession, datasssion, html document tag o uno por defecto del sistema
            //var translation =  _dbContext.Translationss.Where(x => x.Key == key && x.Language.Code == currentLangCode).FirstOrDefault();
            var currentLanguage = _dbContext.Languagess.Where(x => x.Code.ToUpper() == currentLangCode.ToUpper()).FirstOrDefault();
            if (currentLanguage == null) { return key; }
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(currentLanguage.Translations);
            return values != null && values.ContainsKey(key) ? values.GetValueOrDefault(key) : key;
        }

        public Task Seed()
        {
            throw new NotImplementedException();
        }
    }
}
