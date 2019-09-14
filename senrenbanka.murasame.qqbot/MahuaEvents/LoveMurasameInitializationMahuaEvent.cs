using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.MahuaEvents
{
    /// <summary>
    /// 插件初始化事件
    /// </summary>
    public class LoveMurasameInitializationMahuaEvent
        : IInitializationMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public LoveMurasameInitializationMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void Initialized(InitializedContext context)
        {
            Admin.LoadAdmins();
            GomokuCredit.LoadCreditFile();
        }
    }
}
