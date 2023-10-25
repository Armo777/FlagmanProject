namespace FlagmanProject.Models
{
    public class AddBlockDto
    {
        public AddBlockDto(string name, string surname, string phone, string email)
        {
            Name = name;
            Surname = surname;
            Phone = phone;
            Email = email;
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
