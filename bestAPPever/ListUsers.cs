using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace bestAPPever
{
    class ListUsers
    {
        string Connect = "Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111";

        //Список всех пользователей<ИД, имя>
        public List<KeyValuePair<int,string>> getListUsers(string login)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `user_id`, `name` FROM `users` WHERE `name` != '" + login + "'";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                List<KeyValuePair<int, string>> users = new List<KeyValuePair<int, string>>();
                while (myDataReader.Read())
                {
                    users.Add(new KeyValuePair<int, string>(myDataReader.GetInt16(0), myDataReader.GetString(1)));
                }
                myDataReader.Close();
                myConnection.Close();

                //Отсеиваем исходящие заявки и друзей
                List<KeyValuePair<int, string>> listNewFriendsOut = new ListUsers().getListNewFriendsOut(login);
                listNewFriendsOut.AddRange(new ListUsers().getListFriends(login));
                foreach (KeyValuePair<int, string> user in listNewFriendsOut)
                    users.Remove(user);

                return users;
            }
            catch
            {
                return null;
            }
        }

        //Добавление друга
        public void addFriend(int user_id, string user_name, int friend_id, string friend_name, int status)
        {
            string requestSQL = "";
            if (!checkIfFriendAsked(user_id, friend_id, status))
            {
                requestSQL = "INSERT INTO `friends`(`user_id`, `user_name`, `friend_id`, `friend_name`, `status`, `date_request`, `date_add`) VALUES ("
                    + user_id + ",'" + user_name + "'," + friend_id + ",'" + friend_name + "', " + status + ", CURRENT_DATE(), '0000-00-00')";
            }
            else
            {
                requestSQL = "UPDATE `friends` SET `status` = 2, `date_add`= CURRENT_DATE() WHERE ((`user_id` = " + friend_id +
                    "') AND (`friend_id` = " + user_id + "))";
            }
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();
            }
            catch
            {
                //--
            }
        }        

        //Проверка был ли запрос в друзья от пользователя
        private bool checkIfFriendAsked(int user_id, int friend_id, int status)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `user_id`, `user_name`, `friend_id`, `friend_name`, `status` FROM `friends` WHERE `user_id` = "+ user_id
                    + "' AND `friend_id` = "+ friend_id
                    + "' AND `status` = 1";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                if (myCommand.ExecuteScalar() == null)
                {
                    myConnection.Close();
                    return false;
                }
                else
                {
                    myConnection.Close();
                    return true;
                }
            }
            catch
            {
                return true;
            }
        }

        //Удаление друга
        public void removeFriend(string name, int friend_id)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "DELETE FROM `friends` WHERE (`user_name`='" + name + "' AND `friend_id`=" + friend_id +
                    ") OR (`user_id`=" + friend_id + " AND `friend_name`='" + name + "')";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();
            }
            catch
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
                myDataReader.Close();
                myConnection.Close();
                return friendsList;
            }
            catch
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
                myDataReader.Close();
                myConnection.Close();
                return newFriendsList;
            }
            catch
            {
                return null;
            }
        }

        //Получаем список запросов в друзья <ИД, имя>
        public List<KeyValuePair<int, string>> getListNewFriendsOut(string name)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `friend_id`, `friend_name`, `status` FROM `friends` WHERE (`user_name` = '" + name + "') AND (`status` = 1)";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                List<KeyValuePair<int, string>> newFriendsList = new List<KeyValuePair<int, string>>();

                while (myDataReader.Read())
                {
                    newFriendsList.Add(new KeyValuePair<int, string>(myDataReader.GetInt16(0), myDataReader.GetString(1)));
                }
                myDataReader.Close();
                myConnection.Close();
                return newFriendsList;
            }
            catch
            {
                return null;
            }
        }

        //Одобрить заявку в друзья
        public void confirmFriend(int user_id, int friend_id)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "UPDATE `friends` SET `status` = 2, `date_add`= CURRENT_DATE(), `status_notification` = 0 WHERE ((`user_id` = " + friend_id +
                    ") AND (`friend_id` = " + user_id + "))";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();
            }
            catch
            {
                //--
            }
        }

        //Отклонить заявку в друзья
        public void rejectFriend(int user_id, int friend_id)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "DELETE FROM `friends` WHERE ((`user_id` = " + friend_id +
                    ") AND (`friend_id` = " + user_id +
                    ") AND (`status` = 1))";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();
            }
            catch
            {
                //--
            }
        }

        //Отклонить заявку в друзья
        public void cancelFriendRequest(int user_id, int friend_id)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "DELETE FROM `friends` WHERE ((`user_id` = " + friend_id +
                    ") AND (`friend_id` = " + user_id +
                    ") AND (`status` = 1))";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();
            }
            catch
            {
                //--
            }
        }
    }
}
