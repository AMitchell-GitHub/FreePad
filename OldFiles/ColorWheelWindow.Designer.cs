using System.Drawing;

namespace FreePad
{
    partial class ColorWheelWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorWheelWindow));
            this.currentColorPanel = new System.Windows.Forms.Panel();
            this.colorWheelBack = new System.Windows.Forms.Panel();
            this.colorWheel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.colorWheel)).BeginInit();
            this.SuspendLayout();
            // 
            // currentColorPanel
            // 
            this.currentColorPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.currentColorPanel.BackColor = System.Drawing.Color.Black;
            this.currentColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentColorPanel.Location = new System.Drawing.Point(51, 187);
            this.currentColorPanel.Margin = new System.Windows.Forms.Padding(6);
            this.currentColorPanel.Name = "currentColorPanel";
            this.currentColorPanel.Size = new System.Drawing.Size(70, 23);
            this.currentColorPanel.TabIndex = 7;
            // 
            // colorWheelBack
            // 
            this.colorWheelBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.colorWheelBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.colorWheelBack.Location = new System.Drawing.Point(5, 22);
            this.colorWheelBack.Margin = new System.Windows.Forms.Padding(0);
            this.colorWheelBack.Name = "colorWheelBack";
            this.colorWheelBack.Size = new System.Drawing.Size(160, 203);
            this.colorWheelBack.TabIndex = 8;
            this.colorWheelBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorWheelTopBar_MouseDown);
            this.colorWheelBack.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorWheelTopBar_MouseMove);
            // 
            // colorWheel
            // 
            this.colorWheel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.colorWheel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.colorWheel.Image = ((System.Drawing.Image)(resources.GetObject("colorWheel.Image")));
            this.colorWheel.Location = new System.Drawing.Point(10, 27);
            this.colorWheel.Margin = new System.Windows.Forms.Padding(5);
            this.colorWheel.Name = "colorWheel";
            this.colorWheel.Size = new System.Drawing.Size(150, 150);
            this.colorWheel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.colorWheel.TabIndex = 6;
            this.colorWheel.TabStop = false;
            // 
            // ColorWheelWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(170, 230);
            this.Controls.Add(this.colorWheel);
            this.Controls.Add(this.currentColorPanel);
            this.Controls.Add(this.colorWheelBack);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ColorWheelWindow";
            this.ShowInTaskbar = false;
            this.Text = "ColorWheelForm";
            this.Load += new System.EventHandler(this.ColorWheelWindow_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.colorWheel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel currentColorPanel;
        private System.Windows.Forms.Panel colorWheelBack;
        public System.Windows.Forms.PictureBox colorWheel;
    }
}