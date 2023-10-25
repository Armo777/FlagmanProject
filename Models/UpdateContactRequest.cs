namespace FlagmanProject.Models
{
    public class UpdateContactRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
    }
}
