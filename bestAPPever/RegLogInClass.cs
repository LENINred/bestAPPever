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
            KeyValuePair<int, object>[] mas = new KeyValuePair<int, object>[4];
            if (checkTextBoxes(login, pass))
            {
                string message = "";
                int user_id = 0;
                bool loginSuccess = false;
                bool persIs = false;
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
                            persIs = false;
                            message = "Успешный вход";
                        }
                        else
                        {
                            loginSuccess = true;
                            persIs = true;
                            message = "Успешный вход";
                        }
                    }
                    if (user_id == 0)
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
            else
            {
                mas[0] = new KeyValuePair<int, object>(0, -1);
                mas[1] = new KeyValuePair<int, object>(1, false);
                mas[2] = new KeyValuePair<int, object>(2, false);
                mas[3] = new KeyValuePair<int, object>(3, log);
                return mas;
            }
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

        //Проверка валидности логина/пароля
        string log = "";
        private bool checkTextBoxes(string login, string password)
        {
            bool check = true;
            string symbols = "\"+,'{}[]()-*&?^:\\/%$;#№@!`~<>| ";
            foreach (char ch in login)
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
            return check;
        }

        public KeyValuePair<int, object>[] regisrUser(string login, string pass)
        {
            KeyValuePair<int, object>[] mas = new KeyValuePair<int, object>[3];
            if (checkTextBoxes(login, pass))
            {
                if (!checkLogin(login))
                {
                    try
                    {
                        myConnection = new MySqlConnection(Connect);
                        requestSQL = "INSERT INTO `users`(`name`, `password`, `pers`) VALUES ('" + login + "','" + pass + "', 0);" +
                        "\nSELECT  `user_id` FROM `users` WHERE  `name` ='" + login+"'";
                        myCommand = new MySqlCommand(requestSQL, myConnection);
                        myConnection.Open();
                        var user_id = myCommand.ExecuteScalar();
                        mas[0] = new KeyValuePair<int, object>(0, "Регистрация успешна");
                        mas[1] = new KeyValuePair<int, object>(1, true);
                        mas[2] = new KeyValuePair<int, object>(2, user_id);
                        myConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        mas[0] = new KeyValuePair<int, object>(0, ex.Message);
                        mas[1] = new KeyValuePair<int, object>(1, false);
                    }
                }
                else
                {
                    mas[0] = new KeyValuePair<int, object>(0, "Пользователь уже существует");
                    mas[1] = new KeyValuePair<int, object>(1, false);
                }
                return mas;
            }
            else
            {
                return mas;
            }
        }
    }
}