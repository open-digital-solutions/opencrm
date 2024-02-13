namespace OpenCRM.Core.Data
{
    public class UserSessionEntity : BaseEntity
    {
        public required string UserId { get; set; }
        public DateTime IssuedDate { get; set; }
        public required string CypherToken { get; set; }
    }
}
