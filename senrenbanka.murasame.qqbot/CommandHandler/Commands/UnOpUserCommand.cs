using System;
using System.Collections.Generic;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;

namespace senrenbanka.murasame.qqbot.CommandHandler.Commands
{
    [Name("/unop")]
    [OwnerOnly]
    [AdminOnly]
    public class UnOpUserCommand : ICommandTransform
    {
        public IEnumerable<string> Transform(string cmdInput)
        {
            return cmdInput.Substring(cmdInput.IndexOf(" ", StringComparison.Ordinal) + 1).Split( ' ');
        }
    }
}