using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreePad
{
    public partial class ColorWheelWindow : Form
    {
        public Point mouseLocation;

        const int MoveZoneSize = 16;
        const int ReSizeThreshold = 4;

        private Rectangle scrnBounds;
        private Rectangle moveZone;

        // for use when making the Form movable
        private int mdx, mdy, dx, dy;

        private bool mouseIsUp = true;

        public ColorWheelWindow()
        {
            InitializeComponent();
        }

        private void UpdateScreenSize()
        {
            moveZone = new Rectangle(0, 0, MoveZoneSize, MoveZoneSize);
            scrnBounds = this.DisplayRectangle;
            moveZone.Offset(scrnBounds.Right - MoveZoneSize, scrnBounds.Bottom - MoveZoneSize);
        }

        private void ColorWheelTopBar_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void ColorWheelTopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePosition = Control.MousePosition;
                mousePosition.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePosition;
            }
        }

        private void ColorWheelWindow_Load(object sender, EventArgs e)
        {
            UpdateScreenSize();
        }



        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (moveZone.Contains(e.Location))
            {
                mdx = e.X;
                mdy = e.Y;

                this.MouseMove += Form1_MouseMove;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.MouseMove -= Form1_MouseMove;
            UpdateScreenSize();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            dx = Math.Abs(e.X - mdx);
            dy = Math.Abs(e.Y - mdy);

            if (dx < ReSizeThreshold && dy < ReSizeThreshold) return;

            this.Width = e.X;
            this.Height = e.Y;
        }
    }
}
