using Game.Tetris.FigureEntity;
using System.Drawing;

namespace Game.Tetris.Movements
{
    interface IMoveBehavior
    {
        void Move(Figure figure); 
    }
}
