using System.ComponentModel.DataAnnotations.Schema;

namespace Sdc.Data
{
    public class MAT_Leg
    {
        public int Id { get; set; }

        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public int? SetId { get; set; }
        [ForeignKey("SetId")]
        public virtual MAT_Set MAT_Set { get; set; }
        public virtual List<MAT_Round> Rounds { get; set; }
    }
}
