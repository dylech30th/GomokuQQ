using System.Linq;
using Newbe.Mahua;
using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(GetOpListCommand))]
    public class GetOpListCommandHandler : ICommandHandler<GetOpListCommand>
    {
        public void Handle(CommandContext context, GetOpListCommand command, params object[] handleObjects)
        {
            var mahuaApi = CommandFactory.GetMahuaApi();
            var str = $"{{{string.Join(",", Admin.GetAdministrators().Select(admin => admin.Id))}}}";
            mahuaApi.SendGroupMessage(context.FromGroup, str);
        }
    }
}