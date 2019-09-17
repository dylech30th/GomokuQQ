using System.Linq;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(SetGomokuCreditCommand))]
    public class SetCreditCommandHandler : ICommandHandler<SetGomokuCreditCommand>
    {
        public void Handle(string cmdInput, SetGomokuCreditCommand command, params object[] handleObjects)
        {
            var toReply = handleObjects[0] as string;
            var mahuaApi = CommandFactory.GetMahuaApi();

            var p = command.Transform(cmdInput).ToList();
            if (long.TryParse(p[0], out var id) && long.TryParse(p[1], out var credit))
            {
                GomokuCredit.SetOrRewriteCredit(id.ToString(), credit);
                mahuaApi.SendGroupMessage(toReply, "设置GomokuCredit成功");
                return;
            }
            mahuaApi.SendGroupMessage(toReply, "参数错误");
        }
    }
}