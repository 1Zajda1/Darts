using static System.Formats.Asn1.AsnWriter;

namespace Sdc.Model
{
    public class DartHelper
    {
        public int[] allDartValues = new int[]
        {
60 ,57 ,54 ,52, 51,50, 48, 45, 42, 40, 39, 38,36,34,33,32,30,28,27,26,24,22,20, 21,19,18,17,16,15,14,13,12,11,10,9,8,7,6,5,4,3,2,1

        };

        public int[] closingDarts3 = new int[]
        {
170,167,164,161,160,158,157,156,155,154,153,152,151,150,149,
148,147,146,145,144,143,142,141,140,139,138,137,136,135,134,
133,132,131,130,129,128,127,126,125,124,123,122,121,120,119,
118,117,116,115,114,113,112,111,110,109,108,107,106,105,104,
103,102,101,100,99,98,97,96,95,94,93 ,92 ,91,90 ,89 ,88 ,87 ,
86 ,85 ,84 ,83 ,82 ,81 ,80 ,79 ,78 ,77 ,76 ,75 ,74 ,73 ,72 ,
71 ,70 ,69 ,68 ,67 ,66 ,65 ,64 ,63 ,62 ,61 ,60 ,59 ,58 ,57 ,
56 ,55 ,54 ,53 ,52 ,51 ,50 ,49 ,48 ,47 ,46 ,45 ,44 ,43 ,42 ,
41 ,40,38,36,34,32,30,28,26,24,22,20,18,16,14,12,10,8,6,4,2

        };

        public List<int> closingDarts2 = new List<int>
        {
100,98,97,96,95,94,93 ,92 ,91,90 ,89 ,88 ,87 ,
86 ,85 ,84 ,83 ,82 ,81 ,80 ,79 ,78 ,77 ,76 ,75 ,74 ,73 ,72 ,
71 ,70 ,69 ,68 ,67 ,66 ,65 ,64 ,63 ,62 ,61 ,60 ,59 ,58 ,57 ,
56 ,55 ,54 ,53 ,52 ,51 ,50 ,49 ,48 ,47 ,46 ,45 ,44 ,43 ,42 ,
41 ,40,38,36,34,32,30,28,26,24,22,20,18,16,14,12,10,8,6,4,2

        };

        public List<int> GetPossibleFinish(int score, int dartsThrown)
        {
            List<int> possibleFinishes = new List<int>();
            for (int i = 0; i < closingDarts3.Length; i++)
            {
                if (score >= closingDarts3[i])
                {
                    int tempScore = score;
                    int tempDartsThrown = dartsThrown;
                    tempScore -= closingDarts3[i];
                    tempDartsThrown -= 1;
                    if (tempScore == 0)
                    {
                        possibleFinishes.Add(closingDarts3[i]);
                    }
                    else if (tempDartsThrown > 0)
                    {
                        List<int> subPossibleFinishes = GetPossibleFinishes(tempScore, tempDartsThrown, closingDarts3);
                        foreach (int subFinish in subPossibleFinishes)
                        {
                            possibleFinishes.Add(closingDarts3[i] + subFinish);
                        }
                    }
                }
            }

            return possibleFinishes;
        }

        static List<int> GetPossibleFinishes(int score, int dartsThrown, int[] finish)
        {
            List<int> possibleFinishes = new List<int>();
            for (int i = 0; i < finish.Length; i++)
            {
                if (score >= finish[i])
                {
                    int tempScore = score;
                    int tempDartsThrown = dartsThrown;
                    tempScore -= finish[i];
                    tempDartsThrown -= 1;
                    if (tempScore == 0)
                    {
                        possibleFinishes.Add(finish[i]);
                    }
                    else if (tempDartsThrown > 0)
                    {
                        List<int> subPossibleFinishes = GetPossibleFinishes(tempScore, tempDartsThrown, finish);
                        foreach (int subFinish in subPossibleFinishes)
                        {
                            possibleFinishes.Add(finish[i] + subFinish);
                        }
                    }
                }
            }

            return possibleFinishes;
        }

        public static void GetPossibleFinishes2(int score, int dartsThrown, int[] finish, List<int> currentFinish, List<List<int>> possibleFinishes)
        {
            if (dartsThrown == 0)
            {
                if (score == 0)
                {
                    possibleFinishes.Add(currentFinish);
                }
                return;
            }

            for (int i = 0; i < finish.Length; i++)
            {
                if (score >= finish[i])
                {
                    int tempScore = score;
                    int tempDartsThrown = dartsThrown;
                    tempScore -= finish[i];
                    tempDartsThrown -= 1;

                    List<int> tempCurrentFinish = new List<int>(currentFinish);
                    tempCurrentFinish.Add(finish[i]);
                    GetPossibleFinishes2(tempScore, tempDartsThrown, finish, tempCurrentFinish, possibleFinishes);
                }
            }
        }


        public static Tuple<int, int> EloRating(int player1Rating, int player2Rating, int player1Score, int player2Score)
        {
            //int player1Rating = 1600;
            //int player2Rating = 1400;

            //int player1Score = 1; // 1 for win, 0 for draw, -1 for loss
            //int player2Score = -1;

            int k = 40; // k-factor value

            double expectedScore1 = CalculateExpectedScore(player1Rating, player2Rating);
            double expectedScore2 = CalculateExpectedScore(player2Rating, player1Rating);

            player1Rating = UpdateRating(player1Rating, player1Score, expectedScore1, k);
            player2Rating = UpdateRating(player2Rating, player2Score, expectedScore2, k);

            return new Tuple<int, int>(player1Rating, player2Rating);
        }

        static double CalculateExpectedScore(int player1Rating, int player2Rating)
        {
            return 1 / (1 + Math.Pow(10, (player2Rating - player1Rating) / 400.0));
        }

        static int UpdateRating(int playerRating, int playerScore, double expectedScore, int kFactor)
        {
            return (int)Math.Round(playerRating + kFactor * (playerScore - expectedScore));
        }

        public static string ShotString(MatchShot shot)
        {
            if (shot == null)
                return "";
            if (shot.IsTriple)
                return "T" + (shot.Number / 3).ToString();
            if (shot.IsDouble)
                return "D" + (shot.Number / 2).ToString();
            return shot.Number.ToString();
        }

        public static string PoradiBackground(int poradi)
        {
            if (poradi == 1)
                return "gold-background";

            if (poradi == 2)
                return "silver-background";

            if (poradi == 3)
                return "bronze-background";

            return "";
        }
    }
}
