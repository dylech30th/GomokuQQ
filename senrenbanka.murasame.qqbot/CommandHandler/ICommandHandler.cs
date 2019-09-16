namespace senrenbanka.murasame.qqbot.CommandHandler
{
    public interface ICommandHandler<in T> where T : ICommandTransform
    {
        void Handle(string cmdInput, T command, params object[] handleObjects);
    }
}