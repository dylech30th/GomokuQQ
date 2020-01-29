namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Pieces
{
    public class RedKnights : IChinesePiece
    {
        public string Name { get; } = "马";

        public PieceType ChessmanType { get; } = PieceType.Red;
    }
}