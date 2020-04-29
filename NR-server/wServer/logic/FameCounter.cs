using common;
using wServer.realm.entities;
using wServer.realm;

namespace wServer.logic
{
    public class FameCounter
    {
        Player player;
        public Player Host { get { return player; } }

        public FameStats Stats { get; private set; }
        public DbClassStats ClassStats { get; private set; }
        public FameCounter(Player player_)
        {
            player = player_;
            Stats = new FameStats();
            ClassStats = new DbClassStats(player.Client.Account);
        }
    }
}
