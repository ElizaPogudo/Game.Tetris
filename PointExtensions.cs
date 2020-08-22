using System.Drawing;
using System.Linq;

namespace Game.Tetris
{
    public static class PointExtensions
    {
        public static Point Displace(this Point point, int dx, int dy)
        {
            point.Offset(dx, dy);
            return point;
        }
        public static Point[] RotateForward(this Point[] points)
        {
            Point keyPoint = points.First();
            Point[] rotatedPoints = new Point[points.Length];
            rotatedPoints[0] = keyPoint;
            for (int i = 1; i < points.Count(); i++)
            {
                int x = keyPoint.X + (keyPoint.Y - points[i].Y);
                int y = keyPoint.Y + (points[i].X - keyPoint.X);
                rotatedPoints[i] = new Point(x, y);
            }
            return rotatedPoints;
        }

        public static Point[] RotateBack(this Point[] points)
        {
            Point keyPoint = points.First();
            Point[] rotatedPoints = new Point[points.Length];
            rotatedPoints[0] = keyPoint;
            for (int i = 1; i < points.Count(); i++)
            {
                int x = keyPoint.X + (points[i].Y - keyPoint.Y);
                int y = keyPoint.Y + (keyPoint.X - points[i].X);
                rotatedPoints[i] = new Point(x, y);
            }
            return rotatedPoints;
        }

        public static Point[] MoveLeft(this Point[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = points[i].Displace(-1,0);
            }

            return points;
        }

        public static Point[] MoveRight(this Point[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = points[i].Displace(1, 0);
            }

            return points;
        }
    }
}
