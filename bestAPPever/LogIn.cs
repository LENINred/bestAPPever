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
                ConnectToSQL(textBoxLogin.Text, textBoxPass.Text);
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }

        private string ConnectToSQL(string login, string pass)
        {
            try
            {
                string CommandText = "SELECT `name` FROM `tamagoches` WHERE `name` = '" + login + "'";
                string Connect = "Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111";
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);
                myConnection.Open();

                MySqlDataReader MyDataReader = myCommand.ExecuteReader();

                string result = "";
                while (MyDataReader.Read())
                {
                    result = MyDataReader.GetString(0); //Получаем строку
                    //int id = MyDataReader.GetInt32(1); //Получаем целое число
                }
                MessageBox.Show(result);
                MyDataReader.Close();
                myConnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
    }
}
