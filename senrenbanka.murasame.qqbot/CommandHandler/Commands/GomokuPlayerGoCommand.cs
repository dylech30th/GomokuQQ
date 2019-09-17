using System.Collections.Generic;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;

namespace senrenbanka.murasame.qqbot.CommandHandler.Commands
{
    [Name("^\\d{1,2}[a-oA-O]$", MatchOption.RegExp)]
    [Namespace("ns:Gomoku")]
    public class GomokuPlayerGoCommand : ICommandTransform
    {
        public IEnumerable<string> Transform(string cmdInput)
        {
            throw new System.NotImplementedException();
        }
    }
}