using System;
using System.Collections.Generic;
using System.Drawing;

namespace Game.Tetris.FigureEntity
{
    public class Figure
    {
        public bool HasKeyPoint { get; set; }
        public List<Point> Points { get; set; }

        public Figure(List<Point> pointsList, bool hasKeyPoint = false)
        {
            HasKeyPoint = hasKeyPoint;
            Points = pointsList;
        }

        public Figure(Point point) : this(new List<Point> { point })
        { }

        private readonly static Random random = new Random();
        public static Figure GetRandomFigure()
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
                    new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                    FigureTopCell,
                    new Point(FigureTopCell.X, FigureTopCell.Y + 2),
                    new Point(FigureTopCell.X, FigureTopCell.Y + 3)
                }, true);

                case FigureType.J:
                    return new Figure(new List<Point>()
                {
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                   FigureTopCell,
                   new Point(FigureTopCell.X, FigureTopCell.Y + 2),
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y + 2)
                }, true);

                case FigureType.L:
                    return new Figure(new List<Point>()
                {
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                   FigureTopCell,
                   new Point(FigureTopCell.X, FigureTopCell.Y + 2),
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y + 2)
                }, true);

                case FigureType.S:
                    return new Figure(new List<Point>()
                {
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                   FigureTopCell,
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y + 1)
                }, true);

                case FigureType.T:
                    return new Figure(new List<Point>()
                {
                   FigureTopCell,
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1)
                }, true);

                case FigureType.Z:
                    return new Figure(new List<Point>()
                {
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                   FigureTopCell,
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y + 1)
                }, true);

                case FigureType.P: return new Figure(FigureTopCell);
                default: throw new ArgumentOutOfRangeException(nameof(figureType), figureType, null);
            }
        }
    }
}
