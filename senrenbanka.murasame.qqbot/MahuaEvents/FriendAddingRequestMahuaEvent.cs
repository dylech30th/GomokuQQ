using Newbe.Mahua.MahuaEvents;
using Newbe.Mahua;

namespace senrenbanka.murasame.qqbot.MahuaEvents
{
    /// <summary>
    /// 好友申请接受事件
    /// </summary>
    public class FriendAddingRequestMahuaEvent
        : IFriendAddingRequestMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public FriendAddingRequestMahuaEvent(IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessAddingFriendRequest(FriendAddingRequestContext context)
        {
            _mahuaApi.AcceptFriendAddingRequest(context.AddingFriendRequestId, context.FromQq, context.Message);
        }
    }
}
