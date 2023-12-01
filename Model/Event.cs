namespace API_CSharp.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Event(int id, string name, string description, DateTime date)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
        }
    }
}
