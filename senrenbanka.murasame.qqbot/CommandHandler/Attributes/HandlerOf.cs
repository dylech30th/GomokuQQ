using System;

namespace senrenbanka.murasame.qqbot.CommandHandler.Attributes
{
    public class HandlerOf : Attribute
    {
        public string CommandType;

        public HandlerOf(string commandType)
        {
            CommandType = commandType;
        }
    }
}