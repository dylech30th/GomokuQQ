using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.BotImpl.Gomoku;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Resources.CoolQ;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(GomokuPlayerSurrenderCommand))]
    public class GomokuPlayerSurrenderCommandHandler : ICommandHandler<GomokuPlayerSurrenderCommand>
    {
        public void Handle(string cmdInput, GomokuPlayerSurrenderCommand command, params object[] handleObjects)
        {
            var game = (PlayGround) handleObjects[0];
            var context = (GroupMessageReceivedContext) handleObjects[1];
            var mahuaApi = (IMahuaApi) handleObjects[2];

            if (game.IsActivatedAndValid(context.FromQq))
            {
                var isBlackWin = game.BlackPlayer != context.FromQq;
                mahuaApi.SendGroupMessage(context.FromGroup)
                   .Text($"{CqCode.At(context.FromQq)}选择了投降！")
                   .Newline()
                   .Text(game.GetWinMessage(isBlackWin))
                   .Done();
            }
        }
    }
}