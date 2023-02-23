using Sdc.Model;

namespace Sdc.Services
{
    public interface IMatchService
    {
        MatchFull GetMatchFull(int Id);
        bool SaveMatch(MatchFull MatchFull);
        List<MatchFull> GetMatches();

        bool SaveCompleteMatch(MatchFull MatchFull);

        bool AddTournament(Tournament tournament);

        Tournament GetTournament(int Id);

        List<Tournament> GetTournaments();

        bool GeneratePlayoff(Tournament tournament);

        bool ChangeSides(int Id);
        MatchFull GetDetailMatch(int Id);
        bool DeleteMatch(int Id);

    }
}
