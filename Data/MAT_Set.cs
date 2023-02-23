using System.ComponentModel.DataAnnotations.Schema;

namespace Sdc.Data
{
    public class MAT_Set
    {
        public int Id { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public int MatchId { get; set; }
        [ForeignKey("MatchId")]
        public virtual MAT_Match Match { get; set; }
        public virtual List<MAT_Leg> Legs { get; set; }
    }
}
