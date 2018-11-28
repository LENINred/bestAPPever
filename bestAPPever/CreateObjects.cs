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
        public Button createButton(string name, string text, System.Drawing.Point point)
        {
            Button button = new Button();
            button.Name = "button" + name;
            button.Text = text;
            button.Location = point;
            button.Size = new System.Drawing.Size(100, 23);
            return button;
        }

        string p = "";
        public PictureBox createArrow(string name, string path, System.Drawing.Point point)
        {
            p = path;
            PictureBox pictureBox = new PictureBox();
            pictureBox.Name = name;
            pictureBox.Size = new Size(30, 20);
            pictureBox.Location = point;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(p);
            pictureBox.Image = pictureBox.Image;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
            return pictureBox;
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            ((PictureBox)sender).Image = (Image)Properties.Resources.ResourceManager.GetObject(p);
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            ((PictureBox)sender).Image = (Image)Properties.Resources.ResourceManager.GetObject(p + "Tap");
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
            comboBox.Items.Add("Женский");
            comboBox.Items.Add("Мужской");
            return comboBox;
        }
    }
}