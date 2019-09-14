using System;
using System.Linq;
using senrenbanka.murasame.qqbot.Resources.Runtime;

namespace senrenbanka.murasame.qqbot.CommandHandler.Commands
{
    public static class CommandFactory
    {
        public static Type Get(string name)
        {
            var commandTypes = Reflection.GetAllTypesInNamespace("senrenbanka.murasame.qqbot.CommandHandler.Commands").GetAllTypesImplementInterface<ICommandBase>();
            return (from type in commandTypes let instance = type.GetConstructor(new Type[] { })?.Invoke(null) as ICommandBase where instance?.Name == name select type).FirstOrDefault();
        }
    }
}