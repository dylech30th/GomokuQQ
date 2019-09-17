using senrenbanka.murasame.qqbot.CommandHandler.Attributes;
using senrenbanka.murasame.qqbot.CommandHandler.Commands;
using senrenbanka.murasame.qqbot.Persistence;

namespace senrenbanka.murasame.qqbot.CommandHandler.Handlers
{
    [HandlerOf(nameof(GetOwnerCommand))]
    public class GetOwnerCommandHandler : ICommandHandler<GetOwnerCommand>
    {
        public void Handle(CommandContext context, GetOwnerCommand command, params object[] handleObjects)
        {
            CommandFactory.GetMahuaApi().SendGroupMessage(context.FromGroup, Configuration.Me);
        }
    }
}