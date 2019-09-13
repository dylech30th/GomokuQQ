using System.Collections.Generic;
using System.Linq;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.BotImpl.Gomoku
{
    public class GomokuFactory
    {
        private static readonly Dictionary<string, PlayGround> Games = new Dictionary<string, PlayGround>();

        public static PlayGround GetOrCreatePlayGround(string group)
        {
            if (Games.ContainsKey(group))
            {
                return Games[group];
            }

            Games[group] = new PlayGround(group);
            return Games[group];
        }

        public static void SetOrIncreasePlayerCredit(string qq, long increment)
        {
            if (GomokuCredit.GomokuPlayersCredits.Any(p => p.Player == qq))
            {
                GomokuCredit.GomokuPlayersCredits.First(p => p.Player == qq).Credit += increment;
            }
            else
            {
                GomokuCredit.GomokuPlayersCredits.Add(new PlayerCredit
                {
                    Player = qq,
                    Credit = increment
                });
            }
        }

        public static void RemoveGame(string gameId)
        {
            Games.Remove(gameId);
        }
    }
}