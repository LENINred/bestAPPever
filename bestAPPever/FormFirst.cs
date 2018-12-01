using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace bestAPPever
{
    public partial class FormFirst : Form
    {
        public FormFirst()
        {
            InitializeComponent();
        }

        TextBox textBoxLogin;
        TextBox textBoxPassword;
        Button buttonLogIn;
        TextBox textBoxPersName;
        ComboBox comboBoxSex;
        int user_id;
        string Login = "";
        StatusStrip statusStrip;
        private void FormFirst_Load(object sender, System.EventArgs e)
        {
            statusStrip = new CreateObjects().createStatusStrip("Log", "");
            this.Controls.Add(statusStrip);
        }
        
        private void buttonRegLog_Click(object sender, System.EventArgs e)
        {
            showFormRegLog();            
        }

        private void showFormRegLog()
        {
            buttonRegLog.Visible = false;

            this.Controls.Add(new CreateObjects().createTextBox("Login", new System.Drawing.Point(290, 190)));
            this.Controls.Add(new CreateObjects().createTextBox("Pass", new System.Drawing.Point(290, 215), true));
            this.Controls.Add(new CreateObjects().createButton("LogIn", "Войти", new System.Drawing.Point(290, 245)));
            this.Controls.Add(new CreateObjects().createButton("Registration", "Регистрация", new System.Drawing.Point(290, 269)));

            buttonLogIn = (Button)this.Controls.Find("buttonLogIn", true).GetValue(0);
            buttonLogIn.Click += buttonLogIn_Click;

            Button buttonReg = (Button)this.Controls.Find("buttonRegistration", true).GetValue(0);
            buttonReg.Click += ButtonReg_Click;

            textBoxLogin = ((TextBox)this.Controls.Find("textBoxLogin", true).GetValue(0));
            
            textBoxLogin.Focus();
            textBoxPassword = ((TextBox)this.Controls.Find("textBoxPass", true).GetValue(0));

            textBoxLogin.KeyPress += TextBoxPassword_KeyPress;
            textBoxPassword.KeyPress += TextBoxPassword_KeyPress;

            string[] restoredLogPass = restoreLogin();
            if (restoredLogPass != null)
            {
                textBoxLogin.Text = restoredLogPass[0];
                textBoxPassword.Text = restoredLogPass[1];
            }
        }

        private void TextBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') buttonLogIn.PerformClick();
        }
        
        private void ButtonReg_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<int, object>[] userData = new RegLogInClass().regisrUser(textBoxLogin.Text, textBoxPassword.Text);
            user_id = short.Parse(userData[2].Value.ToString());
            
            ((ToolStripLabel)statusStrip.Items.Find("logLabel", true).GetValue(0)).Text = (String)userData[0].Value;

            if ((Boolean)userData[1].Value)
            {
                //moveing to pers creation
                this.Text = "Создание персонажа";
                persCreation();
            }
        }

        TamagochiEditor tamagochiEditor;
        private void buttonLogIn_Click(object sender, System.EventArgs e)
        {
            KeyValuePair<int, object>[] userData = new RegLogInClass().tryToLogIn(textBoxLogin.Text, textBoxPassword.Text);
            user_id = short.Parse(userData[2].Value.ToString());
            
            ((ToolStripLabel)statusStrip.Items.Find("logLabel", true).GetValue(0)).Text = (String)userData[0].Value;

            if ((Boolean)userData[1].Value)
            {
                saveLogin(textBoxLogin.Text, textBoxPassword.Text);
                if (!(Boolean)userData[3].Value)
                {
                    //moveing to pers creation
                    this.Text = "Создание персонажа";
                    persCreation();
                }
                else
                {
                    //moveing to gaming
                    //remove old objects and add pers to screen
                    Login = textBoxLogin.Text;
                    startGame();
                }
            }
            this.Text = "Игра";
        }

        private void saveLogin(string login, string pass)
        {
            string writePath = @"login.txt";
            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(login);
                sw.WriteLine(pass);
            }
        }

        private string[] restoreLogin()
        {
            string path = @"login.txt";
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    return new string[] { sr.ReadLine(), sr.ReadLine() };
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        private void startGame()
        {
            this.Controls.Clear();
            string[] persStats = new TamagochiStatus().getPersStatus(user_id);
            string name = "", sex = "", health = "", hungry = "", feeling = "", look = "";
            name = persStats[0];
            sex = persStats[1];
            health = persStats[2];
            hungry = persStats[3];
            feeling = persStats[4];
            look = persStats[5];
            List<int> parts = new List<int>();
            string temp = "";
            foreach (char ch in look)
            {
                if (ch != '&')
                {
                    temp += ch;
                }
                else
                {
                    parts.Add(short.Parse(temp));
                    temp = "";
                }
                
            }
            Button buttonMenu = new CreateObjects().createButton("Menu", "Меню", new System.Drawing.Point(620, 10));
            buttonMenu.Anchor = AnchorStyles.Right;
            buttonMenu.Size = new Size(44, 22);
            this.Controls.Add(buttonMenu);
            buttonMenu.Click += ButtonMenu_Click;

            this.Controls.Add(new CreateObjects().createLabel("PersName", "Имя: " + name, new System.Drawing.Point(10, 70)));
            this.Controls.Add(new CreateObjects().createLabel("PersSex", "Пол: " + sex, new System.Drawing.Point(10, 100)));
            this.Controls.Add(new CreateObjects().createLabel("PersHealth", "Здоровье: " + health, new System.Drawing.Point(10, 130)));
            this.Controls.Add(new CreateObjects().createLabel("PersHungry", "Голод: " + hungry, new System.Drawing.Point(10, 160)));
            this.Controls.Add(new CreateObjects().createLabel("PersFeeling", "Настроение: " + feeling, new System.Drawing.Point(10, 190)));
            tamagochiEditor = new TamagochiEditor(parts[0], parts[1], parts[2]);
            this.Controls.Add(tamagochiEditor.createTamagoci());
        }
        
        private void ButtonMenu_Click(object sender, EventArgs e)
        {
            try
            {
                this.Controls.Find("panelMenu", true).GetValue(0);
                this.Controls.RemoveByKey("panelMenu");
                this.Controls.RemoveByKey("groupBoxUsers");
            }
            catch(Exception ex)
            {
                Panel panelMenu = new CreateObjects().createMenu(Login, new System.Drawing.Point(460, 45));
                this.Controls.Add(panelMenu);

                Button buttonExit = (Button)this.Controls.Find("buttonExit", true).GetValue(0);
                buttonExit.Click += ButtonExit_Click;
            }
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            showFormRegLog();
            this.Text = "Вход";
        }

        private void persCreation()
        {
            this.Controls.Clear();
            textBoxPersName = new CreateObjects().createTextBox("PersName", new System.Drawing.Point(12, 12));
            comboBoxSex = new CreateObjects().createComboBox("Sex", new System.Drawing.Point(12, 36));
            Button persCreationOk = new CreateObjects().createButton("persCreationOk", "Ok", new System.Drawing.Point(12, 400));
            persCreationOk.Click += PersCreationOk_Click;

            this.Controls.Add(textBoxPersName);
            this.Controls.Add(comboBoxSex);
            this.Controls.Add(persCreationOk);

            this.Controls.AddRange(new CreateObjects().createArrows().ToArray());

            PictureBox headArrowNext = (PictureBox)this.Controls.Find("nextHead", true).GetValue(0);
            headArrowNext.Click += HeadArrowNext_Click;

            PictureBox bodyArrowNext = (PictureBox)this.Controls.Find("nextBody", true).GetValue(0);
            bodyArrowNext.Click += BodyArrowNext_Click;

            PictureBox legsArrowNext = (PictureBox)this.Controls.Find("nextLegs", true).GetValue(0);
            legsArrowNext.Click += LegsArrowNext_Click;

            PictureBox headArrowPrev = (PictureBox)this.Controls.Find("prevHead", true).GetValue(0);
            headArrowPrev.Click += HeadArrowPrev_Click;

            PictureBox bodyArrowPrev = (PictureBox)this.Controls.Find("prevBody", true).GetValue(0);
            bodyArrowPrev.Click += BodyArrowPrev_Click;

            PictureBox legsArrowPrev = (PictureBox)this.Controls.Find("prevLegs", true).GetValue(0);
            legsArrowPrev.Click += LegsArrowPrev_Click;

            Button buttonMenu = new CreateObjects().createButton("Menu", "Меню", new System.Drawing.Point(610, 10));
            buttonMenu.Anchor = AnchorStyles.Right;
            buttonMenu.Size = new Size(44, 22);
            this.Controls.Add(buttonMenu);
            buttonMenu.Click += ButtonMenu_Click;

            tamagochiEditor = new TamagochiEditor(0, 0, 0);
            this.Controls.Add(tamagochiEditor.createTamagoci());
            this.Text = "Создание персонажа";
        }

        int[] persLook = new int[3];
        private void LegsArrowPrev_Click(object sender, EventArgs e)
        {
            persLook[2] = tamagochiEditor.prevLegs();
        }

        private void BodyArrowPrev_Click(object sender, EventArgs e)
        {
            persLook[1] = tamagochiEditor.prevBody();
        }

        private void HeadArrowPrev_Click(object sender, EventArgs e)
        {
            persLook[0] = tamagochiEditor.prevHead();
        }

        private void HeadArrowNext_Click(object sender, EventArgs e)
        {
            persLook[0] = tamagochiEditor.nextHead();
        }

        private void BodyArrowNext_Click(object sender, EventArgs e)
        {
            persLook[1] = tamagochiEditor.nextBody();
        }

        private void LegsArrowNext_Click(object sender, EventArgs e)
        {
            persLook[2] = tamagochiEditor.nextLegs();
        }

        // Проверка валидности имени персонажа
        private void PersCreationOk_Click(object sender, EventArgs e)
        {
            string log = "";
            if (textBoxPersName.Text.Length >= 4)
            {
                if (comboBoxSex.SelectedIndex != -1)
                {
                    if (new TamagochiStatus().firstAdd(textBoxPersName.Text, user_id, comboBoxSex.SelectedIndex, persLook))
                    {
                        log += "Успешное создание персонажа";
                        startGame();
                    }
                    else
                    {
                        log += "Ошибка создания персонажа(Имя занято)";
                    }
                }
                else
                {
                    log += "Выберите пол";
                }
            }
            else
            {
                log += "Имя персонажа должно быть длиннее 4х символов";
            }
            ((ToolStripLabel)statusStrip.Items.Find("logLabel", true).GetValue(0)).Text = log;
            statusStrip.Refresh();
            this.Text = "Игра";
        }

    }
}
