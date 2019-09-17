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
        public void Handle(CommandContext context, UnOpUserCommand command, params object[] handleObjects)
        {
            var mahuaApi = CommandFactory.GetMahuaApi();

            if (long.TryParse(command.Transform(context.Message).ToList()[0], out var id))
            {
                Admin.GetAdministrators().RemoveWhere(p => p.Id == id.ToString());
                mahuaApi.SendGroupMessage(context.FromGroup, "移除成功");
                Admin.SaveAdmins();
                return;
            }
            mahuaApi.SendGroupMessage(context.FromGroup, "参数错误");
        }
    }
}