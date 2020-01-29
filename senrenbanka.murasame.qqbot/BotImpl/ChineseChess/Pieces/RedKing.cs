namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Pieces
{
    public class RedKing : IChinesePiece
    {
        public string Name { get; set; } = "帅";

        public PieceType ChessmanType { get; } = PieceType.Red;
    }
}