using System;

namespace senrenbanka.murasame.qqbot.BotImpl.Gomoku
{
    public static class Axis
    {
        public static ChessmanPoint GetPoint(this ChessmanPoint[,] points, int x, int y)
        {
            return points[x, y];
        }


        public static string NumberToYAxis(int num)
        {
            if (num >= 0 && num <= 14)
            {
                return ((char) ('A' + num)).ToString();
            }
            
            throw new ArgumentException(nameof(num));
        }

        public static int YAxisToNumber(string y)
        {
            var ch = y.ToUpper().ToCharArray();

            if (ch.Length == 1 && ch[0] >= 'A' && ch[0] <= 'O')
            {
                return ch[0] - 'A';
            }

            throw new ArgumentException(nameof(y));
        }
    }
}