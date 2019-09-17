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
        public void Handle(CommandContext context, GomokuPlayerSurrenderCommand command, params object[] handleObjects)
        {
            var game = (PlayGround) handleObjects[0];
            var mahuaApi = CommandFactory.GetMahuaApi();

            if (game.IsActivatedAndValid(context.From))
            {
                var isBlackWin = game.BlackPlayer != context.From;
                mahuaApi.SendGroupMessage(context.FromGroup)
                   .Text($"{CqCode.At(context.From)}选择了投降！")
                   .Newline()
                   .Text(game.GetWinMessage(isBlackWin))
                   .Done();
            }
        }
    }
}