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
            this.topBar = new System.Windows.Forms.Panel();
            this.fileLabel = new System.Windows.Forms.Label();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.fullScreenButton = new System.Windows.Forms.Button();
            this.icon = new System.Windows.Forms.PictureBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.drawingArea = new System.Windows.Forms.Panel();
            this.leftResizeBar = new System.Windows.Forms.Panel();
            this.rightResizeBar = new System.Windows.Forms.Panel();
            this.bottomResizeBar = new System.Windows.Forms.Panel();
            this.drawingSettingsBar = new System.Windows.Forms.Panel();
            this.toolSizeIcon = new System.Windows.Forms.PictureBox();
            this.toolSizeLabel = new System.Windows.Forms.Label();
            this.zoomMagnifierIcon = new System.Windows.Forms.PictureBox();
            this.zoomValueLabel = new System.Windows.Forms.Label();
            this.topBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.drawingSettingsBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toolSizeIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomMagnifierIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // topBar
            // 
            this.topBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.topBar.Controls.Add(this.fileLabel);
            this.topBar.Controls.Add(this.minimizeButton);
            this.topBar.Controls.Add(this.fullScreenButton);
            this.topBar.Controls.Add(this.icon);
            this.topBar.Controls.Add(this.closeButton);
            this.topBar.Location = new System.Drawing.Point(0, 0);
            this.topBar.Name = "topBar";
            this.topBar.Size = new System.Drawing.Size(800, 22);
            this.topBar.TabIndex = 0;
            this.topBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopBar_MouseDown);
            this.topBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopBar_MouseMove);
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.fileLabel.Location = new System.Drawing.Point(27, 3);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(28, 17);
            this.fileLabel.TabIndex = 5;
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
            this.minimizeButton.Location = new System.Drawing.Point(707, -2);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(25, 22);
            this.minimizeButton.TabIndex = 4;
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
            this.fullScreenButton.Location = new System.Drawing.Point(731, -2);
            this.fullScreenButton.Name = "fullScreenButton";
            this.fullScreenButton.Size = new System.Drawing.Size(25, 22);
            this.fullScreenButton.TabIndex = 3;
            this.fullScreenButton.TabStop = false;
            this.fullScreenButton.Text = "☐";
            this.fullScreenButton.UseVisualStyleBackColor = true;
            this.fullScreenButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FullScreenButton_MouseClick);
            // 
            // icon
            // 
            this.icon.Image = ((System.Drawing.Image)(resources.GetObject("icon.Image")));
            this.icon.InitialImage = null;
            this.icon.Location = new System.Drawing.Point(5, 3);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(16, 16);
            this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.icon.TabIndex = 0;
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
            this.closeButton.Location = new System.Drawing.Point(755, -2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(42, 22);
            this.closeButton.TabIndex = 1;
            this.closeButton.TabStop = false;
            this.closeButton.Text = "✕";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CloseButton_MouseClick);
            // 
            // drawingArea
            // 
            this.drawingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawingArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.drawingArea.Location = new System.Drawing.Point(14, 58);
            this.drawingArea.Name = "drawingArea";
            this.drawingArea.Size = new System.Drawing.Size(772, 378);
            this.drawingArea.TabIndex = 0;
            this.drawingArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.drawingArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseDown);
            this.drawingArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseMove);
            this.drawingArea.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseWheel);
            // 
            // leftResizeBar
            // 
            this.leftResizeBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.leftResizeBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.leftResizeBar.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.leftResizeBar.Location = new System.Drawing.Point(0, 60);
            this.leftResizeBar.Name = "leftResizeBar";
            this.leftResizeBar.Size = new System.Drawing.Size(4, 380);
            this.leftResizeBar.TabIndex = 2;
            this.leftResizeBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ResizeLeft_MouseDown);
            this.leftResizeBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ResizeLeft_MouseMove);
            // 
            // rightResizeBar
            // 
            this.rightResizeBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightResizeBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.rightResizeBar.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.rightResizeBar.Location = new System.Drawing.Point(796, 60);
            this.rightResizeBar.Name = "rightResizeBar";
            this.rightResizeBar.Size = new System.Drawing.Size(4, 380);
            this.rightResizeBar.TabIndex = 1;
            this.rightResizeBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ResizeRight_MouseDown);
            this.rightResizeBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ResizeRight_MouseMove);
            // 
            // bottomResizeBar
            // 
            this.bottomResizeBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomResizeBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.bottomResizeBar.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.bottomResizeBar.Location = new System.Drawing.Point(0, 446);
            this.bottomResizeBar.Name = "bottomResizeBar";
            this.bottomResizeBar.Size = new System.Drawing.Size(800, 4);
            this.bottomResizeBar.TabIndex = 0;
            this.bottomResizeBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ResizeBottom_MouseDown);
            this.bottomResizeBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ResizeBottom_MouseMove);
            // 
            // drawingSettingsBar
            // 
            this.drawingSettingsBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.drawingSettingsBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.drawingSettingsBar.Controls.Add(this.toolSizeIcon);
            this.drawingSettingsBar.Controls.Add(this.toolSizeLabel);
            this.drawingSettingsBar.Controls.Add(this.zoomMagnifierIcon);
            this.drawingSettingsBar.Controls.Add(this.zoomValueLabel);
            this.drawingSettingsBar.Location = new System.Drawing.Point(0, 23);
            this.drawingSettingsBar.Name = "drawingSettingsBar";
            this.drawingSettingsBar.Size = new System.Drawing.Size(800, 25);
            this.drawingSettingsBar.TabIndex = 3;
            // 
            // toolSizeIcon
            // 
            this.toolSizeIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.toolSizeIcon.Image = ((System.Drawing.Image)(resources.GetObject("toolSizeIcon.Image")));
            this.toolSizeIcon.InitialImage = null;
            this.toolSizeIcon.Location = new System.Drawing.Point(6, 6);
            this.toolSizeIcon.Name = "toolSizeIcon";
            this.toolSizeIcon.Size = new System.Drawing.Size(13, 13);
            this.toolSizeIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.toolSizeIcon.TabIndex = 2;
            this.toolSizeIcon.TabStop = false;
            this.toolSizeIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolSizeIcon_MouseClick);
            // 
            // toolSizeLabel
            // 
            this.toolSizeLabel.AutoSize = true;
            this.toolSizeLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolSizeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.toolSizeLabel.Location = new System.Drawing.Point(19, 4);
            this.toolSizeLabel.Name = "toolSizeLabel";
            this.toolSizeLabel.Size = new System.Drawing.Size(29, 17);
            this.toolSizeLabel.TabIndex = 2;
            this.toolSizeLabel.Text = "100";
            this.toolSizeLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ToolSizeIcon_MouseClick);
            this.toolSizeLabel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ToolSizeLabel_MouseWheel);
            // 
            // zoomMagnifierIcon
            // 
            this.zoomMagnifierIcon.Image = ((System.Drawing.Image)(resources.GetObject("zoomMagnifierIcon.Image")));
            this.zoomMagnifierIcon.Location = new System.Drawing.Point(63, 6);
            this.zoomMagnifierIcon.Name = "zoomMagnifierIcon";
            this.zoomMagnifierIcon.Size = new System.Drawing.Size(13, 13);
            this.zoomMagnifierIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.zoomMagnifierIcon.TabIndex = 0;
            this.zoomMagnifierIcon.TabStop = false;
            this.zoomMagnifierIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ZoomMagnifierIcon_MouseClick);
            // 
            // zoomValueLabel
            // 
            this.zoomValueLabel.AutoSize = true;
            this.zoomValueLabel.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(203)))));
            this.zoomValueLabel.Location = new System.Drawing.Point(76, 4);
            this.zoomValueLabel.Name = "zoomValueLabel";
            this.zoomValueLabel.Size = new System.Drawing.Size(39, 17);
            this.zoomValueLabel.TabIndex = 1;
            this.zoomValueLabel.Text = "100%";
            this.zoomValueLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ZoomMagnifierIcon_MouseClick);
            this.zoomValueLabel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.zoomValueLabel_MouseWheel);
            // 
            // baseWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.drawingSettingsBar);
            this.Controls.Add(this.bottomResizeBar);
            this.Controls.Add(this.rightResizeBar);
            this.Controls.Add(this.leftResizeBar);
            this.Controls.Add(this.drawingArea);
            this.Controls.Add(this.topBar);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "baseWindow";
            this.Text = "FreePad";
            this.Load += new System.EventHandler(this.InkZoom_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            this.topBar.ResumeLayout(false);
            this.topBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.drawingSettingsBar.ResumeLayout(false);
            this.drawingSettingsBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toolSizeIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomMagnifierIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topBar;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Panel drawingArea;
        private System.Windows.Forms.Panel leftResizeBar;
        private System.Windows.Forms.Panel rightResizeBar;
        private System.Windows.Forms.Panel bottomResizeBar;
        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Button fullScreenButton;
        private System.Windows.Forms.Button minimizeButton;
        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.Panel drawingSettingsBar;
        private System.Windows.Forms.Label zoomValueLabel;
        private System.Windows.Forms.PictureBox zoomMagnifierIcon;
        private System.Windows.Forms.PictureBox toolSizeIcon;
        private System.Windows.Forms.Label toolSizeLabel;
    }
}

