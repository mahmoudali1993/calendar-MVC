using System.ComponentModel.DataAnnotations;

namespace CalendarMVC2.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //Relational data
        public virtual ICollection<Event> Events { get; set; }
    }
}
