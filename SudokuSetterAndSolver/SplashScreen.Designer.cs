namespace SudokuSetterAndSolver
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.animationPb = new System.Windows.Forms.PictureBox();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.animationPb)).BeginInit();
            this.SuspendLayout();
            // 
            // animationPb
            // 
            this.animationPb.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.animationPb.BackColor = System.Drawing.Color.Transparent;
            this.animationPb.Image = ((System.Drawing.Image)(resources.GetObject("animationPb.Image")));
            this.animationPb.Location = new System.Drawing.Point(60, 29);
            this.animationPb.Name = "animationPb";
            this.animationPb.Size = new System.Drawing.Size(150, 131);
            this.animationPb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.animationPb.TabIndex = 0;
            this.animationPb.TabStop = false;
            // 
            // animationTimer
            // 
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(974, 590);
            this.Controls.Add(this.animationPb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash Screen : Siwel Sudoku ";
            ((System.ComponentModel.ISupportInitialize)(this.animationPb)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox animationPb;
        private System.Windows.Forms.Timer animationTimer;
    }
}