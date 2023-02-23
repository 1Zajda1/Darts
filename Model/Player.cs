namespace Sdc.Model
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public int Rank { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CountMatches { get; set; }
        public bool IsAdmin { get; set; }
    }
}
