namespace FittingALineIntoData
{
    partial class MainForm
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
            this.dataPanel = new System.Windows.Forms.Panel();
            this.fitBtn = new System.Windows.Forms.Button();
            this.pointsListBox = new System.Windows.Forms.ListBox();
            this.removePointBtn = new System.Windows.Forms.Button();
            this.addPointBtn = new System.Windows.Forms.Button();
            this.clearAllBtn = new System.Windows.Forms.Button();
            this.bestLineBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dataPanel
            // 
            this.dataPanel.Location = new System.Drawing.Point(12, 12);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(600, 600);
            this.dataPanel.TabIndex = 0;
            this.dataPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DataPanelPaint);
            this.dataPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DataPanelClick);
            // 
            // fitBtn
            // 
            this.fitBtn.Location = new System.Drawing.Point(21, 619);
            this.fitBtn.Name = "fitBtn";
            this.fitBtn.Size = new System.Drawing.Size(80, 28);
            this.fitBtn.TabIndex = 1;
            this.fitBtn.Text = "Fit!";
            this.fitBtn.UseVisualStyleBackColor = true;
            this.fitBtn.Click += new System.EventHandler(this.FitBtnClick);
            // 
            // pointsListBox
            // 
            this.pointsListBox.FormattingEnabled = true;
            this.pointsListBox.Location = new System.Drawing.Point(618, 21);
            this.pointsListBox.Name = "pointsListBox";
            this.pointsListBox.Size = new System.Drawing.Size(156, 563);
            this.pointsListBox.TabIndex = 2;
            // 
            // removePointBtn
            // 
            this.removePointBtn.Location = new System.Drawing.Point(699, 589);
            this.removePointBtn.Name = "removePointBtn";
            this.removePointBtn.Size = new System.Drawing.Size(75, 23);
            this.removePointBtn.TabIndex = 3;
            this.removePointBtn.Text = "Remove";
            this.removePointBtn.UseVisualStyleBackColor = true;
            this.removePointBtn.Click += new System.EventHandler(this.RemovePointBtnClick);
            // 
            // addPointBtn
            // 
            this.addPointBtn.Location = new System.Drawing.Point(618, 590);
            this.addPointBtn.Name = "addPointBtn";
            this.addPointBtn.Size = new System.Drawing.Size(75, 23);
            this.addPointBtn.TabIndex = 4;
            this.addPointBtn.Text = "Add";
            this.addPointBtn.UseVisualStyleBackColor = true;
            this.addPointBtn.Click += new System.EventHandler(this.AddPointBtnClick);
            // 
            // clearAllBtn
            // 
            this.clearAllBtn.Location = new System.Drawing.Point(506, 624);
            this.clearAllBtn.Name = "clearAllBtn";
            this.clearAllBtn.Size = new System.Drawing.Size(75, 23);
            this.clearAllBtn.TabIndex = 5;
            this.clearAllBtn.Text = "Clear";
            this.clearAllBtn.UseVisualStyleBackColor = true;
            this.clearAllBtn.Click += new System.EventHandler(this.ClearAllBtnClick);
            // 
            // bestLineBtn
            // 
            this.bestLineBtn.Location = new System.Drawing.Point(125, 624);
            this.bestLineBtn.Name = "bestLineBtn";
            this.bestLineBtn.Size = new System.Drawing.Size(75, 23);
            this.bestLineBtn.TabIndex = 6;
            this.bestLineBtn.Text = "Best";
            this.bestLineBtn.UseVisualStyleBackColor = true;
            this.bestLineBtn.Click += new System.EventHandler(this.BestLineBtnClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 659);
            this.Controls.Add(this.bestLineBtn);
            this.Controls.Add(this.clearAllBtn);
            this.Controls.Add(this.addPointBtn);
            this.Controls.Add(this.removePointBtn);
            this.Controls.Add(this.pointsListBox);
            this.Controls.Add(this.fitBtn);
            this.Controls.Add(this.dataPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "Fitter";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel dataPanel;
        private System.Windows.Forms.Button fitBtn;
        private System.Windows.Forms.ListBox pointsListBox;
        private System.Windows.Forms.Button removePointBtn;
        private System.Windows.Forms.Button addPointBtn;
        private System.Windows.Forms.Button clearAllBtn;
        private System.Windows.Forms.Button bestLineBtn;
    }
}

