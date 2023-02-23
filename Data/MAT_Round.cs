using System.ComponentModel.DataAnnotations.Schema;

namespace Sdc.Data
{
    public class MAT_Round
    {
        public int Id { get; set; }

        public int RoundCount { get; set; }

        public int LegId { get; set; }
        [ForeignKey("LegId")]
        public virtual MAT_Leg Leg { get; set; }

        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual PLA_Player Player { get; set; }

        public virtual List<MAT_Shot_x_Round> Shots { get; set; }
    }
}
