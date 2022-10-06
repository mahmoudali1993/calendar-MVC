using Microsoft.AspNetCore.Identity;

namespace CalendarMVC2.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Event> Events { get; set; }
    }
}
