using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.BotImpl.Gomoku;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;

namespace senrenbanka.murasame.qqbot.MahuaEvents
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class GomokuCommandGroupMessageReceivedEvent : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public GomokuCommandGroupMessageReceivedEvent(IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            var game = GomokuFactory.GetOrCreatePlayGround(context.FromGroup);
            CommandFactory.Process(context.Message, game, context, _mahuaApi);
        }
    }
}

