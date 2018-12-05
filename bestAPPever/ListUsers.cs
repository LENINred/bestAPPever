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
        public void removeFriend(string name, int friend_id)
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
                string requestSQL = "SELECT `user_id`, `user_name`, `friend_id`, `friend_name`, `status` FROM friends WHERE ((user_name = '" + name + "') OR (friend_name = '" + name + "'))  AND (status = 2)";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                List<KeyValuePair<int, string>> friendsList = new List<KeyValuePair<int, string>>();
                
                while (myDataReader.Read())
                {
                    if (myDataReader.GetString(1) == name)
                    {
                        friendsList.Add(new KeyValuePair<int, string>(myDataReader.GetInt16(2), myDataReader.GetString(3)));
                    }
                    if (myDataReader.GetString(3) == name)
                    {
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

        //Получаем список запросов в друзья <ИД, имя>
        public List<KeyValuePair<int, string>> getListNewFriends(string name)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `user_id`, `user_name`, `status` FROM `friends` WHERE (`friend_name` = '" + name + "') AND (`status` = 1)";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                List<KeyValuePair<int, string>> newFriendsList = new List<KeyValuePair<int, string>>();

                while (myDataReader.Read())
                {
                    newFriendsList.Add(new KeyValuePair<int, string>(myDataReader.GetInt16(0), myDataReader.GetString(1)));
                }
                return newFriendsList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
