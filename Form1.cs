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

        private InkCollector inkCollector = null;

        public float currentZoom = 100;

        int originalWidth, originalHeight;

        bool panning = false;

        public baseWindow()
        {
            InitializeComponent();
        }

        #region Ink Controlling

        private void myInkCollector_NewPackets(object sender, InkCollectorNewPacketsEventArgs e)
        {
            //
        }


        private void InkZoom_Load(object sender, System.EventArgs e)
        {
            // Create the ink collector and associate it with the form
            inkCollector = new InkCollector(this.drawingArea.Handle);

            // Set the pen width
            inkCollector.DefaultDrawingAttributes.Width = 100;

            // Enable ink collection
            inkCollector.Enabled = true;
        }
        private void BrushSize_ValueChanged(object sender, EventArgs e)
        { /*inkCollector.DefaultDrawingAttributes.Width = (float)this.brushSize.Value;*/ }

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
            if (panning && (MouseButtons != MouseButtons.None))
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

                    Matrix m = new Matrix();

                    inkCollector.Renderer.GetViewTransform(ref m);

                    int zoomMult = (int)((100f / currentZoom) * 26.9f);
                    m.Translate(xDiff * zoomMult, yDiff * zoomMult);

                    inkCollector.Renderer.SetViewTransform(m);

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
                    inkCollector.Cursor = System.Windows.Forms.Cursors.SizeAll;
                    inkCollector.Enabled = false;
                    panning = true;
                    break;
                case ('b'):
                    inkCollector.Cursor = System.Windows.Forms.Cursors.Default;
                    inkCollector.Enabled = true;
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
