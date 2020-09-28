﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing.Drawing2D;
using Microsoft.Ink;

namespace FreePad
{
    public partial class MainForm : Syncfusion.Windows.Forms.MetroForm
    {
        public Point mouseLocation;

        public bool deactivated = false;

        Point viewportLocation = new Point(0, 0);

        private InkOverlay inkCollector = null;

        const int mapsWidth = 720;
        const int mapsHeight = 405;

        Dictionary<Point, ManagedBitmap> managedMaps = new Dictionary<Point, ManagedBitmap>();

        bool panning = false;

        public float currentZoom = 100;

        Color colorNoV = Color.Black;

        Task dryInk;

        Tools tools;




        public MainForm()
        {
            InitializeComponent();
            tools = new Tools(this);
            tools.Show();
        }





        #region Ink Controlling

        private void inkCollector_Stroke(object sender, InkCollectorStrokeEventArgs e)
        {
            if (dryInk != null) { dryInk.Wait(); }

            dryInk = Task.Run(() =>
            {
                Matrix originalMatrix = new Matrix();
                inkCollector.Renderer.GetViewTransform(ref originalMatrix);
                Strokes strokesToDry = inkCollector.Ink.Strokes;


                Point p = viewportLocation;

                using (Matrix m = new Matrix())
                {
                    using (Graphics g = CreateGraphics())
                    { inkCollector.Renderer.PixelToInkSpace(g, ref p); g.Dispose(); }
                    m.Translate(p.X, p.Y);
                    inkCollector.Renderer.SetViewTransform(m);
                }

                LinkedList<ManagedBitmap> mapsToDry = new LinkedList<ManagedBitmap>();

                foreach (Stroke s in strokesToDry)
                {
                    for (int pi = 0; pi < s.GetPoints().Length; pi++)
                    {
                        Point sp2 = s.GetPoint(pi);
                        using (Graphics g = CreateGraphics()) { inkCollector.Renderer.InkSpaceToPixel(g, ref sp2); }
                        ManagedBitmap mbmp;
                        if (!managedMaps.ContainsKey(new Point(getGridX(sp2.X), getGridY(sp2.Y))))
                        {
                            mbmp = new ManagedBitmap(getGridX(sp2.X) * mapsWidth, getGridY(sp2.Y) * mapsHeight, new Bitmap(mapsWidth, mapsHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb));
                            //mbmp.g.DrawRectangle(Pens.LightGray, 0, 0, mapsWidth - 1, mapsHeight - 1);
                            //mbmp.g.DrawString("(" + getGridX(sp2.X) + ", " + getGridY(sp2.Y) + ")", this.Font, Brushes.Black, 10f, 10f);
                            managedMaps.Add(new Point(getGridX(sp2.X), getGridY(sp2.Y)), mbmp);
                        }
                        else { mbmp = managedMaps[new Point(getGridX(sp2.X), getGridY(sp2.Y))]; }
                        if (!mapsToDry.Contains(mbmp)) { mapsToDry.AddLast(mbmp); }
                    }
                }

                foreach (ManagedBitmap mbmp in mapsToDry)
                {
                    //Point gridLocation = managedMaps.FirstOrDefault(x => x.Value == mbmp).Key;
                    //System.Diagnostics.Debug.WriteLine(gridLocation.ToString());

                    Point mbmpP1 = new Point(mbmp.x + viewportLocation.X, mbmp.y + viewportLocation.Y);

                    using (Graphics g = CreateGraphics())
                    { inkCollector.Renderer.PixelToInkSpace(g, ref mbmpP1); }
                    Matrix m, m2;
                    m = m2 = new Matrix();
                    inkCollector.Renderer.GetViewTransform(ref m);
                    inkCollector.Renderer.GetViewTransform(ref m2);
                    m.Translate(-mbmpP1.X, -mbmpP1.Y);
                    inkCollector.Renderer.SetViewTransform(m);
                    inkCollector.Renderer.Draw(mbmp.map, strokesToDry);
                    inkCollector.Renderer.SetViewTransform(m2);

                    this.drawingArea.CreateGraphics().DrawImage(mbmp.map, mbmp.x - viewportLocation.X, mbmp.y - viewportLocation.Y, mbmp.map.Width, mbmp.map.Height);

                    mbmp.map.MakeTransparent(this.drawingArea.BackColor);
                }

                inkCollector.Renderer.SetViewTransform(originalMatrix);

                e.Cancel = true;
                if (strokesToDry.Count != 0) { inkCollector.Ink.DeleteStrokes(strokesToDry); }

                return 0;
            });
        }

