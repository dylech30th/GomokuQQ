using System.Linq;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;
using senrenbanka.murasame.qqbot.Resources.Generic;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(UnOpUserCommand))]
    public class UnOpUserCommandHandler : ICommandHandler<UnOpUserCommand>
    {
        public void Handle(string cmdInput, UnOpUserCommand command, params object[] handleObjects)
        {
            var toReply = handleObjects[0] as string;
            var mahuaApi = CommandFactory.GetMahuaApi();

            if (long.TryParse(command.Transform(cmdInput).ToList()[0], out var id))
            {
                Admin.GetAdministrators().RemoveWhere(p => p.Id == id.ToString());
                Admin.SaveAdmins();
                mahuaApi.SendGroupMessage(toReply, "移除成功");
                return;
            }
            mahuaApi.SendGroupMessage(toReply, "参数错误");
        }
    }
}