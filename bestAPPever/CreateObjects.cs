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
            label.Name = "label" + name;
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

        ////////Оповещение////////
        public GroupBox createNotification(string text, Control.ControlCollection collection)
        {
            int y = 385;
            string name = "groupBoxNotification";
            GroupBox groupBoxNotification = new GroupBox();
            groupBoxNotification.Text = "Оповещение";
            groupBoxNotification.Size = new Size(297, 63);

            Label labelNotification = new Label();
            labelNotification.Text = text;
            labelNotification.AutoSize = true;
            labelNotification.Location = new Point(20, 20);
            groupBoxNotification.Controls.Add(labelNotification);

            int t = 0;
            foreach(Control c in collection)
            {
                if (c.Name.Contains("groupBoxNotification"))
                {
                    t++;
                    y -= 75;
                }
            }
            if (t <= 2)
            {
                groupBoxNotification.Name = name + t;
                groupBoxNotification.Location = new Point(355, y);
            }
            else
            {
                t = 0;
                y = 385;
                groupBoxNotification.Name = name + 0;
                groupBoxNotification.Location = new Point(355, 385);
            }
            return groupBoxNotification;
        }
        ////////////////

        /////////////////////Создание Меню/////////////////////
        Panel panel;
        string Login;
        int User_id;
        public Panel createMenu(string login, int user_id, Point location)
        {
            User_id = user_id;
            Login = login;
            panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Name = "panelMenu";
            panel.Dock = DockStyle.Right;
            panel.Location = location;

            Label labelLogin = createLabel("UserLogin", login, new Point(80, 30));
            Button buttonListFriends = createButton("ListFriends", "Список друзей", new Point(50, 60));
            Button buttonFindUsers = createButton("FindFriends", "Поиск друзей", new Point(50, 85));
            Button buttonSettings = createButton("Settings", "Настройки", new Point(50, 110));
            Button buttonLogOut = createButton("Exit", "Выход", new Point(50, 135));
            panel.Controls.Add(labelLogin);
            panel.Controls.Add(buttonListFriends);
            panel.Controls.Add(buttonFindUsers);
            panel.Controls.Add(buttonSettings);
            panel.Controls.Add(buttonLogOut);

            buttonFindUsers.Click += ButtoтFindUsers_Click;
            buttonListFriends.Click += ButtonListFriends_Click;

            return panel;
        }

        public PictureBox createNotificatioDOT(Point point)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Name = "pictureDOT";
            pictureBox.Size = new Size(10, 10);
            pictureBox.Location = point;

            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics graphics = Graphics.FromImage(pictureBox.Image);
            Brush brush = new SolidBrush(Color.Red);
            graphics.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, pictureBox.Width, pictureBox.Height));

            return pictureBox;
        }

        //////////Панель всех людей//////////
        private void ButtoтFindUsers_Click(object sender, System.EventArgs e)
        {
            Form form = (Form)panel.GetContainerControl();
            try
            {
                form.Controls.Find("groupBoxUsers", true).GetValue(0);
                form.Controls.RemoveByKey("groupBoxUsers");
            }
            catch(Exception ex)
            {
                form.Controls.RemoveByKey("tabControlFriends");
                form.Controls.Add(createGroupBoxUsers());
            }
        }

        //Создание панели для списка пользователей
        public GroupBox createGroupBoxUsers()
        {
            GroupBox groupBoxUsers = new GroupBox();
            groupBoxUsers.Name = "groupBoxUsers";
            groupBoxUsers.Text = "Поиск людей";
            groupBoxUsers.Size = new Size(panel.Size.Width - 40, panel.Size.Height);
            groupBoxUsers.Location = new Point(panel.Location.X - groupBoxUsers.Size.Width, 0);
            createUsersList(new ListUsers().getListUsers(), groupBoxUsers);

            return groupBoxUsers;
        }

        //Формирование списка всех пользователей
        private void createUsersList(List<KeyValuePair<int, string>> listUsers, GroupBox groupBoxUsers)
        {
            List<KeyValuePair<int, string>> listNewFriends = new ListUsers().getListNewFriends(Login);
            List<KeyValuePair<int, string>> listNewFriendsOut = new ListUsers().getListNewFriendsOut(Login);
            listNewFriendsOut.AddRange(new ListUsers().getListFriends(Login));
            if (listNewFriendsOut.Count > 0)
                foreach (KeyValuePair<int, string> newFriendOut in listNewFriendsOut)
                    listUsers.Remove(newFriendOut);

            listUsers.Remove(new KeyValuePair<int, string>(User_id, Login));

            int y = 20;
            foreach (KeyValuePair<int, string> user in listUsers)
            {
                Label nameUser = new Label();
                Button buttonAdd = new Button();

                nameUser.Text = user.Value;
                nameUser.AutoSize = true;
                nameUser.Location = new Point(10, y + 5);

                buttonAdd.Size = new Size(30, 25);
                buttonAdd.Font = new Font("Arial", 10, FontStyle.Bold);
                buttonAdd.Text = "+";
                buttonAdd.Tag = user.Key + ";" + user.Value;

                if (listNewFriends.IndexOf(user) == -1)
                {
                    buttonAdd.Location = new Point(75, y);
                    buttonAdd.Click += ButtonAdd_Click;
                }
                else
                {
                    buttonAdd.Location = new Point(75, y);
                    buttonAdd.Click += ButtonConfirm_Click;

                    Button buttonReject = new Button();
                    buttonReject.Size = new Size(30, 25);
                    buttonReject.Font = new Font("Arial", 10, FontStyle.Bold);
                    buttonReject.Text = "x";
                    buttonReject.Location = new Point(115, y);
                    buttonReject.Tag = user.Key + ";" + user.Value;
                    buttonReject.Click += ButtonReject_Click;
                    
                    groupBoxUsers.Controls.Add(buttonReject);
                }

                groupBoxUsers.Controls.Add(nameUser);
                groupBoxUsers.Controls.Add(buttonAdd);
                y += 30;
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            int id = short.Parse(((Button)sender).Tag.ToString().Substring(0, ((Button)sender).Tag.ToString().IndexOf(';')));
            string name = ((Button)sender).Tag.ToString().Substring((((Button)sender).Tag.ToString().IndexOf(';') + 1), ((((Button)sender).Tag.ToString().Length) - ((Button)sender).Tag.ToString().IndexOf(';')) - 1);
            new ListUsers().addFriend(User_id, Login, id, name, 1);
        }
        ////////////////////

        //////////Панель друзей/заявок//////////
        TabControl tabControlFriends = new TabControl();
        private void ButtonListFriends_Click(object sender, EventArgs e)
        {
            Form form = (Form)panel.GetContainerControl();
            try
            {
                form.Controls.Find("tabControlFriends", true).GetValue(0);
                form.Controls.RemoveByKey("tabControlFriends");
            }
            catch (Exception ex)
            {
                form.Controls.RemoveByKey("groupBoxUsers");

                form.Controls.Add(createTabControlFriends());
            }
        }

        //Формирование вкладок друзей/заявок
        public TabControl createTabControlFriends()
        {
            TabControl tabControlFriends = new TabControl();
            tabControlFriends.Name = "tabControlFriends";

            TabPage tabFriends = new TabPage();
            tabFriends.Text = "Друзья";
            tabControlFriends.TabPages.Add(tabFriends);
            createFriendsList(new ListUsers().getListFriends(Login), tabFriends);

            List<KeyValuePair<int, string>> listNewFriends = new ListUsers().getListNewFriends(Login);
            if (listNewFriends.Count > 0)
            {
                TabPage tabNewFriends = new TabPage();
                tabNewFriends.Text = "Входящие заявки";
                tabControlFriends.TabPages.Add(tabNewFriends);
                createNewFriendsList(listNewFriends, tabNewFriends);
            }

            List<KeyValuePair<int, string>> listNewFriendsOut = new ListUsers().getListNewFriendsOut(Login);
            if (listNewFriendsOut.Count > 0)
            {
                TabPage tabNewFriendsOut = new TabPage();
                tabNewFriendsOut.Text = "Исходящие заявки";
                tabControlFriends.TabPages.Add(tabNewFriendsOut);
                createNewFriendsListOut(listNewFriendsOut, tabNewFriendsOut);
            }

            tabControlFriends.Size = new Size(panel.Size.Width - 40, panel.Size.Height);
            tabControlFriends.Location = new Point(panel.Location.X - tabControlFriends.Size.Width, 0);
                     
            return tabControlFriends;
        }

        //Формирование списка друзей
        private void createFriendsList(List<KeyValuePair<int, string>> listUsers, TabPage tabFriends)
        {
            int y = 20;
            foreach (KeyValuePair<int, string> user in listUsers)
            {
                Label nameUser = new Label();
                Button buttonRemove = new Button();

                nameUser.Text = user.Value;
                nameUser.AutoSize = true;
                nameUser.Location = new Point(10, y + 5);

                buttonRemove.Size = new Size(30, 25);
                buttonRemove.Font = new Font("Arial", 10, FontStyle.Bold);
                buttonRemove.Text = "x";
                buttonRemove.Location = new Point(75, y);
                buttonRemove.Tag = user.Key + ";" + user.Value;
                buttonRemove.Click += ButtonRemove_Click;

                tabFriends.Controls.Add(nameUser);
                tabFriends.Controls.Add(buttonRemove);
                y += 30;
            }
        }

        //Формирование списка входящих заявок
        private void createNewFriendsList(List<KeyValuePair<int, string>> listUsers, TabPage tabFriends)
        {
            int y = 20;
            foreach (KeyValuePair<int, string> user in listUsers)
            {
                Label nameUser = new Label();
                Button buttonConfirm = new Button();
                Button buttonReject = new Button();

                nameUser.Text = user.Value;
                nameUser.AutoSize = true;
                nameUser.Location = new Point(10, y + 5);

                buttonConfirm.Size = new Size(30, 25);
                buttonConfirm.Font = new Font("Arial", 10, FontStyle.Bold);
                buttonConfirm.Text = "+";
                buttonConfirm.Location = new Point(65, y);
                buttonConfirm.Tag = user.Key + ";" + user.Value;
                buttonConfirm.Click += ButtonConfirm_Click;

                buttonReject.Size = new Size(30, 25);
                buttonReject.Font = new Font("Arial", 10, FontStyle.Bold);
                buttonReject.Text = "x";
                buttonReject.Location = new Point(105, y);
                buttonReject.Tag = user.Key + ";" + user.Value;
                buttonReject.Click += ButtonReject_Click;

                tabFriends.Controls.Add(nameUser);
                tabFriends.Controls.Add(buttonConfirm);
                tabFriends.Controls.Add(buttonReject);
                y += 30;
            }
        }

        //Формирование списка исходящих заявок
        private void createNewFriendsListOut(List<KeyValuePair<int, string>> listUsers, TabPage tabFriends)
        {
            int y = 20;
            foreach (KeyValuePair<int, string> user in listUsers)
            {
                Label nameUser = new Label();
                Button buttonRejectOut = new Button();

                nameUser.Text = user.Value;
                nameUser.AutoSize = true;
                nameUser.Location = new Point(10, y + 5);

                buttonRejectOut.Size = new Size(30, 25);
                buttonRejectOut.Font = new Font("Arial", 10, FontStyle.Bold);
                buttonRejectOut.Text = "x";
                buttonRejectOut.Location = new Point(105, y);
                buttonRejectOut.Tag = user.Key + ";" + user.Value;
                buttonRejectOut.Click += ButtonRejectOut_Click;

                tabFriends.Controls.Add(nameUser);
                tabFriends.Controls.Add(buttonRejectOut);
                y += 30;
            }
        }

        private void ButtonRejectOut_Click(object sender, EventArgs e)
        {
            int id = short.Parse(((Button)sender).Tag.ToString().Substring(0, ((Button)sender).Tag.ToString().IndexOf(';')));
            string name = ((Button)sender).Tag.ToString().Substring((((Button)sender).Tag.ToString().IndexOf(';') + 1), ((((Button)sender).Tag.ToString().Length) - ((Button)sender).Tag.ToString().IndexOf(';')) - 1);
            new ListUsers().rejectFriendOut(id, User_id);
            ((Form)panel.GetContainerControl()).Controls.RemoveByKey("tabControlFriends");
            ((Form)panel.GetContainerControl()).Controls.Add(createTabControlFriends());
        }

        private void ButtonReject_Click(object sender, EventArgs e)
        {
            int id = short.Parse(((Button)sender).Tag.ToString().Substring(0, ((Button)sender).Tag.ToString().IndexOf(';')));
            string name = ((Button)sender).Tag.ToString().Substring((((Button)sender).Tag.ToString().IndexOf(';') + 1), ((((Button)sender).Tag.ToString().Length) - ((Button)sender).Tag.ToString().IndexOf(';')) - 1);
            new ListUsers().rejectFriend(User_id, id);
            ((Form)panel.GetContainerControl()).Controls.RemoveByKey("tabControlFriends");
            ((Form)panel.GetContainerControl()).Controls.Add(createTabControlFriends());
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            int id = short.Parse(((Button)sender).Tag.ToString().Substring(0, ((Button)sender).Tag.ToString().IndexOf(';')));
            string name = ((Button)sender).Tag.ToString().Substring((((Button)sender).Tag.ToString().IndexOf(';') + 1), ((((Button)sender).Tag.ToString().Length) - ((Button)sender).Tag.ToString().IndexOf(';')) - 1);
            new ListUsers().confirmFriend(User_id, id);
            ((Form)panel.GetContainerControl()).Controls.RemoveByKey("tabControlFriends");
            ((Form)panel.GetContainerControl()).Controls.Add(createTabControlFriends());
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            int id = short.Parse(((Button)sender).Tag.ToString().Substring(0, ((Button)sender).Tag.ToString().IndexOf(';')));
            string name = ((Button)sender).Tag.ToString().Substring((((Button)sender).Tag.ToString().IndexOf(';') + 1), ((((Button)sender).Tag.ToString().Length) - ((Button)sender).Tag.ToString().IndexOf(';')) - 1);
            new ListUsers().removeFriend(Login, id);
        }
        ////////////////////
    }
}