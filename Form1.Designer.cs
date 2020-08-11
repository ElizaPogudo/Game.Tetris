namespace Game.Tetris
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameFieldPictureBox = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.scoreLabelName = new System.Windows.Forms.Label();
            this.score = new System.Windows.Forms.Label();
            this.gameHintPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gameFieldPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameHintPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // gameFieldPictureBox
            // 
            this.gameFieldPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameFieldPictureBox.Location = new System.Drawing.Point(12, 12);
            this.gameFieldPictureBox.Name = "gameFieldPictureBox";
            this.gameFieldPictureBox.Size = new System.Drawing.Size(230, 460);
            this.gameFieldPictureBox.TabIndex = 0;
            this.gameFieldPictureBox.TabStop = false;
            this.gameFieldPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.gameFieldPictureBoxOnPaint);
            // 
            // scoreLabelName
            // 
            this.scoreLabelName.AutoSize = true;
            this.scoreLabelName.Location = new System.Drawing.Point(261, 127);
            this.scoreLabelName.Name = "scoreLabelName";
            this.scoreLabelName.Size = new System.Drawing.Size(35, 13);
            this.scoreLabelName.TabIndex = 1;
            this.scoreLabelName.Text = "Очки:";
            // 
            // score
            // 
            this.score.AutoSize = true;
            this.score.Location = new System.Drawing.Point(302, 127);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(13, 13);
            this.score.TabIndex = 2;
            this.score.Text = "0";
            // 
            // gameHintPictureBox
            // 
            this.gameHintPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gameHintPictureBox.Location = new System.Drawing.Point(264, 12);
            this.gameHintPictureBox.Name = "gameHintPictureBox";
            this.gameHintPictureBox.Size = new System.Drawing.Size(92, 92);
            this.gameHintPictureBox.TabIndex = 3;
            this.gameHintPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(562, 482);
            this.Controls.Add(this.gameHintPictureBox);
            this.Controls.Add(this.score);
            this.Controls.Add(this.scoreLabelName);
            this.Controls.Add(this.gameFieldPictureBox);
            this.Name = "Form1";
            this.Text = "Тетрис";
            ((System.ComponentModel.ISupportInitialize)(this.gameFieldPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameHintPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox gameFieldPictureBox;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label scoreLabelName;
        private System.Windows.Forms.Label score;
        private System.Windows.Forms.PictureBox gameHintPictureBox;
    }
}

