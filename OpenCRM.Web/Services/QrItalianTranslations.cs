using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.TranslationService;

namespace OpenCRM.Web.Services;

public class QrItalianTranslations
{
	public QrItalianTranslations() { }

	private List<TranslationByLanguage> qritalianTranslations = new()
	{
		new TranslationByLanguage()
		{
			ID = Guid.Empty,
			Key = "KEY_QR_GENERATORE",
			Translation = "KEY_QR_GENERATORE",
			LanguageId = Guid.Empty,
			LanguageCode = "IT"
		},

		new TranslationByLanguage()
		{
			ID = Guid.Empty,
			Key = "KEY_QR_TESTO",
			Translation = "KEY_QR_TESTO",
			LanguageId = Guid.Empty,
		},

        new TranslationByLanguage()
        {
            ID = Guid.Empty,
            Key = "KEY_QR_INSERISCI_IL_TESTO",
            Translation = "KEY_QR_INSERISCI_IL_TESTO",
            LanguageId = Guid.Empty,
        },

        new TranslationByLanguage()
		{
			ID = Guid.Empty,
			Key = "KEY_QR_GENERARE_CODICE",
			Translation = "KEY_QR_GENERARE_CODICE",
			LanguageId = Guid.Empty,
		},

		new TranslationByLanguage()
		{
			ID = Guid.Empty,
			Key = "KEY_QR_QR_CODICE",
			Translation = "KEY_QR_QR_CODICE",
			LanguageId = Guid.Empty,
		},

		new TranslationByLanguage()
		{
			ID = Guid.Empty,
			Key = "KEY_QR_SCARICA_IL_CODICE_QR",
			Translation = "KEY_QR_SCARICA_IL_CODICE_QR",
			LanguageId = Guid.Empty,
		},
	};

	public List<TranslationByLanguage> Translations { get { return qritalianTranslations; } }
}
