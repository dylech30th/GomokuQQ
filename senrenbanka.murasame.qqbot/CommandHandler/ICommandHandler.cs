using Newbe.Mahua;

namespace senrenbanka.murasame.qqbot.CommandHandler
{
    public interface ICommandHandler<in T> where T : ICommandTransform
    {
        void Handle(T command, IMahuaApi replier, string toReply);
    }
}