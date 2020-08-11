using Game.Tetris.FigureEntity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Game.Tetris
{
    public class TetrisGame
    {
        private const int CellSize = 23;
        private const int GameFieldWidth = 230;
        private const int GameFieldHeight = 460;
        private const int GameFieldWidthInCells = 10;
        private const int GameFieldHeightInCells = 20;

        private Figure activeFigure;
        private Figure nextFigure;
        private List<Point> nonFreePoints = new List<Point>();
        private bool isPause;

        public int Total { get; set; }
        public event Action TotalChanged = delegate { };
        public event Action Defeat = delegate { };

        public void Restart()
        {
            Total = 0;
            TotalChanged();
            nonFreePoints.Clear();
            activeFigure = Figure.GetRandomFigure();
            nextFigure = Figure.GetRandomFigure();
            isPause = false;
        }
        public void Draw(Graphics graphics)
        {
            for (int i = 0; i < GameFieldWidthInCells; i++)
            {
                graphics.DrawLine(Pens.Gray, i * CellSize, 0, i * CellSize, GameFieldHeight);
            }

            for (int i = 0; i < GameFieldHeightInCells; i++)
            {
                graphics.DrawLine(Pens.Gray, 0, i * CellSize, GameFieldWidth, i * CellSize);
            }

            foreach (Point point in nonFreePoints)
            {
                graphics.FillRectangle(Brushes.Blue, point.X * CellSize, point.Y * CellSize, CellSize, CellSize);
            }

            foreach (Point point in activeFigure.Points)
            {
                graphics.FillRectangle(Brushes.Blue, point.X * CellSize, point.Y * CellSize, CellSize, CellSize);
            }

        }
        public void DrawHint(Graphics graphics)
        {
            for (int i = 0; i < 4; i++)
            {
                graphics.DrawLine(Pens.Gray, i * CellSize, 0, i * CellSize, GameFieldHeight);
            }

            for (int i = 0; i < 4; i++)
            {
                graphics.DrawLine(Pens.Gray, 0, i * CellSize, GameFieldWidth, i * CellSize);
            }

            foreach (Point point in nextFigure.Points)
            {
                graphics.FillRectangle(Brushes.Blue, (point.X - 3) * CellSize, point.Y * CellSize, CellSize, CellSize);
            }
        }
        public void MoveDown()
        {
            if (isPause)
                return;

            if (!IsReachedDestinationPoint(activeFigure.Points))
            {
                for (int i = 0; i < activeFigure.Points.Count; i++)
                {
                    activeFigure.Points[i] = activeFigure.Points[i].Displace(0, 1);
                }
            }
            else
            {
                GetNextFigure();
            }
        }
        public void MoveRight()
        {
            if (!IsReachedRightBorder(activeFigure))
            {
                for (int i = 0; i < activeFigure.Points.Count; i++)
                {
                    activeFigure.Points[i] = activeFigure.Points[i].Displace(1, 0);
                }
            }
        }
        public void MoveLeft()
        {
            if (!IsReachedLeftBorder(activeFigure))
            {
                for (int i = 0; i < activeFigure.Points.Count; i++)
                {
                    activeFigure.Points[i] = activeFigure.Points[i].Displace(-1, 0);
                }
            }
        }
        public void MoveUp()
        {
            if (activeFigure.HasKeyPoint)
            {
                Point keyPoint = activeFigure.Points[0];
                List<Point> rotatedPoints = new List<Point>() { keyPoint };

                for (int i = 1; i < activeFigure.Points.Count(); i++)
                {
                    int x = keyPoint.X + (keyPoint.Y - activeFigure.Points[i].Y);
                    int y = keyPoint.Y + (activeFigure.Points[i].X - keyPoint.X);
                    rotatedPoints.Add(new Point(x, y));
                }

                if (CanRotate(rotatedPoints))
                {
                    activeFigure.Points.Clear();
                    activeFigure.Points = rotatedPoints;
                }
            }
        }
        public void MoveSpace()
        {
            while (!IsReachedDestinationPoint(activeFigure.Points))
            {
                for (int i = 0; i < activeFigure.Points.Count; i++)
                {
                    activeFigure.Points[i] = activeFigure.Points[i].Displace(0, 1);
                }
            }
            GetNextFigure();
        }

        private bool IsReachedRightBorder(Figure figure)
        {
            return figure.Points.Any(p => p.X == GameFieldWidthInCells - 1);
        }
        private bool IsReachedLeftBorder(Figure figure)
        {
            return figure.Points.Any(p => p.X == 0);
        }
        private bool IsReachedDestinationPoint(List<Point> figurePoints)
        {
            IEnumerable<Point> nextActiveFigurePoints = figurePoints.Select(p => p.Displace(0, 1));

            if (nextActiveFigurePoints.Any(p => nonFreePoints.Contains(p))
                || figurePoints.Any(p => p.Y == GameFieldHeightInCells - 1))
            {
                nonFreePoints.AddRange(figurePoints);
                FilledHorizontalLinesCount();
                return true;
            }
            return false;
        }
        private bool IsDefeat(Figure figure)
        {
            return nonFreePoints.Any(p => figure.Points.Contains(p));
        }
        private bool CanRotate(List<Point> figurePoints)
        {
            return !figurePoints.Any(p => nonFreePoints.Contains(p)) && figurePoints.All(p => p.X >= 0 && p.X < GameFieldWidthInCells);
        }
        private int FilledHorizontalLinesCount()
        {
            var yAxisValuesOfFilledLines = nonFreePoints.GroupBy(p => p.Y, (key, points) => new { Key = key, Count = points.Count() })
                .Where(x => x.Count == GameFieldWidthInCells).Select(x => x.Key);
            int rowsCount = yAxisValuesOfFilledLines.Count();

            if (rowsCount > 0)
            {
                var setToRemove = new HashSet<int>(yAxisValuesOfFilledLines);
                nonFreePoints.RemoveAll(p => setToRemove.Contains(p.Y));
                for (int i = 0; i < nonFreePoints.Count; i++)
                {
                    nonFreePoints[i] = nonFreePoints[i].Displace(0, rowsCount);
                }
                Total += CountTotal(rowsCount);
                TotalChanged();
            }

            return rowsCount;
        } 
        private int CountTotal(int rowsCount)
        {
            if (rowsCount == 0)
                return 0;
            if (rowsCount == 1)
                return 100;
            else
            {
                return (CountTotal(rowsCount - 1) + 50) * 2;
            }
        }
        private void GetNextFigure()
        {
            activeFigure = nextFigure;
            nextFigure = Figure.GetRandomFigure();
            if (IsDefeat(activeFigure))
            {
                isPause = true;
                Defeat();
                return;
            }
        }
    }
}
