using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Newbe.Mahua.MahuaEvents;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.HelpDocs;
using senrenbanka.murasame.qqbot.Resources.Runtime;

namespace senrenbanka.murasame.qqbot.MahuaApis
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
            if (context.Message.StartsWith("/help"))
            {
                var types = Reflection.GetAllTypesInNamespace("senrenbanka.murasame.qqbot.HelpDocs").GetAllTypesImplementInterface<IDocsBase>();
                var sb = new StringBuilder();
                foreach (var type in types)
                {
                    var instance = type.GetConstructor(new Type[] { })?.Invoke(null);
                    sb.Append(instance);
                }

                _mahuaApi.SendGroupMessage(context.FromGroup, sb.ToString());
            }
        }
    }
}
