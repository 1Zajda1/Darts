using System.ComponentModel.DataAnnotations.Schema;

namespace Sdc.Data
{
    public class PLA_Player
    {
        public PLA_Player()
        {
            Player1Matches = new List<MAT_Match>();
            Player2Matches = new List<MAT_Match>();
            MAT_Round = new List<MAT_Round>();
            PLA_EloHistories = new List<PLA_EloHistory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        //[Column(TypeName = "decimal(10,2)")]
        public int Rank { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Image { get; set; }

        [InverseProperty("Player1")]
        public virtual List<MAT_Match> Player1Matches { get; set; }
        [InverseProperty("Player2")]
        public virtual List<MAT_Match> Player2Matches { get; set; }
        public virtual List<MAT_Round>? MAT_Round { get; set; }
        public virtual List<PLA_EloHistory>? PLA_EloHistories { get; set; }

        public bool IsAdmin { get; set; }
    }
}
