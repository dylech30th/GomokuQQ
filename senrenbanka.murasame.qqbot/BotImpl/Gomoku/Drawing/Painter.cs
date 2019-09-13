using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;

namespace senrenbanka.murasame.qqbot.BotImpl.Gomoku.Drawing
{
    public class Painter
    {
        public static async Task<Image> DrawChessBoard()
        {
            var img = new Bitmap(Configuration.ChessBoardImageWidth, Configuration.ChessBoardImageHeight);

            await Task.Run(() =>
            {
                using (var graphics = Graphics.FromImage(img))
                {
                    FillWhite(graphics);
                    DrawBorder(graphics);
                    DrawCrossLine(graphics);
                    DrawIdentifier(graphics);
                    DrawStars(graphics);
                }
            });

            return img;
        }

        public static void DrawChessman(Image chessBoard, ChessmanPoint chessman, bool highlight)
        {
            using (var graphics = Graphics.FromImage(chessBoard))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                var point = chessman.ParseChessmanPoint();
                var rectangleF = new RectangleF(point.X - 20, point.Y - 20, 40, 40);

                switch (chessman.Chessman)
                {
                    case Chessman.Black:
                        graphics.FillEllipse(Brushes.Black, rectangleF);
                        break;
                    case Chessman.White:
                        graphics.FillEllipse(Brushes.White, rectangleF);
                        using (var pen = new Pen(Color.Black, 0.5f))
                        {
                            graphics.DrawEllipse(pen, rectangleF);
                        }
                        break;
                    case Chessman.Empty:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                if (highlight)
                {
                    using (var pen = new Pen(Color.Crimson, 1f))
                    {
                        graphics.DrawLine(pen, new Point(point.X - 20, point.Y - 20), new Point(point.X - 20, point.Y - 15));
                        graphics.DrawLine(pen, new Point(point.X - 20, point.Y - 20), new Point(point.X - 15, point.Y - 20));
                        graphics.DrawLine(pen, new Point(point.X - 20, point.Y + 20), new Point(point.X - 20, point.Y + 15));
                        graphics.DrawLine(pen, new Point(point.X - 20, point.Y + 20), new Point(point.X - 15, point.Y + 20));
                        graphics.DrawLine(pen, new Point(point.X + 20, point.Y - 20), new Point(point.X + 15, point.Y - 20));
                        graphics.DrawLine(pen, new Point(point.X + 20, point.Y - 20), new Point(point.X + 20, point.Y - 15));
                        graphics.DrawLine(pen, new Point(point.X + 20, point.Y + 20), new Point(point.X + 20, point.Y + 15));
                        graphics.DrawLine(pen, new Point(point.X + 20, point.Y + 20), new Point(point.X + 15, point.Y + 20));
                    }
                }
            }
        }

        private static void FillWhite(Graphics graphics)
        {
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(232, 180, 130)), new RectangleF(0, 0, Configuration.ChessBoardImageWidth, Configuration.ChessBoardImageHeight));
        }

        private static void DrawBorder(Graphics graphics)
        {
            using (var pen = new Pen(Color.Black, 1f))
            {
                graphics.DrawRectangle(pen, 0.5f, 0.5f, 649, 649);
            }
        }

        private static void DrawCrossLine(Graphics graphics)
        {
            using (var pen = new Pen(Color.Black, 1f))
            {
                foreach (var horizontalLine in Configuration.HorizontalLines)
                {
                    graphics.DrawLine(pen, new Point(horizontalLine, 50), new Point(horizontalLine, Configuration.ChessBoardSize + 50));
                }
                foreach (var verticalLine in Configuration.VerticalLines)
                {
                    graphics.DrawLine(pen, new Point(50, verticalLine), new Point(Configuration.ChessBoardSize + 50, verticalLine));
                }
            }
        }

        private static void DrawIdentifier(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            
            for (var i = 0; i < Configuration.HorizontalLines.Length; i++)
            {
                using (var graphicPath = new GraphicsPath())
                {
                    graphicPath.AddString(i.ToString(), new FontFamily("consolas"), (int) FontStyle.Regular, 20, new Point(Configuration.HorizontalLines[i], 20), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment     = StringAlignment.Center
                    });
                    graphics.FillPath(new SolidBrush(Color.Black), graphicPath);
                }
            }
            for (var i = 0; i < Configuration.HorizontalLines.Length; i++)
            {
                using (var graphicPath = new GraphicsPath())
                {
                    graphicPath.AddString(Axis.NumberToYAxis(i), new FontFamily("consolas"), (int) FontStyle.Regular, 20, new Point(20, Configuration.HorizontalLines[i]), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment     = StringAlignment.Center
                    });
                    graphics.FillPath(new SolidBrush(Color.Black), graphicPath);
                }
            }
        }

        private static void DrawStars(Graphics graphics)
        {
            foreach (var point in Configuration.Stars)
            {
                graphics.FillEllipse(Brushes.Black, new RectangleF(point.X - 3, point.Y - 3, 6, 6));
            }
        }
    }
}