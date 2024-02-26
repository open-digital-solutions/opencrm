using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using System.Text.Json;


namespace OpenCRM.Core.Web.Services.TranslationService
{
    public class TranslationService<TDBContext> : ITranslationService where TDBContext : DataContext
    {
        private readonly TDBContext _dbContext;
        public TranslationService(TDBContext dBContext)
        {
            _dbContext = dBContext;
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

        public string? GetTranslationValue(string key)
        {
            var currentLangCode = "ESes"; //TODO debe de venir del language service que lo saca del usersession, datasssion, html document tag o uno por defecto del sistema
            //var translation =  _dbContext.Translationss.Where(x => x.Key == key && x.Language.Code == currentLangCode).FirstOrDefault();
            var currentLanguage = _dbContext.Languagess.Where(x => x.Code.ToUpper() == currentLangCode.ToUpper()).FirstOrDefault();
            
            if (currentLanguage == null) { return key; }
             var dataModel = currentLanguage.ToDataModel<TranslationModel>();
            
            if (dataModel == null) { return key; }
             var values = dataModel.Translations.Translations;
            
            return values != null && values.ContainsKey(key) ? values.GetValueOrDefault(key) : key;
        }


        public List<TranslationModel<TDataModel>> GetTranslationListAsync<TDataModel>()
        {
            List<TranslationModel<TDataModel>> result = new();
            try
            {
                var translations = _dbContext.Translationss.ToList();

                if (translations == null || translations.Count == 0) return result;

                foreach (var translation in translations)
                {

                    if (translation == null || string.IsNullOrWhiteSpace(translation.ID.ToString()))
                    {
                        //TODO Handle this error
                        continue;
                    }
                    var dataModel = translation.ToDataModel<TDataModel>();

                    if (dataModel == null) continue;

                    result.Add(dataModel);
                }
            }
            catch (Exception ex)
            {
                //TODO Handle this error
                Console.WriteLine(ex.ToString());
                return new List<TranslationModel<TDataModel>>();
            }
            return result;
        }

        public async Task<TranslationModel<TDataModel>?> AddTranslation<TDataModel>(TranslationModel<TDataModel> model)
        {
            try
            {
                //TODO: Handle errors and exceptions
                var entity = Activator.CreateInstance<TranslationEntity>();
                //entity.ID = model.ID;
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

        public async Task DeleteTranslation<TDataModel>(Guid Id)
        {
            //TODO: Handle errors and exceptions
            var entity = await _dbContext.Translationss.FindAsync(Id);
            if (entity == null)
            {
                return;
            }
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
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

        public Task Seed()
        {
            throw new NotImplementedException();
        }
    }
}
