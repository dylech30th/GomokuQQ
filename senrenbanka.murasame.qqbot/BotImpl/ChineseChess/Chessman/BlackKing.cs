namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Chessman
{
    public class BlackKing : IChineseChessman
    {
        public string Name { get; set; } = "将";

        public ChessmanType ChessmanType { get; set; } = ChessmanType.Black;
    }
}