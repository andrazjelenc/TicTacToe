namespace TicTacToe
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.LowRadioButton = new System.Windows.Forms.RadioButton();
            this.MediumRadioButton = new System.Windows.Forms.RadioButton();
            this.HardRadioButton = new System.Windows.Forms.RadioButton();
            this.EvilRadioButton = new System.Windows.Forms.RadioButton();
            this.DiffLabel = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.CPUscoreLabel = new System.Windows.Forms.Label();
            this.UserScoreLabel = new System.Windows.Forms.Label();
            this.PlayButton = new System.Windows.Forms.Button();
            this.TieScoreLabel = new System.Windows.Forms.Label();
            this.TotalGamesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LowRadioButton
            // 
            this.LowRadioButton.AutoSize = true;
            this.LowRadioButton.Location = new System.Drawing.Point(9, 42);
            this.LowRadioButton.Name = "LowRadioButton";
            this.LowRadioButton.Size = new System.Drawing.Size(45, 17);
            this.LowRadioButton.TabIndex = 0;
            this.LowRadioButton.TabStop = true;
            this.LowRadioButton.Text = "Low";
            this.LowRadioButton.UseVisualStyleBackColor = true;
            // 
            // MediumRadioButton
            // 
            this.MediumRadioButton.AutoSize = true;
            this.MediumRadioButton.Location = new System.Drawing.Point(9, 65);
            this.MediumRadioButton.Name = "MediumRadioButton";
            this.MediumRadioButton.Size = new System.Drawing.Size(62, 17);
            this.MediumRadioButton.TabIndex = 1;
            this.MediumRadioButton.TabStop = true;
            this.MediumRadioButton.Text = "Medium";
            this.MediumRadioButton.UseVisualStyleBackColor = true;
            // 
            // HardRadioButton
            // 
            this.HardRadioButton.AutoSize = true;
            this.HardRadioButton.Location = new System.Drawing.Point(9, 88);
            this.HardRadioButton.Name = "HardRadioButton";
            this.HardRadioButton.Size = new System.Drawing.Size(48, 17);
            this.HardRadioButton.TabIndex = 2;
            this.HardRadioButton.TabStop = true;
            this.HardRadioButton.Text = "Hard";
            this.HardRadioButton.UseVisualStyleBackColor = true;
            // 
            // EvilRadioButton
            // 
            this.EvilRadioButton.AutoSize = true;
            this.EvilRadioButton.Location = new System.Drawing.Point(9, 111);
            this.EvilRadioButton.Name = "EvilRadioButton";
            this.EvilRadioButton.Size = new System.Drawing.Size(42, 17);
            this.EvilRadioButton.TabIndex = 3;
            this.EvilRadioButton.TabStop = true;
            this.EvilRadioButton.Text = "Evil";
            this.EvilRadioButton.UseVisualStyleBackColor = true;
            // 
            // DiffLabel
            // 
            this.DiffLabel.AutoSize = true;
            this.DiffLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DiffLabel.Location = new System.Drawing.Point(6, 22);
            this.DiffLabel.Name = "DiffLabel";
            this.DiffLabel.Size = new System.Drawing.Size(98, 17);
            this.DiffLabel.TabIndex = 4;
            this.DiffLabel.Text = "Difficulty level:";
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ScoreLabel.Location = new System.Drawing.Point(6, 146);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(56, 17);
            this.ScoreLabel.TabIndex = 5;
            this.ScoreLabel.Text = "Scores:";
            // 
            // CPUscoreLabel
            // 
            this.CPUscoreLabel.AutoSize = true;
            this.CPUscoreLabel.Location = new System.Drawing.Point(12, 169);
            this.CPUscoreLabel.Name = "CPUscoreLabel";
            this.CPUscoreLabel.Size = new System.Drawing.Size(41, 13);
            this.CPUscoreLabel.TabIndex = 6;
            this.CPUscoreLabel.Text = "CPU: 0";
            // 
            // UserScoreLabel
            // 
            this.UserScoreLabel.AutoSize = true;
            this.UserScoreLabel.Location = new System.Drawing.Point(12, 192);
            this.UserScoreLabel.Name = "UserScoreLabel";
            this.UserScoreLabel.Size = new System.Drawing.Size(41, 13);
            this.UserScoreLabel.TabIndex = 7;
            this.UserScoreLabel.Text = "User: 0";
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(9, 261);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(75, 23);
            this.PlayButton.TabIndex = 8;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // TieScoreLabel
            // 
            this.TieScoreLabel.AutoSize = true;
            this.TieScoreLabel.Location = new System.Drawing.Point(12, 214);
            this.TieScoreLabel.Name = "TieScoreLabel";
            this.TieScoreLabel.Size = new System.Drawing.Size(34, 13);
            this.TieScoreLabel.TabIndex = 9;
            this.TieScoreLabel.Text = "Tie: 0";
            // 
            // TotalGamesLabel
            // 
            this.TotalGamesLabel.AutoSize = true;
            this.TotalGamesLabel.Location = new System.Drawing.Point(12, 236);
            this.TotalGamesLabel.Name = "TotalGamesLabel";
            this.TotalGamesLabel.Size = new System.Drawing.Size(77, 13);
            this.TotalGamesLabel.TabIndex = 10;
            this.TotalGamesLabel.Text = "Total games: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(378, 296);
            this.Controls.Add(this.TotalGamesLabel);
            this.Controls.Add(this.TieScoreLabel);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.UserScoreLabel);
            this.Controls.Add(this.CPUscoreLabel);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.DiffLabel);
            this.Controls.Add(this.EvilRadioButton);
            this.Controls.Add(this.HardRadioButton);
            this.Controls.Add(this.MediumRadioButton);
            this.Controls.Add(this.LowRadioButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton LowRadioButton;
        private System.Windows.Forms.RadioButton MediumRadioButton;
        private System.Windows.Forms.RadioButton HardRadioButton;
        private System.Windows.Forms.RadioButton EvilRadioButton;
        private System.Windows.Forms.Label DiffLabel;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Label CPUscoreLabel;
        private System.Windows.Forms.Label UserScoreLabel;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Label TieScoreLabel;
        private System.Windows.Forms.Label TotalGamesLabel;
    }
}

