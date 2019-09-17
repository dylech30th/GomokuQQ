using System.Linq;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(SetGomokuCreditCommand))]
    public class SetCreditCommandHandler : ICommandHandler<SetGomokuCreditCommand>
    {
        public void Handle(CommandContext context, SetGomokuCreditCommand command, params object[] handleObjects)
        {
            var mahuaApi = CommandFactory.GetMahuaApi();

            var p = command.Transform(context.Message).ToList();
            if (long.TryParse(p[0], out var id) && long.TryParse(p[1], out var credit))
            {
                GomokuCredit.SetOrRewriteCredit(id.ToString(), credit);
                mahuaApi.SendGroupMessage(context.FromGroup, "设置GomokuCredit成功");
                return;
            }
            mahuaApi.SendGroupMessage(context.FromGroup, "参数错误");
        }
    }
}