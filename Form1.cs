using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            game.Defeat += Game_Defeat;

            timer.Interval = 300; // ms
            timer.Start();
            timer.Tick += TimerOnTick;
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
