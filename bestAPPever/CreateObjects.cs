using System.Windows.Forms;

namespace bestAPPever
{
    class CreateObjects
    {
        public TextBox createTextBox(string name, System.Drawing.Point point)
        {
            TextBox textBox = new TextBox();
            textBox.Name = "textBox" + name;
            textBox.Location = point;
            textBox.Size = new System.Drawing.Size(100, 12);
            return textBox;
        }

        public Button createButton(string name, System.Drawing.Point point)
        {
            Button button = new Button();
            button.Name = "button" + name;
            button.Text = name;
            button.Location = point;
            button.Size = new System.Drawing.Size(100, 23);
            return button;
        }

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

        public Label createCheckBox(string name, string text, System.Drawing.Point point)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Location = new System.Drawing.Point(12, 110);
            checkBox.Name = "label" + name;
            checkBox.AutoSize = true;
            checkBox.Text = text;
            return checkBox;
        }
    }
}
