//using OpenDHS.Shared;
using OpenCRM.Core.Data;
//using OpenDHS.Shared.Data;

namespace OpenCRM.Core.Web.Services.LanguageService
{
    public class LanguageService<TDBContext> : ILanguageService where TDBContext : DataContext
    {
        private readonly TDBContext _dbContext;
        public LanguageService(TDBContext dBContext)
        {
            _dbContext = dBContext;
            Seed();
        }

        public async Task<LanguageModel<LanguageEntity>?> GetLanguageAsync<LanguageEntity>(Guid id)
        {
            try
            {
                var language = await _dbContext.Languagess.FindAsync(id);
                if (language == null || string.IsNullOrWhiteSpace(language.Code))
                {
                    //TODO: Handle this error
                    return null;
                }
                return language.ToDataModel<LanguageEntity>();
            }
            catch (Exception ex)
            {
                //TODO: Handle this error
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<LanguageModel<TDataModel>> GetLanguageListAsync<TDataModel>()
        {
            List<LanguageModel<TDataModel>> result = new();
            try
            {
                var languages = _dbContext.Languagess.ToList();

                if (languages == null || languages.Count == 0) return result;

                foreach (var language in languages)
                {

                    if (language == null || string.IsNullOrWhiteSpace(language.ID.ToString()))
                    {

                        //TODO Handle this error
                        continue;
                    }
                    var dataModel = language.ToDataModel<TDataModel>();

                    if (dataModel == null) continue;

                    result.Add(dataModel);
                }
            }
            catch (Exception ex)
            {

                //TODO Handle this error
                Console.WriteLine(ex.ToString());
                return new List<LanguageModel<TDataModel>>();
            }
            return result;
        }

        public async Task<LanguageModel<TDataModel>?> AddLanguage<TDataModel>(LanguageModel<TDataModel> model)
        {
            try
            {
                //TODO: Handle errors and exceptions
                var entity = Activator.CreateInstance<LanguageEntity>();
                entity.Code = model.Code;
                entity.Name = model.Name;
                _dbContext.Languagess.Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity.ToDataModel<TDataModel>();
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

        public async Task<LanguageModel<TDataModel>?> EditLanguage<TDataModel>(LanguageModel<TDataModel> model)
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
                return entity.ToDataModel<TDataModel>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task addLanguageSeedAsync(String Code, String Name) {

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
        public async Task Seed()
        {

            _ = addLanguageSeedAsync("EN", "ENGLISH");
            _ = addLanguageSeedAsync("ES", "ESPAÑOL");
            //_ = addLanguageSeedAsync("IT", "ITALIANO");

        }
    }
}