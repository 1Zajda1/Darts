using Sdc.Model;

namespace Sdc.Services
{
    public interface IPlayerService
    {
        Player GetPlayer(int? Id);
        Player GetPlayerByPassword(string username, string password);
        bool SavePlayer(Player player);
        List<Player> GetPlayers();
        List<StatsModel> GetStats(int? playerId);
        List<StatsModel> GetCloseStats(int? playerId);

        Tuple<int, int, double?> GetPercent(int? playerId);

    }
}
