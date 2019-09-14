using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.MahuaEvents
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
            Admin.SaveAdmins();
            GomokuCredit.SaveCreditFile();
        }
    }
}
