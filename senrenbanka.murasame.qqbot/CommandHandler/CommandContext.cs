namespace senrenbanka.murasame.qqbot.CommandHandler
{
    public class CommandContext
    {
        public string From { get; set; }

        public string FromGroup { get; set; }

        public string Message { get; set; }

        public string Namespace { get; set; }

        public CommandContext(string from, string fromGroup, string message, string @namespace)
        {
            Namespace = @namespace;
            From = from;
            FromGroup = fromGroup;
            Message = message;
        }
    }
}