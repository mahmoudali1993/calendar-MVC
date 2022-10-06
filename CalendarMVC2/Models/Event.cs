using System.ComponentModel.DataAnnotations;

namespace CalendarMVC2.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        //Relational data
        public virtual Location Location { get; set; }
        public virtual User User { get; set; }
        public Event(IFormCollection form, Location location)
        {
            Id = Int32.Parse(form["Id"]);
            Name = form["Name"];
            Description = form["Description"];
            StartTime = DateTime.Parse(form["StartTime"]);
            EndTime = DateTime.Parse(form["EndTime"]);
            Location = location;
        }
        public void UpdateEvent(IFormCollection form, Location location)
        {
            Id = Int32.Parse(form["Id"]);
            Name = form["Name"];
            Description = form["Description"];
            StartTime = DateTime.Parse(form["StartTime"]);
            EndTime = DateTime.Parse(form["EndTime"]);
            Location = location;
        }
        public Event()
        {

        }
       
    }
}
