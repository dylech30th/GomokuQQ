using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.Persistence;
using senrenbanka.murasame.qqbot.Resources.Runtime;

namespace senrenbanka.murasame.qqbot.CommandHandler.Commands
{
    public static class CommandFactory
    {
        public static Type Get(string name)
        {
            var commandTypes = Assembly.GetExecutingAssembly().GetClasses().GetAllTypesImplementInterface<ICommandTransform>();

            foreach (var commandType in commandTypes)
            {
                var metadata = commandType.GetCustomAttribute<Name>();

                switch (metadata.MatchOption)
                {
                    case MatchOption.PlainText:
                        if (metadata.CommandName == name)
                        {
                            return commandType;
                        } 
                        break;
                    case MatchOption.RegExp:
                        if (Regex.Match(name, metadata.CommandName).Success && name.Length >= metadata.LengthLeft && name.Length <= metadata.LengthRight)
                        {
                            return commandType;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return null;
        }

        public static bool Process(CommandContext context, params object[] handleObjects)
        {
            var index = context.Message.IndexOf(" ", StringComparison.Ordinal);
            var commandName = index == -1 ? context.Message : context.Message.Substring(0, index);

            var type = Get(commandName);

            if (type != null && IsCallerHasPrivilege(context, type) && typeof(ICommandTransform).IsAssignableFrom(type))
            {
                var handlerType = Assembly.GetExecutingAssembly().GetClasses().FirstOrDefault(t => t.GetCustomAttribute<HandlerOf>()?.CommandType == type.Name && !t.HasCustomAttribute<Deprecated>() && !IsExcluded(t, context.FromGroup));
                if (handlerType != null && handlerType.IsInheritFromGeneric(typeof(ICommandHandler<>), typeof(ICommandTransform)))
                {
                    var handler = handlerType.GetNonParameterConstructor().Invoke(null);
                    handler.CallMethod("Handle", context.Message, type.GetNonParameterConstructor().Invoke(null), handleObjects);
                    return true;
                }
            }
            return false;
        }

        private static bool IsCallerHasPrivilege(CommandContext context, Type commandType)
        {
            if (commandType.HasCustomAttribute<OwnerOnly>() && context.From != Configuration.Me)
            {
                return false;
            }

            return !commandType.HasCustomAttribute<AdminOnly>() || context.From == Configuration.Me || Admin.GetAdministrators().All(admin => admin.Id != context.From);
        }

        private static bool IsExcluded(MemberInfo type, string group)
        {
            var attr = type.GetCustomAttribute<Exclude>();
            return attr != null && attr.Excludes.Contains(group);
        }

        public static IMahuaApi GetMahuaApi()
        {
            return MahuaRobotManager.Instance.CreateSession().MahuaApi;
        }
    }
}