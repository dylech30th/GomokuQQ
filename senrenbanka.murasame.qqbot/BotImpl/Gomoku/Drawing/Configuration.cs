using System.Drawing;

namespace senrenbanka.murasame.qqbot.BotImpl.Gomoku.Drawing
{
    public class Configuration
    {
        public const int ChessBoardImageWidth = 660;

        public const int ChessBoardImageHeight = 660;

        /// <summary>
        /// 14(格数) * 40(每格的像素宽)
        /// </summary>
        public const int ChessBoardSize = 560;

        public static int[] HorizontalLines = 
        {
            50, 90, 130, 170, 210, 250, 290, 330, 370, 410, 450, 490, 530, 570, 610
        };

        public static int[] VerticalLines = HorizontalLines;

        public static Point[] Stars =
        {
            new Point(170, 170),
            new Point(170, 490),
            new Point(330, 330),
            new Point(490, 170),
            new Point(490, 490)
        };
    }
}