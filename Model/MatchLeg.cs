namespace Sdc.Model
{
    public class MatchLeg
    {
        public MatchLeg()
        {
            MatchRounds = new List<MatchRound>();
        }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public bool IsDone { get; set; }
        public bool IsCollapsed { get; set; }
        public List<MatchRound> MatchRounds { get; set; }
    }
}
