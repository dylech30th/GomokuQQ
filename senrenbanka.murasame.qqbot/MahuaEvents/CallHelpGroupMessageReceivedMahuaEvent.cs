using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.HelpDocs;

namespace senrenbanka.murasame.qqbot.MahuaEvents
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class CallHelpGroupMessageReceivedMahuaEvent
        : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public CallHelpGroupMessageReceivedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            if (context.Message == "/help")
            {
                _mahuaApi.SendGroupMessage(context.FromGroup, Docs.GetDocsString());
            }
        }
    }
}
