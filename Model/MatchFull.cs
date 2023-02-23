using Sdc.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sdc.Model
{
    public class MatchFull
    {
        public MatchFull()
        {
            DateTimeCreated = DateTime.Now;
            DateTimePlaned = DateTime.Now;
            Sets = new List<MatchSet>();
            NumberOfLegs = 1;
            NumberOfSets = 1;
        }

        public int Id { get; set; }

        [Required]
        public int? Player1Id { get; set; }
        public string Player1Name { get; set; }

        [Required]
        public int? Player2Id { get; set; }
        public string Player2Name { get; set; } 

        public int? Player1Score { get; set; }
        public int? Player1ScoreSets { get; set; }
        public int? Player1ScoreLegs { get; set; }
        public int? Player2ScoreSets { get; set; }
        public int? Player2ScoreLegs { get; set; }
        public int? Player2Score { get; set; }

        public int NumberOfLegs { get; set; }
        public int NumberOfSets { get; set; }
        /// <summary>
        /// 501,301
        /// </summary>
        public int FullScore { get; set; }
        public int Group { get; set; }

        public int? PositionInPlayoff { get; set; }
        public int? TournamentId { get; set; }

        public virtual List<MatchSet> Sets { get; set; }

        public DateTime? DateTimePlayed { get; set; }
        public DateTime? DateTimeDone { get; set; }
        public DateTime? DateTimePlaned { get; set; }
        public DateTime? DateTimeCreated { get; set; }
    }
}
