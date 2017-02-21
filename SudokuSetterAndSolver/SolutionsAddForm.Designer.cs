namespace SudokuSetterAndSolver
{
    partial class SolutionsAddForm
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
            this.directoryLocationTb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addSolutionsBtn
            // 
            this.addSolutionsBtn.Location = new System.Drawing.Point(258, 292);
            this.addSolutionsBtn.Name = "addSolutionsBtn";
            this.addSolutionsBtn.Size = new System.Drawing.Size(410, 68);
            this.addSolutionsBtn.TabIndex = 0;
            this.addSolutionsBtn.Text = "Add Solutions";
            this.addSolutionsBtn.UseVisualStyleBackColor = true;
            this.addSolutionsBtn.Click += new System.EventHandler(this.addSolutionsBtn_Click);
            // 
            // directoryLocationTb
            // 
            this.directoryLocationTb.Location = new System.Drawing.Point(258, 124);
            this.directoryLocationTb.Name = "directoryLocationTb";
            this.directoryLocationTb.Size = new System.Drawing.Size(392, 38);
            this.directoryLocationTb.TabIndex = 1;
            // 
            // SolutionsAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 410);
            this.Controls.Add(this.directoryLocationTb);
            this.Controls.Add(this.addSolutionsBtn);
            this.Name = "SolutionsAddForm";
            this.Text = "SolutionsAddForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addSolutionsBtn;
        private System.Windows.Forms.TextBox directoryLocationTb;
    }
}