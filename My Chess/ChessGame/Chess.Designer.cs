namespace ChessGame
{
    partial class Chess
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Restat = new System.Windows.Forms.Button();
            this.colorDialogC = new System.Windows.Forms.ColorDialog();
            this.WhiteCells = new System.Windows.Forms.Button();
            this.BlackCells = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Restat
            // 
            this.Restat.Location = new System.Drawing.Point(650, 12);
            this.Restat.Name = "Restat";
            this.Restat.Size = new System.Drawing.Size(78, 29);
            this.Restat.TabIndex = 0;
            this.Restat.Text = "Restart";
            this.Restat.UseVisualStyleBackColor = true;
            this.Restat.UseWaitCursor = true;
            this.Restat.Click += new System.EventHandler(this.Restart);
            // 
            // WhiteCells
            // 
            this.WhiteCells.Location = new System.Drawing.Point(650, 58);
            this.WhiteCells.Name = "WhiteCells";
            this.WhiteCells.Size = new System.Drawing.Size(78, 27);
            this.WhiteCells.TabIndex = 1;
            this.WhiteCells.Text = "WhiteCells";
            this.WhiteCells.UseVisualStyleBackColor = true;
            this.WhiteCells.UseWaitCursor = true;
            this.WhiteCells.Click += new System.EventHandler(this.White);
            // 
            // BlackCells
            // 
            this.BlackCells.Location = new System.Drawing.Point(650, 103);
            this.BlackCells.Name = "BlackCells";
            this.BlackCells.Size = new System.Drawing.Size(78, 29);
            this.BlackCells.TabIndex = 2;
            this.BlackCells.Text = "BlackCells";
            this.BlackCells.UseVisualStyleBackColor = true;
            this.BlackCells.UseWaitCursor = true;
            this.BlackCells.Click += new System.EventHandler(this.Black);
            // 
            // Chess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 661);
            this.Controls.Add(this.BlackCells);
            this.Controls.Add(this.WhiteCells);
            this.Controls.Add(this.Restat);
            this.Name = "Chess";
            this.Text = "Chess";
            this.UseWaitCursor = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Restat;
        private System.Windows.Forms.ColorDialog colorDialogC;
        private System.Windows.Forms.Button WhiteCells;
        private System.Windows.Forms.Button BlackCells;
    }
}

