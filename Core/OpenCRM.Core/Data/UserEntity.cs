using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDHS.Shared.Data
{
    public class UserEntity : IdentityUser<Guid>, IHasTimestamps
    {
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        public Guid? MediaId { get; set; }
        public MediaEntity? Avatar { get; set; }


        [Column(TypeName = "jsonb")]
        public required string UserExtras { get; set; }

        DateTime? IHasTimestamps.AddedAt { get; set;  } = DateTime.UtcNow;
        DateTime? IHasTimestamps.UpdatedAt { get; set; } = DateTime.UtcNow;
        DateTime? IHasTimestamps.DeletedAt { get; set; } = DateTime.UtcNow;
    }
}
