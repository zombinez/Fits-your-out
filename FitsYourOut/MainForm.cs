using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FitsYourOut
{
    public partial class MainForm : Form
    {
        private TextBox minPriceBox;
        private TextBox maxPriceBox;
        private double minPrice;
        private double maxPrice;
        private Timer timer;
        private Item[] collection;
        private ItemPage childForm;
        private List<Button> buttons;

        public MainForm()
        {
            InitializeComponent();
            minPrice = 0;
            maxPrice = 0;
            buttons = new List<Button>();
            collection = Algorythms.GetItemsCollection().Values.ToArray();

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(OnFrameChanged);
            timer.Enabled = true;

            Size = new Size(1280, 720);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            Paint += new PaintEventHandler(OnPaint);
            DoubleBuffered = true;
            MaximumSize = new Size(1280, 720);
            minPriceBox = new TextBox();
            minPriceBox.Location = new Point(60, 60);
            minPriceBox.Font = new Font(FontFamily.GenericSansSerif, 30F, FontStyle.Regular);
            minPriceBox.Width = (int)minPriceBox.Font.Size * 3;
            minPriceBox.BorderStyle = BorderStyle.None;
            minPriceBox.BackColor = Color.Gray;
            minPriceBox.KeyPress += new KeyPressEventHandler(Input);
            minPriceBox.KeyDown += new KeyEventHandler(DeleteChar);
            Controls.Add(minPriceBox);

            maxPriceBox = new TextBox();
            maxPriceBox.Location = new Point(minPriceBox.Location.X + minPriceBox.Width + 10, minPriceBox.Location.Y);
            maxPriceBox.Font = new Font(FontFamily.GenericSansSerif, 30F, FontStyle.Regular);
            maxPriceBox.Width = (int)minPriceBox.Font.Size * 3;
            maxPriceBox.BorderStyle = BorderStyle.None;
            maxPriceBox.BackColor = Color.Gray;
            maxPriceBox.KeyPress += new KeyPressEventHandler(Input);
            maxPriceBox.KeyDown += new KeyEventHandler(DeleteChar);
            Controls.Add(maxPriceBox);

            //Вместо кнопки сделаешь picturebox
            var closeButton = new Button();
            closeButton.Location = new Point(0, 60);
            closeButton.Size = new Size(48, 48);
            closeButton.Text = "Закрыть";
            closeButton.Click += new EventHandler((sender, args) => Close());
            closeButton.BackColor = Color.Transparent;
            Controls.Add(closeButton);

            AddButtons();
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            if(int.TryParse(minPriceBox.Text, out int newMinPrice) && newMinPrice != minPrice)
            {
                minPrice = newMinPrice;
                collection = Algorythms.GetItemsByPrice(minPrice, maxPrice == 0 ? double.MaxValue : maxPrice, Algorythms.GetItemsCollection().Values);
                RemoveButtons();
                AddButtons();
            }
            else if (minPriceBox.Text == "" && newMinPrice != minPrice)
            {
                minPrice = 0;
                collection = Algorythms.GetItemsByPrice(-1, maxPrice == 0 ? double.MaxValue : maxPrice, Algorythms.GetItemsCollection().Values);
                RemoveButtons();
                AddButtons();
            }

            if (int.TryParse(maxPriceBox.Text, out int newMaxPrice) && newMaxPrice != maxPrice)
            {
                maxPrice = newMaxPrice;
                collection = Algorythms.GetItemsByPrice(minPrice, maxPrice == 0 ? double.MaxValue : maxPrice, Algorythms.GetItemsCollection().Values);
                RemoveButtons();
                AddButtons();
            }
            else if(maxPriceBox.Text == "" && newMaxPrice != maxPrice)
            {
                maxPrice = 0;
                collection = Algorythms.GetItemsByPrice(minPrice, maxPrice == 0 ? double.MaxValue : maxPrice, Algorythms.GetItemsCollection().Values);
                RemoveButtons();
                AddButtons();
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
            var b = new SolidBrush(Color.Black);
            e.Graphics.DrawString(minPrice.ToString(), Font, b, 0, 0);
            e.Graphics.DrawString(maxPrice.ToString(), Font, b, 0, Font.Height + 5);
            e.Graphics.DrawString(collection.Length.ToString(), Font, b, 0, (Font.Height + 5) * 2);
        }

        public void AddButtons()
        {
            for (var i = 0; i < collection.Length; i++)
            {
                //Сделаешь через PictureBox
                var button = new Button();
                button.Location = new Point(1100, 60 + 45 * i);
                button.Size = new Size(120, 40);
                button.Text = collection[i].Name;
                button.Name = collection[i].Name;
                button.Click += new EventHandler((sender, args) =>
                {
                    //У каждой карточки по нажатию должно происходить описанное ниже, но в форму передается предмет, который представляет собой эта карточка
                    childForm = new ItemPage(collection.Where(item => item.Name == button.Name).First(), maxPrice, minPrice);
                    childForm.ShowDialog();
                });
                buttons.Add(button);
                Controls.Add(button);
            }
            Invalidate();
        }

        public void RemoveButtons()
        {
            foreach (var button in buttons)
                Controls.Remove(button);
            buttons.Clear();
        }
    }
}
