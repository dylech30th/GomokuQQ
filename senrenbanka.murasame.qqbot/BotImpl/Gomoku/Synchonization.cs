using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using senrenbanka.murasame.qqbot.Resources.Primitive;

namespace senrenbanka.murasame.qqbot.BotImpl.Gomoku
{
    public enum PlayerJoinStat
    {
        Success, HaveJoined, GameFull
    }

    public enum GameState
    {
        AbortCuzChessBoardFull,
        Success,
        IllegalDropLocation,
        IrrelevantMessage
    }

    public struct DropResult
    {
        public DropResult(GameState gameState, Point dropPoint)
        {
            GameState = gameState;
            DropPoint = dropPoint;
        }

        public GameState GameState { get; set; }

        public Point DropPoint { get; set; }
    }

    public partial class PlayGround
    {
        public volatile bool GameFull;
        public bool GameStarted;

        public PlayerJoinStat PlayerJoinGame(string player)
        {
            if (!GameFull)
            {
                if (BlackPlayer == null)
                {
                    BlackPlayer = player;
                    return PlayerJoinStat.Success;
                }
                if (player != BlackPlayer)
                {
                    WhitePlayer = player;
                    GameFull   = true;
                    StartGame();
                    return PlayerJoinStat.Success;
                }
                return PlayerJoinStat.HaveJoined;
            }
            return PlayerJoinStat.GameFull;
        }

        public void StartGame()
        {
            GameStarted = true;
            SaveImage();
        }

        private void SaveImage()
        {
            var converter = new ImageConverter();
            File.WriteAllBytes($"E:\\CQPro\\data\\image\\{GameId}\\ChessBoard.jpg", converter.ConvertTo(ChessBoardImage, typeof(byte[])) as byte[] ?? throw new InvalidOperationException());
        }

        public DropResult ProcessGoCommand(string cmdInput)
        {
            var result = ParseCommand(cmdInput, out var point);

            if (result == GameState.Success)
            {
                var (success, draw) = PlayerGo(point.X, point.Y);
                if (success)
                {
                    SaveImage();
                    return draw ? new DropResult(GameState.AbortCuzChessBoardFull, point) : new DropResult(GameState.Success, point);
                }
                return new DropResult(GameState.IllegalDropLocation, point);
            }
            return new DropResult
            {
                GameState = result
            };
        }

        public bool IsBlackOrWhiteTurn(string qq)
        {
            return BlackPlayer == qq && Role == Role.Black || WhitePlayer == qq && Role == Role.White;
        }

        public bool IsMessageFromPlayer(string qq)
        {
            return BlackPlayer == qq || WhitePlayer == qq;
        }

        private static GameState ParseCommand(string command, out Point point)
        {
            var match = Regex.Match(command, "(?<XCoordinate>\\d{1,2})(?<YCoordinate>[a-oA-O])");
            var reverse = Regex.Match(command, "(?<YCoordinate>[a-oA-O])(?<XCoordinate>\\d{1,2})");

            var xVal = (match.Success ? match : reverse).Groups["XCoordinate"].Value;
            var yVal = (match.Success ? match : reverse).Groups["YCoordinate"].Value;

            if (xVal.IsNullOrEmpty() || yVal.IsNullOrEmpty())
            {
                point = new Point(0, 0);
                return GameState.IrrelevantMessage;
            }

            var x = int.Parse(xVal);
            var y = Axis.YAxisToNumber(yVal);

            if (x > 14)
            {
                point  = new Point(0, 0);
                return GameState.IllegalDropLocation;
            }

            point  = new Point(x, y);
            return GameState.Success;
        }

        public void Dispose()
        {
            Task.Run(() =>
            {
                GomokuFactory.RemoveGame(GameId);
                BlackPlayer = null;
                BlackPlayer = null;
                GameFull    = false;
                GameStarted = false;
                ChessBoardImage?.Dispose();
                File.Delete($"E:\\CQPro\\data\\image\\{GameId}\\ChessBoard.png");
                Directory.Delete($"E:\\CQPro\\data\\image\\{GameId}");
            });
        }
    }
}