namespace OpenCRM.Core.Web.Models
{
    public class TranslationModel
    {
        public required string Key { get; set; }

        public List<TranslationByLanguage>? Translations { get; set; }
    }
}
