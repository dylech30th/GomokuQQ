using System.Collections.Generic;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;

namespace senrenbanka.murasame.qqbot.CommandHandler.Commands
{
    [Name("/gj")]
    public class GomokuPlayerJoinGameCommand : ICommandTransform
    {
        public IEnumerable<string> Transform(string cmdInput)
        {
            throw new System.NotImplementedException();
        }
    }
}