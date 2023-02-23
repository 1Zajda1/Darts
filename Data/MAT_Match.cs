using System.ComponentModel.DataAnnotations.Schema;

namespace Sdc.Data
{
    public class MAT_Match
    {
        public MAT_Match()
        {
            DateTimeCreated = DateTime.Now;
            MAT_Sets = new List<MAT_Set>();
        }

        public int Id { get; set; }
        public int? Player1Score { get; set; }
        public int? Player2Score { get; set; }

        public int? Player1Id { get; set; }
        [InverseProperty("Player1Matches")]
        [ForeignKey("Player1Id")]
        public virtual PLA_Player? Player1 { get; set; }

        public int? Player2Id { get; set; }
        [InverseProperty("Player2Matches")]
        [ForeignKey("Player2Id")]
        public virtual PLA_Player? Player2 { get; set; }


        public int? TournamentId { get; set; }
        [ForeignKey("TournamentId")]
        public virtual TUR_Tournament? Tournament { get; set; }

        public int Group { get; set; }

        public int NumberOfLegs { get; set; }
        public int NumberOfSets { get; set; }

        /// <summary>
        /// pozice v jaké se nachází playoff .. 1 => finále , 2,3 => semifinále , 4,5,6,7 => čtvrtfinále
        /// </summary>
        public int? PositionInPlayoff { get; set; }
        
        /// <summary>
        /// 501,301
        /// </summary>
        public int FullScore { get; set; }

        public virtual List<MAT_Set> MAT_Sets { get; set; }

        public DateTime? DateTimePlayed { get; set; }
        public DateTime? DateTimeDone { get; set; }
        public DateTime? DateTimePlaned { get; set; }
        public DateTime? DateTimeCreated { get; set; }

    }
}
