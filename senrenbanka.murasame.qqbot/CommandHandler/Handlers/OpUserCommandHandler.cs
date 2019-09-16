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

        public void Handle(string cmdInput, OpUserCommand command, params object[] handleObjects)
        {
            var replier = handleObjects[0] as IMahuaApi;
            var toReply = handleObjects[1] as string;

            if (long.TryParse(command.Transform(cmdInput).ToList()[0], out var id))
            {
                Admin.Administrators.Add(new Administrator {Id = id.ToString()});
                Admin.SaveAdmins();
                replier?.SendGroupMessage(toReply, "设置OP成功");
                return;
            }
            replier?.SendGroupMessage(toReply, "参数错误");
        }
    }
}