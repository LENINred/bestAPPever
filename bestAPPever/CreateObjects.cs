using System.Drawing;
using System.Windows.Forms;

namespace bestAPPever
{
    class CreateObjects
    {
        //Создание текстовых полей
        public TextBox createTextBox(string name, System.Drawing.Point point)
        {
            TextBox textBox = new TextBox();
            textBox.Name = "textBox" + name;
            textBox.Location = point;
            textBox.Size = new System.Drawing.Size(100, 12);
            textBox.MaxLength = 16;
            return textBox;
        }

        public TextBox createTextBox(string name, System.Drawing.Point point, bool hide)
        {
            TextBox textBox = new TextBox();
            textBox.Name = "textBox" + name;
            textBox.Location = point;
            textBox.Size = new System.Drawing.Size(100, 12);
            textBox.UseSystemPasswordChar = hide;
            textBox.MaxLength = 16;
            return textBox;
        }

        //Создание кнопок
        public Button createButton(string name, System.Drawing.Point point)
        {
            Button button = new Button();
            button.Name = "button" + name;
            button.Text = name;
            button.Location = point;
            button.Size = new System.Drawing.Size(100, 23);
            return button;
        }

        public PictureBox createArrow(string name, System.Drawing.Point point)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Name = name;
            pictureBox.Size = new Size(40, 15);
            pictureBox.Location = point;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Image = new Bitmap(40, 15);
            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
                g.DrawString(">>>", new Font("Arial", 12f), Brushes.Black, new Point(0, 0));
                g.Save();
            }
            pictureBox.Image = pictureBox.Image;
            return pictureBox;
        }

        //Создание меток
        public Label createLabel(string name, string text, System.Drawing.Point point)
        {
            Label label = new Label();
            label.Location = point;
            label.Name = "label"+name;
            label.AutoSize = true;
            label.Text = text;
            return label;
        }

        public Label createLabel(string name, string text, System.Drawing.Point point, System.Drawing.Size size)
        {
            Label label = new Label();
            label.Location = point;
            label.Name = "label" + name;
            label.AutoSize = true;
            label.Size = size;
            label.Text = text;
            return label;
        }

        public ComboBox createComboBox(string name, System.Drawing.Point point)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Location = point;
            comboBox.Name = "comboBox" + name;
            comboBox.Text = "Пол";
            comboBox.Size = new System.Drawing.Size(100, 12);
            comboBox.Items.Add("Мужской");
            comboBox.Items.Add("Женский");
            return comboBox;
        }
    }
}