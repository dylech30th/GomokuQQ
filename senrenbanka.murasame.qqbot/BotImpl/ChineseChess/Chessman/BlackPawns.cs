namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Chessman
{
    public class BlackPawns : IChineseChessman
    {
        public string Name { get; set; } = "卒";

        public ChessmanType ChessmanType { get; set; } = ChessmanType.Black;
    }
}