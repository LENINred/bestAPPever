using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace bestAPPever
{
    class RegLogInClass
    {
        string Connect = "Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111";
        MySqlConnection myConnection;
        string requestSQL;
        MySqlCommand myCommand;
        public string checkLogin(string login, string pass)
        {
            string log = "";
            try
            {
                myConnection = new MySqlConnection(Connect);
                requestSQL = "SELECT `name` FROM `tamagoches` WHERE `name` = '" + login + "' AND `password` = '" + pass + "'";                
                myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                var responseSQL = myCommand.ExecuteScalar();
                if (responseSQL != null)
                {
                    log = "Успешный вход";
                }
                else
                {
                    log = "Пользователя не существует\nлибо логин и пароль\n введены не верно";
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return log;
        }

        private bool checkLogin(string login)
        {
            bool log = false;
            try
            {
                myConnection = new MySqlConnection(Connect);
                requestSQL = "SELECT `name` FROM `tamagoches` WHERE `name` = '" + login + "'";
                myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                var responseSQL = myCommand.ExecuteScalar();
                if (responseSQL != null)
                {
                    log = true;
                }
                else
                {
                    log = false;
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return log;
        }

        public string regisrUser(string login, string pass)
        {
            string log = "";
            if (!checkLogin(login))
            {
                try
                {
                    myConnection = new MySqlConnection(Connect);
                    requestSQL = "INSERT INTO `tamagoches`(`name`, `password`) VALUES ('" + login + "','" + pass + "')";
                    myCommand = new MySqlCommand(requestSQL, myConnection);
                    myConnection.Open();
                    myCommand.ExecuteScalar();
                    log = "Регистрация успешна";
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    log = ex.Message;
                }
            }
            else
            {
                log = "Пользователь уже существует";
            }
            return log;
        }

    }
}
