using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using senrenbanka.murasame.qqbot.Resources.Generic;
using senrenbanka.murasame.qqbot.Resources.Primitive;

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

        public static long? GetCredit(string player)
        {
            return GomokuPlayersCredits.FirstOrDefault(p => p.Player == player)?.Credit;
        }

        public const string Filename = "player_credit.json";

        public static void SetOrIncreaseCredit(string id, long credit)
        {
            if (GomokuPlayersCredits.Any(p => p.Player == id))
            {
                GomokuPlayersCredits.Update(p => p.Player == id, i => i.Credit += credit);
            }
            else
            {
                GomokuPlayersCredits.Add(new PlayerCredit { Credit = credit, Player = id});
            }
        }

        public static void SetOrRewriteCredit(string id, long credit)
        {
            if (GomokuPlayersCredits.Any(p => p.Player == id))
            {
                GomokuPlayersCredits.Update(p => p.Player == id, i => i.Credit = credit);
            }
            else
            {
                GomokuPlayersCredits.Add(new PlayerCredit { Credit = credit, Player = id});
            }
        }

        public static void SaveCreditFile()
        {
            Task.Run(() =>
            {
                if (!File.Exists(Filename))
                {
                    File.Create(Filename);
                }
                File.WriteAllText(Filename, GomokuPlayersCredits.ToJson());
            });
        }

        public static void LoadCreditFile()
        {
            if (File.Exists(Filename))
            {
                GomokuPlayersCredits = File.ReadAllText(Filename).FromJson<List<PlayerCredit>>();
            }
        }
    }
}