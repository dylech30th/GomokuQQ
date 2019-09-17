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
        public void Handle(CommandContext context, GomokuPlayerVoteEndCommand command, params object[] handleObjects)
        {
            var game = (PlayGround) handleObjects[0];
            var mahuaApi = CommandFactory.GetMahuaApi();

            if (game.IsActivatedAndValid(context.From))
            {
                if (game.VoteForEnd.isVoting && game.IsMessageFromPlayer(context.From) && context.From != game.VoteForEnd.qq)
                {
                    mahuaApi.SendGroupMessage(context.FromGroup, $"投票通过,结束ID为{game.GameId}的游戏");
                    game.Dispose();
                }
                else if (!game.VoteForEnd.isVoting && game.IsMessageFromPlayer(context.From))
                {
                    mahuaApi.SendGroupMessage(context.FromGroup, $"{CqCode.At(context.From)}发起结束游戏投票!同意请输入/ve");
                    game.VoteForEnd = (context.From, true);
                }
            }
        }
    }
}