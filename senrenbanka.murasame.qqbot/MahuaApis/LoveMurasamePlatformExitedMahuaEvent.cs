using Newbe.Mahua.MahuaEvents;
using System;
using System.IO;
using System.Threading.Tasks;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.Persistence;
using senrenbanka.murasame.qqbot.Resources.Primitive;

namespace senrenbanka.murasame.qqbot.MahuaApis
{
    /// <summary>
    /// 机器人平台退出事件
    /// </summary>
    public class LoveMurasamePlatformExitedMahuaEvent : IPlatfromExitedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public LoveMurasamePlatformExitedMahuaEvent(IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void Exited(PlatfromExitedContext context)
        {
            Task.Run(() => File.WriteAllText(GomokuCredit.Filename, GomokuCredit.GomokuPlayersCredits.ToJson()));
        }
    }
}
