using System.Linq;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(OpUserCommand))]
    public class OpUserCommandHandler : ICommandHandler<OpUserCommand>
    {
        public void Handle(CommandContext context, OpUserCommand command, params object[] handleObjects)
        {
            var mahuaApi = CommandFactory.GetMahuaApi();

            if (long.TryParse(command.Transform(context.Message).ToList()[0], out var id))
            {
                mahuaApi.SendGroupMessage(context.FromGroup, "设置OP成功");
                Admin.AddAdmin(id.ToString());
                return;
            }
            mahuaApi.SendGroupMessage(context.FromGroup, "参数错误");
        }
    }
}