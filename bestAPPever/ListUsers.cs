using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bestAPPever
{
    class ListUsers
    {
        string Connect = "Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111";

        //Список всех пользователей<ИД, имя>
        public List<KeyValuePair<int,string>> getListUsers()
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `user_id`, `name` FROM `users`";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                List<KeyValuePair<int, string>> users = new List<KeyValuePair<int, string>>();
                while (myDataReader.Read())
                {
                    users.Add(new KeyValuePair<int, string>(myDataReader.GetInt16(0), myDataReader.GetString(1)));
                }
                return users;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Добавление друга
        public void addFriend(int user_id, string user_name, int friend_id, string friend_name, int status)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "INSERT INTO `friends`(`user_id`, `user_name`, `friend_id`, `friend_name`, `status`, `date_request`, `date_add`) VALUES ("
                    + user_id + ",'" + user_name + "'," + friend_id + ",'" + friend_name + "', " + status + ", CURRENT_DATE(), '0000-00-00')";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();
            }
            catch (Exception ex)
            {
                //--
            }
        }

        //Удаление друга
        public void removeFriend(string name, string friend_id)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "DELETE FROM `friends` WHERE `user_name`='" + name + "' AND `friend_id`=" + friend_id;
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();
            }
            catch (Exception ex)
            {
                //--
            }
        }

        //Получаем список друзей<ИД, имя>
        public List<KeyValuePair<int, string>> getListFriends(string name)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `friend_id`, `friend_name`, `status` FROM friends WHERE user_name = '" + name + "'";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                List<KeyValuePair<int, string>> friendsList = new List<KeyValuePair<int, string>>();
                while (myDataReader.Read())
                {
                    if (myDataReader.GetInt16(2) == 2) {
                        friendsList.Add(new KeyValuePair<int, string>(myDataReader.GetInt16(0), myDataReader.GetString(1)));
                    }                    
                }
                return friendsList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
