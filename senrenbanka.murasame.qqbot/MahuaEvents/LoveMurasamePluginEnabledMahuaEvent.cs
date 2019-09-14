using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.MahuaEvents
{
    /// <summary>
    /// 插件被启用事件
    /// </summary>
    public class LoveMurasamePluginEnabledMahuaEvent
        : IPluginEnabledMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public LoveMurasamePluginEnabledMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void Enabled(PluginEnabledContext context)
        {
            Admin.LoadAdmins();
            GomokuCredit.LoadCreditFile();
        }
    }
}
