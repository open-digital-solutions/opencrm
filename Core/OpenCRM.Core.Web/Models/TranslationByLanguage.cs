namespace OpenCRM.Core.Web.Models
{
    public class TranslationByLanguage
    {
        public required Guid ID { get; set; }

        public string? Key { get; set; }

        public string? Translation { get; set; }

        public required Guid LanguageId { get; set; }

        public string? LanguageCode { get; set; }
    }
}