using Game.Tetris.Figures;
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
        private List<Point> occupiedPoints = new List<Point>();
        private bool isPause;

        public int GamePoints { get; set; }
        public event Action GamePointsChanged = delegate { };
        public event Action Defeat = delegate { };

        public void Restart()
        {
            GamePoints = 0;
            GamePointsChanged();
            occupiedPoints.Clear();
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

            foreach (Point point in occupiedPoints)
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
                for (int i = 0; i < activeFigure.Points.Length; i++)
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
            Point[] currentPoints = activeFigure.Points.ToArray();
            if (!IsReachedRightBorder(activeFigure) && CanMoveRight(currentPoints))
            {
                activeFigure.Points.MoveRight();
            }
        }

        public void MoveLeft()
        {
            Point[] currentPoints = activeFigure.Points.ToArray();
            if (!IsReachedLeftBorder(activeFigure) && CanMoveLeft(currentPoints))
            {
                activeFigure.Points.MoveLeft();
            }
        }

        public void MoveUp()
        {
            if (activeFigure.HasKeyPoint)
            {
                Point[] rotatedPoints = activeFigure.RotateFigure();

                if (CanRotate(rotatedPoints) && !IsReachedDestinationPoint(activeFigure.Points))
                {
                    Array.Clear(activeFigure.Points, 0, activeFigure.Points.Length);
                    activeFigure.Points = rotatedPoints;
                }
            }
        }

        public void MoveSpace()
        {
            while (!IsReachedDestinationPoint(activeFigure.Points))
            {
                for (int i = 0; i < activeFigure.Points.Length; i++)
                {
                    activeFigure.Points[i] = activeFigure.Points[i].Displace(0, 1);
                }
            }
            GetNextFigure();
        }

        #region Проверки
        private bool IsReachedRightBorder(Figure figure)
        {
            return figure.Points.Any(p => p.X == GameFieldWidthInCells - 1);
        }
        private bool IsReachedLeftBorder(Figure figure)
        {
            return figure.Points.Any(p => p.X == 0);
        }
        private bool IsReachedDestinationPoint(Point[] figurePoints)
        {
            IEnumerable<Point> nextActiveFigurePoints = figurePoints.Select(p => p.Displace(0, 1));

            if (nextActiveFigurePoints.Any(p => occupiedPoints.Contains(p))
                || figurePoints.Any(p => p.Y == GameFieldHeightInCells - 1))
            {
                occupiedPoints.AddRange(figurePoints);
                DropBlocks();
                return true;
            }
            return false;
        }
        private bool IsDefeat(Figure figure)
        {
            return occupiedPoints.Any(p => figure.Points.Contains(p));
        }
        private bool CanRotate(Point[] figurePoints)
        {
            return !figurePoints.Any(p => occupiedPoints.Contains(p)) && figurePoints.All(p => p.X >= 0 && p.X < GameFieldWidthInCells);
        }
        private bool CanMoveRight(Point[] figurePoints)
        {
            return !figurePoints.MoveRight().Any(p => occupiedPoints.Contains(p));
        }
        private bool CanMoveLeft(Point[] figurePoints)
        {
            return !figurePoints.MoveLeft().Any(p => occupiedPoints.Contains(p));
        }

        #endregion

        #region Вспомогательные методы
        private void DropBlocks()
        {
            int[] yAxisValuesOfFilledLines = occupiedPoints.GroupBy(p => p.Y, (key, points) => new { Key = key, Count = points.Count() })
                .Where(x => x.Count == GameFieldWidthInCells).Select(x => x.Key).OrderByDescending(x => x).ToArray();

            int rowsCount = 0;

            bool IsIncreaseByOne = true;

            if (!yAxisValuesOfFilledLines.Length.Equals( 0) && yAxisValuesOfFilledLines[0] == GameFieldHeightInCells - 1)
            {
                if (yAxisValuesOfFilledLines.Length == 1)
                {
                    rowsCount = 1;
                }
                else
                {
                    int i = 0;
                    rowsCount = 1;
                    while (IsIncreaseByOne && i < yAxisValuesOfFilledLines.Length - 1)
                    {
                        if (yAxisValuesOfFilledLines[i] == yAxisValuesOfFilledLines[i+1] + 1)
                        {
                            rowsCount++;
                            i++;
                        }
                        else
                        {
                            IsIncreaseByOne = false;
                        }
                    }
                }
            }
            

            if (rowsCount > 0)
            {
                var setToRemove = new HashSet<int>(yAxisValuesOfFilledLines);
                occupiedPoints.RemoveAll(p => setToRemove.Contains(p.Y));
                for (int i = 0; i < occupiedPoints.Count; i++)
                {
                    occupiedPoints[i] = occupiedPoints[i].Displace(0, rowsCount);
                }

                GamePoints += GetGamePoints(rowsCount);
                GamePointsChanged();
            }
        } 
        private int GetGamePoints(int rowsCount)
        {
            return (int) (100 * Math.Pow(2, rowsCount)) - 100;
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

        #endregion

    }
}
