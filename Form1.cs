using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

using Microsoft.Ink;

namespace FreePad
{
    public partial class baseWindow : Form
    {
        public Point mouseLocation;
        public Point mouseStartPosition;
        public Point originalLocation;

        private InkOverlay inkCollector = null;

        public float currentZoom = 100;
        Point viewportLocation = new Point(0,0);


        int originalWidth, originalHeight;

        bool panning = false;

        Color colorNoV = Color.Black;

        const int mapsWidth = 720;
        const int mapsHeight = 405;
        //LinkedList<ManagedBitmap> managedMaps = new LinkedList<ManagedBitmap>();
        Dictionary<Point, ManagedBitmap> managedMaps = new Dictionary<Point, ManagedBitmap>();

        Task dryInk;





        public baseWindow()
        {
            InitializeComponent();
        }


        #region Color Wheel

        public void updatePenColor()
        {
            inkCollector.DefaultDrawingAttributes.Color = colorNoV;
            this.currentColorPanel.BackColor = colorNoV;
        }


        public Color colorFromWheel(MouseEventArgs e)
        {
            float slope = 730f / (float)this.colorWheel.Height;
            int x = (int)(e.X * slope);
            int y = (int)(e.Y * slope);
            if (x > 1 && x < 729 && y > 1 && y < 729)
            {
                Color newColor = ((Bitmap)this.colorWheel.Image).GetPixel(x, y);
                if (newColor.A != 0)
                {
                    return newColor;
                }
            }
            return Color.FromArgb(0, 0, 0, 0);
        }

        private void ColorWheel_MouseDown(object sender, MouseEventArgs e)
        {
            if (colorFromWheel(e) != Color.FromArgb(0, 0, 0, 0))
            {
                colorNoV = colorFromWheel(e);
                updatePenColor();
            }
        }

