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
        public void addFriend(string name, string FriendID)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "UPDATE `users` SET `friends` = CONCAT(`friends`, '" + FriendID + "') WHERE `name` = '" + name + "'";
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
        public void removeFriend(string name, string FriendID)
        {
            string newFriendList = getNewFriendList(name, FriendID);
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "UPDATE `users` SET `friends` = '" + newFriendList + "' WHERE `name` = '" + name + "'";
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

        private string getNewFriendList(string name, string FriendID)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `friends` FROM `users` WHERE `name` = '" + name + "'";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                string frinds = myCommand.ExecuteScalar().ToString();
                myConnection.Close();
                return frinds.Replace((FriendID + "&"), "");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Получаем список друзей<ИД, имя>
        public List<KeyValuePair<int, string>> getListFriends(string name)
        {
            List<int> usersID = getFrindsID(name);
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `user_id`, `name` FROM `users`";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                List<KeyValuePair<int, string>> friendsList = new List<KeyValuePair<int, string>>();
                while (myDataReader.Read())
                {
                    if (usersID.IndexOf(myDataReader.GetInt16(0)) != -1)
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

        private List<int> getFrindsID(string name)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `friends` FROM `users` WHERE `name` = '" + name + "'";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                string frinds = myCommand.ExecuteScalar().ToString();
                myConnection.Close();

                List<int> usersID = new List<int>();
                string temp = "";
                foreach (char ch in frinds)
                {
                    if (ch != '&')
                    {
                        temp += ch;
                    }
                    else
                    {
                        usersID.Add(short.Parse(temp));
                        temp = "";
                    }

                }
                return usersID;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
