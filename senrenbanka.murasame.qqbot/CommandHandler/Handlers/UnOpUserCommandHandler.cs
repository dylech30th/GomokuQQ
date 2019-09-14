using System.Linq;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;
using senrenbanka.murasame.qqbot.Resources.Generic;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(UnOpUserCommand))]
    public class UnOpUserCommandHandler : ICommandHandler<UnOpUserCommand>
    {
        public void Handle(UnOpUserCommand command, IMahuaApi replier, string toReply)
        {
            if (long.TryParse(command.Parameters.ToList()[0], out var id))
            {
                Admin.Administrators.RemoveWhere(p => p.Id == id.ToString());
                Admin.SaveAdmins();
                replier.SendGroupMessage(toReply, "移除成功");
                return;
            }
            replier.SendGroupMessage(toReply, "参数错误");
        }
    }
}