using System.Drawing;

namespace Game.Tetris
{
    public static class PointExtensions
    {
        public static Point Displace(this Point point, int dx, int dy)
        {
            point.Offset(dx, dy);
            return point;
        }
    }
}
