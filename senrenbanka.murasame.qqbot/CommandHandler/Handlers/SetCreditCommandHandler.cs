using System.Linq;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(SetGomokuCreditCommand))]
    public class SetCreditCommandHandler : ICommandHandler<SetGomokuCreditCommand>
    {
        public void Handle(SetGomokuCreditCommand command, IMahuaApi replier, string toReply)
        {
            var p = command.Parameters.ToList();
            if (long.TryParse(p[0], out var id) && long.TryParse(p[1], out var credit))
            {
                GomokuCredit.SetOrRewriteCredit(id.ToString(), credit);
                replier.SendGroupMessage(toReply, "设置GomokuCredit成功");
                return;
            }
            replier.SendGroupMessage(toReply, "参数错误");
        }
    }
}