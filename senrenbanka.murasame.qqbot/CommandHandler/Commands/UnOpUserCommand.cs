using System;
using System.Collections.Generic;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;

namespace senrenbanka.murasame.qqbot.CommandHandler.Commands
{
    [OwnerOnly]
    [AdminOnly]
    public class UnOpUserCommand : ICommandTransform
    {
        public string Name { get; set; } = "/unop";
        
        public IEnumerable<string> Parameters { get; set; }

        public void Transform(string cmdInput)
        {
            Parameters = cmdInput.Substring(cmdInput.IndexOf(" ", StringComparison.Ordinal) + 1).Split( ' ');
        }
    }
}