        private void ColorWheel_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons == MouseButtons.Left)
            {
                if (colorFromWheel(e) != Color.FromArgb(0, 0, 0, 0))
                {
                    colorNoV = colorFromWheel(e);
                    updatePenColor();
                }
            }
        }

        #endregion





        #region Ink Controlling


        private void inkCollector_NewPackets(object sender, InkCollectorNewPacketsEventArgs e)
        {
            
        }

        private void inkCollector_Stroke(object sender, InkCollectorStrokeEventArgs e)
        {
            if (dryInk != null)   {   dryInk.Wait();   }
            
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
                            mbmp = new ManagedBitmap(getGridX(sp2.X)*mapsWidth, getGridY(sp2.Y)*mapsHeight, new Bitmap(mapsWidth, mapsHeight));
                            mbmp.g.DrawRectangle(Pens.LightGray, 0, 0, mapsWidth - 1, mapsHeight - 1);
                            mbmp.g.DrawString("(" + getGridX(sp2.X) + ", " + getGridY(sp2.Y) + ")", this.Font, Brushes.Black, 10f, 10f);
                            managedMaps.Add(new Point(getGridX(sp2.X), getGridY(sp2.Y)), mbmp);
                        }
                        else   {   mbmp = managedMaps[new Point(getGridX(sp2.X), getGridY(sp2.Y))];   }
                        if (!mapsToDry.Contains(mbmp)) { mapsToDry.AddLast(mbmp); }
                    }
                }

                foreach(ManagedBitmap mbmp in mapsToDry)
                {
                    //Point gridLocation = managedMaps.FirstOrDefault(x => x.Value == mbmp).Key;
                    //System.Diagnostics.Debug.WriteLine(gridLocation.ToString());

                    Point mbmpP1 = new Point(mbmp.x + viewportLocation.X, mbmp.y + viewportLocation.Y);

                    using (Graphics g = CreateGraphics())
                    {  inkCollector.Renderer.PixelToInkSpace(g, ref mbmpP1);  }
                    Matrix m, m2;
                    m = m2 = new Matrix();
                    inkCollector.Renderer.GetViewTransform(ref m);
                    inkCollector.Renderer.GetViewTransform(ref m2);
                    m.Translate(-mbmpP1.X, -mbmpP1.Y);
                    inkCollector.Renderer.SetViewTransform(m);
                    inkCollector.Renderer.Draw(mbmp.map, strokesToDry);
                    inkCollector.Renderer.SetViewTransform(m2);

                    this.drawingArea.CreateGraphics().DrawImage(mbmp.map, mbmp.x - viewportLocation.X, mbmp.y - viewportLocation.Y);

                    mbmp.map.MakeTransparent(this.drawingArea.BackColor);
                }

                inkCollector.Renderer.SetViewTransform(originalMatrix);

                e.Cancel = true;
                if (strokesToDry.Count != 0) { inkCollector.Ink.DeleteStrokes(strokesToDry); }

                return 0;
            });
        }

        public int getGridX(int x)
        { return (int)((x - (x % mapsWidth))/mapsWidth); }
        public int getGridY(int y)
        { return (int)((y - (y % mapsHeight))/mapsHeight); }

        private void DrawingArea_Paint(object sender, PaintEventArgs e)
        {
            Rectangle viewportRect = new Rectangle(viewportLocation, new Size(this.drawingArea.Width, this.drawingArea.Height));
            foreach (ManagedBitmap mbmp in managedMaps.Values)
            {
                if (viewportRect.IntersectsWith(mbmp.rectMap))   { e.Graphics.DrawImage(mbmp.map, mbmp.x - viewportLocation.X, mbmp.y - viewportLocation.Y); }
            }
        }

        private void Ink_Load(object sender, System.EventArgs e)
        {
            inkCollector = new InkOverlay(this.drawingArea.Handle);
            inkCollector.AutoRedraw = false;

            inkCollector.DefaultDrawingAttributes.Width = 100;

            inkCollector.NewPackets += new InkCollectorNewPacketsEventHandler(inkCollector_NewPackets);
            inkCollector.Stroke += new InkCollectorStrokeEventHandler(inkCollector_Stroke);

            ManagedBitmap mbmp = new ManagedBitmap(0, 0, new Bitmap(mapsWidth, mapsHeight));
            mbmp.g.DrawRectangle(Pens.LightGray, 0, 0, mapsWidth - 1, mapsHeight - 1);
            mbmp.g.DrawString("(" + getGridX(0) + ", " + getGridY(0) + ")", this.Font, Brushes.Black, 10f, 10f);
            managedMaps.Add(new Point(0, 0), mbmp);

            inkCollector.Enabled = true;
        }

        private void BrushSize_ValueChanged(object sender, EventArgs e)
        { /*inkCollector.DefaultDrawingAttributes.Width = (float)this.brushSize.Value;*/
                }

                public void updateBrushSize(float newBrushSize)
        {
            newBrushSize = Math.Max(Math.Min(newBrushSize, 1000), 1);
            this.toolSizeLabel.Text = ""+newBrushSize;
            inkCollector.DefaultDrawingAttributes.Width = newBrushSize;
        }

        public float changeZoomLevel(float newZoom)
        {
            currentZoom = Math.Max(Math.Min(newZoom, 500), 10);
            this.zoomValueLabel.Text = currentZoom+"%";
            return currentZoom;
        }

        private void DrawingArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (!this.drawingArea.Focused) { this.drawingArea.Focus(); }
            if (panning)
            {
                mouseStartPosition = new Point(e.X, e.Y);
            }
        }

        private void DrawingArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (panning && (MouseButtons == MouseButtons.Left))
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    //################################################################################################ PLEASE WORK ON THIS!!! #########################################################################################################
                    int yDiffZoom = e.Y - mouseStartPosition.Y;
                    float newZoom = currentZoom - (yDiffZoom / 5f);
                    changeZoomLevel(newZoom);
                    Matrix m = new Matrix();

                    Point currentMousePositionBefore = new Point(e.X, e.Y);
                    Point currentMousePositionAfter = new Point(e.X, e.Y);

                    Graphics tempGraphics = CreateGraphics();
                    inkCollector.Renderer.PixelToInkSpace(tempGraphics, ref currentMousePositionBefore);

                    m.Scale(currentZoom / 100, currentZoom / 100);

                    inkCollector.Renderer.SetViewTransform(m);

                    inkCollector.Renderer.PixelToInkSpace(tempGraphics, ref currentMousePositionAfter);
                    tempGraphics.Dispose();

                    int xDiff = currentMousePositionAfter.X - currentMousePositionBefore.X;
                    int yDiff = currentMousePositionAfter.Y - currentMousePositionBefore.Y;

                    m.Translate(xDiff, yDiff);

                    inkCollector.Renderer.SetViewTransform(m);
                    Refresh();
                    mouseStartPosition = new Point(e.X, e.Y);
                }
                else
                {
                    Point currentMousePosition = new Point(e.X, e.Y);

                    int xDiff = currentMousePosition.X - mouseStartPosition.X;
                    int yDiff = currentMousePosition.Y - mouseStartPosition.Y;
                    int zoomMult = (int)(100f / currentZoom);

                    /*Matrix m = new Matrix();

                    inkCollector.Renderer.GetViewTransform(ref m);

                    m.Translate(xDiff * zoomMult, yDiff * zoomMult);

                    inkCollector.Renderer.SetViewTransform(m);*/

                    viewportLocation.Offset(-xDiff * zoomMult, -yDiff * zoomMult);

                    Refresh();

                    mouseStartPosition = currentMousePosition;
                }
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
            if (e.Delta > 0)    { updateBrushSize(inkCollector.DefaultDrawingAttributes.Width + 25); }
            else                { updateBrushSize(inkCollector.DefaultDrawingAttributes.Width - 25); }
        }

        #endregion







        #region Window Control Functions

        private void CloseButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void FullScreenButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            { this.WindowState = FormWindowState.Normal; }
            else { this.WindowState = FormWindowState.Maximized; }
            if (!this.drawingArea.Focused) { this.drawingArea.Focus(); }
        }

        private void MinimizeButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.drawingArea.Focused) { this.drawingArea.Focus(); }
            if (this.WindowState == FormWindowState.Minimized)
            { this.WindowState = FormWindowState.Normal; }
            else { this.WindowState = FormWindowState.Minimized; }
        }

        private void TopBar_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void TopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePosition = Control.MousePosition;
                mousePosition.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePosition;
            }
        }

        private void ResizeLeft_MouseDown(object sender, MouseEventArgs e)
        {
            originalWidth = this.Width;
            originalLocation = Location;
            mouseStartPosition = Control.MousePosition;
        }

        private void ResizeLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int xDiff = Control.MousePosition.X - mouseStartPosition.X;
                Location = new Point(originalLocation.X + xDiff, originalLocation.Y);
                this.Width = originalWidth - xDiff;
            }
        }

        private void ResizeRight_MouseDown(object sender, MouseEventArgs e)
        {
            originalWidth = this.Width;
            mouseStartPosition = Control.MousePosition;
        }

        private void ResizeRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int xDiff = Control.MousePosition.X - mouseStartPosition.X;
                this.Width = originalWidth + xDiff;
            }
        }

        private void ResizeBottom_MouseDown(object sender, MouseEventArgs e)
        {
            originalHeight = this.Height;
            mouseStartPosition = Control.MousePosition;
        }

        private void ResizeBottom_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int yDiff = Control.MousePosition.Y - mouseStartPosition.Y;
                this.Height = originalHeight + yDiff;
            }
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
                    updateBrushSize(inkCollector.DefaultDrawingAttributes.Width*5);
                    if (panning) { panning = !panning; }
                    break;

            };
            e.Handled = true;
        }

        private void DrawingArea_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                Matrix m = new Matrix();

                inkCollector.Renderer.GetViewTransform(ref m);

                m.Translate(e.Delta*6, 0);

                inkCollector.Renderer.SetViewTransform(m);

                Refresh();
            }
            else if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                bool up = false;
                Matrix m = new Matrix();

                Point currentMousePositionBefore = new Point(e.X, e.Y);
                Point currentMousePositionAfter = new Point(e.X, e.Y);

                Graphics tempGraphics = CreateGraphics();
                inkCollector.Renderer.PixelToInkSpace(tempGraphics, ref currentMousePositionBefore);

                if (e.Delta > 0) { up = !up; }
                if (up)   { changeZoomLevel(currentZoom + 20); }
                else      { changeZoomLevel(currentZoom - 20); }
                m.Scale(currentZoom/100, currentZoom / 100);

                inkCollector.Renderer.SetViewTransform(m);

                inkCollector.Renderer.PixelToInkSpace(tempGraphics, ref currentMousePositionAfter);
                tempGraphics.Dispose();

                int xDiff = currentMousePositionAfter.X - currentMousePositionBefore.X;
                int yDiff = currentMousePositionAfter.Y - currentMousePositionBefore.Y;

                m.Translate(xDiff, yDiff);

                inkCollector.Renderer.SetViewTransform(m);
                Refresh();

            }
            else
            {
                Matrix m = new Matrix();

                inkCollector.Renderer.GetViewTransform(ref m);

                m.Translate(0, e.Delta*6);

                inkCollector.Renderer.SetViewTransform(m);

                Refresh();
            }
        }

        #endregion
    }
}
