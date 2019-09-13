using System.Collections.Generic;
using System.Linq;

namespace senrenbanka.murasame.qqbot.Persistence
{
    public class PlayerCredit
    {
        public string Player { get; set; }

        public long Credit { get; set; }
    }

    public class GomokuCredit
    {
        public static List<PlayerCredit> GomokuPlayersCredits = new List<PlayerCredit>();

        public static long GetCredit(string player)
        {
            return GomokuPlayersCredits.First(p => p.Player == player).Credit;
        }

        public const string Filename = "player_credit.json";
    }
}