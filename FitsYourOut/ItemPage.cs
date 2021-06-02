using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        private readonly List<Button> buttons;

        public ItemPage(Item item, double mxPrice, double mnPrice)
        {
            InitializeComponent();
            maxPrice = mxPrice == 0 ? double.MaxValue : mxPrice;
            minPrice = mnPrice;
            selectedTags = new HashSet<Tags>();
            selectedItems = new Stack<Item>();
            buttons = new List<Button>();
            Refresh(item);

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(OnFrameChanged);
            timer.Enabled = true;

            Size = new Size(1280, 720);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            Paint += new PaintEventHandler(OnPaint);
            DoubleBuffered = true;
            MaximumSize = new Size(1280, 720);
            
            //Сделай picturebox
            var backButton = new Button();
            backButton.Location = new Point(60, 60);
            backButton.Size = new Size(40, 40);
            backButton.Text = "Вернуться";
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

        private void OnFrameChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            //Это все можешь удалить, это чисто для демонстрации того, что алгоритмы написаны правильно
            var b = new SolidBrush(Color.Black);
            e.Graphics.DrawString($"Минимальная цена: {minPrice}", Font, b, 0, 0);
            e.Graphics.DrawString($"Максимальная цена: {maxPrice}", Font, b, 0, Font.Height + 5);
            if(matchingItems.Length > 0)
            {
                e.Graphics.DrawString("Подобранные предметы:", Font, b, 0, (Font.Height + 5) * 2);
                for (var i = 0; i < matchingItems.Length; i++)
                    e.Graphics.DrawString(matchingItems[i].Name, Font, b, 0, (Font.Height + 5) * (3 + i));
            }
            else e.Graphics.DrawString("Подходящих предметов не найдено", Font, b, 0, (Font.Height + 5) * 2);
        }

        public void Refresh(Item item)
        {
            foreach (var button in buttons)
                Controls.Remove(button);
            buttons.Clear();
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
            for (var i = 0; i < matchingItems.Length; i++)
            {
                var button = new Button();
                button.Location = new Point(1100, 60 + 45 * i);
                button.Size = new Size(40, 40);
                button.Text = matchingItems[i].Name;
                button.Name = matchingItems[i].Name;
                button.Click += new EventHandler((sender, args) =>
                {
                    //У карточек подходящих предметов должно быть такое поведение, но передается предмет из карточки
                    selectedItems.Push(mainItem);
                    Refresh(matchingItems.Where(currentItem => currentItem.Name == button.Name).First());
                });
                buttons.Add(button);
                Controls.Add(button);
            }
            Invalidate();
        }
    }
}
