namespace Sdc.Model
{
    public class MatchShot
    {
        public int? Number { get; set; }
        public bool IsFilled { get; set; }
        public bool IsForClosing { get; set; }
        public bool IsClosingDart { get; set; }
        public bool IsSkipped { get; set; }
        public bool IsDouble { get; set; }
        public bool IsTriple { get; set; }
        public bool IsOvershot { get; set; }

        public bool IsDone => IsFilled || IsSkipped;
    }
}
