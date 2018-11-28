using System;
using System.Collections.Generic;
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
        private void buttonRegLog_Click(object sender, System.EventArgs e)
        {
            buttonRegLog.Visible = false;

            this.Controls.Add(new CreateObjects().createTextBox("Login", new System.Drawing.Point(12, 12)));
            this.Controls.Add(new CreateObjects().createTextBox("Pass", new System.Drawing.Point(12, 36), true));
            this.Controls.Add(new CreateObjects().createButton("LogIn", "Войти", new System.Drawing.Point(12, 60)));
            this.Controls.Add(new CreateObjects().createButton("Registration", "Регистрация", new System.Drawing.Point(12, 85)));

            buttonLogIn = (Button)this.Controls.Find("buttonLogIn", true).GetValue(0);
            buttonLogIn.Click += buttonLogIn_Click;

            Button buttonReg = (Button)this.Controls.Find("buttonRegistration", true).GetValue(0);
            buttonReg.Click += ButtonReg_Click;

            textBoxLogin = ((TextBox)this.Controls.Find("textBoxLogin", true).GetValue(0));
            textBoxPassword = ((TextBox)this.Controls.Find("textBoxPass", true).GetValue(0));
            textBoxPassword.KeyPress += TextBoxPassword_KeyPress;
        }

        private void TextBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') buttonLogIn.PerformClick();
        }

        KeyValuePair<int, object>[] userData;
        private void ButtonReg_Click(object sender, System.EventArgs e)
        {
            if (checkTextBoxes(textBoxLogin.Text, textBoxPassword.Text))
            {
                userData = new RegLogInClass().checkLogin(textBoxLogin.Text, textBoxPassword.Text);
                this.Controls.RemoveByKey("labelLog");
                RegLogInClass reglogInClass = new RegLogInClass();
                KeyValuePair<Boolean, string> pairLog = reglogInClass.regisrUser(textBoxLogin.Text, textBoxPassword.Text);
                this.Controls.Add(new CreateObjects().createLabel("Log", pairLog.Value, new System.Drawing.Point(12, 110)));
                if (pairLog.Key)
                {
                    //moveing to pers creation
                    user_id = (int)userData[0].Value;
                    persCreation();
                }
            }
        }

        TamagochiEditor tamagochiEditor;
        int user_id;
        private void buttonLogIn_Click(object sender, System.EventArgs e)
        {
            if (checkTextBoxes(textBoxLogin.Text, textBoxPassword.Text))
            {
                this.Controls.RemoveByKey("labelLog");
                userData = new RegLogInClass().checkLogin(textBoxLogin.Text, textBoxPassword.Text);
                this.Controls.Add(new CreateObjects().createLabel("Log", (String)userData[3].Value, new System.Drawing.Point(12, 110)));
                if ((Boolean)userData[1].Value)
                {
                    user_id = (int)userData[0].Value;
                    if (!(Boolean)userData[2].Value)
                    {
                        //moveing to pers creation
                        persCreation();
                    }
                    else
                    {
                        //moveing to gaming
                        //remove old objects and add pers to screen
                        startGame();
                    }
                }
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
            int[] parts = new int[3];
            string temp = "";
            foreach (char ch in look)
            {
                if (ch != '&')
                {
                    temp += ch;
                }
                else
                {
                    parts[i] = short.Parse(temp);
                    temp = "";
                }
            }


            this.Controls.Add(new CreateObjects().createLabel("PersName", "Имя " + name, new System.Drawing.Point(12, 12)));
            this.Controls.Add(new CreateObjects().createLabel("PersSex", "Пол " + sex, new System.Drawing.Point(100, 12)));
            this.Controls.Add(new CreateObjects().createLabel("PersHealth", "Здоровье " + health, new System.Drawing.Point(150, 12)));
            this.Controls.Add(new CreateObjects().createLabel("PersHungry", "Голод " + hungry, new System.Drawing.Point(12, 30)));
            this.Controls.Add(new CreateObjects().createLabel("PersFeeling", "Шта? " + feeling, new System.Drawing.Point(100, 30)));
            tamagochiEditor = new TamagochiEditor(parts[0], parts[1], parts[2]);
            this.Controls.Add(tamagochiEditor.createTamagoci());
        }

        TextBox textBoxPersName;
        ComboBox comboBoxSex;
        private void persCreation()
        {
            this.Controls.Clear();
            textBoxPersName = new CreateObjects().createTextBox("PersName", new System.Drawing.Point(12, 12));
            comboBoxSex = new CreateObjects().createComboBox("Sex", new System.Drawing.Point(12, 36));
            this.Controls.Add(textBoxPersName);
            this.Controls.Add(comboBoxSex);
            Button persCreationOk = new CreateObjects().createButton("persCreationOk", "Ok", new System.Drawing.Point(12, 400));
            persCreationOk.Click += PersCreationOk_Click;
            this.Controls.Add(persCreationOk);

            PictureBox headArrowNext = new CreateObjects().createArrow("head", "arrowRight", new System.Drawing.Point(200, 85));
            headArrowNext.Click += HeadArrowNext_Click;
            this.Controls.Add(headArrowNext);

            PictureBox bodyArrowNext = new CreateObjects().createArrow("body", "arrowRight", new System.Drawing.Point(200, 185));
            bodyArrowNext.Click += BodyArrowNext_Click;
            this.Controls.Add(bodyArrowNext);

            PictureBox legsArrowNext = new CreateObjects().createArrow("legs", "arrowRight", new System.Drawing.Point(200, 300));
            legsArrowNext.Click += LegsArrowNext_Click;
            this.Controls.Add(legsArrowNext);

            PictureBox headArrowPrev = new CreateObjects().createArrow("head", "arrowLeft", new System.Drawing.Point(150, 85));
            headArrowPrev.Click += HeadArrowPrev_Click;
            this.Controls.Add(headArrowPrev);

            PictureBox bodyArrowPrev = new CreateObjects().createArrow("body", "arrowLeft", new System.Drawing.Point(150, 185));
            bodyArrowPrev.Click += BodyArrowPrev_Click;
            this.Controls.Add(bodyArrowPrev);

            PictureBox legsArrowPrev = new CreateObjects().createArrow("legs", "arrowLeft", new System.Drawing.Point(150, 300));
            legsArrowPrev.Click += LegsArrowPrev_Click;
            this.Controls.Add(legsArrowPrev);

            tamagochiEditor = new TamagochiEditor(0, 0, 0);
            this.Controls.Add(tamagochiEditor.createTamagoci());
        }

        private void PersCreationOk_Click(object sender, EventArgs e)
        {
            this.Controls.RemoveByKey("labelLog");
            string log = "";
            if (textBoxPersName.Text.Length >= 4)
            {
                if(comboBoxSex.SelectedIndex != -1)
                {
                    if(new TamagochiStatus().firstAdd(textBoxPersName.Text, user_id, comboBoxSex.SelectedIndex, persLook))
                    {
                        log += "Успешное создание персонажа";
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
            this.Controls.Add(new CreateObjects().createLabel("Log", log, new System.Drawing.Point(12, 425)));
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

        //Проверка валидности логина/пароля
        private bool checkTextBoxes(string login, string password)
        {
            this.Controls.RemoveByKey("labelLog");
            string log = "";
            bool check = true;
            string symbols = "\"+,'{}[]()-*&?^:\\/%$;#№@!`~<>| ";
            foreach(char ch in login)
            {
                if (symbols.Contains(ch.ToString()))
                {
                    log += "Вы ввели недопустимый символ в имени\n";
                    check = false;
                    break;
                }               
            }
            if ((login == "") || (login.Length < 3))
            {
                log += "Имя должно содержать больше 3-х символов\n";
                check = false;
            }
            if (password.Contains(" "))
            {
                log += "Пароль не может содержать пробелов\n";
                check = false;
            }

            if ((password == "") || (password.Length < 6))
            {
                log += "Пароль должен состоять из 6-сьти и более символов\n";
                check = false;
            }
            this.Controls.Add(new CreateObjects().createLabel("Log", log, new System.Drawing.Point(12, 110)));
            return check;
        }

        private void FormFirst_Load(object sender, System.EventArgs e)
        {

        }
    }
}
