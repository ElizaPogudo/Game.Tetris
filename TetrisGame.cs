using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Game.Tetris.FigureEntity;
using Game.Tetris.Movements;

namespace Game.Tetris
{
    public class TetrisGame
    {
        private const int CellSize = 23;
        private const int GameFieldWidth = 230;
        private const int GameFieldHeight = 460;
        private const int GameFieldWidthInCells = 10;
        private const int GameFieldHeightInCells = 20;

        private readonly Random random = new Random();
        private Direction moveDirection;
        private Figure activeFigure;
        private List<Figure> fallenFigures = new List<Figure>();
        private bool isPause;
        public event Action Defeat = delegate { };

        IMoveBehavior[] movementBehaviors = new IMoveBehavior[]
            {
                new DownMoveBehavior(),
                new LeftMoveBehavior(),
                new RightMoveBehavior()
            };

        public void Restart()
        {
            moveDirection = Direction.None;
            fallenFigures.Clear();
            activeFigure = GetRandomFigure();
            isPause = false;
        }
        private Figure GetRandomFigure()
        {
            Point FigureTopCell = new Point(4, 0);

            FigureType figureType = (FigureType)random.Next(0, 7);
            switch (figureType)
            {
                case FigureType.O:
                    return new Figure(new List<Point>()
                {
                    FigureTopCell,
                    new Point(FigureTopCell.X + 1, FigureTopCell.Y),
                    new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                    new Point(FigureTopCell.X + 1, FigureTopCell.Y + 1)
                });

                case FigureType.I:
                    return new Figure(new List<Point>()
                {
                    FigureTopCell,
                    new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                    new Point(FigureTopCell.X, FigureTopCell.Y + 2),
                    new Point(FigureTopCell.X, FigureTopCell.Y + 3)
                }, 2);

                case FigureType.J:
                    return new Figure(new List<Point>()
                {
                   FigureTopCell,
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                   new Point(FigureTopCell.X, FigureTopCell.Y + 2),
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y + 2)
                }, 2);

                case FigureType.L:
                    return new Figure(new List<Point>()
                {
                   FigureTopCell,
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                   new Point(FigureTopCell.X, FigureTopCell.Y + 2),
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y + 2)
                }, 2);

                case FigureType.S:
                    return new Figure(new List<Point>()
                {
                   FigureTopCell,
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y + 1)
                }, 3);

                case FigureType.T:
                    return new Figure(new List<Point>()
                {
                   FigureTopCell,
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1)
                }, 1);

                case FigureType.Z:
                    return new Figure(new List<Point>()
                {
                   FigureTopCell,
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y + 1)
                }, 3);

                case FigureType.P: return new Figure(FigureTopCell);
                default: throw new ArgumentOutOfRangeException(nameof(figureType), figureType, null);
            }
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

            foreach (Figure figure in fallenFigures)
            {
                foreach (Point point in figure.Points)
                {
                    graphics.FillRectangle(Brushes.Blue, point.X * CellSize, point.Y * CellSize, CellSize, CellSize);
                }
            }
            foreach (Point point in activeFigure.Points)
            {
                graphics.FillRectangle(Brushes.Blue, point.X * CellSize, point.Y * CellSize, CellSize, CellSize);
            }

        }
        public void DisplaceFigure(Direction direction)
        {
            if (isPause || !CanChangeDirection(direction, activeFigure))
                return;

            moveDirection = direction;

            switch (moveDirection)
            {
                case Direction.Left:
                    movementBehaviors[1].Move(activeFigure);
                    moveDirection = Direction.None;
                    break;
                case Direction.Right:
                    movementBehaviors[2].Move(activeFigure);
                    moveDirection = Direction.None;
                    break;
                case Direction.Down:
                    if (!IsReachedDestinationPoint(activeFigure))
                        movementBehaviors[0].Move(activeFigure);
                    moveDirection = Direction.None;
                    break;
            }
        }
        public void Move()
        {
            if (isPause)
                return;

            if (!IsReachedDestinationPoint(activeFigure)) 
                movementBehaviors[0].Move(activeFigure);
            else 
            { 
                activeFigure = GetRandomFigure();
                moveDirection = Direction.None;
                if (IsDefeat(activeFigure))
                {
                    isPause = true;
                    Defeat();
                    return;
                }
            }
        }

        private bool IsReachedDestinationPoint(Figure figure)
        {
            IEnumerable<Point> nonFreePoints = fallenFigures.SelectMany(f => f.Points);
            IEnumerable<Point> nextActiveFigurePoints = figure.Points.Select(p => p.Displace(0, 1));

            if (nextActiveFigurePoints.Any(p => nonFreePoints.Contains(p))
                || figure.Points.Any(p => p.Y == GameFieldHeightInCells - 1))
            {
                fallenFigures.Add(figure);
                return true;
            }
            return false;
        }

        private bool IsDefeat(Figure figure)
        {
            return fallenFigures.SelectMany(f => f.Points).Any(p => figure.Points.Contains(p));
        }

        private bool CanChangeDirection(Direction direction, Figure figure)
        {
            switch (direction)
            {
                case Direction.Left:
                    return !figure.Points.Any(p => p.X == 0);
                case Direction.Right:
                    return !figure.Points.Any(p => p.X == GameFieldWidthInCells - 1);
                case Direction.Space:
                    return moveDirection != Direction.Space;
            }
            return true;
        }
    }
}
