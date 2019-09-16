using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
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

        public static bool Process(string command, params object[] handleObjects)
        {
            // 获取命令名
            var index = command.IndexOf(" ", StringComparison.Ordinal);
            var commandName = index == -1 ? command : command.Substring(0, index);

            // 反射获取对应的命令实体类
            var type = Get(commandName);

            if (type != null && typeof(ICommandTransform).IsAssignableFrom(type))
            {
                var handlerType = Assembly.GetExecutingAssembly().GetClasses().FirstOrDefault(t => t.GetCustomAttribute<HandlerOf>()?.CommandType == type.Name);
                // 检查handlerType是否是ICommandHandler<>的子类
                if (handlerType.IsInheritFromGeneric(typeof(ICommandHandler<>), typeof(ICommandTransform)))
                {   
                    // 获取Handler实例
                    var handler = handlerType.GetNonParameterConstructor().Invoke(null);
                    // 反射调用Handler的Handle方法, 实现命令处理
                    handler.CallMethod("Handle", command, type.GetNonParameterConstructor().Invoke(null), handleObjects);
                    return true;
                }
            }
            return false;
            // 我之所以写这么多注释是因为这方法我总是忘了我写的啥
        }
    }
}