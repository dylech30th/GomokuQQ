namespace senrenbanka.murasame.qqbot.CommandHandler
{
    public interface ICommandTransform : ICommandBase
    {
        void Transform(string cmdInput);
    }
}