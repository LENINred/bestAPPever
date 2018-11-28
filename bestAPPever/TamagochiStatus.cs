using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace bestAPPever
{
    class TamagochiStatus
    {
        string Connect = "Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111";
        public void getStatus(int userId)
        {

        }

        private bool checkPersExist(string name)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `name` FROM `tamagoches` WHERE `name` = '" + name + "'";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
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
                return true;
            }
        }

        public bool firstAdd(string name, int userId, int sex)
        {
            if (checkPersExist(name))
            {
                try
                {
                    MySqlConnection myConnection = new MySqlConnection(Connect);
                    string requestSQL = "INSERT INTO `tamagoches`(`name`, `user_index`, `sex`, `health`, `hungry`, `feeling`) VALUES ('" + name + "','" + userId + "','" + sex + "','50','50','50')";
                    MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                    myConnection.Open();
                    myCommand.ExecuteScalar();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /*
        public bool setStatus(int tamagochiId)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `name` FROM `users` WHERE `name` = '" + login + "'";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
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
        }*/
    }
}
