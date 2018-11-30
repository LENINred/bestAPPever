using System.Collections.Generic;
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
            textBox.KeyPress += TextBox_KeyPress;
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
            textBox.KeyPress += TextBox_KeyPress;
            return textBox;
        }

        //Удаление по Ctrl+BackSpace
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\u007f')
            {
                ((TextBox)sender).Clear();
                e.Handled = true;
            }
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

        //Поле для вывода логов
        public StatusStrip createStatusStrip(string name, string text)
        {
            StatusStrip statusStrip = new StatusStrip();
            ToolStripLabel logLabel = new ToolStripLabel();
            logLabel.Text = text;
            statusStrip.Name = "statusStrip";
            statusStrip.SizingGrip = false;
            statusStrip.ForeColor = Color.Red;
            statusStrip.Items.Add(logLabel);
            return statusStrip;
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
        
        //Создание Меню
        Panel panel;
        public Panel createMenu(string login, Point location)
        {
            panel = new Panel();

            Label labelLogin = createLabel("UserLogin", login, new Point(80, 30));
            Button buttonListFriends = createButton("ListFriends", "Список друзей", new Point(50, 60));
            Button buttoтFindFriends = createButton("FindFriends", "Поиск друзей", new Point(50, 85));
            Button buttonExit = createButton("Exit", "Выход", new Point(50, 115));

            panel.Controls.Add(labelLogin);
            panel.Controls.Add(buttonListFriends);
            panel.Controls.Add(buttoтFindFriends);
            panel.Controls.Add(buttonExit);
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Name = "panelMenu";
            panel.Dock = DockStyle.Right;
            panel.Location = location;
            panel.LocationChanged += Panel_LocationChanged;

            buttoтFindFriends.Click += ButtoтFindFriends_Click;

            return panel;
        }

        private void Panel_LocationChanged(object sender, System.EventArgs e)
        {
            panelUsers.Location = new Point(panel.Location.X - panelUsers.Size.Width, 0);
        }
        
        public GroupBox createUsersGroupBox()
        {
            GroupBox groupUsers = new GroupBox();
            groupUsers.Name = "panelUsers";
            groupUsers.Text = "Поиск людей";
            groupUsers.Size = new Size(panel.Size.Width - 40, panel.Size.Height);
            groupUsers.Location = new Point(panel.Location.X - groupUsers.Size.Width, 0);
                        

            return groupUsers;
        }

        private void createListUsers(List<string> listUsers)
        {
            int y = 20;
            foreach (string user in listUsers)
            {
                Label nameUser = new Label();
                Button buttonAdd = new Button();

                nameUser.Text = user;
                nameUser.AutoSize = true;
                nameUser.Location = new Point(10, y + 5);

                buttonAdd.Size = new Size(30, 25);
                buttonAdd.Font = new Font("Arial", 10, FontStyle.Bold);
                buttonAdd.Text = "+";
                buttonAdd.Location = new Point(75, y);

                panelUsers.Controls.Add(nameUser);
                panelUsers.Controls.Add(buttonAdd);
                y += 30;
            }
        }

        GroupBox panelUsers = new GroupBox();
        private void ButtoтFindFriends_Click(object sender, System.EventArgs e)
        {
            Form form = (Form)panel.GetContainerControl();
            
            if(panelUsers.Name != "panelUsers")
            {
                panelUsers = createUsersGroupBox();
                form.Controls.Add(panelUsers);
                createListUsers(new ListUsers().getListUsers());
            }
            else
            {
                form.Controls.RemoveByKey("panelUsers");
                panelUsers.Name = "null";
            }
        }
    }
}