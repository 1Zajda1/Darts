using System.Security.Cryptography.Xml;

namespace Sdc.Model
{
    public class Tournament
    {
        public Tournament()
        {
            Players = new List<int>();
            Matches = new List<MatchFull>();
            SelectedPlayers = new List<SelectedPlayer>();
            NumberOfGroups = 1;
            NumberOfSets = 1;
            NumberOfLegs = 1;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        private int numberOfPlayers;
        public int NumberOfPlayers { get => numberOfPlayers; set
            {
                numberOfPlayers = value;
                SelectedPlayers.Clear();
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    SelectedPlayers.Add(new SelectedPlayer());
                }
            }
        }

        public List<int> Players { get; set; }
        public List<MatchFull> Matches { get; set; }

        public int NumberOfMatches { get; set; }
        public int NumberOfGroups { get; set; }
        public int NumberOfLegs { get; set; }
        public int NumberOfSets { get; set; }
        public int FullScore { get; set; }
        public int PlayerIsPlayoff { get; set; }

        public List<SelectedPlayer> SelectedPlayers { get; set; }
        public bool HasPlayoff { get; set; }
    }

    public class SelectedPlayer
    {
        public int SelectedValue { get; set; }
    }

    public class Group
    {
        public Group()
        {
            Players = new List<int>();
        }
        public List<int> Players { get; set; }

        public int GroupName { get; set; }
    }
}
