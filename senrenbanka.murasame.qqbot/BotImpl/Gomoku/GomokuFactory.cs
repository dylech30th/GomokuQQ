using System.Collections.Generic;

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

        public static bool TryGetPlayGround(string group, out PlayGround playGround)
        {
            if (Games.ContainsKey(group))
            {
                playGround = Games[group];
                return true;
            }

            playGround = null;
            return false;
        }

        public static void RemoveGame(string gameId)
        {
            Games.Remove(gameId);
        }
    }
}