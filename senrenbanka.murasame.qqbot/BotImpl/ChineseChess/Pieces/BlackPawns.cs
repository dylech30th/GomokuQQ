namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Pieces
{
    public class BlackPawns : IChinesePiece
    {
        public string Name { get; } = "卒";

        public PieceType ChessmanType { get; } = PieceType.Black;
    }
}