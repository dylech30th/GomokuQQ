using System;
using System.Drawing;
using System.IO;
using senrenbanka.murasame.qqbot.BotImpl.Gomoku.Drawing;

namespace senrenbanka.murasame.qqbot.BotImpl.Gomoku
{
    public partial class PlayGround : IDisposable
    {
        public readonly string GameId;

        public Image ChessBoardImage;
        private readonly ChessmanPoint[,] _chessBoard = new ChessmanPoint[15,15];

        public string BlackPlayer;
        public string WhitePlayer;

        public int Steps = 0;

        public Role Role { get; set; }

        public PlayGround(string gameId)
        {
            GameId = gameId;
            RandomizeFirst();

            Directory.CreateDirectory($"data\\image\\{GameId}");

            for (var i = 0; i < 15; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    _chessBoard[i, j] = new ChessmanPoint(i, j, Chessman.Empty);
                }
            }
            DrawChessBoard(default(Point));
        }

        private void RandomizeFirst()
        {
            var rnd = new Random().Next(2);
            Role = rnd == 0 ? Role.White : Role.Black;
        }

        public Winner Check(ChessmanPoint coordinate)
        {
            var checker = new Judgment(_chessBoard);
            return checker.Check(coordinate);
        }

        public void DrawChessBoard(Point lastModify)
        {
            ChessBoardImage = Painter.DrawChessBoard();
            foreach (var chessmanPoint in _chessBoard)
            {
                if (chessmanPoint.Chessman != Chessman.Empty)
                {
                    Painter.DrawChessman(ChessBoardImage, chessmanPoint, lastModify != default(Point) && chessmanPoint.X == lastModify.X && chessmanPoint.Y == lastModify.Y);
                }
            }
        }

        public (bool goSuccess, bool draw) PlayerGo(int x, int y)
        {
            if (!CheckPointIsRational(x, y))
            {
                return (false, false);
            }

            switch (Role)
            {
                case Role.White:
                    WhiteGo(x, y);
                    return (true, CheckChessBoardIsFull());
                case Role.Black:
                    BlackGo(x, y);
                    return (true, CheckChessBoardIsFull());
                default:
                    return (false, false);
            }
        }

        public void Regret(Point point)
        {
            _chessBoard[point.X, point.Y] = new ChessmanPoint(point.X, point.Y, Chessman.Empty);
            Role = Role == Role.Black ? Role.White : Role.Black;
        }

        public Chessman IsWin(int x, int y)
        {
            switch (Check(_chessBoard.GetPoint(x, y)))
            {
                case Winner.Black:
                    Timer.Stop();
                    return Chessman.Black;
                case Winner.White:
                    Timer.Stop();
                    return Chessman.White;
                case Winner.None:
                    return Chessman.Empty;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool CheckChessBoardIsFull()
        {
            for (var i = 0; i < 15; i++)
            {
                for (var j = 0; j < 15; j++)
                {
                    if (_chessBoard[i, j].Chessman == Chessman.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool CheckPointIsRational(int x, int y)
        {
            return x <= 14 && y <= 14 && _chessBoard[x, y].Chessman == Chessman.Empty;
        }

        private void WhiteGo(int x, int y)
        {
            Role = Role.Black;
            
            _chessBoard[x, y] = new ChessmanPoint(x, y, Chessman.White);
            DrawChessBoard(new Point(x, y));

            Steps++;
        }

        private void BlackGo(int x, int y)
        {
            Role = Role.White;
            _chessBoard[x, y] = new ChessmanPoint(x, y, Chessman.Black);
            DrawChessBoard(new Point(x, y));

            Steps++;
        }
    }

    public enum Role
    {
        White,
        Black
    }
}