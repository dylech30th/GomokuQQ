using System.Collections.Generic;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;

namespace senrenbanka.murasame.qqbot.CommandHandler.Commands
{
    [Name("/gt")]
    [Namespace("ns:Query")]
    public class GetGomokuCreditTrendCommand : ICommandTransform
    {
        public IEnumerable<string> Transform(string cmdInput)
        {
            throw new System.NotImplementedException();
        }
    }
}