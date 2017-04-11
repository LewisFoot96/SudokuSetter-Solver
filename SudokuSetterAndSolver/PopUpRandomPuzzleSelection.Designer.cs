namespace SudokuSetterAndSolver
{
    partial class PopUpRandomPuzzleSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopUpRandomPuzzleSelection));
            this.puzzleTypeSelection = new System.Windows.Forms.ComboBox();
            this.confirmBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // puzzleTypeSelection
            // 
            this.puzzleTypeSelection.FormattingEnabled = true;
            this.puzzleTypeSelection.Items.AddRange(new object[] {
            "Irregular",
            "9*9",
            "4*4"});
            this.puzzleTypeSelection.Location = new System.Drawing.Point(251, 63);
            this.puzzleTypeSelection.Name = "puzzleTypeSelection";
            this.puzzleTypeSelection.Size = new System.Drawing.Size(254, 39);
            this.puzzleTypeSelection.TabIndex = 0;
            // 
            // confirmBtn
            // 
            this.confirmBtn.Location = new System.Drawing.Point(263, 135);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(228, 86);
            this.confirmBtn.TabIndex = 1;
            this.confirmBtn.Text = "Confirm";
            this.confirmBtn.UseVisualStyleBackColor = true;
            this.confirmBtn.Click += new System.EventHandler(this.confirmBtn_Click);
            // 
            // PopUpRandomPuzzleSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(535, 261);
            this.Controls.Add(this.confirmBtn);
            this.Controls.Add(this.puzzleTypeSelection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopUpRandomPuzzleSelection";
            this.Text = "Puzzle Select";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox puzzleTypeSelection;
        private System.Windows.Forms.Button confirmBtn;
    }
}