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
            var toReply = handleObjects[0] as string;
            var mahuaApi = CommandFactory.GetMahuaApi();

            if (long.TryParse(command.Transform(cmdInput).ToList()[0], out var id))
            {
                Admin.AddAdmin(id.ToString());
                Admin.SaveAdmins();
                mahuaApi.SendGroupMessage(toReply, "设置OP成功");
                return;
            }
            mahuaApi.SendGroupMessage(toReply, "参数错误");
        }
    }
}