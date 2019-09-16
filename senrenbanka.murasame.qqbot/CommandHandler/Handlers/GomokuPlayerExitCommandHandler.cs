using System.Text;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.BotImpl.Gomoku;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;
using senrenbanka.murasame.qqbot.Resources.CoolQ;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(GomokuPlayerExitCommand))]
    public class GomokuPlayerExitCommandHandler : ICommandHandler<GomokuPlayerExitCommand>
    {
        public void Handle(string cmdInput, GomokuPlayerExitCommand command, params object[] handleObjects)
        {
            var game = (PlayGround) handleObjects[0];
            var context = (GroupMessageReceivedContext) handleObjects[1];
            var mahuaApi = (IMahuaApi) handleObjects[2];

            if (game != null && game.IsMessageFromPlayer(context.FromQq))
            {
                var sb = new StringBuilder();
                sb.AppendLine($"{CqCode.At(context.FromQq)}离开游戏，游戏结束！");
            
                if (game.GameStarted)
                {
                    sb.Append($"根据退赛惩罚机制,{CqCode.At(context.FromQq)}将会被扣除20000点Gomoku Credit");
                    GomokuCredit.SetOrIncreaseCredit(context.FromQq, -30000);
                }
                mahuaApi.SendGroupMessage(context.FromGroup, sb.ToString());

                game.Dispose();
            }
        }
    }
}