using Newbe.Mahua.MahuaEvents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.Persistence;
using senrenbanka.murasame.qqbot.Resources.Primitive;

namespace senrenbanka.murasame.qqbot.MahuaApis
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
            Task.Run(() =>
            {
                if (!File.Exists(GomokuCredit.Filename)) File.Create(GomokuCredit.Filename);
                GomokuCredit.GomokuPlayersCredits = File.ReadAllText(GomokuCredit.Filename, Encoding.UTF8).FromJson<List<PlayerCredit>>() ?? new List<PlayerCredit>();
            });
        }
    }
}
