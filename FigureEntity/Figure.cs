using System.Collections.Generic;
using System.Drawing;

namespace Game.Tetris.FigureEntity
{
    public class Figure
    {
        public int KeyPoint { get; set; }
        public List<Point> Points { get; set; }

        public Figure(List<Point> pointsList, int keyPoint = 0)
        {
            KeyPoint = keyPoint;
            Points = pointsList;
        }

        public Figure(Point point) : this(new List<Point> { point })
        { }
    }
}
