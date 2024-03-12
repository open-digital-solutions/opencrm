namespace OpenCRM.Core.Web.Services.TranslationService
{
    public class TranslationByLanguage
    {
		public required Guid ID { get; set; }

        public string? Key { get; set; }

        public string? Translation { get; set; }

		public required Guid LanguageId { get; set; }

		public string? LanguageCode { get; set; }
	}

    public class TranslationModel
    {
        public required string Key { get; set; }

        public List<TranslationByLanguage>? Translations { get; set; }
    }
}