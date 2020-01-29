namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Pieces
{
    public interface IChinesePiece
    {
        string Name { get; }

        PieceType ChessmanType { get; }
    }
}