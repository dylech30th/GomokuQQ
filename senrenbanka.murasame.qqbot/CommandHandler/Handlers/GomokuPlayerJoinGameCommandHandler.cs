using System;
using System.Text;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.BotImpl.Gomoku;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Resources.CoolQ;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(GomokuPlayerJoinGameCommand))]
    public class GomokuPlayerJoinGameCommandHandler : ICommandHandler<GomokuPlayerJoinGameCommand>
    {
        public void Handle(CommandContext context, GomokuPlayerJoinGameCommand command, params object[] handleObjects)
        {
            var game = (PlayGround) handleObjects[0];
            var mahuaApi = CommandFactory.GetMahuaApi();

            var result = game.PlayerJoinGame(context.From);

            switch (result)
            {
                case PlayerJoinStat.GameFull:
                    mahuaApi.SendGroupMessage(context.FromGroup, "游戏已满，请等待游戏结束");
                    return;
                case PlayerJoinStat.HaveJoined:
                    mahuaApi.SendGroupMessage(context.FromGroup, "你已经加入过了");
                    return;
                case PlayerJoinStat.Success:
                    if (game.GameFull)
                    {
                        mahuaApi.SendGroupMessage(context.FromGroup, $"白方:{CqCode.At(context.From)}成功加入！");
                        game.StartGame();
                        PrintStartMessage(context.FromGroup, game, mahuaApi);
                    }
                    else
                    {
                        mahuaApi.SendGroupMessage(context.FromGroup, $"黑方:{CqCode.At(context.From)}成功加入！");
                    }
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void PrintStartMessage(string group, PlayGround game, IMahuaApi mahuaApi)
        {
            var message = new StringBuilder("开始游戏！\n");
            message.AppendLine("---------------------------------");
            message.AppendLine("命令列表: (您随时都可以使用/help查看命令)");
            message.AppendLine("   落子: x坐标y坐标(先后顺序不固定,0a和a0效果等同)");
            message.AppendLine("   退出: /ge");
            message.AppendLine("   投降: /gf");
            message.AppendLine("   投票结束: /ve");
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

            mahuaApi.SendGroupMessage(group)
               .Text(message.ToString())
               .Newline()
               .Text(CqCode.Image($"{game.GameId}\\ChessBoard_{game.Steps}.jpg"))
               .Done();
        }
    }
}