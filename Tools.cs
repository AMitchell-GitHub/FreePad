using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Syncfusion.WinForms.Controls;

namespace FreePad
{
    public partial class Tools : Syncfusion.Windows.Forms.MetroForm
    {
        bool beingMoved = false;
        public bool deactivated = false;
        public bool wasLoaded = false;
        public bool docked = true;
        public Point lastLocation;
        MainForm mf;

        public Tools(MainForm gmf)
        {
            InitializeComponent();
            mf = gmf;
        }

        private void Tools_ResizeBegin(object sender, EventArgs e)
        {
            beingMoved = true;
        }

        private void Tools_ResizeEnd(object sender, EventArgs e)
        {
            beingMoved = false;
            if (this.Location == new Point(mf.Location.X + 12, mf.Location.Y + 30)) { docked = true; }
            else { docked = false; }
        }

        private void Tools_Move(object sender, EventArgs e)
        {
            if (beingMoved)
            {
                Rectangle dropArea = new Rectangle(mf.Location.X-8, mf.Location.Y+20, 40, 40);
                if (dropArea.Contains(this.Location)) {   Location = new Point(mf.Location.X + 12, mf.Location.Y + 30);   }
            }
        }




        private void Tools_Deactivate(object sender, EventArgs e)
        {
            deactivated = true;
            this.TopMost = false;
        }

        private void Tools_Activated(object sender, EventArgs e)
        {  deactivated = false;  }

        public void load()
        {
            mf.debug("B");
            if (!this.TopMost) { this.TopMost = true; }
            this.Show();
            this.Location = this.lastLocation;
            if (this.docked)
            { this.Location = new Point(mf.Location.X + 12, mf.Location.Y + 30); }
            this.wasLoaded = true;
        }
    }
}
