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
        public MiniForm mini;

        public Tools(MainForm givenMainForm, Point dockingPointRelative)
        {
            InitializeComponent();
            this.mini = new MiniForm(givenMainForm, dockingPointRelative, this);
        }

        private void Tools_ResizeBegin(object sender, EventArgs e)
        {
            mini.MiniForm_ResizeBegin(sender, e);
        }

        private void Tools_ResizeEnd(object sender, EventArgs e)
        {
            mini.MiniForm_ResizeEnd(sender, e);
        }

        private void Tools_Move(object sender, EventArgs e)
        {
            mini.MiniForm_Move(sender, e);
        }




        public void load()
        {
            /*mini.mf.debug("loaded tools");
            this.TopMost = true;
            this.Show();*/
            if (mini.docked) { this.Location = new Point(mini.mf.Location.X + 12, mini.mf.Location.Y + 30); }
        }

        private void Tools_Activated(object sender, EventArgs e)
        {
            mini.MiniForm_Activated(sender, e);
        }

        private void Tools_Deactivate(object sender, EventArgs e)
        {
            mini.MiniForm_Deactivated(sender, e);
        }
    }
}
