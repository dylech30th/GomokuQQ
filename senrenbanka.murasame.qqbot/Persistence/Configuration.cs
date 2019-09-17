using System.IO;
using System.Text.RegularExpressions;

namespace senrenbanka.murasame.qqbot.Persistence
{
    public class Configuration
    {
        public static string Me
        {
            get
            {
                if (File.Exists("owner.txt"))
                {
                    var content = File.ReadAllText("owner.txt");
                    if (Regex.Match(content, "^\\d{5,10}$").Success)
                    {
                        return content;
                    }
                }
                return "2653221698";
            }
        }
    }
}