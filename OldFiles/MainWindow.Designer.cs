namespace FreePad
{
    partial class baseWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Dispose Override
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (inkCollector != null)
                {
                    inkCollector.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(baseWindow));
            this.drawingArea = new FreePad.DrawingPanel();
            this.toolSizeIcon = new System.Windows.Forms.PictureBox();
            this.toolSizeLabel = new System.Windows.Forms.Label();
            this.zoomMagnifierIcon = new System.Windows.Forms.PictureBox();
            this.zoomValueLabel = new System.Windows.Forms.Label();
            this.colorWheelWindowButton = new System.Windows.Forms.Button();
            this.fileLabel = new System.Windows.Forms.Label();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.fullScreenButton = new System.Windows.Forms.Button();
            this.icon = new System.Windows.Forms.PictureBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.toolSizeIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomMagnifierIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawingArea
            // 
            this.drawingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawingArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.drawingArea.Location = new System.Drawing.Point(10, 10);
            this.drawingArea.Margin = new System.Windows.Forms.Padding(0);
            this.drawingArea.Name = "drawingArea";
            this.drawingArea.Size = new System.Drawing.Size(772, 376);
            this.drawingArea.TabIndex = 0;
            this.drawingArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.drawingArea.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingArea_Paint);
            this.drawingArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseDown);
            this.drawingArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseMove);
            this.drawingArea.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseWheel);
            // 
            // toolSizeIcon
            // 
            this.toolSizeIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.toolSizeIcon.Image = ((System.Drawing.Image)(resources.GetObject("toolSizeIcon.Image")));
            this.toolSizeIcon.InitialImage = null;
            this.toolSizeIcon.Location = new System.Drawing.Point(7, 32);
            this.toolSizeIcon.Margin = new System.Windows.Forms.Padding(0);
            this.toolSizeIcon.Name = "toolSizeIcon";
            this.toolSizeIcon.Size = new System.Drawing.Size(13, 13);
            this.toolSizeIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.toolSizeIcon.TabIndex = 11;
            this.toolSizeIcon.TabStop = false;
            this.toolSizeIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolSizeIcon_MouseClick);
            // 
            // toolSizeLabel
            // 
            this.toolSizeLabel.AutoSize = true;
            this.toolSizeLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolSizeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.toolSizeLabel.Location = new System.Drawing.Point(20, 30);
            this.toolSizeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.toolSizeLabel.Name = "toolSizeLabel";
            this.toolSizeLabel.Size = new System.Drawing.Size(29, 17);
            this.toolSizeLabel.TabIndex = 12;
            this.toolSizeLabel.Text = "100";
            this.toolSizeLabel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ToolSizeLabel_MouseWheel);
            // 
            // zoomMagnifierIcon
            // 
            this.zoomMagnifierIcon.Image = ((System.Drawing.Image)(resources.GetObject("zoomMagnifierIcon.Image")));
            this.zoomMagnifierIcon.Location = new System.Drawing.Point(55, 32);
            this.zoomMagnifierIcon.Margin = new System.Windows.Forms.Padding(0);
            this.zoomMagnifierIcon.Name = "zoomMagnifierIcon";
            this.zoomMagnifierIcon.Size = new System.Drawing.Size(13, 13);
            this.zoomMagnifierIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.zoomMagnifierIcon.TabIndex = 7;
            this.zoomMagnifierIcon.TabStop = false;
            this.zoomMagnifierIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ZoomMagnifierIcon_MouseClick);
            // 
            // zoomValueLabel
            // 
            this.zoomValueLabel.AutoSize = true;
            this.zoomValueLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.zoomValueLabel.Location = new System.Drawing.Point(69, 30);
            this.zoomValueLabel.Margin = new System.Windows.Forms.Padding(0);
            this.zoomValueLabel.Name = "zoomValueLabel";
            this.zoomValueLabel.Size = new System.Drawing.Size(39, 17);
            this.zoomValueLabel.TabIndex = 9;
            this.zoomValueLabel.Text = "100%";
            this.zoomValueLabel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.zoomValueLabel_MouseWheel);
            // 
            // colorWheelWindowButton
            // 
            this.colorWheelWindowButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.colorWheelWindowButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorWheelWindowButton.FlatAppearance.BorderSize = 0;
            this.colorWheelWindowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorWheelWindowButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorWheelWindowButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.colorWheelWindowButton.Location = new System.Drawing.Point(368, -2);
            this.colorWheelWindowButton.Margin = new System.Windows.Forms.Padding(0);
            this.colorWheelWindowButton.Name = "colorWheelWindowButton";
            this.colorWheelWindowButton.Size = new System.Drawing.Size(118, 22);
            this.colorWheelWindowButton.TabIndex = 16;
            this.colorWheelWindowButton.Text = "Show Color wheel";
            this.colorWheelWindowButton.UseVisualStyleBackColor = true;
            this.colorWheelWindowButton.Click += new System.EventHandler(this.ColorWheelWindowButton_Click);
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.fileLabel.Location = new System.Drawing.Point(24, 4);
            this.fileLabel.Margin = new System.Windows.Forms.Padding(0);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(28, 17);
            this.fileLabel.TabIndex = 15;
            this.fileLabel.Text = "File";
            this.fileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // minimizeButton
            // 
            this.minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.minimizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.minimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.minimizeButton.Location = new System.Drawing.Point(708, -2);
            this.minimizeButton.Margin = new System.Windows.Forms.Padding(0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(25, 22);
            this.minimizeButton.TabIndex = 14;
            this.minimizeButton.TabStop = false;
            this.minimizeButton.Text = "–";
            this.minimizeButton.UseVisualStyleBackColor = true;
            this.minimizeButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MinimizeButton_MouseClick);
            // 
            // fullScreenButton
            // 
            this.fullScreenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fullScreenButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.fullScreenButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.fullScreenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.fullScreenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fullScreenButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fullScreenButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.fullScreenButton.Location = new System.Drawing.Point(732, -2);
            this.fullScreenButton.Margin = new System.Windows.Forms.Padding(0);
            this.fullScreenButton.Name = "fullScreenButton";
            this.fullScreenButton.Size = new System.Drawing.Size(25, 22);
            this.fullScreenButton.TabIndex = 13;
            this.fullScreenButton.TabStop = false;
            this.fullScreenButton.Text = "☐";
            this.fullScreenButton.UseVisualStyleBackColor = true;
            this.fullScreenButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FullScreenButton_MouseClick);
            // 
            // icon
            // 
            this.icon.Image = ((System.Drawing.Image)(resources.GetObject("icon.Image")));
            this.icon.InitialImage = null;
            this.icon.Location = new System.Drawing.Point(5, 4);
            this.icon.Margin = new System.Windows.Forms.Padding(0);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(16, 16);
            this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.icon.TabIndex = 8;
            this.icon.TabStop = false;
            this.icon.WaitOnLoad = true;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.closeButton.Location = new System.Drawing.Point(756, -2);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(42, 22);
            this.closeButton.TabIndex = 10;
            this.closeButton.TabStop = false;
            this.closeButton.Text = "✕";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CloseButton_MouseClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 1);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel2.Controls.Add(this.drawingArea);
            this.panel2.Location = new System.Drawing.Point(4, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(792, 396);
            this.panel2.TabIndex = 18;
            // 
            // baseWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolSizeIcon);
            this.Controls.Add(this.toolSizeLabel);
            this.Controls.Add(this.zoomValueLabel);
            this.Controls.Add(this.zoomMagnifierIcon);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.icon);
            this.Controls.Add(this.colorWheelWindowButton);
            this.Controls.Add(this.fullScreenButton);
            this.Controls.Add(this.fileLabel);
            this.Controls.Add(this.minimizeButton);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "baseWindow";
            this.Text = "FreePad";
            this.Load += new System.EventHandler(this.Ink_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BaseWindow_MouseMove);
            this.Resize += new System.EventHandler(this.BaseWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.toolSizeIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomMagnifierIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DrawingPanel drawingArea;
        private System.Windows.Forms.PictureBox toolSizeIcon;
        private System.Windows.Forms.Label toolSizeLabel;
        private System.Windows.Forms.PictureBox zoomMagnifierIcon;
        private System.Windows.Forms.Label zoomValueLabel;
        private System.Windows.Forms.Button colorWheelWindowButton;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.Button minimizeButton;
        private System.Windows.Forms.Button fullScreenButton;
        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

