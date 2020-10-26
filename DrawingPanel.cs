using MyScript.IInk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FreePad
{
    public class DrawingPanel : Panel, IRenderTarget
    {
        public DrawingPanel()
        {
            this.DoubleBuffered = true;
        }

        public void Invalidate(Renderer renderer, int x, int y, int width, int height, LayerType layers)
        {
            this.Invalidate();
        }

        public void Invalidate(Renderer renderer, LayerType layers)
        {
            this.Invalidate();
        }
    }
}
