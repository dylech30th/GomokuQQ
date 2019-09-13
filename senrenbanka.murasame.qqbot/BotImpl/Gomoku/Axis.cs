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
            switch (num)
            {
                case 0:
                    return "A";
                case 1:
                    return "B";
                case 2:
                    return "C";
                case 3:
                    return "D";
                case 4:
                    return "E";
                case 5:
                    return "F";
                case 6:
                    return "G";
                case 7:
                    return "H";
                case 8:
                    return "I";
                case 9:
                    return "J";
                case 10:
                    return "K";
                case 11:
                    return "L";
                case 12:
                    return "M";
                case 13:
                    return "N";
                case 14:
                    return "O";
                default:
                    throw new ArgumentException(nameof(num));
            }
        }

        public static int YAxisToNumber(string y)
        {
            switch (y.ToUpper())
            {
                case "A":
                    return 0;
                case "B":
                    return 1;
                case "C":
                    return 2;
                case "D":
                    return 3;
                case "E":
                    return 4;
                case "F":
                    return 5;
                case "G":
                    return 6;
                case "H":
                    return 7;
                case "I":
                    return 8;
                case "J":
                    return 9;
                case "K":
                    return 10;
                case "L":
                    return 11;
                case "M":
                    return 12;
                case "N":
                    return 13;
                case "O":
                    return 14;
                default:
                    throw new ArgumentException(nameof(y));
            }
        }
    }
}