using System.Drawing;
using System.Linq;

namespace Game.Tetris.Figures
{
    public static class FigureExtensions
    {
        public static Point[] RotateFigure(this Figure figure)
        {
            FigureType[] twoSideFigures = { FigureType.I, FigureType.S, FigureType.Z };

            if (twoSideFigures.Contains(figure.Type) && figure.IsRotated)
            {
                figure.IsRotated = false;
                return figure.Points.RotateBack();
            }

            figure.IsRotated = true;
            return figure.Points.RotateForward();
        }

    }
}
