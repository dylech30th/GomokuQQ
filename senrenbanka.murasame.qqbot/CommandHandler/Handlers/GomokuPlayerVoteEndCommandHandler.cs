using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using senrenbanka.murasame.qqbot.BotImpl.Gomoku;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Resources.CoolQ;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(GomokuPlayerVoteEndCommand))]
    public class GomokuPlayerVoteEndCommandHandler : ICommandHandler<GomokuPlayerVoteEndCommand>
    {
        public void Handle(string cmdInput, GomokuPlayerVoteEndCommand command, params object[] handleObjects)
        {
            var game = (PlayGround) handleObjects[0];
            var context = (GroupMessageReceivedContext) handleObjects[1];
            var mahuaApi = (IMahuaApi) handleObjects[2];

            if (game.IsActivatedAndValid(context.FromQq))
            {
                if (game.VoteForEnd.isVoting && game.IsMessageFromPlayer(context.FromQq) && context.FromQq != game.VoteForEnd.qq)
                {
                    mahuaApi.SendGroupMessage(context.FromGroup, $"投票通过,结束ID为{game.GameId}的游戏");
                    game.Dispose();
                }
                else if (!game.VoteForEnd.isVoting && game.IsMessageFromPlayer(context.FromQq))
                {
                    mahuaApi.SendGroupMessage(context.FromGroup, $"{CqCode.At(context.FromQq)}发起结束游戏投票!同意请输入/ve");
                    game.VoteForEnd = (context.FromQq, true);
                }
            }
        }
    }
}