namespace OpenCRM.Core.Web.Services.LanguageService
{
    public class LanguageModel
    {      
        public required Guid ID { get; set; }

        public required string Code { get; set; }

        public required string Name { get; set; }
    }
}