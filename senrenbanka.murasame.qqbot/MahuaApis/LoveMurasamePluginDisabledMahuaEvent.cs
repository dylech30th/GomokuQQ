using Newbe.Mahua.MahuaEvents;
using System.IO;
using System.Threading.Tasks;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.Persistence;
using senrenbanka.murasame.qqbot.Resources.Primitive;

namespace senrenbanka.murasame.qqbot.MahuaApis
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
            Task.Run(() => File.WriteAllText(GomokuCredit.Filename, GomokuCredit.GomokuPlayersCredits.ToJson()));
        }
    }
}
