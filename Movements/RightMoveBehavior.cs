using Game.Tetris.FigureEntity;

namespace Game.Tetris.Movements
{
    public class RightMoveBehavior : IMoveBehavior
    {
        public void Move(Figure figure)
        {
            for (int i = 0; i < figure.Points.Count; i++)
            {
                figure.Points[i] = figure.Points[i].Displace(1, 0);
            }
        }
    }
}
