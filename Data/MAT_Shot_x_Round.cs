using System.ComponentModel.DataAnnotations.Schema;

namespace Sdc.Data
{
    public class MAT_Shot_x_Round
    {
        public int Id { get; set; } 

        public int ShotCount { get; set; }
        public int? ShotId { get; set; }
        [ForeignKey("ShotId")]
        public virtual MAT_Shot Shot { get; set; }

        public int? RoundId { get; set; }
        [ForeignKey("RoundId")]
        public virtual MAT_Round Round { get; set; }
    }
}
