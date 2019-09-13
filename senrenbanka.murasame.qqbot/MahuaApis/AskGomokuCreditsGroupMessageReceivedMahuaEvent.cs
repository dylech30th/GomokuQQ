using System.Linq;
using Newbe.Mahua.MahuaEvents;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.MahuaApis
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class AskGomokuCreditsGroupMessageReceivedMahuaEvent
        : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public AskGomokuCreditsGroupMessageReceivedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            if (context.Message.Equals("/gc"))
            {
                _mahuaApi.SendGroupMessage(context.FromGroup, GomokuCredit.GomokuPlayersCredits.Any(p => p.Player == context.FromQq) ? $"您的Gomoku Credit当前为: {GomokuCredit.GomokuPlayersCredits.First(p => p.Player == context.FromQq).Credit}" : "哎呀，您还没有玩过五子棋，输入/gomoku join来一盘吧?");
            }
        }
    }
}
