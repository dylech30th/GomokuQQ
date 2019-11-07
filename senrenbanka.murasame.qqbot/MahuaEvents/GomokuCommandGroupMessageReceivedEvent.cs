using System;
using System.Text;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.BotImpl.Gomoku;
using senrenbanka.murasame.qqbot.CommandHandler;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Resources.CoolQ;

namespace senrenbanka.murasame.qqbot.MahuaEvents
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
            const string joinGameCmd = "/gj";

            PlayGround game;
            if (context.Message == joinGameCmd)
            {
                game = GomokuFactory.GetOrCreatePlayGround(context.FromGroup);
                PlayerJoinGame(game, context);
            }
            else if (GomokuFactory.TryGetPlayGround(context.FromGroup, out game))
            {
                CommandFactory.Process(new CommandContext(context.FromQq, context.FromGroup, context.Message, CommandFactory.GomokuCommandsNs), game);
            }
        }

        private void PlayerJoinGame(PlayGround game, GroupMessageReceivedContext context)
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
                        PrintStartMessage(context.FromGroup, game);
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

        private void PrintStartMessage(string group, PlayGround game)
        {
            var message = new StringBuilder("开始游戏！\n");
            message.AppendLine("---------------------------------");
            message.AppendLine("命令列表: (您随时都可以使用/help查看命令)");
            message.AppendLine("   落子: x坐标y坐标");
            message.AppendLine("   退出: /ge");
            message.AppendLine("   投降: /gf");
            message.AppendLine("   投票结束: /ve");
            message.AppendLine("   查看Gomoku Credit: /gc");
            message.AppendLine("   查看Gomoku Credit排行榜: /gt");
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

            _mahuaApi.SendGroupMessage(group)
               .Text(message.ToString())
               .Newline()
               .Text(CqCode.Image($"{game.GameId}\\ChessBoard_{game.Steps}.jpg"))
               .Done();
        }
    }
}

