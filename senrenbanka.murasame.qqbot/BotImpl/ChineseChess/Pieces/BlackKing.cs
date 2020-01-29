namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Pieces
{
    public class BlackKing : IChinesePiece
    {
        public string Name { get; } = "将";

        public PieceType ChessmanType { get; } = PieceType.Black;
    }
}