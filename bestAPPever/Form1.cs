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
        private void FormFirst_Load(object sender, System.EventArgs e)
        {
            //--            
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
            this.Text = "Регистрация";
            RegLogInClass reglogInClass = new RegLogInClass();
            KeyValuePair<int, object>[] userData = reglogInClass.regisrUser(textBoxLogin.Text, textBoxPassword.Text);
            user_id = short.Parse(userData[2].Value.ToString());

            this.Controls.RemoveByKey("statusStrip");
            this.Controls.Add(new CreateObjects().createStatusStrip("Log", (String)userData[0].Value));

            if ((Boolean)userData[1].Value)
            {
                //moveing to pers creation
                persCreation();
            }
        }

        TamagochiEditor tamagochiEditor;
        private void buttonLogIn_Click(object sender, System.EventArgs e)
        {
            this.Controls.RemoveByKey("statusStrip");
            KeyValuePair<int, object>[] userData = new RegLogInClass().checkLogin(textBoxLogin.Text, textBoxPassword.Text);
            user_id = short.Parse(userData[0].Value.ToString());

            this.Controls.Add(new CreateObjects().createStatusStrip("Log", (String)userData[3].Value));
            if ((Boolean)userData[1].Value)
            {
                saveLogin(textBoxLogin.Text, textBoxPassword.Text);
                if (!(Boolean)userData[2].Value)
                {
                    //moveing to pers creation
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
                    string[] lp = new string[2];
                    lp[0] = sr.ReadLine();
                    lp[1] = sr.ReadLine();
                    return lp;
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
            int i = 0;
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

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            showFormRegLog();
            this.Text = "Вход";
        }

        Panel panelMenu = new Panel();
        private void ButtonMenu_Click(object sender, EventArgs e)
        {
            if (panelMenu.Name != "panelMenu")
            {
                panelMenu = new CreateObjects().createMenu(Login, new System.Drawing.Point(460, 45));
                this.Controls.Add(panelMenu);
                Button buttonExit = (Button)this.Controls.Find("buttonExit", true).GetValue(0);
                buttonExit.Click += ButtonExit_Click;

                Button buttonFindFriends = (Button)this.Controls.Find("buttonFindFriends", true).GetValue(0);
                buttonFindFriends.Click += ButtonFindFriends_Click;
                
            }
            else
            {
                this.Controls.Remove(panelMenu);
                this.Controls.RemoveByKey("panelUsers");
                panelMenu.Name = "null";
            }
        }

        private void ButtonFindFriends_Click(object sender, EventArgs e)
        {

        }

        private void persCreation()
        {
            this.Text = "Создание персонажа";
            this.Controls.Clear();
            textBoxPersName = new CreateObjects().createTextBox("PersName", new System.Drawing.Point(12, 12));
            comboBoxSex = new CreateObjects().createComboBox("Sex", new System.Drawing.Point(12, 36));
            this.Controls.Add(textBoxPersName);
            this.Controls.Add(comboBoxSex);
            Button persCreationOk = new CreateObjects().createButton("persCreationOk", "Ok", new System.Drawing.Point(12, 400));
            persCreationOk.Click += PersCreationOk_Click;
            this.Controls.Add(persCreationOk);

            PictureBox headArrowNext = new CreateObjects().createArrow("head", "arrowRight", new System.Drawing.Point(290, 140));
            headArrowNext.Click += HeadArrowNext_Click;
            this.Controls.Add(headArrowNext);

            PictureBox bodyArrowNext = new CreateObjects().createArrow("body", "arrowRight", new System.Drawing.Point(290, 250));
            bodyArrowNext.Click += BodyArrowNext_Click;
            this.Controls.Add(bodyArrowNext);

            PictureBox legsArrowNext = new CreateObjects().createArrow("legs", "arrowRight", new System.Drawing.Point(290, 340));
            legsArrowNext.Click += LegsArrowNext_Click;
            this.Controls.Add(legsArrowNext);

            PictureBox headArrowPrev = new CreateObjects().createArrow("head", "arrowLeft", new System.Drawing.Point(100, 140));
            headArrowPrev.Click += HeadArrowPrev_Click;
            this.Controls.Add(headArrowPrev);

            PictureBox bodyArrowPrev = new CreateObjects().createArrow("body", "arrowLeft", new System.Drawing.Point(100, 250));
            bodyArrowPrev.Click += BodyArrowPrev_Click;
            this.Controls.Add(bodyArrowPrev);

            PictureBox legsArrowPrev = new CreateObjects().createArrow("legs", "arrowLeft", new System.Drawing.Point(100, 340));
            legsArrowPrev.Click += LegsArrowPrev_Click;
            this.Controls.Add(legsArrowPrev);

            Button buttonMenu = new CreateObjects().createButton("Menu", "Меню", new System.Drawing.Point(620, 10));
            buttonMenu.Anchor = AnchorStyles.Right;
            buttonMenu.Size = new Size(44, 22);
            this.Controls.Add(buttonMenu);
            buttonMenu.Click += ButtonMenu_Click;

            tamagochiEditor = new TamagochiEditor(0, 0, 0);
            this.Controls.Add(tamagochiEditor.createTamagoci());
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

        // Проверка валидности создания персонажа
        private void PersCreationOk_Click(object sender, EventArgs e)
        {
            this.Controls.RemoveByKey("statusStrip");
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
            this.Controls.Add(new CreateObjects().createStatusStrip("Log", log));
            this.Text = "Игра";
        }

    }
}
