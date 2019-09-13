using Newbe.Mahua.MahuaEvents;
using System;
using Newbe.Mahua;

namespace senrenbanka.murasame.qqbot.MahuaApis
{
    /// <summary>
    /// 入群邀请接收事件
    /// </summary>
    public class GroupJoiningInvitationReceivedMahuaEvent : IGroupJoiningInvitationReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public GroupJoiningInvitationReceivedMahuaEvent(IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessJoinGroupRequest(GroupJoiningRequestReceivedContext context)
        {
            _mahuaApi.AcceptGroupJoiningInvitation(context.GroupJoiningRequestId, context.ToGroup, context.FromQq);
        }
    }
}
