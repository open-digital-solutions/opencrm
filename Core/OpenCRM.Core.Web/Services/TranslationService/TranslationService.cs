﻿using Microsoft.EntityFrameworkCore;
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

        public async Task AddKeysTranslations<TDataModel>(string key, List<TranslationLanguageCodeModel> keyTranslations)
        {
            try
            {
                foreach (var translation in keyTranslations)
                {
                    await AddTranslation(new TranslationModel<TDataModel>()
                    {
                        ID = translation.ID,
                        Key = key,
                        LanguageId = translation.LanguageId,
                        Translation = translation.Translation,
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task EditKeysTranslations<TDataModel>(string key, List<TranslationLanguageCodeModel> keyTranslations)
        {
            try
            {
                foreach (var translation in keyTranslations)
                {
                    await EditTranslation(new TranslationModel<TDataModel>()
                    {
                        ID = translation.ID,
                        Key = key,
                        LanguageId = translation.LanguageId,
                        Translation = translation.Translation,
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task DeleteKeysTranslation<TDataModel>(string key, List<TranslationLanguageCodeModel> keyTranslations)
        {
            try
            {
                foreach (var translation in keyTranslations)
                {
                    await DeleteTranslation<TDataModel>(translation.ID);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<TranslationModel<TranslationEntity>?> GetTranslationByIdAsync<TranslationEntity>(Guid id)
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

        public List<TranslationModel<TDataModel>>? GetTranslationsByKey<TDataModel>(string key)
        {
            try
            {
                var translations = _dbContext.Translationss.Where(t => t.Key == key).ToList();

                if (translations != null)
                {
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
                }
                return null;
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

        public List<TranslationLanguageCodeModel>? GetKeyTranslations<TDataModel>(string key)
        {
            var keytranslations = GetTranslationsByKey<TDataModel>(key);
            var languages = _languageService.GetLanguageListAsync();

            if (keytranslations != null && languages != null)
            {
                var keyTranslationByLanguage = new List<TranslationLanguageCodeModel>();

                foreach (var translation in keytranslations)
                {
                    var language = languages.Find(l => l.ID == translation.LanguageId);

                    if (language != null)
                    {
                        keyTranslationByLanguage.Add(new TranslationLanguageCodeModel()
                        {
                            ID = translation.ID,
                            Translation = translation.Translation,
                            LanguageCode = language.Code,
                            LanguageId = translation.LanguageId
                        });
                    }
                }
                return keyTranslationByLanguage;
            }
            return null;
        }

        public Dictionary<string, List<TranslationLanguageCodeModel>>? GetKeysTranslations<TDataModel>()
        {
            var translations = GetTranslationListAsync<TDataModel>();
            var languages = _languageService.GetLanguageListAsync();

            if (translations != null && languages != null)
            {
                var keyTranslations = new Dictionary<string, List<TranslationLanguageCodeModel>>();

                foreach (var translation in translations)
                {
                    var language = languages.Find(l => l.ID == translation.LanguageId);

                    if (language != null)
                    {
                        if (!keyTranslations.ContainsKey(translation.Key))
                            keyTranslations.Add(translation.Key, new List<TranslationLanguageCodeModel>());

                        keyTranslations[translation.Key].Add(new TranslationLanguageCodeModel()
                        {
                            ID = translation.ID,
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

        public Dictionary<string, string> KeyTranslationsValueToString(Dictionary<string, List<TranslationLanguageCodeModel>> keyTranslations)
        {
            var keyTranslationsValue = new Dictionary<string, string>();

            foreach (var key in keyTranslations.Keys)
            {
                var value = keyTranslations[key];
                string valueToString = key + ": { ";

                for (int i = 0; i < value.Count(); i++)
                {
                    var item = value[i];
                    valueToString += $"{item.LanguageCode} : '{item.Translation}'";
                    valueToString += (i != value.Count() - 1) ? ", " : " }";
                }
                keyTranslationsValue.Add(key, valueToString);
            }
            return keyTranslationsValue;
        }

        public async Task<string?> GetTranslationValueAsync(string key)
        {
            var currentLanguage = await _languageService.GetCurrentLanguage();
            if (currentLanguage == null) { return key; }
            var translationValue = await this._dbContext.Translationss.FirstOrDefaultAsync((t) => t.Key == key && t.LanguageId == currentLanguage.ID);
            if (translationValue == null) { return key; }
            return translationValue.Translation;
        }

        public async Task Seed()
        {
            var languages = _dbContext.Languagess.ToList();        


            if (languages != null && languages.Count > 0)
            {
                foreach (var language in languages)
                {
                    var ksExist = _dbContext.Translationss.FirstOrDefault(t => t.Key == "KEY_MANAGE_WELCOME");
                    if (ksExist == null) {
                        _dbContext.Translationss.Add(new TranslationEntity
                        {
                            Key = "KEY_MANAGE_WELCOME",
                            Translation = "KEY_MANAGE_WELCOME",
                            LanguageId = language.ID
                        });
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
        }

    }
}
