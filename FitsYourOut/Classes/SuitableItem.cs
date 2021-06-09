using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitsYourOut.Classes
{
    public class SuitableItem : Control
    {
        public Item Item;
        public SuitableItem(Item item)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Size = new Size(200, 300);
            Item = item;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            var g = e.Graphics;

            g.DrawImage(Properties.Resources.itemshadow, 0, 0, 200, 300);
            g.DrawImage(Item.Image, 15, 15, 167, 270);
        }
    }
}
