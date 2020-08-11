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
            gameHintPictureBox.Paint += GameHintPictureBoxOnPaint;
            KeyDown += OnKeyDown;
            game.Defeat += GameOnDefeat;
            game.TotalChanged += GameOnTotalChanged;

            timer.Interval = 700; // ms
            timer.Start();
            timer.Tick += TimerOnTick;
        }

        private void GameHintPictureBoxOnPaint(object sender, PaintEventArgs paintEventArgs)
        {
            game.DrawHint(paintEventArgs.Graphics);
        }

        private void GameOnTotalChanged()
        {
            score.Text = game.Total.ToString();
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.KeyCode)
            {
                case Keys.Left:
                    game.MoveLeft();
                    gameFieldPictureBox.Refresh();
                    break;
                case Keys.Right:
                    game.MoveRight();
                    gameFieldPictureBox.Refresh();
                    break;
                case Keys.Up:
                    game.MoveUp();
                    gameFieldPictureBox.Refresh();
                    break;
                case Keys.Down:
                    game.MoveDown();
                    gameFieldPictureBox.Refresh();
                    break;
                case Keys.Space:
                    game.MoveSpace();
                    gameFieldPictureBox.Refresh();
                    break;
            }
        }

        private void GameOnDefeat()
        {
            timer.Stop();
            MessageBox.Show($"Игра закончена! Ваш счет {game.Total}.");
            game.Restart();
            timer.Start();
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            game.MoveDown();
            gameFieldPictureBox.Refresh();
            gameHintPictureBox.Refresh();
        }

        private void gameFieldPictureBoxOnPaint(object sender, PaintEventArgs paintEventArgs)
        {
            game.Draw(paintEventArgs.Graphics);
        }
    }
}
