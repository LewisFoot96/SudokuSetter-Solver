namespace SudokuSetterAndSolver
{
    partial class PopUpSolverScreen
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
            this.solveSudokuSelectionCb = new System.Windows.Forms.ComboBox();
            this.confirmSolverSelectionBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // solveSudokuSelectionCb
            // 
            this.solveSudokuSelectionCb.FormattingEnabled = true;
            this.solveSudokuSelectionCb.Items.AddRange(new object[] {
            "16*16",
            "9*9",
            "4*4"});
            this.solveSudokuSelectionCb.Location = new System.Drawing.Point(97, 77);
            this.solveSudokuSelectionCb.Name = "solveSudokuSelectionCb";
            this.solveSudokuSelectionCb.Size = new System.Drawing.Size(254, 39);
            this.solveSudokuSelectionCb.TabIndex = 0;
            // 
            // confirmSolverSelectionBtn
            // 
            this.confirmSolverSelectionBtn.Location = new System.Drawing.Point(113, 155);
            this.confirmSolverSelectionBtn.Name = "confirmSolverSelectionBtn";
            this.confirmSolverSelectionBtn.Size = new System.Drawing.Size(228, 86);
            this.confirmSolverSelectionBtn.TabIndex = 1;
            this.confirmSolverSelectionBtn.Text = "Confirm";
            this.confirmSolverSelectionBtn.UseVisualStyleBackColor = true;
            this.confirmSolverSelectionBtn.Click += new System.EventHandler(this.confirmSolverSelectionBtn_Click);
            // 
            // PopUpSolverScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 288);
            this.Controls.Add(this.confirmSolverSelectionBtn);
            this.Controls.Add(this.solveSudokuSelectionCb);
            this.Name = "PopUpSolverScreen";
            this.Text = "Sudoku Solve Selection";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox solveSudokuSelectionCb;
        private System.Windows.Forms.Button confirmSolverSelectionBtn;
    }
}