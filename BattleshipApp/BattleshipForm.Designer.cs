using BattleshipLibrary;

namespace BattleshipApp
{
    partial class BattleshipForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleshipForm));
            this.NameOfGame = new System.Windows.Forms.Label();
            this.StartGame = new System.Windows.Forms.Button();
            this.ExitGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameOfGame
            // 
            this.NameOfGame.BackColor = System.Drawing.Color.Transparent;
            this.NameOfGame.Font = new System.Drawing.Font("Snap ITC", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameOfGame.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NameOfGame.Location = new System.Drawing.Point(108, 90);
            this.NameOfGame.Name = "NameOfGame";
            this.NameOfGame.Size = new System.Drawing.Size(497, 137);
            this.NameOfGame.TabIndex = 0;
            this.NameOfGame.Text = "Battleship";
            this.NameOfGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartGame
            // 
            this.StartGame.BackColor = System.Drawing.Color.Orange;
            this.StartGame.FlatAppearance.BorderSize = 3;
            this.StartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartGame.Font = new System.Drawing.Font("Snap ITC", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartGame.ForeColor = System.Drawing.SystemColors.InfoText;
            this.StartGame.Location = new System.Drawing.Point(110, 317);
            this.StartGame.Name = "StartGame";
            this.StartGame.Size = new System.Drawing.Size(154, 47);
            this.StartGame.TabIndex = 1;
            this.StartGame.Text = "Start";
            this.StartGame.UseVisualStyleBackColor = false;
            this.StartGame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // ExitGame
            // 
            this.ExitGame.BackColor = System.Drawing.Color.Orange;
            this.ExitGame.FlatAppearance.BorderSize = 3;
            this.ExitGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitGame.Font = new System.Drawing.Font("Snap ITC", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitGame.ForeColor = System.Drawing.SystemColors.InfoText;
            this.ExitGame.Location = new System.Drawing.Point(451, 317);
            this.ExitGame.Name = "ExitGame";
            this.ExitGame.Size = new System.Drawing.Size(154, 47);
            this.ExitGame.TabIndex = 2;
            this.ExitGame.Text = "Exit";
            this.ExitGame.UseVisualStyleBackColor = false;
            this.ExitGame.Click += new System.EventHandler(this.ExitGame_Click);
            // 
            // BattleshipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.BackgroundImage = global::BattleshipApp.Properties.Resources.BattleshipMainImage;
            this.ClientSize = new System.Drawing.Size(709, 477);
            this.Controls.Add(this.ExitGame);
            this.Controls.Add(this.StartGame);
            this.Controls.Add(this.NameOfGame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BattleshipForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BattleshipForm";
            //this.Load += new System.EventHandler(this.BattleshipForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label NameOfGame;
        private System.Windows.Forms.Button StartGame;
        private System.Windows.Forms.Button ExitGame;
    }
}