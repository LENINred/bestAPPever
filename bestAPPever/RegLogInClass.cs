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
        public KeyValuePair<int, object>[] checkLogin(string login, string pass)
        {
            string message = "";
            int user_id = 0;
            bool loginSuccess = false;
            bool persIs = false;
            KeyValuePair<int, object>[] mas = new KeyValuePair<int, object>[4];            
            try
            {
                myConnection = new MySqlConnection(Connect);
                requestSQL = "SELECT `user_id`, `name`, `pers` FROM `users` WHERE `name` = '" + login + "' AND `password` = '" + pass + "'";                
                myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                while (myDataReader.Read())
                {
                    user_id = myDataReader.GetInt32(0);
                    if (myDataReader.GetInt32(2) == 0)
                    {
                        loginSuccess = true;
                        persIs = true;
                        message = "Успешный вход";
                    }
                    else
                    {
                        loginSuccess = true;
                        persIs = false;
                        message = "Успешный вход";
                    }                    
                }
                if(user_id == 0)
                {
                    message = "Пользователя не существует\nлибо логин и пароль\nвведены не верно";
                }

                mas[0] = new KeyValuePair<int, object>(0, user_id);
                mas[1] = new KeyValuePair<int, object>(1, loginSuccess);
                mas[2] = new KeyValuePair<int, object>(2, persIs);
                mas[3] = new KeyValuePair<int, object>(3, message);

                myDataReader.Close();
                myConnection.Close();
            }
            catch (Exception ex)
            {                
                mas[0] = new KeyValuePair<int, object>(0, -1);
                mas[1] = new KeyValuePair<int, object>(1, false);
                mas[2] = new KeyValuePair<int, object>(2, false);
                mas[3] = new KeyValuePair<int, object>(3, ex.Message);
            }
            return mas;
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