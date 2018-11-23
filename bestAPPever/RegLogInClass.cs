using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace bestAPPever
{
    class RegLogInClass
    {
        string Connect = "Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111";
        MySqlConnection myConnection;
        string requestSQL;
        MySqlCommand myCommand;
        public KeyValuePair<bool[], string> checkLogin(string login, string pass)
        {
            KeyValuePair<bool[], string> pairLog = new KeyValuePair<bool[], string>();//Создание пары ключ,значение (bool[вход, наличие перса], Сообщение)
            try
            {
                myConnection = new MySqlConnection(Connect);
                requestSQL = "SELECT `name`, `pers` FROM `users` WHERE `name` = '" + login + "' AND `password` = '" + pass + "'";                
                myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                while (myDataReader.Read())
                {
                    if (myDataReader.GetInt32(1) == 0)
                    {
                        pairLog = new KeyValuePair<bool[], string>(new bool[]{ true, true }, "Успешный вход");
                    }
                    else
                    {
                        pairLog = new KeyValuePair<bool[], string>(new bool[] { true, false }, "Успешный вход");
                    }
                }
                if(pairLog.Value == null)
                {
                    pairLog = new KeyValuePair<bool[], string>(new bool[] { false, false }, "Пользователя не существует\nлибо логин и пароль\n введены не верно");
                }
                myDataReader.Close();
                myConnection.Close();
            }
            catch (Exception ex)
            {
                pairLog = new KeyValuePair<bool[], string>(new bool[] { false, false }, ex.Message);
            }
            return pairLog;
        }

        private bool checkLogin(string login)
        {
            try
            {
                myConnection = new MySqlConnection(Connect);
                requestSQL = "SELECT `name` FROM `users` WHERE `name` = '" + login + "'";
                myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                var responseSQL = myCommand.ExecuteScalar();
                if (responseSQL != null)
                {
                    myConnection.Close();
                    return true;
                }
                else
                {
                    myConnection.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public KeyValuePair<Boolean, string> regisrUser(string login, string pass)
        {
            KeyValuePair<Boolean, string> pairLog;
            if (!checkLogin(login))
            {
                try
                {
                    myConnection = new MySqlConnection(Connect);
                    requestSQL = "INSERT INTO `users`(`name`, `password`, `pers`) VALUES ('" + login + "','" + pass + "', 0)";
                    myCommand = new MySqlCommand(requestSQL, myConnection);
                    myConnection.Open();
                    myCommand.ExecuteScalar();
                    pairLog = new KeyValuePair<bool, string>(true, "Регистрация успешна");
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    pairLog = new KeyValuePair<bool, string>(false, ex.Message);
                }
            }
            else
            {
                pairLog = new KeyValuePair<bool, string>(false, "Пользователь уже существует");
            }
            return pairLog;
        }

    }
}
