namespace SudokuSetterAndSolver
{
    partial class DevelopmentForm
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
            this.addSolutionsBtn = new System.Windows.Forms.Button();
            this.generatePuzzleBtn = new System.Windows.Forms.Button();
            this.convertFileBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addSolutionsBtn
            // 
            this.addSolutionsBtn.Location = new System.Drawing.Point(293, 310);
            this.addSolutionsBtn.Name = "addSolutionsBtn";
            this.addSolutionsBtn.Size = new System.Drawing.Size(166, 90);
            this.addSolutionsBtn.TabIndex = 12;
            this.addSolutionsBtn.Text = "Solutions";
            this.addSolutionsBtn.UseVisualStyleBackColor = true;
            this.addSolutionsBtn.Click += new System.EventHandler(this.addSolutionsBtn_Click);
            // 
            // generatePuzzleBtn
            // 
            this.generatePuzzleBtn.Location = new System.Drawing.Point(293, 163);
            this.generatePuzzleBtn.Name = "generatePuzzleBtn";
            this.generatePuzzleBtn.Size = new System.Drawing.Size(166, 90);
            this.generatePuzzleBtn.TabIndex = 11;
            this.generatePuzzleBtn.Text = "Create 10 Puzzles";
            this.generatePuzzleBtn.UseVisualStyleBackColor = true;
            this.generatePuzzleBtn.Click += new System.EventHandler(this.generatePuzzleBtn_Click);
            // 
            // convertFileBtn
            // 
            this.convertFileBtn.Location = new System.Drawing.Point(293, 30);
            this.convertFileBtn.Name = "convertFileBtn";
            this.convertFileBtn.Size = new System.Drawing.Size(166, 81);
            this.convertFileBtn.TabIndex = 10;
            this.convertFileBtn.Text = "Convert";
            this.convertFileBtn.UseVisualStyleBackColor = true;
            this.convertFileBtn.Click += new System.EventHandler(this.convertFileBtn_Click);
            // 
            // DevelopmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 470);
            this.Controls.Add(this.addSolutionsBtn);
            this.Controls.Add(this.generatePuzzleBtn);
            this.Controls.Add(this.convertFileBtn);
            this.Name = "DevelopmentForm";
            this.Text = "Development ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addSolutionsBtn;
        private System.Windows.Forms.Button generatePuzzleBtn;
        private System.Windows.Forms.Button convertFileBtn;
    }
}