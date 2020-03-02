namespace ChessGame
{
    partial class Setting_form
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
            this.WhiteCells = new System.Windows.Forms.Button();
            this.BlackCells = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.CD = new System.Windows.Forms.ColorDialog();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // WhiteCells
            // 
            this.WhiteCells.Location = new System.Drawing.Point(45, 12);
            this.WhiteCells.Name = "WhiteCells";
            this.WhiteCells.Size = new System.Drawing.Size(98, 43);
            this.WhiteCells.TabIndex = 2;
            this.WhiteCells.Text = "WhiteCells";
            this.WhiteCells.UseVisualStyleBackColor = true;
            this.WhiteCells.Click += new System.EventHandler(this.White);
            // 
            // BlackCells
            // 
            this.BlackCells.Location = new System.Drawing.Point(45, 61);
            this.BlackCells.Name = "BlackCells";
            this.BlackCells.Size = new System.Drawing.Size(98, 45);
            this.BlackCells.TabIndex = 3;
            this.BlackCells.Text = "BlackCells";
            this.BlackCells.UseVisualStyleBackColor = true;
            this.BlackCells.Click += new System.EventHandler(this.Black);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 45);
            this.button1.TabIndex = 4;
            this.button1.Text = "Skins";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Setting_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 166);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BlackCells);
            this.Controls.Add(this.WhiteCells);
            this.Name = "Setting_form";
            this.Text = "Setting_form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button WhiteCells;
        private System.Windows.Forms.Button BlackCells;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColorDialog CD;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
    }
}