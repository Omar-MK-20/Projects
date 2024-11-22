namespace Digital_Clock
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblDay = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnShowBorder = new System.Windows.Forms.Button();
            this.PrTimes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("DS-Digital", 60F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTime.Location = new System.Drawing.Point(16, 67);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(316, 79);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "00:00:00";
            this.lblTime.Click += new System.EventHandler(this.lblTime_Click);
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("DS-Digital", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblMonth.Location = new System.Drawing.Point(12, 140);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(282, 63);
            this.lblMonth.TabIndex = 2;
            this.lblMonth.Text = "7-10-2023";
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.Font = new System.Drawing.Font("DS-Digital", 35F);
            this.lblDay.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblDay.Location = new System.Drawing.Point(326, 154);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(158, 46);
            this.lblDay.TabIndex = 3;
            this.lblDay.Text = "Sunday";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.FlatAppearance.BorderSize = 3;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("DS-Digital", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnClose.Location = new System.Drawing.Point(538, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(28, 27);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "X";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnMinimize.FlatAppearance.BorderSize = 3;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMinimize.Font = new System.Drawing.Font("DS-Digital", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnMinimize.Location = new System.Drawing.Point(504, 8);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(28, 27);
            this.btnMinimize.TabIndex = 6;
            this.btnMinimize.Text = "-";
            this.btnMinimize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnShowBorder
            // 
            this.btnShowBorder.BackColor = System.Drawing.Color.Transparent;
            this.btnShowBorder.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnShowBorder.FlatAppearance.BorderSize = 3;
            this.btnShowBorder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowBorder.Font = new System.Drawing.Font("DS-Digital", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowBorder.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnShowBorder.Location = new System.Drawing.Point(12, 8);
            this.btnShowBorder.Name = "btnShowBorder";
            this.btnShowBorder.Size = new System.Drawing.Size(121, 27);
            this.btnShowBorder.TabIndex = 7;
            this.btnShowBorder.Text = "Show Border";
            this.btnShowBorder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnShowBorder.UseVisualStyleBackColor = false;
            this.btnShowBorder.Click += new System.EventHandler(this.btnShowBorder_Click);
            // 
            // PrTimes
            // 
            this.PrTimes.BackColor = System.Drawing.Color.Transparent;
            this.PrTimes.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.PrTimes.FlatAppearance.BorderSize = 3;
            this.PrTimes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PrTimes.Font = new System.Drawing.Font("DS-Digital", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrTimes.ForeColor = System.Drawing.Color.DodgerBlue;
            this.PrTimes.Location = new System.Drawing.Point(369, 89);
            this.PrTimes.Name = "PrTimes";
            this.PrTimes.Size = new System.Drawing.Size(163, 27);
            this.PrTimes.TabIndex = 9;
            this.PrTimes.Text = "prayer Times";
            this.PrTimes.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PrTimes.UseVisualStyleBackColor = false;
            this.PrTimes.Click += new System.EventHandler(this.PrTimes_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(573, 212);
            this.Controls.Add(this.PrTimes);
            this.Controls.Add(this.btnShowBorder);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnShowBorder;
        private System.Windows.Forms.Button PrTimes;
    }
}

