//using OpenDHS.Shared;
using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.TranslationService;
using System.Text.Json;
using System.Xml.Linq;
//using OpenDHS.Shared.Data;

namespace OpenCRM.Core.Web.Services.LanguageService
{
    public class LanguageService<TDBContext> : ILanguageService where TDBContext : DataContext
    {
        private readonly TDBContext _dbContext;
        public LanguageService(TDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<LanguageModel<TTranslationModel>?> GetLanguage<TTranslationModel>(Guid id) where TTranslationModel : TranslationModel, new()
        {

            try
            {
                var language = await _dbContext.Languagess.FindAsync(id);
                if (language == null)
                {
                    //TODO: Handle this error
                    return null;
                }

                return language.ToDataModel<TTranslationModel>();
            }
            catch (Exception ex)
            {
                //TODO: Handle this error
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<LanguageModel<TranslationModel>?> GetLanguageAsync<LanguageEntity>(Guid id)
        {
            try
            {
                var language = await _dbContext.Languagess.FindAsync(id);
                if (language == null || string.IsNullOrWhiteSpace(language.Code))
                {
                    //TODO: Handle this error
                    return null;
                }
                return language.ToDataModel<TranslationModel>();
            }
            catch (Exception ex)
            {
                //TODO: Handle this error
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<LanguageModel<TTranslationModel>> GetLanguageListAsync<TTranslationModel>() where TTranslationModel : TranslationModel, new()
        {
            List<LanguageModel<TTranslationModel>> result = new();
            try
            {
                var languages = _dbContext.Languagess.OrderBy(lang => lang.AddedAt).ToList();

                if (languages == null || languages.Count == 0) return result;

                foreach (var language in languages)
                {

                    if (language == null || string.IsNullOrWhiteSpace(language.ID.ToString()))
                    {

                        //TODO Handle this error
                        continue;
                    }
                    var dataModel = language.ToDataModel<TTranslationModel>();

                    if (dataModel == null) continue;

                    result.Add(dataModel);
                }
            }
            catch (Exception ex)
            {

                //TODO Handle this error
                Console.WriteLine(ex.ToString());
                return new List<LanguageModel<TTranslationModel>>();
            }
            return result;
        }

        public async Task<LanguageModel<TTranslationModel>?> AddLanguage<TTranslationModel>(LanguageModel<TTranslationModel> model) where TTranslationModel : TranslationModel, new()
        {
            try
            {
                //TODO: Handle errors and exceptions
                var entity = Activator.CreateInstance<LanguageEntity>();
                entity.Code = model.Code;
                entity.Name = model.Name;
                entity.Translations = JsonSerializer.Serialize(model.Translations);
                _dbContext.Languagess.Add(entity);
                await _dbContext.SaveChangesAsync();

                return entity.ToDataModel<TTranslationModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task DeleteLanguage<TDataModel>(Guid Id)
        {
            //TODO: Handle errors and exceptions
            var entity = await _dbContext.Languagess.FindAsync(Id);
            if (entity == null)
            {
                return;
            }
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<LanguageModel<TTranslationModel>?> EditLanguage<TTranslationModel>(LanguageModel<TTranslationModel> model) where TTranslationModel : TranslationModel, new()
        {
            try
            {
                //TODO: Handle errors and exceptions
                var entity = await _dbContext.Languagess.FindAsync(model.ID);
                if (entity == null) return null;
                entity.Code = model.Code;
                entity.Name = model.Name;
                entity.Translations = JsonSerializer.Serialize(model.Translations);
                _dbContext.Languagess.Update(entity);
                await _dbContext.SaveChangesAsync();
                return entity.ToDataModel<TTranslationModel>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /*public async Task addLanguageSeedAsync(String Code, String Name)
        {

            var entity = _dbContext.Languagess.Where<LanguageEntity>(c => c.Code == Code);

            if (entity.Count<LanguageEntity>() == 0)
            {
                var entityEN = Activator.CreateInstance<LanguageEntity>();
                entityEN.Code = Code;
                entityEN.Name = Name;
                _dbContext.Languagess.Add(entityEN);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SeedAsync()
        {
            var english = LanguageModel<TranslationModel>.GetNewInstance("EN-gb", "English");
            english.Translations = new TranslationModel();
            await AddLanguage(english);

            var espanol = LanguageModel<TranslationModel>.GetNewInstance("ESes", "Espanol");
            espanol.Translations = new TranslationModel();
            espanol.Translations.KeyAccept = "Aceptar";
            espanol.Translations.KeyCreate = "Crear";
            await AddLanguage(espanol);
        }*/

        public async Task SeedAsync()
        {
            var existingEnglish = await _dbContext.Languagess.FirstOrDefaultAsync(l => l.Code == "EN-gb");
            if (existingEnglish == null)
            {
                var english = LanguageModel<TranslationModel>.GetNewInstance("EN-gb", "English");
                english.Translations = new TranslationModel();
                await AddLanguage(english);
            }

            var existingSpanish = await _dbContext.Languagess.FirstOrDefaultAsync(l => l.Code == "ESes");
            if (existingSpanish == null)
            {
                var espanol = LanguageModel<TranslationModel>.GetNewInstance("ESes", "Español");
                espanol.Translations = new TranslationModel();
                espanol.Translations.KeyAccept = "Aceptar";
                espanol.Translations.KeyCreate = "Crear";
                await AddLanguage(espanol);
            }
        }
    }
}