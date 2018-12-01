using System;
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
            logLabel.Name = "logLabel";
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
            pictureBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(p);
            pictureBox.Image = pictureBox.Image;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
            return pictureBox;
        }

        public List<PictureBox> createArrows()
        {
            List<PictureBox> arrows = new List<PictureBox>();
            PictureBox headArrowNext = new CreateObjects().createArrow("nextHead", "arrowRight", new System.Drawing.Point(290, 140));
            PictureBox bodyArrowNext = new CreateObjects().createArrow("nextBody", "arrowRight", new System.Drawing.Point(290, 250));
            PictureBox legsArrowNext = new CreateObjects().createArrow("nextLegs", "arrowRight", new System.Drawing.Point(290, 340));
            PictureBox headArrowPrev = new CreateObjects().createArrow("prevHead", "arrowLeft", new System.Drawing.Point(100, 140));
            PictureBox bodyArrowPrev = new CreateObjects().createArrow("prevBody", "arrowLeft", new System.Drawing.Point(100, 250));
            PictureBox legsArrowPrev = new CreateObjects().createArrow("prevLegs", "arrowLeft", new System.Drawing.Point(100, 340));

            arrows.Add(headArrowNext);
            arrows.Add(bodyArrowNext);
            arrows.Add(legsArrowNext);
            arrows.Add(headArrowPrev);
            arrows.Add(bodyArrowPrev);
            arrows.Add(legsArrowPrev);
            return arrows;
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
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Name = "panelMenu";
            panel.Dock = DockStyle.Right;
            panel.Location = location;
            panel.LocationChanged += Panel_LocationChanged;

            Label labelLogin = createLabel("UserLogin", login, new Point(80, 30));
            Button buttonListFriends = createButton("ListFriends", "Список друзей", new Point(50, 60));
            Button buttoтFindFriends = createButton("FindFriends", "Поиск друзей", new Point(50, 85));
            Button buttonExit = createButton("Exit", "Выход", new Point(50, 115));
            panel.Controls.Add(labelLogin);
            panel.Controls.Add(buttonListFriends);
            panel.Controls.Add(buttoтFindFriends);
            panel.Controls.Add(buttonExit);

            buttoтFindFriends.Click += ButtoтFindFriends_Click;

            return panel;
        }

        GroupBox groupBoxUsers = new GroupBox();
        private void ButtoтFindFriends_Click(object sender, System.EventArgs e)
        {
            Form form = (Form)panel.GetContainerControl();
            try
            {
                form.Controls.Find("groupBoxUsers", true).GetValue(0);
                form.Controls.RemoveByKey("groupBoxUsers");
            }
            catch(Exception ex)
            {
                groupBoxUsers = createUsersGroupBox();
                form.Controls.Add(groupBoxUsers);
                createListUsers(new ListUsers().getListUsers());
            }
        }

        public GroupBox createUsersGroupBox()
        {
            GroupBox groupUsers = new GroupBox();
            groupUsers.Name = "groupBoxUsers";
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

                groupBoxUsers.Controls.Add(nameUser);
                groupBoxUsers.Controls.Add(buttonAdd);
                y += 30;
            }
        }

        private void Panel_LocationChanged(object sender, System.EventArgs e)
        {
            groupBoxUsers.Location = new Point(panel.Location.X - groupBoxUsers.Size.Width, 0);
        }
    }
}