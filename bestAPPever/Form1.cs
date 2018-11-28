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

        private void ButtonReg_Click(object sender, System.EventArgs e)
        {
            if (checkTextBoxes(textBoxLogin.Text, textBoxPassword.Text))
            {
                this.Controls.RemoveByKey("labelLog");
                RegLogInClass reglogInClass = new RegLogInClass();
                KeyValuePair<Boolean, string> pairLog = reglogInClass.regisrUser(textBoxLogin.Text, textBoxPassword.Text);
                this.Controls.Add(new CreateObjects().createLabel("Log", pairLog.Value, new System.Drawing.Point(12, 110)));
                if (pairLog.Key)
                {
                    //moveing to pers creation
                    //remove old objects and add new
                    persCreation();
                }
            }
        }

        TamagochiEditor tamagochiClass;
        private void buttonLogIn_Click(object sender, System.EventArgs e)
        {
            if (checkTextBoxes(textBoxLogin.Text, textBoxPassword.Text))
            {
                this.Controls.RemoveByKey("labelLog");
                RegLogInClass reglogInClass = new RegLogInClass();
                //KeyValuePair<bool[], string> pairLog = reglogInClass.checkLogin(textBoxLogin.Text, textBoxPassword.Text);
                KeyValuePair<int, object>[] mas = reglogInClass.checkLogin(textBoxLogin.Text, textBoxPassword.Text);
                this.Controls.Add(new CreateObjects().createLabel("Log", (String)mas[3].Value, new System.Drawing.Point(12, 110)));
                if ((Boolean)mas[1].Value)
                {
                    if ((Boolean)mas[2].Value)
                    {
                        //moveing to pers creation
                        //remove old objects and add new
                        persCreation();
                    }
                    else
                    {
                        //moveing to gaming
                        //remove old objects and add pers to screen
                    }
                }
            }
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

            tamagochiClass = new TamagochiEditor(0, 0, 0);
            this.Controls.Add(tamagochiClass.createTamagoci());
        }

        private void PersCreationOk_Click(object sender, EventArgs e)
        {
            this.Controls.RemoveByKey("labelLog");
            string log = "";
            if (textBoxPersName.Text.Length >= 4)
            {
                if(comboBoxSex.SelectedIndex != -1)
                {
                    if(new TamagochiStatus().firstAdd(textBoxPersName.Text, 0, comboBoxSex.SelectedIndex))
                    {
                        log += "Успешное создание персонажа";
                    }
                    else
                    {
                        log += "Ошибка создания персонажа";
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

        private void LegsArrowPrev_Click(object sender, EventArgs e)
        {
            tamagochiClass.prevLegs();
        }

        private void BodyArrowPrev_Click(object sender, EventArgs e)
        {
            tamagochiClass.prevBody();
        }

        private void HeadArrowPrev_Click(object sender, EventArgs e)
        {
            tamagochiClass.prevHead();
        }

        private void HeadArrowNext_Click(object sender, EventArgs e)
        {
            tamagochiClass.nextHead();
        }

        private void BodyArrowNext_Click(object sender, EventArgs e)
        {
            tamagochiClass.nextBody();
        }

        private void LegsArrowNext_Click(object sender, EventArgs e)
        {
            tamagochiClass.nextLegs();
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
