using System.Drawing;
using senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Pieces;

namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Rules
{
    public class CannonsRule : IChineseChessRules
    {
        public bool MovementRational(IChinesePiece chessman, Point from, Point to, Chessboard chessboard)
        {
            var fromPiece = chessboard.GetPiece(from);
            var toPiece = chessboard.GetPiece(to);
            if (fromPiece.CollisionWith(toPiece))
            {
                if (fromPiece.GetBetween(toPiece, chessboard).Count == 1 && fromPiece.FatalMovement(toPiece, chessboard))
                {
                    return true;
                }
            }
        }
    }
}