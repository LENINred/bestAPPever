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
            textBox.KeyPress += TextBox_KeyPress;
            textBox.ContextMenu = new ContextMenu();
            return textBox;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string symbols = "\"`'~!@#$%^&*()+\\№;%:?*-=.,/| ";
            if ((symbols.Contains(e.KeyChar.ToString())) || (e.KeyChar == '\u0016'))
            {
                e.Handled = true;
            }
        }

        public TextBox createTextBox(string name, System.Drawing.Point point, bool hide)
        {
            TextBox textBox = new TextBox();
            textBox.Name = "textBox" + name;
            textBox.Location = point;
            textBox.Size = new System.Drawing.Size(100, 12);
            textBox.UseSystemPasswordChar = hide;
            textBox.KeyPress += TextBox_KeyPress1;
            textBox.ContextMenu = new ContextMenu();
            return textBox;
        }

        private void TextBox_KeyPress1(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar.ToString() == " ") || (e.KeyChar == '\u0016'))
            {
                e.Handled = true;
            }
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


        //Создание меток
        public Label createLabel(string name, string text, System.Drawing.Point point)
        {
            Label label = new Label();
            label.Location = new System.Drawing.Point(12, 110);
            label.Name = "label"+name;
            label.AutoSize = true;
            label.Text = text;
            return label;
        }

        public Label createLabel(string name, string text, System.Drawing.Point point, System.Drawing.Size size)
        {
            Label label = new Label();
            label.Location = new System.Drawing.Point(12, 110);
            label.Name = "label" + name;
            label.AutoSize = true;
            label.Size = size;
            label.Text = text;
            return label;
        }

        public ComboBox createComboBox(string name, string text, System.Drawing.Point point)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Location = new System.Drawing.Point(12, 110);
            comboBox.Name = "comboBox" + name;
            comboBox.Text = text;
            comboBox.Items.Add("Мужской");
            comboBox.Items.Add("Женский");
            return comboBox;
        }
    }
}