        public int getGridX(int x)
        { return (int)((x - (x % mapsWidth)) / mapsWidth); }
        public int getGridY(int y)
        { return (int)((y - (y % mapsHeight)) / mapsHeight); }

        private void DrawingArea_Paint(object sender, PaintEventArgs e)
        {
            Rectangle viewportRect = new Rectangle(viewportLocation, new Size(this.drawingArea.Width, this.drawingArea.Height));
            foreach (ManagedBitmap mbmp in managedMaps.Values)
            {
                if (viewportRect.IntersectsWith(mbmp.rectMap)) { e.Graphics.DrawImage(mbmp.map, mbmp.x - viewportLocation.X, mbmp.y - viewportLocation.Y, mbmp.map.Width, mbmp.map.Height); }
            }
        }

        private void Ink_Load(object sender, System.EventArgs e)
        {
            this.MaximizedBounds = System.Windows.Forms.Screen.FromHandle(this.Handle).WorkingArea;
            inkCollector = new InkOverlay(this.drawingArea.Handle);
            inkCollector.AutoRedraw = false;

            inkCollector.DefaultDrawingAttributes.Width = 100;

            //inkCollector.NewPackets += new InkCollectorNewPacketsEventHandler(inkCollector_NewPackets);
            inkCollector.Stroke += new InkCollectorStrokeEventHandler(inkCollector_Stroke);

            ManagedBitmap mbmp = new ManagedBitmap(0, 0, new Bitmap(mapsWidth, mapsHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb));
            mbmp.g.DrawRectangle(Pens.LightGray, 0, 0, mapsWidth - 1, mapsHeight - 1);
            mbmp.g.DrawString("(" + getGridX(0) + ", " + getGridY(0) + ")", this.Font, Brushes.Black, 10f, 10f);
            managedMaps.Add(new Point(0, 0), mbmp);

            inkCollector.Enabled = true;
        }


        public void updateBrushSize(float newBrushSize)
        {
            //newBrushSize = Math.Max(Math.Min(newBrushSize, 1000), 1);
            //this.toolSizeLabel.Text = "" + newBrushSize;
            //inkCollector.DefaultDrawingAttributes.Width = newBrushSize;
        }

        public float changeZoomLevel(float newZoom)
        {
            //currentZoom = Math.Max(Math.Min(newZoom, 500), 10);
            //this.zoomValueLabel.Text = currentZoom + "%";
            return 0f;
        }

