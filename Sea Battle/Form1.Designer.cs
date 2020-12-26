namespace Sea_Battle
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.pictureGraphics = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureGraphics)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureGraphics
            // 
            this.pictureGraphics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureGraphics.Location = new System.Drawing.Point(0, 0);
            this.pictureGraphics.Name = "pictureGraphics";
            this.pictureGraphics.Size = new System.Drawing.Size(1264, 681);
            this.pictureGraphics.TabIndex = 0;
            this.pictureGraphics.TabStop = false;
            this.pictureGraphics.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureGraphics_MouseClick);
            this.pictureGraphics.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureGraphics_MouseMove);
            // 
            // timer
            // 
            this.timer.Interval = 40;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.pictureGraphics);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Sea Battle";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureGraphics)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureGraphics;
        private System.Windows.Forms.Timer timer;
    }
}

