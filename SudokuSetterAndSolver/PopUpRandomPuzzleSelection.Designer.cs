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
            this.puzzleTypeSelection = new System.Windows.Forms.ComboBox();
            this.confirmBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // puzzleTypeSelection
            // 
            this.puzzleTypeSelection.FormattingEnabled = true;
            this.puzzleTypeSelection.Items.AddRange(new object[] {
            "Irregular",
            "16*16",
            "9*9",
            "4*4"});
            this.puzzleTypeSelection.Location = new System.Drawing.Point(75, 88);
            this.puzzleTypeSelection.Name = "puzzleTypeSelection";
            this.puzzleTypeSelection.Size = new System.Drawing.Size(331, 39);
            this.puzzleTypeSelection.TabIndex = 0;
            // 
            // confirmBtn
            // 
            this.confirmBtn.Location = new System.Drawing.Point(149, 175);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(180, 84);
            this.confirmBtn.TabIndex = 1;
            this.confirmBtn.Text = "Confirm";
            this.confirmBtn.UseVisualStyleBackColor = true;
            this.confirmBtn.Click += new System.EventHandler(this.confirmBtn_Click);
            // 
            // PopUpRandomPuzzleSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 323);
            this.Controls.Add(this.confirmBtn);
            this.Controls.Add(this.puzzleTypeSelection);
            this.Name = "PopUpRandomPuzzleSelection";
            this.Text = "Puzzle Selection";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox puzzleTypeSelection;
        private System.Windows.Forms.Button confirmBtn;
    }
}