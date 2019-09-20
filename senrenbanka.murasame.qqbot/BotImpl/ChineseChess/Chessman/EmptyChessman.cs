namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Chessman
{
    public class EmptyChessman : IChineseChessman
    {
        public string Name { get; set; } = null;

        public ChessmanType ChessmanType { get; set; } = ChessmanType.None;
    }
}