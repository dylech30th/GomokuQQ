using Newbe.Mahua.MahuaEvents;
using System;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.Persistence;
using senrenbanka.murasame.qqbot.Resources.CoolQ;

namespace senrenbanka.murasame.qqbot.BotImpl.TestBot
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class TestGroupMessageReceivedMahuaEvent : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public TestGroupMessageReceivedMahuaEvent(IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            if (context.Message.StartsWith("/image") && context.FromQq == Configuration.Me)
            {
                var image = context.Message.Substring(7);
                _mahuaApi.SendGroupMessage(context.FromGroup, CqCode.Image(image));
            }
        }
    }
}
