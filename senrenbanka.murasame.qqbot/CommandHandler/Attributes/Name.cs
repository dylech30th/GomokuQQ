using System;

namespace senrenbanka.murasame.qqbot.CommandHandler.Attributes
{
    public class Name : Attribute
    {
        public Name(string commandName, int lengthLeft = -1, int lengthRight = -1, MatchOption matchOption = MatchOption.PlainText)
        {
            MatchOption = matchOption;
            CommandName = commandName;
            LengthLeft = lengthLeft;
            LengthRight = lengthRight;
        }

        public int LengthLeft { get; }

        public int LengthRight { get; }

        public string CommandName { get; }

        public MatchOption MatchOption { get; }
    }

    public enum MatchOption
    {
        PlainText, RegExp
    }
}