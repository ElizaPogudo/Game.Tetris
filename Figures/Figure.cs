using System;
using System.Drawing;

namespace Game.Tetris.Figures
{
    public class Figure
    {
        private static readonly Random random = new Random();
        public bool HasKeyPoint { get; set; }
        public Point[] Points { get; set; }
        public FigureType Type { get; set; }
        public bool IsRotated { get; set; } = false;

        public Figure(Point[] points, FigureType type, bool hasKeyPoint = false)
        {
            HasKeyPoint = hasKeyPoint;
            Points = points;
            Type = type;
        }
        public Figure(Point point, FigureType type) : this(new Point[] { point }, type)
        { }

        public static Figure GetRandomFigure()
        {
            Point basePoint = new Point(4, 0);

            FigureType figureType = (FigureType)random.Next(0, 8);
            switch (figureType)
            {
                case FigureType.O:
                    return new Figure(new Point[]
                {
                    basePoint,
                    new Point(basePoint.X + 1, basePoint.Y),
                    new Point(basePoint.X, basePoint.Y + 1),
                    new Point(basePoint.X + 1, basePoint.Y + 1)
                }, figureType);

                case FigureType.I:
                    return new Figure(new Point[]
                {
                    new Point(basePoint.X, basePoint.Y + 1),
                    basePoint,
                    new Point(basePoint.X, basePoint.Y + 2),
                    new Point(basePoint.X, basePoint.Y + 3)
                }, figureType, true);

                case FigureType.J:
                    return new Figure(new Point[]
                {
                   new Point(basePoint.X, basePoint.Y + 1),
                   basePoint,
                   new Point(basePoint.X, basePoint.Y + 2),
                   new Point(basePoint.X - 1, basePoint.Y + 2)
                }, figureType, true);

                case FigureType.L:
                    return new Figure(new Point[]
                {
                   new Point(basePoint.X, basePoint.Y + 1),
                   basePoint,
                   new Point(basePoint.X, basePoint.Y + 2),
                   new Point(basePoint.X + 1, basePoint.Y + 2)
                }, figureType, true);

                case FigureType.S:
                    return new Figure(new Point[]
                {
                   new Point(basePoint.X, basePoint.Y + 1),
                   basePoint,
                   new Point(basePoint.X + 1, basePoint.Y),
                   new Point(basePoint.X - 1, basePoint.Y + 1)
                }, figureType, true);

                case FigureType.T:
                    return new Figure(new Point[]
                {
                   basePoint,
                   new Point(basePoint.X - 1, basePoint.Y),
                   new Point(basePoint.X + 1, basePoint.Y),
                   new Point(basePoint.X, basePoint.Y + 1)
                }, figureType, true);

                case FigureType.Z:
                    return new Figure(new Point[]
                {
                   new Point(basePoint.X, basePoint.Y + 1),
                   basePoint,
                   new Point(basePoint.X - 1, basePoint.Y),
                   new Point(basePoint.X + 1, basePoint.Y + 1)
                }, figureType, true);

                case FigureType.P: return new Figure(basePoint, figureType);
                default: throw new ArgumentOutOfRangeException(nameof(figureType), figureType, null);
            }
        }
        
    }
}
