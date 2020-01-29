namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Pieces
{
    public class EmptyChessman : IChinesePiece
    {
        public string Name { get; } = null;

        public PieceType ChessmanType { get; } = PieceType.None;
    }
}