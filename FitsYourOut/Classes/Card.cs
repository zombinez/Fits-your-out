using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using System.Reflection;

namespace FitsYourOut.Classes
{
    public class Card : Control
    {
        public Image Image { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }

        public PrivateFontCollection Fonts { get; set; }
        

        public Card(PrivateFontCollection fonts)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;
            Fonts = fonts;
            Size = new Size(270, 391);
           
            
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            Graphics g = e.Graphics;


            g.DrawImage(Properties.Resources.card2, 0 , 0, 270, 391);
            g.DrawImage(Image, 47, 47, 176, 261);
            g.DrawString(ItemName, new Font(Fonts.Families[6], 12), new SolidBrush(Color.Black), 46, 312);
            g.DrawString($"{ItemPrice}$", new Font(Fonts.Families[0], 11), new SolidBrush(Color.Black), 46, 330);
            
        }

        
    }
}
