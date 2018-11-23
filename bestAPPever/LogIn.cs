using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace bestAPPever
{
    public partial class FormLogIn : Form
    {
        public FormLogIn()
        {
            InitializeComponent();
        }

        private void FormLogIn_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            if((textBoxLogin.Text != String.Empty) && (textBoxPass.Text != String.Empty))
            {
                LogRegSQL(textBoxLogin.Text, textBoxPass.Text);
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }
        
        private string LogRegSQL(string login, string pass)
        {
            try
            {
                string responseSQL = "";
                string requestSQL = "SELECT `name` FROM `tamagoches` WHERE `name` = '" + login + "' AND `password` = " + pass + "";
                string Connect = "Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111";
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                var t = myCommand.ExecuteScalar();
                if (t != null)
                {
                    responseSQL = t.ToString();
                }
                myConnection.Close();
                if(responseSQL == login)
                {
                    labelLog.Text = "Успешный вход";
                }
                else
                {
                    labelLog.Text = "Логин либо пароль\n введены не верно";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
    }
}
