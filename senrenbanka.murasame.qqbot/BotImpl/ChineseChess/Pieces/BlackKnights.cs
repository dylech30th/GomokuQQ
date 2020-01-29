namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Pieces
{
    public class BlackKnights : IChinesePiece
    {
        public string Name { get; } = "马";

        public PieceType ChessmanType { get; } = PieceType.Black;
    }
}