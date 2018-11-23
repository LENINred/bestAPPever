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

        string textBoxLogin;
        string textBoxPassword;
        private void buttonRegLog_Click(object sender, System.EventArgs e)
        {
            buttonRegLog.Visible = false;

            this.Controls.Add(new CreateObjects().createTextBox("Login", new System.Drawing.Point(12, 12)));
            this.Controls.Add(new CreateObjects().createTextBox("Pass", new System.Drawing.Point(12, 36), true));
            this.Controls.Add(new CreateObjects().createButton("LogIn", new System.Drawing.Point(12, 60)));
            this.Controls.Add(new CreateObjects().createButton("Registration", new System.Drawing.Point(12, 85)));

            Button buttonLogIn = (Button)this.Controls.Find("buttonLogIn", true).GetValue(0);
            buttonLogIn.Click += buttonLogIn_Click;

            Button buttonReg = (Button)this.Controls.Find("buttonRegistration", true).GetValue(0);
            buttonReg.Click += ButtonReg_Click;
        }

        private void ButtonReg_Click(object sender, System.EventArgs e)
        {
            textBoxLogin = ((TextBox)this.Controls.Find("textBoxLogin", true).GetValue(0)).Text;
            textBoxPassword = ((TextBox)this.Controls.Find("textBoxPass", true).GetValue(0)).Text;
            if (checkTextBoxes(textBoxLogin, textBoxPassword))
            {
                this.Controls.RemoveByKey("labelLog");
                RegLogInClass reglogInClass = new RegLogInClass();
                KeyValuePair<Boolean, string> pairLog = reglogInClass.regisrUser(textBoxLogin, textBoxPassword);
                this.Controls.Add(new CreateObjects().createLabel("Log", pairLog.Value, new System.Drawing.Point(12, 110)));
                if (pairLog.Key)
                {
                    //move to pers creation
                    //remove old objects and add new
                    this.Controls.Clear();
                    this.Controls.Add(new CreateObjects().createTextBox("PersName", new System.Drawing.Point(12, 12)));
                }
            }
        }

        private void buttonLogIn_Click(object sender, System.EventArgs e)
        {
            textBoxLogin = ((TextBox)this.Controls.Find("textBoxLogin", true).GetValue(0)).Text;
            textBoxPassword = ((TextBox)this.Controls.Find("textBoxPass", true).GetValue(0)).Text;
            if (checkTextBoxes(textBoxLogin, textBoxPassword))
            {
                this.Controls.RemoveByKey("labelLog");
                RegLogInClass reglogInClass = new RegLogInClass();
                KeyValuePair<bool[], string> pairLog = reglogInClass.checkLogin(textBoxLogin, textBoxPassword);
                this.Controls.Add(new CreateObjects().createLabel("Log", pairLog.Value, new System.Drawing.Point(12, 110)));
                if (pairLog.Key[0])
                {
                    if (pairLog.Key[1])
                    {
                        //move to pers creation
                        //remove old objects and add new
                    }
                    else
                    {
                        //move to gaming
                        //remove old objects and add pers to screen
                    }
                }
            }
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
