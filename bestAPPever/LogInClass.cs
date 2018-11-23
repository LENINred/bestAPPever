using MySql.Data.MySqlClient;
using System;

namespace bestAPPever
{
    class LogInClass
    {

        public string LogRegSQL(string login, string pass)
        {
            string log = "";
            try
            {
                string requestSQL = "SELECT `name` FROM `tamagoches` WHERE `name` = '" + login + "' AND `password` = '" + pass + "'";
                string Connect = "Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111";
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                var responseSQL = myCommand.ExecuteScalar();
                if (responseSQL != null)
                {
                    log = "Успешный вход";
                }
                else
                {
                    log = "Логин либо пароль\n введены не верно";
                }
                myConnection.Close();
            }
            catch (Exception ex)
            {
                log = ex.Message;
            }
            return log;
        }
    }
}
