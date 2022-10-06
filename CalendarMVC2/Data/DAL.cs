using CalendarMVC2.Models;

namespace CalendarMVC2.Data
{
    public interface IDAL
    {
        public List<Event> GetEvents();
        public List<Event> GetMyEvents(string userid);
        public Event GetEvent(int id);
        public void CreateEvent(IFormCollection form);
        public void UpdateEvent(IFormCollection form);
        public void DeleteEvent(int id);
        public List<Location> GetLocations();
        public Location GetLocation(int id);
        public void CreateLocation(Location location);

    }
    public class DAL : IDAL
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        public List<Event> GetEvents()
        {
            return _db.Events.ToList();
        }
        public List<Event> GetMyEvents(string userid)
        {
            return _db.Events.Where(x => x.User.Id == userid).ToList();
        }
        public Event GetEvent(int id)
        {
            return _db.Events.FirstOrDefault(x => x.Id == id);
        }
        public void CreateEvent(IFormCollection form)
        {
            var newevent = new Event(form, _db.Locations.FirstOrDefault(x => x.Name == form["Location"]));
            _db.Events.Add(newevent);
            _db.SaveChanges();
        }

        public void UpdateEvent(IFormCollection form)
        {
            var myevent =_db.Events.FirstOrDefault(x => x.Id == int.Parse(form["Id"]));
            var location = _db.Locations.FirstOrDefault(x => x.Name == form["Location"]);
            myevent.UpdateEvent(form, location);
            _db.Entry(myevent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.SaveChanges();
        }
        public void DeleteEvent(int id)
        {
            var myevent = _db.Events.Find(id);
            _db.Events.Remove(myevent);
            _db.SaveChanges();
        }
        public List<Location> GetLocations()
        {
            return _db.Locations.ToList();
        }
        public Location GetLocation(int id)
        {
            return _db.Locations.Find(id);
        }
        public void CreateLocation(Location location)
        {
            _db.Locations.Add(location);
            _db.SaveChanges();
        }
    }
}
