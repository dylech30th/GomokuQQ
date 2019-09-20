using System.Linq;
using System.Text;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(GetGomokuCreditTrendCommand))]
    public class GetGomokuCreditTrendCommandHandler : ICommandHandler<GetGomokuCreditTrendCommand>
    {
        public void Handle(CommandContext context, GetGomokuCreditTrendCommand command, params object[] handleObjects)
        {
            var mahuaApi = CommandFactory.GetMahuaApi();
            var groupMembers = mahuaApi.GetGroupMemebersWithModel(context.FromGroup).Model.ToArray();

            var sortedList = GomokuCredit.GomokuPlayersCredits
               .Where(p => groupMembers.Any(member => member.Qq == p.Player))
               .OrderByDescending(player => player.Credit).Take(10);

            mahuaApi.SendGroupMessage(context.FromGroup, string.Join("\n", sortedList.Select(playerCredit => $"玩家: {groupMembers.First(p => p.Qq == playerCredit.Player).NickName} QQ: {playerCredit.Player} Gomoku Credit: {playerCredit.Credit}")));
        }
    }
}