using System;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.BotImpl.Gomoku;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Resources.CoolQ;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(GomokuPlayerGoCommand))]
    public class GomokuPlayerGoCommandHandler : ICommandHandler<GomokuPlayerGoCommand>
    {
        public void Handle(CommandContext context, GomokuPlayerGoCommand command, params object[] handleObjects)
        {
            var game = (PlayGround) handleObjects[0];

            var mahuaApi = CommandFactory.GetMahuaApi();

            if (game.IsActivatedAndValid(context.From) && game.IsBlackOrWhiteTurn(context.From))
            {
                var dropResult = game.ProcessGoCommand(context.Message);
                switch (dropResult.GameState)
                {
                    case GameState.Success:
                        mahuaApi.SendGroupMessage(context.FromGroup)
                           .Text($"{(game.Role == Role.White ? "黑方成功落子" : "白方成功落子")}: [{dropResult.DropPoint.X},{Axis.NumberToYAxis(dropResult.DropPoint.Y)}]")
                           .Newline()
                           .Text(CqCode.Image($"{game.GameId}\\ChessBoard_{game.Steps}.jpg"))
                           .Done();
                        break;
                    case GameState.IllegalDropLocation:
                        mahuaApi.SendGroupMessage(context.FromGroup, "诶呀,您落子的位置好像不太合理,再重新试试吧?");
                        return;
                    case GameState.AbortCuzChessBoardFull:
                        mahuaApi.SendGroupMessage(context.FromGroup)
                           .Text("棋盘上已经没有地方了,和棋!")
                           .Newline()
                           .Text("游戏结束！")
                           .Done();
                        game.Dispose();
                        return;
                    case GameState.IrrelevantMessage:
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var winResult = game.IsWin(dropResult.DropPoint.X, dropResult.DropPoint.Y);
                if (winResult != Chessman.Empty)
                {
                    var isBlackWin = winResult == Chessman.Black;
                    mahuaApi.SendGroupMessage(context.FromGroup, game.GetWinMessage(isBlackWin));
                }
                else
                {
                    mahuaApi.SendGroupMessage(context.FromGroup, $"请{(game.Role == Role.White ? $"白方{CqCode.At(game.WhitePlayer)}走子！" : $"黑方{CqCode.At(game.BlackPlayer)}走子！")}");
                }
            }
        }
    }
}