using Newbe.Mahua.MahuaEvents;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.BotImpl.Gomoku;
using senrenbanka.murasame.qqbot.Persistence;
using senrenbanka.murasame.qqbot.Resources.CoolQ;

namespace senrenbanka.murasame.qqbot.MahuaApis
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class GomokuCommandGroupMessageReceivedEvent : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public GomokuCommandGroupMessageReceivedEvent(IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            var game = GomokuFactory.GetOrCreatePlayGround(context.FromGroup);
            ProcessJoinGameRequest(context, game);
            if (game != null && game.GameFull && game.GameStarted && game.IsMessageFromPlayer(context.FromQq))
            {
                var regex = Regex.Match(context.Message, "^\\d{1,2}[a-oA-O]*");
                if (regex.Success && regex.Value.Length == 2 || regex.Value.Length == 3) 
                {
                    ProcessPlayerGoCommand(game, context);
                }
                if (context.Message == "/gf")
                {
                    ProcessPlayerSurrender(game, context);
                }
            }

            if (game != null && game.IsMessageFromPlayer(context.FromQq))
            {
                switch (context.Message)
                {
                    case "/ge":
                        ProcessPlayerExit(game, context);
                        break;
                    case "/gs":
                        ProcessEndGame(game, context);
                        break;
                }
            }
        }

        private void ProcessJoinGameRequest(GroupMessageReceivedContext context, PlayGround game)
        {
            if (context.Message.Equals("/gj"))
            {
                var result = game.PlayerJoinGame(context.FromQq);

                switch (result)
                {
                    case PlayerJoinStat.GameFull:
                        _mahuaApi.SendGroupMessage(context.FromGroup, "游戏已满，请等待游戏结束");
                        return;
                    case PlayerJoinStat.HaveJoined:
                        _mahuaApi.SendGroupMessage(context.FromGroup, "你已经加入过了");
                        return;
                    case PlayerJoinStat.Success:
                        if (game.GameFull)
                        {
                            _mahuaApi.SendGroupMessage(context.FromGroup, $"白方:{CqCode.At(context.FromQq)}成功加入！");
                            game.StartGame();
                            PrintStartMessage(context, game);
                        }
                        else
                        {
                            _mahuaApi.SendGroupMessage(context.FromGroup, $"黑方:{CqCode.At(context.FromQq)}成功加入！");
                        }
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void PrintStartMessage(GroupMessageReceivedContext context, PlayGround game)
        {
            var message = new StringBuilder("开始游戏！\n");
            message.AppendLine("---------------------------------");
            message.AppendLine("命令列表: (您随时都可以使用/help查看命令)");
            message.AppendLine("   落子: x坐标y坐标(先后顺序不固定,0a和a0效果等同)");
            message.AppendLine("   退出: /ge");
            message.AppendLine("   结束游戏: /gs");
            message.AppendLine("   投降: /gf");
            message.AppendLine("   查看Gomoku Credit: /gc");
            message.AppendLine("---------------------------------");

            switch (game.Role)
            {
                case Role.Black:
                    message.Append($"黑方:{CqCode.At(game.BlackPlayer)}先手！请{CqCode.At(game.BlackPlayer)}走子！");
                    break;
                case Role.White:
                    message.Append($"白方:{CqCode.At(game.WhitePlayer)}先手！请{CqCode.At(game.WhitePlayer)}走子！");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _mahuaApi.SendGroupMessage(context.FromGroup, message.ToString());
            SendImage(context, game);
        }

        private void ProcessPlayerGoCommand(PlayGround game, GroupMessageReceivedContext context)
        {
            if (game.IsBlackOrWhiteTurn(context.FromQq))
            {
                var dropResult = game.ProcessGoCommand(context.Message);
                switch (dropResult.GameState)
                {
                    case GameState.Success:
                        _mahuaApi.SendGroupMessage(context.FromGroup, $"{(game.Role == Role.White ? "黑方成功落子" : "白方成功落子")}: [{dropResult.DropPoint.X},{Axis.NumberToYAxis(dropResult.DropPoint.Y)}]");
                        SendImage(context, game);
                        break;
                    case GameState.IllegalDropLocation:
                        _mahuaApi.SendGroupMessage(context.FromGroup, "诶呀,您落子的位置好像不太合理,再重新试试吧?");
                        return;
                    case GameState.AbortCuzChessBoardFull:
                        _mahuaApi.SendGroupMessage(context.FromGroup)
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
                    SendWinMessage(game, context, isBlackWin);
                }
                else
                {
                    _mahuaApi.SendGroupMessage(context.FromGroup, $"请{(game.Role == Role.White ? $"白方{CqCode.At(game.WhitePlayer)}走子！" : $"黑方{CqCode.At(game.BlackPlayer)}走子！")}");
                }
            }
        }

        private void SendWinMessage(PlayGround game, GroupMessageReceivedContext context, bool isBlackWin)
        {
            var winner = isBlackWin ? game.BlackPlayer : game.WhitePlayer;
            var loser = isBlackWin ? game.WhitePlayer : game.BlackPlayer;

            SetCredit(winner, loser);

            _mahuaApi.SendGroupMessage(context.FromGroup, $"{(isBlackWin ? "黑" : "白")}方{CqCode.At(winner)}胜利！游戏结束！");
            _mahuaApi.SendGroupMessage(context.FromGroup)
               .Text($"胜者{CqCode.At(winner)}获得10000 Gomoku Credit, 现在有{GomokuCredit.GetCredit(winner)} Gomoku Credit")
               .Newline()
               .Text($"败者{CqCode.At(loser)}扣去10000 Gomoku Credit, 现在有{GomokuCredit.GetCredit(loser)} Gomoku Credit")
               .Done();

            game.Dispose();
        }

        private static void SetCredit(string winner, string loser)
        {
            GomokuFactory.SetOrIncreasePlayerCredit(winner, 10000);
            GomokuFactory.SetOrIncreasePlayerCredit(loser, -10000);
        }

        private void ProcessPlayerExit(PlayGround game, GroupMessageReceivedContext context)
        {
            _mahuaApi.SendGroupMessage(context.FromGroup, $"{CqCode.At(context.FromQq)}离开游戏，游戏结束！");
            game.Dispose();
        }

        private void ProcessEndGame(PlayGround game, GroupMessageReceivedContext context)
        {
            _mahuaApi.SendGroupMessage(context.FromGroup, $"{CqCode.At(context.FromQq)}选择了结束游戏，游戏结束！");
            game.Dispose();
        }

        private void ProcessPlayerSurrender(PlayGround game, GroupMessageReceivedContext context)
        {
            var isBlackWin = game.BlackPlayer != context.FromQq;
            _mahuaApi.SendGroupMessage(context.FromGroup, $"{CqCode.At(context.FromQq)}选择了投降!");
            
            SendWinMessage(game, context, isBlackWin);
        }

        private void SendImage(GroupMessageReceivedContext context, PlayGround game)
        {
            _mahuaApi.SendGroupMessage(context.FromGroup, CqCode.Image($"{game.GameId}\\ChessBoard.jpg"));
        }
    }
}

