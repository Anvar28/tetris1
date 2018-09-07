namespace tetris1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lScore = new System.Windows.Forms.Label();
            this.lNewGame = new System.Windows.Forms.Label();
            this.lPausa = new System.Windows.Forms.Label();
            this.lGameOver = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(310, 605);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(339, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Очки:";
            // 
            // lScore
            // 
            this.lScore.AutoSize = true;
            this.lScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lScore.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lScore.Location = new System.Drawing.Point(401, 12);
            this.lScore.Name = "lScore";
            this.lScore.Size = new System.Drawing.Size(19, 20);
            this.lScore.TabIndex = 2;
            this.lScore.Text = "0";
            // 
            // lNewGame
            // 
            this.lNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lNewGame.ForeColor = System.Drawing.Color.Maroon;
            this.lNewGame.Location = new System.Drawing.Point(343, 41);
            this.lNewGame.Name = "lNewGame";
            this.lNewGame.Size = new System.Drawing.Size(126, 25);
            this.lNewGame.TabIndex = 2;
            this.lNewGame.Text = "Новая игра";
            this.lNewGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lNewGame.UseCompatibleTextRendering = true;
            this.lNewGame.Click += new System.EventHandler(this.lNewGame_Click);
            this.lNewGame.MouseLeave += new System.EventHandler(this.lNewGame_MouseLeave);
            this.lNewGame.MouseHover += new System.EventHandler(this.lNewGame_MouseHover);
            // 
            // lPausa
            // 
            this.lPausa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lPausa.ForeColor = System.Drawing.Color.Maroon;
            this.lPausa.Location = new System.Drawing.Point(343, 77);
            this.lPausa.Name = "lPausa";
            this.lPausa.Size = new System.Drawing.Size(126, 25);
            this.lPausa.TabIndex = 2;
            this.lPausa.Text = "Пауза";
            this.lPausa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lPausa.UseCompatibleTextRendering = true;
            this.lPausa.Click += new System.EventHandler(this.lPausa_Click);
            this.lPausa.MouseLeave += new System.EventHandler(this.lNewGame_MouseLeave);
            this.lPausa.MouseHover += new System.EventHandler(this.lNewGame_MouseHover);
            // 
            // lGameOver
            // 
            this.lGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lGameOver.ForeColor = System.Drawing.Color.Red;
            this.lGameOver.Location = new System.Drawing.Point(343, 156);
            this.lGameOver.Name = "lGameOver";
            this.lGameOver.Size = new System.Drawing.Size(126, 60);
            this.lGameOver.TabIndex = 2;
            this.lGameOver.Text = "Игра закончена";
            this.lGameOver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lGameOver.UseCompatibleTextRendering = true;
            this.lGameOver.Visible = false;
            this.lGameOver.Click += new System.EventHandler(this.lPausa_Click);
            this.lGameOver.MouseLeave += new System.EventHandler(this.lNewGame_MouseLeave);
            this.lGameOver.MouseHover += new System.EventHandler(this.lNewGame_MouseHover);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 631);
            this.Controls.Add(this.lScore);
            this.Controls.Add(this.lGameOver);
            this.Controls.Add(this.lPausa);
            this.Controls.Add(this.lNewGame);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Тетрис";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lScore;
        private System.Windows.Forms.Label lNewGame;
        private System.Windows.Forms.Label lPausa;
        private System.Windows.Forms.Label lGameOver;
    }
}