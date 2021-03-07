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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.server_update_timer = new System.Windows.Forms.Timer(this.components);
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
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(490, 250);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 24);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "is Server";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(490, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 65);
            this.button1.TabIndex = 2;
            this.button1.Text = "play online";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(651, 280);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 65);
            this.button2.TabIndex = 3;
            this.button2.Text = "play offline";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(490, 361);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(100, 20);
            this.textBox_ip.TabIndex = 4;
            this.textBox_ip.Text = "127.0.0.1";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(490, 387);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(100, 20);
            this.textBox_port.TabIndex = 5;
            this.textBox_port.Text = "8005";
            // 
            // server_update_timer
            // 
            this.server_update_timer.Interval = 20;
            this.server_update_timer.Tick += new System.EventHandler(this.server_update_timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.pictureGraphics);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Sea Battle";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureGraphics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureGraphics;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        protected System.Windows.Forms.Timer server_update_timer;
    }
}

