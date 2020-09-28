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
using System.Runtime.InteropServices;

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

        Dictionary<Point, ManagedBitmap> managedMaps = new Dictionary<Point, ManagedBitmap>();

        Task dryInk;

        ColorWheelWindow colorWheelWindow = new ColorWheelWindow();

        Rectangle resizeWindowRectangleTop;
        Rectangle resizeWindowRectangleBottom;
        Rectangle resizeWindowRectangleLeft;
        Rectangle resizeWindowRectangleRight;
        Rectangle resizeWindowRectangleTopRight;
        Rectangle resizeWindowRectangleTopLeft;
        Rectangle resizeWindowRectangleBottomRight;
        Rectangle resizeWindowRectangleBottomLeft;





        public baseWindow()
        {
            //InitializeComponent();
            //updateResizeRectangle();
            FreePadBaseForm fpbf = new FreePadBaseForm();
        }



        #region Color Wheel

        private void ColorWheelWindowButton_Click(object sender, EventArgs e)
        {
            if (!colorWheelWindow.Visible)
            {   colorWheelWindow.Show();   }
            else
            {   colorWheelWindow.Hide();   }
        }

        public void updatePenColor()
        {
            inkCollector.DefaultDrawingAttributes.Color = colorNoV;
            colorWheelWindow.colorWheel.BackColor = colorNoV;
        }


        public Color colorFromWheel(MouseEventArgs e)
        {
            float slope = 730f / (float)colorWheelWindow.colorWheel.Height;
            int x = (int)(e.X * slope);
            int y = (int)(e.Y * slope);
            if (x > 1 && x < 729 && y > 1 && y < 729)
            {
                Color newColor = ((Bitmap)colorWheelWindow.colorWheel.Image).GetPixel(x, y);
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
                            mbmp = new ManagedBitmap(getGridX(sp2.X)*mapsWidth, getGridY(sp2.Y)*mapsHeight, new Bitmap(mapsWidth, mapsHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb));
                            //mbmp.g.DrawRectangle(Pens.LightGray, 0, 0, mapsWidth - 1, mapsHeight - 1);
                            //mbmp.g.DrawString("(" + getGridX(sp2.X) + ", " + getGridY(sp2.Y) + ")", this.Font, Brushes.Black, 10f, 10f);
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
        { return (int)((x - (x % mapsWidth))/mapsWidth); }
        public int getGridY(int y)
        { return (int)((y - (y % mapsHeight))/mapsHeight); }

        private void DrawingArea_Paint(object sender, PaintEventArgs e)
        {
            Rectangle viewportRect = new Rectangle(viewportLocation, new Size(this.drawingArea.Width, this.drawingArea.Height));
            foreach (ManagedBitmap mbmp in managedMaps.Values)
            {
                if (viewportRect.IntersectsWith(mbmp.rectMap))   { e.Graphics.DrawImage(mbmp.map, mbmp.x - viewportLocation.X, mbmp.y - viewportLocation.Y, mbmp.map.Width, mbmp.map.Height); }
            }
        }

        private void Ink_Load(object sender, System.EventArgs e)
        {
            this.MaximizedBounds = System.Windows.Forms.Screen.FromHandle(this.Handle).WorkingArea;
            inkCollector = new InkOverlay(this.drawingArea.Handle);
            inkCollector.AutoRedraw = false;

            inkCollector.DefaultDrawingAttributes.Width = 100;

            inkCollector.NewPackets += new InkCollectorNewPacketsEventHandler(inkCollector_NewPackets);
            inkCollector.Stroke += new InkCollectorStrokeEventHandler(inkCollector_Stroke);

            ManagedBitmap mbmp = new ManagedBitmap(0, 0, new Bitmap(mapsWidth, mapsHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb));
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

        private void BaseWindow_Resize(object sender, EventArgs e)
        {
            updateResizeRectangle();
        }

        private void updateResizeRectangle()
        {
            int X = Location.X, Y = Location.Y, W = Width, H = Height;

            this.resizeWindowRectangleTop =     new Rectangle(X+3, Y, W-89, 3);
            this.resizeWindowRectangleBottom =  new Rectangle(X+3, Y+H-3, W-6, 3);
            this.resizeWindowRectangleLeft =    new Rectangle(X, Y+3, 3, H-6);
            this.resizeWindowRectangleRight =   new Rectangle(X+W-3, Y+3, 3, H-6);

            this.resizeWindowRectangleTopLeft =         new Rectangle(X, Y, 3, 3);
            this.resizeWindowRectangleTopRight =        new Rectangle(X+W-3, Y, 3, 3);
            this.resizeWindowRectangleBottomRight =      new Rectangle(X, Y+H-3, 3, 3);
            this.resizeWindowRectangleBottomLeft =     new Rectangle(X+W-3, Y+H-3, 3, 3);
        }

        private void BaseWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                Point p = Control.MousePosition;

                if (!this.resizeWindowRectangleBottom.Contains(p) &&
                    !this.resizeWindowRectangleTop.Contains(p) &&
                    !this.resizeWindowRectangleRight.Contains(p) &&
                    !this.resizeWindowRectangleLeft.Contains(p) &&
                    !this.resizeWindowRectangleBottomRight.Contains(p) &&
                    !this.resizeWindowRectangleBottomLeft.Contains(p) &&
                    !this.resizeWindowRectangleTopRight.Contains(p) &&
                    !this.resizeWindowRectangleTopLeft.Contains(p) &&
                     this.Cursor != System.Windows.Forms.Cursors.Default)
                { this.Cursor = System.Windows.Forms.Cursors.Default; }

                else if ((this.resizeWindowRectangleTop.Contains(p) ||
                          this.resizeWindowRectangleBottom.Contains(p)) &&
                          this.Cursor != System.Windows.Forms.Cursors.SizeNS)
                { this.Cursor = System.Windows.Forms.Cursors.SizeNS; }

                else if ((this.resizeWindowRectangleLeft.Contains(p) ||
                          this.resizeWindowRectangleRight.Contains(p)) &&
                          this.Cursor != System.Windows.Forms.Cursors.SizeWE)
                { this.Cursor = System.Windows.Forms.Cursors.SizeWE; }

                else if ((this.resizeWindowRectangleTopLeft.Contains(p) ||
                          this.resizeWindowRectangleBottomLeft.Contains(p)) &&
                          this.Cursor != System.Windows.Forms.Cursors.SizeNWSE)
                { this.Cursor = System.Windows.Forms.Cursors.SizeNWSE; }

                else if ((this.resizeWindowRectangleTopRight.Contains(p) ||
                          this.resizeWindowRectangleBottomRight.Contains(p)) &&
                          this.Cursor != System.Windows.Forms.Cursors.SizeNESW)
                { this.Cursor = System.Windows.Forms.Cursors.SizeNESW; }
            }
            else if(e.Button == MouseButtons.Left)
            {
                Point p = Control.MousePosition;

                if (this.resizeWindowRectangleBottom.Contains(p))
                {
                    //mousePosition.Offset(p.X, p.Y);
                }
            }

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
