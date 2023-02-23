using System.ComponentModel.DataAnnotations.Schema;

namespace Sdc.Data
{
    public class PLA_EloHistory
    {
        public PLA_EloHistory() { }

        public int Id { get; set; }

        public DateTime DateTimeChanged { get; set; }

        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual PLA_Player Player { get; set; }
        //[Column(TypeName = "decimal(10,2)")]
        public int RankOld { get; set; }
        //[Column(TypeName = "decimal(10,2)")]
        public int RankNew { get; set; }

        public int RankDifference => RankOld - RankNew;
    }
}
