namespace SudokuSetterAndSolver
{
    partial class TemplateScreen
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
            this.gameBanner = new System.Windows.Forms.PictureBox();
            this.mainMenuBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // gameBanner
            // 
            this.gameBanner.Image = global::SudokuSetterAndSolver.Properties.Resources.SSSGameScreenFullBanner_fw;
            this.gameBanner.Location = new System.Drawing.Point(1, -2);
            this.gameBanner.Name = "gameBanner";
            this.gameBanner.Size = new System.Drawing.Size(1467, 250);
            this.gameBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gameBanner.TabIndex = 8;
            this.gameBanner.TabStop = false;
            // 
            // mainMenuBtn
            // 
            this.mainMenuBtn.Location = new System.Drawing.Point(643, 909);
            this.mainMenuBtn.Name = "mainMenuBtn";
            this.mainMenuBtn.Size = new System.Drawing.Size(170, 91);
            this.mainMenuBtn.TabIndex = 9;
            this.mainMenuBtn.Text = "Main Menu ";
            this.mainMenuBtn.UseVisualStyleBackColor = true;
            this.mainMenuBtn.Click += new System.EventHandler(this.mainMenuBtn_Click);
            // 
            // TemplateScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1468, 1012);
            this.Controls.Add(this.mainMenuBtn);
            this.Controls.Add(this.gameBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TemplateScreen";
            this.Text = "TemplateScreen";
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox gameBanner;
        private System.Windows.Forms.Button mainMenuBtn;
    }
}