using System;

namespace senrenbanka.murasame.qqbot.BotImpl.Gomoku
{
    public class Judgment
    {
        private class Step
        {
            public Step(int leftX, int leftY, int rightX, int rightY)
            {
                LeftX = leftX;
                LeftY = leftY;
                RightX = rightX;
                RightY = rightY;
            }

            public int LeftX { get; }
            public int LeftY { get; }
            public int RightX { get; }
            public int RightY { get; }

        }

        private readonly ChessmanPoint[,] _chessBoard;
        public Judgment(ChessmanPoint[,] chessBoard)
        {
            _chessBoard = chessBoard;
        }

        public Winner Check(ChessmanPoint coordinate)
        {
            var verticalWinner = CheckVertical(coordinate);
            var horizontalWinner = CheckHorizontal(coordinate);
            var leadingDiagonalWinner = CheckLeadingDiagonal(coordinate);
            var diagonalWinner = CheckDiagonal(coordinate);
            return verticalWinner != Winner.None ? verticalWinner :
                horizontalWinner != Winner.None ? horizontalWinner :
                leadingDiagonalWinner != Winner.None ? leadingDiagonalWinner :
                diagonalWinner != Winner.None ? diagonalWinner : Winner.None;
        }

        private static Step ValidateStepLong(string method)
        {
            (int item1, int item2) = Validator.Validate<LeftStep, Judgment, Tuple<int, int>>(method, p => new Tuple<int, int>(p.StepX, p.StepY));
            (int item3, int item4) = Validator.Validate<RightStep, Judgment, Tuple<int, int>>(method, p => new Tuple<int, int>(p.StepX, p.StepY));
            return new Step(item1, item2, item3, item4);
        }

        private Winner Check(ChessmanPoint coordinate, Step stepLong, Chessman chessman)
        {
            var count = 0;
            var up = coordinate;
            // check the right side from self's coordinate
            while (up.X <= 14 && up.Y <= 14 && up.Chessman == chessman)
            {
                if (count == 5)
                    return chessman == Chessman.White ? Winner.White : Winner.Black;
                count++;
                try
                {
                    up = _chessBoard.GetPoint(up.X + stepLong.RightX, up.Y + stepLong.RightY);
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            up = coordinate;
            // check the left side from self's coordinate
            while (up.X >= 0 && up.Y >= 0 && up.Chessman == chessman)
            {
                if (count == 5)
                    return chessman == Chessman.White ? Winner.White : Winner.Black;
                count++;
                try
                {
                    up = _chessBoard.GetPoint(up.X + stepLong.LeftX, up.Y + stepLong.LeftY);
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }

            return Winner.None;
        }

        private Winner CheckBoth(ChessmanPoint coordinate, string validateMethod)
        {
            var stepLong = ValidateStepLong(validateMethod);
            return Check(coordinate, stepLong, Chessman.White) == Winner.White ? Winner.White :
                Check(coordinate, stepLong, Chessman.Black) == Winner.Black ? Winner.Black : Winner.None;
        }

        [LeftStep(0, -1), RightStep(0, 1)]
        private Winner CheckVertical(ChessmanPoint coordinate)
        {
            return CheckBoth(coordinate, nameof(CheckVertical));
        }

        [LeftStep(-1, 0), RightStep(1, 0)]
        private Winner CheckHorizontal(ChessmanPoint coordinate)
        {
            return CheckBoth(coordinate, nameof(CheckHorizontal));
        }

        [LeftStep(1, -1), RightStep(-1, 1)]
        private Winner CheckLeadingDiagonal(ChessmanPoint coordinate)
        {
            return CheckBoth(coordinate, nameof(CheckLeadingDiagonal));
        }

        [LeftStep(-1, -1), RightStep(1, 1)]
        private Winner CheckDiagonal(ChessmanPoint coordinate)
        {
            return CheckBoth(coordinate, nameof(CheckDiagonal));
        }
    }
}