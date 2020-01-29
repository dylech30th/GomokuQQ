using System.Drawing;
using senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Pieces;

namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Rules
{
    public interface IChineseChessRules
    {
        bool MovementRational(IChinesePiece chessman, Point from, Point to, Chessboard chessboard);
    }
}