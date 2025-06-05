namespace EventsAPI.DTOs
{
    public class EventDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int MaxRegistrations { get; set; }
        public string Location { get; set; }
    }
}
