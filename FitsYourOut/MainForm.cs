using FitsYourOut.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FitsYourOut
{
    public partial class MainForm : ShadowForm
    {
        private readonly TextBox minPriceBox;
        private readonly TextBox maxPriceBox;
        private double minPrice;
        private double maxPrice;
        private Gender gender;
        private readonly Timer timer;
        private Item[] collection;
        private ItemPage childForm;
        private readonly List<Card> cards;
        public PrivateFontCollection FontCollection { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            gender = Gender.All;
            minPrice = 0;
            maxPrice = 0;
            collection = Algorythms.GetItemsCollection().Values.GetItemsByGender(gender);

            FontCollection = new PrivateFontCollection();
            FontsInitialize();

            cards = new List<Card>();

            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(OnFrameChanged);
            timer.Enabled = true;

            Size = new Size(1280, 720);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            Paint += new PaintEventHandler(OnPaint);
            DoubleBuffered = true;
            MaximumSize = new Size(1280, 720);

            minPriceBox = new TextBoxWithPlaceHolder("Min");
            minPriceBox.Location = new Point(499, 157);
            minPriceBox.Font = new Font(FontCollection.Families[6], 13f);
            minPriceBox.Width = (int)minPriceBox.Font.Size * 6;
            minPriceBox.BorderStyle = BorderStyle.None;
            minPriceBox.BackColor = Color.White;
            minPriceBox.KeyPress += new KeyPressEventHandler(Input);
            minPriceBox.KeyDown += new KeyEventHandler(DeleteChar);
            Controls.Add(minPriceBox);

            maxPriceBox = new TextBoxWithPlaceHolder("Max");
            maxPriceBox.Location = new Point(685, 157);
            maxPriceBox.Font = new Font(FontCollection.Families[6], 13f);
            maxPriceBox.Width = (int)minPriceBox.Font.Size * 6;
            maxPriceBox.BorderStyle = BorderStyle.None;
            maxPriceBox.BackColor = Color.White;
            maxPriceBox.KeyPress += new KeyPressEventHandler(Input);
            maxPriceBox.KeyDown += new KeyEventHandler(DeleteChar);
            Controls.Add(maxPriceBox);

            var filter = new PictureBox();
            filter.Image = Properties.Resources.window_filter;
            filter.Location = new Point(400, 60);
            filter.Size = new Size(470, 190);
            filter.Click += new EventHandler((sender, args) => 
            {
                gender = (Gender)(((int)gender + 1) % 3);
                RefreshItems();
            });
            Controls.Add(filter);

            //Вместо кнопки сделаешь picturebox
            var closeButton = new PictureBox();
            closeButton.Location = new Point(Size.Width - 100, Size.Height - 630);
            closeButton.Image = Properties.Resources.close_btn2;
            closeButton.Size = new Size(25, 25);
            closeButton.Click += new EventHandler((sender, args) => Close());
            closeButton.MouseMove += (s, e) => Cursor.Current = Cursors.Hand;
            Controls.Add(closeButton);
            
            AddCards();
            AutoScroll = true;
            MouseClick += (s,e) => ActiveControl = null;
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            if(double.TryParse(minPriceBox.Text, out double newMinPrice) && newMinPrice != minPrice)
            {
                minPrice = newMinPrice;
                RefreshItems();
            }
            else if (minPriceBox.Text == "" && newMinPrice != minPrice)
            {
                minPrice = 0;
                RefreshItems();
            }

            if (double.TryParse(maxPriceBox.Text, out double newMaxPrice) && newMaxPrice != maxPrice)
            {
                maxPrice = newMaxPrice;
                RefreshItems();
            }
            else if(maxPriceBox.Text == "" && newMaxPrice != maxPrice)
            {
                maxPrice = 0;
                RefreshItems();
            }
            Invalidate();
        }

        private void Input(object sender, KeyPressEventArgs e)
        {
            var symbol = e.KeyChar;
            if(!char.IsDigit(symbol))
            {
                e.Handled = true;
            }
        }

        private void DeleteChar(object sender, KeyEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (e.KeyCode == Keys.Back && textBox != null && textBox.Text.Length != 0)
            {
                textBox.ResetText();
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
        }

        public void AddCards()
        {
            for (var i = 0; i < collection.Length; i++)
            {
                var card = new Card(FontCollection);
                card.Location = new Point(70 + (card.Width + 16) * (i % 4), 250 + card.Height * (i/4));
                card.MouseMove += (s, e) => Cursor.Current = Cursors.Hand;
                card.Name = collection[i].Name;
                card.ItemName = collection[i].Name;
                card.Image = collection[i].Image;
                card.ItemPrice = collection[i].Price;
                card.Click += new EventHandler((sender, args) =>
                {
                    childForm = new ItemPage(collection.Where(item => item.Name == card.Name).First(), maxPrice, minPrice, FontCollection);
                    childForm.ShowDialog();
                });
                cards.Add(card);
                Controls.Add(card);
            }
            Invalidate();
        }

        public void RemoveCards()
        {
            foreach (var c in cards)
                Controls.Remove(c);
            cards.Clear();
        }

        public void FontsInitialize()
        {
            var curDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var resources = Path.Combine(curDir, "Resources");
            FontCollection.AddFontFile(Path.Combine(resources, "CircularStd-Black.ttf")); //0
            FontCollection.AddFontFile(Path.Combine(resources, "CircularStd-BlackItalic.ttf")); //1
            FontCollection.AddFontFile(Path.Combine(resources, "CircularStd-Bold.ttf")); //2
            FontCollection.AddFontFile(Path.Combine(resources, "CircularStd-Book.ttf")); //3
            FontCollection.AddFontFile(Path.Combine(resources, "CircularStd-BookItalic.ttf")); //4
            FontCollection.AddFontFile(Path.Combine(resources, "CircularStd-Medium.ttf")); //5
            FontCollection.AddFontFile(Path.Combine(resources, "CircularStd-MediumItalic.ttf")); //6
        }

        public void RefreshItems()
        {
            collection = Algorythms.GetItemsByPrice(minPrice, maxPrice == 0 ? double.MaxValue : maxPrice, Algorythms.GetItemsCollection().Values).GetItemsByGender(gender);
            RemoveCards();
            AddCards();
        }
    }
}
