using Microsoft.AspNetCore.Mvc.Razor;
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

        public async Task<List<TranslationModel>?> AddTranslations(string key)
        {
			try
			{
                var languages = _languageService.GetLanguageListAsync();

                if (languages != null)
                {
                    var addedTranslations = new List<TranslationModel>();

                    foreach (var language in languages)
                    {
                        try
                        {
                            var entity = Activator.CreateInstance<TranslationEntity>();
                            entity.Key = key;
                            entity.Translation = key;
                            entity.LanguageId = language.ID;
                            _dbContext.Translationss.Add(entity);
                            await _dbContext.SaveChangesAsync();

                            addedTranslations.Add(new TranslationModel()
                            {
                                ID = entity.ID,
                                Key = key,
                                Translation = entity.Translation,
                                LanguageId = entity.LanguageId,
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return null;
                        }
                    }
                    return addedTranslations;
                }
                return null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}

        public async Task<List<TranslationModel>?> AddTranslations(string key, List<TranslationLanguageCodeModel> keyTranslations)
        {
            try
            {
                var addedTranslations = new List<TranslationModel>();

                foreach (var translation in keyTranslations)
                {
					try
					{
						var model = new TranslationModel()
						{
							ID = translation.ID,
							Key = key,
							LanguageId = translation.LanguageId,
							Translation = translation.Translation,
						};

						var entity = Activator.CreateInstance<TranslationEntity>();
						entity.Key = model.Key;
						entity.Translation = model.Translation;
						entity.LanguageId = model.LanguageId;
						_dbContext.Translationss.Add(entity);
						await _dbContext.SaveChangesAsync();

                        addedTranslations.Add(new TranslationModel()
                        {
                            ID = entity.ID,
                            Key = key,
                            Translation = entity.Translation,
                            LanguageId = entity.LanguageId,
                        });
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						return null;
					}
				}
                return addedTranslations;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<List<TranslationModel>?> EditTranslations(string key, List<TranslationLanguageCodeModel> keyTranslations)
        {
            try
            {
                var editedTranslations = new List<TranslationModel>();

                foreach (var translation in keyTranslations)
                {
                    var model = new TranslationModel()
                    {
                        ID = translation.ID,
                        Key = key,
                        LanguageId = translation.LanguageId,
                        Translation = translation.Translation,
                    };

					try
					{
						//TODO: Handle errors and exceptions
						var entity = await _dbContext.Translationss.FindAsync(model.ID);
						if (entity == null) return null;
						entity.Key = model.Key;
						entity.Translation = model.Translation;
						_dbContext.Translationss.Update(entity);
						await _dbContext.SaveChangesAsync();

                        editedTranslations.Add(new TranslationModel()
						{
							ID = entity.ID,
							Key = key,
							Translation = entity.Translation,
							LanguageId = entity.LanguageId,
						});
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
						return null;
					}
				}
				return editedTranslations;
			}
			catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

		public async Task DeleteTranslation(string key, List<TranslationLanguageCodeModel> keyTranslations)
		{
            try
            {
                foreach (var translation in keyTranslations)
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
						Console.WriteLine(e.Message);
					}
				}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<TranslationModel?> GetTranslationById(Guid id)
        {
            try
            {
                var translation = await _dbContext.Translationss.FindAsync(id);

                if (translation == null || string.IsNullOrWhiteSpace(translation.Key.ToString()))
                {
                    Console.WriteLine("Translation is null or translation key is null or empty");
                    return null;
                }

                return new TranslationModel()
                {
                    ID = id,
                    Key = translation.Key,
                    Translation = translation.Translation,
                    LanguageId = translation.LanguageId
                };
            }
            catch (Exception ex)
            {
                //TODO: Handle this error
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<TranslationModel>? GetTranslationsByKey(string key)
        {
            try
            {
                var translations = _dbContext.Translationss.Where(t => t.Key == key).ToList();

                if (translations != null)
                {
                    if (translations != null && translations.Count != 0)
                    {
                        var result = new List<TranslationModel>();

                        foreach (var translation in translations)
                        {
                            if (translation != null && !string.IsNullOrWhiteSpace(translation.ID.ToString()))
                            {
                                result.Add(new TranslationModel()
                                {
                                    ID = translation.ID,
                                    Key = translation.Key,
                                    Translation = translation.Translation,
                                    LanguageId = translation.LanguageId
                                });
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

		public List<TranslationModel>? GetTranslationsList()
		{
			try
			{
				var translations = _dbContext.Translationss.ToList();

				if (translations != null && translations.Count != 0)
				{
					var result = new List<TranslationModel>();
					foreach (var translation in translations)
					{
						if (translation != null && !string.IsNullOrWhiteSpace(translation.ID.ToString()))
						{
							result.Add(new TranslationModel()
							{
								ID = translation.ID,
								Key = translation.Key,
								Translation = translation.Translation,
								LanguageId = translation.LanguageId
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
		
        public List<TranslationLanguageCodeModel>? GetTranslationsWithLanguagesCode(string key)
        {
            var keytranslations = GetTranslationsByKey(key);
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
                            Key = key,
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

		public Dictionary<string, List<TranslationLanguageCodeModel>>? GetTranslationsToDictionary()
        {
            var translations = GetTranslationsList();
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

        public List<DataBlockModel<TranslationLanguageCodeModel>> ToListDataBlockModel(List<TranslationModel> translations)
        {
            var response = new List<DataBlockModel<TranslationLanguageCodeModel>>();
            var languages = _languageService.GetLanguageListAsync();

            if (languages != null)
            {
                foreach (var item in translations)
                {
                    var language = languages.Find(l => l.ID == item.LanguageId);

                    if (language != null)
                    {
                        response.Add(new DataBlockModel<TranslationLanguageCodeModel>()
                        {
                            ID = item.ID,
                            Code = item.Key,
                            Description = item.Translation,
                            Type = typeof(TranslationLanguageCodeModel).ToString(),
                            Data = new TranslationLanguageCodeModel()
                            {
                                ID = item.ID,
                                Key = item.Key,
                                Translation = item.Translation,
                                LanguageCode = language.Code
                            }
                        });
                    }
                }
            }

            return response;
        }

        public async Task<string?> GetTranslationValue(string key)
        {
            var currentLanguage = await _languageService.GetCurrentLanguage();
            if (currentLanguage == null) { return key; }
            var translationValue = await _dbContext.Translationss.FirstOrDefaultAsync((t) => t.Key == key && t.LanguageId == currentLanguage.ID);
            if (translationValue == null) { return key; }
            return translationValue.Translation;
        }

        public virtual async Task Seed()
        {
			var languages = _languageService.GetLanguageListAsync();
			var italialLanguage = languages?.FirstOrDefault(l => l.Code == "IT");

			if (italialLanguage != null)
			{
				ItalianTranslations italianTranslations = new ItalianTranslations();
				var translations = italianTranslations.Translations;

				foreach (var translation in translations)
				{
					_dbContext.Translationss.Add(new TranslationEntity
					{
						Key = translation.Key,
						Translation = translation.Translation,
						LanguageId = italialLanguage.ID
					});
				}
			}
			await _dbContext.SaveChangesAsync();
		}
    }
}
