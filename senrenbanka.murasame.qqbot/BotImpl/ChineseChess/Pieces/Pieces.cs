using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Documents;

namespace senrenbanka.murasame.qqbot.BotImpl.ChineseChess.Pieces
{
    public static class Pieces
    {
        public static readonly Point InvalidCoordinate = new Point(-1, -1);

        public static bool CollisionWith(this IChinesePiece piece, IChinesePiece another)
        {
            return piece.ChessmanType != PieceType.None &&
                   another.ChessmanType != PieceType.None &&
                   piece.ChessmanType != another.ChessmanType;
        }

        public static List<IChinesePiece> GetBetween(this IChinesePiece piece, IChinesePiece another, Chessboard chessboard)
        {
            var pos1 = chessboard.FindPos(piece);
            var pos2 = chessboard.FindPos(another);

            var list = new List<IChinesePiece>();
            if (pos1.Valid() && pos2.Valid())
            {
                if (pos1 == pos2) return list;
                if (pos1.X == pos2.X)
                {
                    var x = pos1.X;
                    for (var i = (pos1.Y > pos2.Y ? pos2 : pos1).Y + 1; i < (pos1.Y > pos2.Y ? pos1 : pos2).Y; i++)
                    {
                        var currentPiece = chessboard.GetPiece(new Point(x, i));
                        if (currentPiece.NotEmptyPiece())
                        {
                            list.Add(currentPiece);
                        }
                    }
                }
                else if (pos1.Y == pos2.Y)
                {
                    var y = pos1.Y;
                    for (var i = (pos1.X > pos2.X ? pos2 : pos1).X; i < (pos1.X > pos2.X ? pos1 : pos2).X; i++)
                    {
                        var currentPiece = chessboard.GetPiece(new Point(i, y));
                        if (currentPiece.NotEmptyPiece())
                        {
                            list.Add(currentPiece);
                        }
                    }
                }

                return list;
            }

            throw new InvalidOperationException();
        }

        public static bool FatalMovement(this IChinesePiece piece, IChinesePiece another, Chessboard chessboard)
        {

        }

        private static bool NotEmptyPiece(this IChinesePiece piece)
        {
            return piece.ChessmanType != PieceType.None;
        }

        public static bool Valid(this Point point)
        {
            return point != InvalidCoordinate;
        }
    }
}