using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace bestAPPever
{
    class TamagochiStatus
    {
        string Connect = "Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111";
        public string[] getPersStatus(int userId)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `name`, `sex`, `health`, `hungry`, `feeling`, `look` FROM `tamagoches` WHERE `user_id` = '" + userId + "'";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                string name = "", sex = "", health = "", hungry = "", feeling = "", look = "";
                while (myDataReader.Read())
                {
                    name = myDataReader.GetString(0);
                    sex = myDataReader.GetInt32(1).ToString();
                    health = myDataReader.GetString(2);
                    hungry = myDataReader.GetString(3);
                    feeling = myDataReader.GetString(4);
                    look = myDataReader.GetString(5);
                }
                return new string[] { name, sex, health, hungry, feeling, look };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
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

        public bool firstAdd(string name, int userId, int sex, int[] persLook)
        {
            if (!checkPersExist(name))
            {
                string persLookS = "";
                foreach (int i in persLook)
                    persLookS += i + "&";
                try
                {
                    MySqlConnection myConnection = new MySqlConnection(Connect);
                    string requestSQL = "INSERT INTO `tamagoches`(`name`, `user_id`, `sex`, `health`, `hungry`, `feeling`, `look`) VALUES ('" + name + "'," + userId + ",'" + sex + "','50','50','50','" + persLookS + "');"+
                        "\nUPDATE  `users` SET  `pers` =1 WHERE  `user_id` =" + userId;
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
    }
}
