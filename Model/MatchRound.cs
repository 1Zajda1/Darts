using Sdc.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sdc.Model
{
    public class MatchRound
    {
        public MatchRound()
        {
            Shot1 = new MatchShot();
            Shot2 = new MatchShot();
            Shot3 = new MatchShot();
        }
        public int RoundCount { get; set; }
        public MatchShot Shot1 { get; set; }
        public MatchShot Shot2 { get; set; }
        public MatchShot Shot3 { get; set; }

        public bool IsComplete => Shot1.IsDone && Shot2.IsDone && Shot3.IsDone;

        public int PlayerId { get; set; }
        public int RoundSum => Shot1.Number.GetValueOrDefault() + Shot2.Number.GetValueOrDefault() + Shot3.Number.GetValueOrDefault();

        public bool IsAnyClosingDart => Shot1.IsClosingDart || Shot2.IsClosingDart || Shot3.IsClosingDart;

        public string Background => IsAnyClosingDart ? "bg-success text-white" : "bg-light";


        
    }
}
