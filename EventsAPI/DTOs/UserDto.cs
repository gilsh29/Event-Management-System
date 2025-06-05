namespace EventsAPI.DTOs
{
    public class UserDto
    {
        public int ID { get; set; }  // Optional if you're generating the ID in DB
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
