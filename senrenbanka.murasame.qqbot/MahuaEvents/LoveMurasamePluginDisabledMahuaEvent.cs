using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.MahuaEvents
{
    /// <summary>
    /// 插件被禁用事件
    /// </summary>
    public class LoveMurasamePluginDisabledMahuaEvent : IPluginDisabledMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public LoveMurasamePluginDisabledMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void Disable(PluginDisabledContext context)
        {
            Admin.SaveAdmins();
            GomokuCredit.SaveCreditFile();
        }
    }
}
