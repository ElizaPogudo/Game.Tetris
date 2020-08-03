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
            ((System.ComponentModel.ISupportInitialize)(this.gameFieldPictureBox)).BeginInit();
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(562, 482);
            this.Controls.Add(this.gameFieldPictureBox);
            this.Name = "Form1";
            this.Text = "Тетрис";
            ((System.ComponentModel.ISupportInitialize)(this.gameFieldPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox gameFieldPictureBox;
        private System.Windows.Forms.Timer timer;
    }
}

