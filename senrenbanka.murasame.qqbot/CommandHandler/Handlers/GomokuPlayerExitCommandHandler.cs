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
        public void Handle(CommandContext context, GomokuPlayerExitCommand command, params object[] handleObjects)
        {
            var game = (PlayGround) handleObjects[0];

            if (game != null && game.IsMessageFromPlayer(context.From))
            {
                var sb = new StringBuilder();
                sb.Append($"{CqCode.At(context.From)}离开游戏，游戏结束！");
            
                if (game.GameStarted)
                {
                    sb.Append($"\n根据退赛惩罚机制,{CqCode.At(context.From)}将会被扣除20000点Gomoku Credit");
                    GomokuCredit.SetOrIncreaseCredit(context.From, -30000);
                }
                CommandFactory.GetMahuaApi().SendGroupMessage(context.FromGroup, sb.ToString());

                game.Dispose();
            }
        }
    }
}