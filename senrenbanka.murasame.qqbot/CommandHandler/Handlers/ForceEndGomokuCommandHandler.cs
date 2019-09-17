using System.Linq;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.BotImpl.Gomoku;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(ForceEndGomokuCommand))]
    public class ForceEndGomokuCommandHandler : ICommandHandler<ForceEndGomokuCommand>
    {
        public void Handle(string cmdInput, ForceEndGomokuCommand command, params object[] handleObjects)
        {
            var toReply = handleObjects[0] as string;
            var mahuaApi = CommandFactory.GetMahuaApi();

            if (long.TryParse(command.Transform(cmdInput).ToList()[0], out var group))
            {
                var game = GomokuFactory.GetOrCreatePlayGround(group.ToString());
                if (game != null)
                {
                    mahuaApi.SendGroupMessage(toReply, $"成功结束游戏: {game.GameId}");
                    game.Dispose();
                    return;
                }
                mahuaApi.SendGroupMessage(toReply, "参数错误");
            }
        }
    }
}