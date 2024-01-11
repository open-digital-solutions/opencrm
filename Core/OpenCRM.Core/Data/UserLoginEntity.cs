using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OpenDHS.Shared.Data
{
    public class UserLoginEntity : IdentityUserLogin<Guid>, IHasTimestamps
    {
        public DateTime? AddedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime? DeletedAt { get; set; } = DateTime.UtcNow;
    }
}
