using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Forms;

namespace FreePad
{
    public partial class MiniForm
    {
        public const int dockingPointOffset = 20;

        public bool beingMoved = false;
        public bool docked = true;
        public Point dockingPointRelative;
        public MainForm mf;

        public MetroForm metro;

        public bool isDeactivated = false;

        public MiniForm (MainForm givenMainForm, Point givenDockingPointRelative, MetroForm givenMetro)
        {
            mf = givenMainForm;
            dockingPointRelative = givenDockingPointRelative;
            metro = givenMetro;
        }



        public void MiniForm_ResizeBegin(object sender, EventArgs e)
        {
            beingMoved = true;
        }

        public void MiniForm_ResizeEnd(object sender, EventArgs e)
        {
            beingMoved = false;
            if (metro.Location == new Point(mf.Location.X + dockingPointRelative.X, mf.Location.Y + dockingPointRelative.Y)) { docked = true; }
            else { docked = false; }
        }

        public void MiniForm_Move(object sender, EventArgs e)
        {
            if (beingMoved)
            {
                Rectangle dropArea = new Rectangle(
                    mf.Location.X + dockingPointRelative.X - dockingPointOffset, 
                    mf.Location.Y + dockingPointRelative.Y - dockingPointOffset, 
                    dockingPointOffset*2, dockingPointOffset*2);
                if (dropArea.Contains(metro.Location)) { metro.Location = new Point(mf.Location.X + dockingPointRelative.X, mf.Location.Y + dockingPointRelative.Y); }
            }
        }


        public void MiniForm_Deactivated(object sender, EventArgs e)
        { this.isDeactivated = true; }

        public void MiniForm_Activated(object sender, EventArgs e)
        { this.isDeactivated = false; }
    }
}
