using FitsYourOut.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace FitsYourOut
{
    public partial class ItemPage : Form
    {
        private Item mainItem;
        private readonly Stack<Item> selectedItems;
        private readonly HashSet<Tags> selectedTags;
        private Item[] matchingItems;
        private readonly double maxPrice;
        private readonly double minPrice;
        private readonly Timer timer;
        private readonly List<SuitableItem> suitableItems;
        public PrivateFontCollection Fonts { get; set; }

        public readonly Font Bold;

        public ItemPage(Item item, double mxPrice, double mnPrice, PrivateFontCollection fontfamily)
        {
            InitializeComponent();
            maxPrice = mxPrice == 0 ? double.MaxValue : mxPrice;
            minPrice = mnPrice;
            selectedTags = new HashSet<Tags>();
            selectedItems = new Stack<Item>();
            suitableItems = new List<SuitableItem>();
            Refresh(item);

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(OnFrameChanged);
            timer.Enabled = true;

            //AutoScroll = true;
            Fonts = fontfamily;
            Bold = new Font(Fonts.Families[0], 48);
            Size = new Size(1280, 720);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            Paint += new PaintEventHandler(OnPaint);
            DoubleBuffered = true;
            MaximumSize = new Size(1280, 720);
            
            var backButton = new PictureBox();
            backButton.Location = new Point(56, 56);
            backButton.Image = Properties.Resources.close_btn;
            backButton.Size = new Size(backButton.Image.Width - 35, backButton.Image.Height);
            backButton.MouseMove += (s, e) => Cursor.Current = Cursors.Hand;
            backButton.MouseEnter += BackButton_MouseEnter;
            backButton.Click += new EventHandler((sender, args) => 
            {
                if(selectedItems.Count == 0)
                    Close();
                else
                {
                    Refresh(selectedItems.Pop());
                }
            });
            backButton.BackColor = Color.Transparent;
            Controls.Add(backButton);
            Invalidate();
        }

        private void BackButton_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            var g = e.Graphics;

            g.DrawImage(Properties.Resources.itemshadow, 20, 65, 405, 610);
            g.DrawImage(mainItem.Image, 55, 100, 335, 540);
            g.DrawString(mainItem.Name, new Font(Fonts.Families[0], 48), new SolidBrush(Color.Black), new Point(422, 110));
            g.DrawString(mainItem.Description, new Font(Fonts.Families[3], 12), new SolidBrush(Color.Black), new Point(432, 177));
            g.DrawString(mainItem.Articul, new Font(Fonts.Families[0], 10), new SolidBrush(Color.Gray), new Point(450 + mainItem.Name.Length * ((int)Bold.Size - 15), 151));
            g.DrawString($"{mainItem.Price}$", new Font(Fonts.Families[0], 25), new SolidBrush(Color.Black), new Point(1118, 128));
            g.DrawString("Suitable items", new Font(Fonts.Families[0], 14), new SolidBrush(Color.Black), new Point(755, 325));
        }

        public void Refresh(Item item)
        {
            foreach (var button in suitableItems)
                Controls.Remove(button);
            suitableItems.Clear();
            mainItem = item;
            foreach (var tag in item.Tags)
                if (!selectedTags.Contains(tag))
                    selectedTags.Add(tag);
            var items = Algorythms.GetItemsByPrice(minPrice, maxPrice, item.GetSuitableItems(Algorythms.GetItemsCollection().Values.Where(e => e != mainItem && !selectedItems.Contains(e)), selectedTags));
            if (items.Length < 3)
                matchingItems = items.Concat(Algorythms.GetItemsByPrice(minPrice, maxPrice,
                    Algorythms.GetSuitableItemsBySingleTag(item, Algorythms.GetItemsCollection().Values.Where(e => e != mainItem && !items.Contains(e) && !selectedItems.Contains(e)), selectedTags.First()))).ToArray();
            else
                matchingItems = items;
            for (var i = 0; i < 4; i++)
            {
                var suitableItem = new SuitableItem(matchingItems[i]);
                suitableItem.Location = new Point(422 + (suitableItem.Width) * i, 345);
                suitableItem.MouseMove += (s, e) => Cursor.Current = Cursors.Hand;
                suitableItem.Text = matchingItems[i].Name;
                suitableItem.Name = matchingItems[i].Name;

                suitableItem.Click += new EventHandler((sender, args) =>
                {
                    //У карточек подходящих предметов должно быть такое поведение, но передается предмет из карточки
                    selectedItems.Push(mainItem);
                    Refresh(suitableItem.Item);
                });
                suitableItems.Add(suitableItem);
                Controls.Add(suitableItem);
                
            }
            Invalidate();
        }
    }
}
