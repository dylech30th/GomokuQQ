namespace senrenbanka.murasame.qqbot.CommandHandler
{
    public interface ICommandHandler<in T> where T : ICommandTransform
    {
        void Handle(CommandContext context, T command, params object[] handleObjects);
    }
}