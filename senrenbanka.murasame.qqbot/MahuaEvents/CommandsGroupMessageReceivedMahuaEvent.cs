using System;
using System.Linq;
using System.Reflection;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.CommandHandler;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;
using senrenbanka.murasame.qqbot.Resources.Runtime;

namespace senrenbanka.murasame.qqbot.MahuaEvents
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class CommandsGroupMessageReceivedMahuaEvent
        : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public CommandsGroupMessageReceivedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            var index = context.Message.IndexOf(" ", StringComparison.Ordinal);

            if (index != -1)
            {
                var commandName = context.Message.Substring(0, index);
                var type = CommandFactory.Get(commandName);

                if (type != null)
                {
                    if (type.HasCustomAttribute<OwnerOnly>())
                    {
                        if (context.FromQq == Configuration.Me)
                            Process(type, context.Message, context.FromQq, _mahuaApi, context.FromGroup);
                        else
                            _mahuaApi.SendGroupMessage(context.FromGroup, "You don't have permission to do that");
                    }

                    if (type.HasCustomAttribute<AdminOnly>())
                    {
                        if (Admin.Administrators.Any(ad => ad.Id == context.FromQq))
                            Process(type, context.Message, context.FromQq, _mahuaApi, context.FromGroup);
                        else
                            _mahuaApi.SendGroupMessage(context.FromGroup, "You don't have permission to do that");
                    }
                    else
                        Process(type, context.Message, context.FromQq, _mahuaApi, context.FromGroup);
                }
            }
        }

        public static bool Process(Type type, string command, string id, IMahuaApi replier, string toReply)
        {
            var handlerType = Reflection.GetAllTypesInNamespace("senrenbanka.murasame.qqbot.CommandHandler.Handlers").FirstOrDefault(t => t.GetCustomAttribute<HandlerOf>().CommandType == type.Name);

            if (handlerType.IsInheritFromGeneric(typeof(ICommandHandler<>), typeof(ICommandTransform)) && Admin.Administrators.Any(ad => ad.Id == id))
            {
                if (type.GetNonParameterConstructor().Invoke(null) is ICommandTransform typeInstance)
                {
                    var handler = handlerType.GetNonParameterConstructor().Invoke(null);
                    typeInstance.CallMethod("Transform", command);
                    handler.CallMethod("Handle", typeInstance, replier, toReply);
                    return true;
                }
            }
            return false;
        }
    }
}
