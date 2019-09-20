using System.Drawing;
using senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Chessman;

namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Rules
{
    public interface IChineseChessRules
    {
        bool MovementRational(IChineseChessman chessman, Point from, Point to, Chessboard chessboard);
    }
}