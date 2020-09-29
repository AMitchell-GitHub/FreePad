using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FreePad
{
    class ManagedBitmap
    {
        public int x, y;
        public Bitmap map;
        public Graphics g;
        public Rectangle rectMap;

        public ManagedBitmap(int givenX, int givenY, Bitmap givenMap) { this.x = givenX; this.y = givenY; this.map = givenMap; this.g = Graphics.FromImage(this.map); rectMap = new Rectangle(this.x, this.y, this.map.Width, this.map.Height); }

        public bool crossesMap (Rectangle rectStroke)
        {
            Rectangle rectMap = new Rectangle(x, y, map.Width, map.Height);
            return (rectStroke.IntersectsWith(rectMap));
        }
        private bool inBetweenInc(int val, int min, int max)
        { return (val >= min && val <= max); }

        public Rectangle currentRectMap(Point viewportLoc)
        {   return new Rectangle(this.x - viewportLoc.X, this.y - viewportLoc.Y, this.map.Width, this.map.Height);   }
    }
}