        private void DrawingArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.drawingArea.Focused) { this.drawingArea.Focus(); }
            if (panning)
            {
                mouseLocation = new Point(e.X, e.Y);
            }
        }

        private void DrawingArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (panning && (MouseButtons == MouseButtons.Left))
            {
                Point currentMousePosition = new Point(e.X, e.Y);

                int xDiff = currentMousePosition.X - mouseLocation.X;
                int yDiff = currentMousePosition.Y - mouseLocation.Y;
                int zoomMult = (int)(100f / currentZoom);

                viewportLocation.Offset(-xDiff * zoomMult, -yDiff * zoomMult);

                Refresh();

                mouseLocation = currentMousePosition;
            }
        }

        private void ZoomMagnifierIcon_MouseClick(object sender, MouseEventArgs e)
        {
            changeZoomLevel(100);
            Matrix m = new Matrix();
            m.Scale(currentZoom / 100, currentZoom / 100);
            inkCollector.Renderer.SetViewTransform(m);
            Refresh();
        }

        private void zoomValueLabel_MouseWheel(object sender, MouseEventArgs e)
        {
            bool up = false;
            if (e.Delta > 0) { up = !up; }
            if (up) { changeZoomLevel(currentZoom + 20); }
            else { changeZoomLevel(currentZoom - 20); }

            Matrix m = new Matrix();
            m.Scale(currentZoom / 100, currentZoom / 100);
            inkCollector.Renderer.SetViewTransform(m);
            Refresh();
        }

        private void ToolSizeIcon_MouseClick(object sender, MouseEventArgs e)
        { updateBrushSize(100); }

        private void ToolSizeLabel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) { updateBrushSize(inkCollector.DefaultDrawingAttributes.Width + 25); }
            else { updateBrushSize(inkCollector.DefaultDrawingAttributes.Width - 25); }
        }

        #endregion






        #region keyBinds

        private void keyPressed(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case ('['):
                    updateBrushSize(inkCollector.DefaultDrawingAttributes.Width - 25);
                    break;
                case (']'):
                    updateBrushSize(inkCollector.DefaultDrawingAttributes.Width + 25);
                    break;
                case ('{'):
                    updateBrushSize(inkCollector.DefaultDrawingAttributes.Width - 100);
                    break;
                case ('}'):
                    updateBrushSize(inkCollector.DefaultDrawingAttributes.Width + 100);
                    break;
                case ('h'):
                    if (dryInk != null) { dryInk.Wait(); }
                    inkCollector.Cursor = System.Windows.Forms.Cursors.SizeAll;
                    inkCollector.Enabled = false;
                    panning = true;
                    break;
                case ('b'):
                    inkCollector.Cursor = System.Windows.Forms.Cursors.Default;
                    inkCollector.Enabled = true;
                    inkCollector.DefaultDrawingAttributes.Color = colorNoV;
                    updateBrushSize(100);
                    if (panning) { panning = !panning; }
                    break;
                case ('e'):
                    inkCollector.Cursor = System.Windows.Forms.Cursors.Default;
                    inkCollector.Enabled = true;
                    inkCollector.DefaultDrawingAttributes.Color = this.drawingArea.BackColor;
                    updateBrushSize(inkCollector.DefaultDrawingAttributes.Width * 5);
                    if (panning) { panning = !panning; }
                    break;
                case ('k'):
                    inkCollector.DefaultDrawingAttributes.Color = Color.Black;
                    updateBrushSize(150);
                    break;
                case ((char)Keys.Escape):
                    managedMaps.Clear();
                    viewportLocation = new Point(0, 0);
                    ManagedBitmap mbmp = new ManagedBitmap(0, 0, new Bitmap(mapsWidth, mapsHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb));
                    mbmp.g.DrawRectangle(Pens.LightGray, 0, 0, mapsWidth - 1, mapsHeight - 1);
                    mbmp.g.DrawString("(" + getGridX(0) + ", " + getGridY(0) + ")", this.Font, Brushes.Black, 10f, 10f);
                    managedMaps.Add(new Point(0, 0), mbmp);
                    Refresh();
                    break;
            };
            e.Handled = true;
        }

        #endregion


        public void debug(String str)
        {
            System.Diagnostics.Debug.WriteLine(str);
        }



        

        private void MainForm_Move(object sender, EventArgs e)
        {
            if (tools.docked)
            {   tools.Location = new Point(this.Location.X + 12, this.Location.Y + 30);   }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {  tools.lastLocation = tools.Location; tools.TopMost = false; tools.WindowState = FormWindowState.Minimized;  }

            else if (WindowState == FormWindowState.Normal)
            {  tools.WindowState = FormWindowState.Normal;  tools.load(); MainForm_Move(sender, e);  }
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (tools.deactivated)
            {
                this.deactivated = true;
                tools.TopMost = false;
                tools.wasLoaded = false;
                debug("F");
            }
            debug("C");
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            this.deactivated = false;
            if (!tools.wasLoaded)
            {
                tools.load();
            }
            debug("A");
        }
    }
}