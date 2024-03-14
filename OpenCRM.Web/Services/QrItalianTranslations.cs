using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.TranslationService;

namespace OpenCRM.Web.Services;

public class QrItalianTranslations
{
	private Dictionary<string, TranslationByLanguage> qritalianTranslations = new Dictionary<string, TranslationByLanguage>();

    public QrItalianTranslations()
    {
		qritalianTranslations.Add("KEY_QR_GENERATORE", new TranslationByLanguage()
		{
			ID = Guid.Empty,
			Key = "KEY_QR_GENERATORE",
			Translation = "KEY_QR_GENERATORE",
			LanguageId = Guid.Empty,
			LanguageCode = "IT"
		});

		qritalianTranslations.Add("KEY_QR_TESTO", new TranslationByLanguage()
		{
			ID = Guid.Empty,
			Key = "KEY_QR_TESTO",
			Translation = "KEY_QR_TESTO",
			LanguageId = Guid.Empty,
            LanguageCode = "IT"
        });

		qritalianTranslations.Add("KEY_QR_INSERISCI_IL_TESTO", new TranslationByLanguage()
		{
			ID = Guid.Empty,
			Key = "KEY_QR_INSERISCI_IL_TESTO",
			Translation = "KEY_QR_INSERISCI_IL_TESTO",
			LanguageId = Guid.Empty,
            LanguageCode = "IT"
        });

		qritalianTranslations.Add("KEY_QR_GENERARE_CODICE", new TranslationByLanguage()
		{
			ID = Guid.Empty,
			Key = "KEY_QR_GENERARE_CODICE",
			Translation = "KEY_QR_GENERARE_CODICE",
			LanguageId = Guid.Empty,
            LanguageCode = "IT"
        });

		qritalianTranslations.Add("KEY_QR_QR_CODICE", new TranslationByLanguage()
		{
			ID = Guid.Empty,
			Key = "KEY_QR_QR_CODICE",
			Translation = "KEY_QR_QR_CODICE",
			LanguageId = Guid.Empty,
			LanguageCode = "IT"
		});

		qritalianTranslations.Add("KEY_QR_SCARICA_IL_CODICE_QR", new TranslationByLanguage()
		{
			ID = Guid.Empty,
			Key = "KEY_QR_SCARICA_IL_CODICE_QR",
			Translation = "KEY_QR_SCARICA_IL_CODICE_QR",
			LanguageId = Guid.Empty,
			LanguageCode = "IT"
		});
	}

	public Dictionary<string, TranslationByLanguage> Translations { get { return qritalianTranslations; } }
}
