using System.Collections.Generic;

namespace senrenbanka.murasame.qqbot.CommandHandler
{
    public interface ICommandTransform
    {
        IEnumerable<string> Transform(string cmdInput);
    }
}