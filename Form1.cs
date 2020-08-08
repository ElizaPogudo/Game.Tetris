using System;
using System.Windows.Forms;

namespace Game.Tetris
{
    public partial class Form1 : Form
    {
        private TetrisGame game = new TetrisGame();
        public Form1()
        {
            InitializeComponent();
            game.Restart();

            gameFieldPictureBox.Paint += gameFieldPictureBoxOnPaint;
            KeyDown += OnKeyDown;
            game.Defeat += Game_Defeat;

            timer.Interval = 700; // ms
            timer.Start();
            timer.Tick += TimerOnTick;
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.KeyCode)
            {
                case Keys.Left:
                    game.DisplaceFigure(Direction.Left);
                    gameFieldPictureBox.Refresh();
                    break;
                case Keys.Right:
                    game.DisplaceFigure(Direction.Right);
                    gameFieldPictureBox.Refresh();
                    break;
                case Keys.Up:
                    break;
                case Keys.Down:
                    game.DisplaceFigure(Direction.Down);
                    gameFieldPictureBox.Refresh();
                    break;
                case Keys.Space:
                    break;
            }
        }

        private void Game_Defeat()
        {
            timer.Stop();
            MessageBox.Show($"Игра закончена!");
            game.Restart();
            timer.Start();
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            game.Move();
            gameFieldPictureBox.Refresh();
        }

        private void gameFieldPictureBoxOnPaint(object sender, PaintEventArgs paintEventArgs)
        {
            game.Draw(paintEventArgs.Graphics);
        }
    }
}
