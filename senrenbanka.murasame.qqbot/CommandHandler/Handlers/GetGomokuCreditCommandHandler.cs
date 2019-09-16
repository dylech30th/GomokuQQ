using System;
using System.Linq;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(GetGomokuCreditCommand))]
    public class GetGomokuCreditCommandHandler : ICommandHandler<GetGomokuCreditCommand>
    {
        public void Handle(string cmdInput, GetGomokuCreditCommand command, params object[] handleObjects)
        {
            var qq = command.Transform(cmdInput).ToList()[0];
            var credit = GomokuCredit.GetCredit(qq);

            var replier = handleObjects[0] as IMahuaApi;
            var toReply = handleObjects[1] as string;

            replier?.SendGroupMessage(toReply, credit.HasValue ? $"{qq}的GomokuCredit为{credit.Value}" : $"{qq}尚没有游戏记录");
        }
    }
}