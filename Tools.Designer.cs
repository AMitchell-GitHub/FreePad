namespace FreePad
{
    partial class Tools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tools));
            this.penSizeIcon = new System.Windows.Forms.PictureBox();
            this.penSizeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.penSizeIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // penSizeIcon
            // 
            this.penSizeIcon.Image = ((System.Drawing.Image)(resources.GetObject("penSizeIcon.Image")));
            this.penSizeIcon.Location = new System.Drawing.Point(4, 8);
            this.penSizeIcon.Margin = new System.Windows.Forms.Padding(0);
            this.penSizeIcon.Name = "penSizeIcon";
            this.penSizeIcon.Size = new System.Drawing.Size(16, 16);
            this.penSizeIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.penSizeIcon.TabIndex = 0;
            this.penSizeIcon.TabStop = false;
            // 
            // penSizeLabel
            // 
            this.penSizeLabel.AutoSize = true;
            this.penSizeLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.penSizeLabel.Location = new System.Drawing.Point(24, 8);
            this.penSizeLabel.Name = "penSizeLabel";
            this.penSizeLabel.Size = new System.Drawing.Size(29, 17);
            this.penSizeLabel.TabIndex = 1;
            this.penSizeLabel.Text = "100";
            this.penSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Tools
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.BorderThickness = 0;
            this.CaptionAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.CaptionBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.CaptionBarHeight = 20;
            this.CaptionFont = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CaptionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(64, 274);
            this.Controls.Add(this.penSizeLabel);
            this.Controls.Add(this.penSizeIcon);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(76, 300);
            this.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.MinimizeBox = false;
            this.Name = "Tools";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowMaximizeBox = false;
            this.ShowMinimizeBox = false;
            this.Text = "Tools";
            this.Activated += new System.EventHandler(this.Tools_Activated);
            this.Deactivate += new System.EventHandler(this.Tools_Deactivate);
            this.ResizeBegin += new System.EventHandler(this.Tools_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Tools_ResizeEnd);
            this.Move += new System.EventHandler(this.Tools_Move);
            ((System.ComponentModel.ISupportInitialize)(this.penSizeIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox penSizeIcon;
        public System.Windows.Forms.Label penSizeLabel;
    }
}