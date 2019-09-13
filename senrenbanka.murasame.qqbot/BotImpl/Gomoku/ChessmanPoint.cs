using System.Drawing;

namespace senrenbanka.murasame.qqbot.BotImpl.Gomoku
{
    public struct ChessmanPoint
    {
        public ChessmanPoint(int x, int y, Chessman chessman)
        {
            X        = x;
            Y        = y;
            Chessman = chessman;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public Chessman Chessman { get; set; }

        public Point ParseChessmanPoint()
        {
            var actualX = X * 40 + 50;
            var actualY = Y * 40 + 50;

            return new Point(actualX, actualY);
        }
    }
}