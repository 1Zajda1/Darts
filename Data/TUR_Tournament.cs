namespace Sdc.Data
{
    public class TUR_Tournament
    {
        public TUR_Tournament()
        {
            MAT_Matches = new List<MAT_Match>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfPlayers { get; set; }
        public int NumberOfGroups { get; set; }
        public int NumberOfLegs { get; set; }
        public int NumberOfSets { get; set; }
        /// <summary>
        /// 501,301
        /// </summary>
        public int FullScore { get; set; }
        public int PlayerIsPlayoff { get; set; }

        public virtual List<MAT_Match>? MAT_Matches { get; set; }
        public bool HasPlayoff { get; set; }
    }
}
