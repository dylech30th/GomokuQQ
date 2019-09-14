using System.Collections.Generic;

namespace senrenbanka.murasame.qqbot.CommandHandler
{
    public interface ICommandBase
    {
        string Name { get; set; }

        IEnumerable<string> Parameters { get; set; }
    }
}