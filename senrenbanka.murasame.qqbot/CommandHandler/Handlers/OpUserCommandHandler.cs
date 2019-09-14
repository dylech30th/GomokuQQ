using System.Linq;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(OpUserCommand))]
    public class OpUserCommandHandler : ICommandHandler<OpUserCommand>
    {
        public void Handle(OpUserCommand command, IMahuaApi replier, string toReply)
        {
            if (long.TryParse(command.Parameters.ToList()[0], out var id))
            {
                Admin.Administrators.Add(new Administrator {Id = id.ToString()});
                Admin.SaveAdmins();
                replier.SendGroupMessage(toReply, "设置OP成功");
                return;
            }
            replier.SendGroupMessage(toReply, "参数错误");
        }
    }
}