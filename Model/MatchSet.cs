namespace Sdc.Model
{
    public class MatchSet
    {
        public MatchSet()
        {
            MatchLegs = new List<MatchLeg>();
        }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public bool IsDone { get; set; }
        public bool IsCollapsed { get; set; }
        public List<MatchLeg> MatchLegs { get; set; }
    }
}
