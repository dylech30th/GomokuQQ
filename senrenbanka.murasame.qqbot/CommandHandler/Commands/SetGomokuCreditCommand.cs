using System;
using System.Collections.Generic;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;

namespace senrenbanka.murasame.qqbot.CommandHandler.Commands
{
    [Name("/gcset")]
    [AdminOnly]
    [Namespace("ns:Query")]
    public class SetGomokuCreditCommand : ICommandTransform
    {
        public IEnumerable<string> Transform(string cmdInput)
        {
            return cmdInput.Substring(cmdInput.IndexOf(" ", StringComparison.Ordinal) + 1).Split( ' ');
        }
    }
}