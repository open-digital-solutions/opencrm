using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenCRM.Core.Data
{
    public class UserEntity : IdentityUser<Guid>, IHasTimestamps
    {
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        [Column(TypeName = "jsonb")]
        public string Data { get; set; } = "{}";

        DateTime? IHasTimestamps.AddedAt { get; set;  } = DateTime.UtcNow;
        DateTime? IHasTimestamps.UpdatedAt { get; set; } = DateTime.UtcNow;
        DateTime? IHasTimestamps.DeletedAt { get; set; } = DateTime.UtcNow;
    }
}
