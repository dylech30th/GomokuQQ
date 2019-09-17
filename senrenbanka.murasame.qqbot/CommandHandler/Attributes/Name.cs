using System;

namespace senrenbanka.murasame.qqbot.CommandHandler.Attributes
{
    public class Name : Attribute
    {
        public Name(string commandName, MatchOption matchOption = MatchOption.PlainText)
        {
            MatchOption = matchOption;
            CommandName = commandName;
        }

        public string CommandName { get; }

        public MatchOption MatchOption { get; }
    }

    public enum MatchOption
    {
        PlainText, RegExp
    }
}