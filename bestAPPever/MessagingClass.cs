using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace bestAPPever
{
    class MessagingClass
    {
        //Список диалогов
        public List<KeyValuePair<int, string>> getListDialogues(string login)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection("Database = messages; Data Source = 195.114.3.231; User Id = tamagochi_m; Password = 111");
                string requestSQL = "SELECT `friend_id`, `friend_name` FROM `" + login + "_dialogues` WHERE 1";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                List<KeyValuePair<int, string>> listDialogues = new List<KeyValuePair<int, string>>();
                while (myDataReader.Read())
                {
                    listDialogues.Add(new KeyValuePair<int, string>(myDataReader.GetInt16(0), myDataReader.GetString(1)));
                }
                myDataReader.Close();
                myConnection.Close();

                return listDialogues;
            }
            catch
            {
                return null;
            }
        }

        //Загрузка переписки
        public DataTable getTableMessages(string login, int id, int from_id)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection("Database=messages;Data Source=195.114.3.231;User Id=tamagochi_m;Password=111");
                string requestSQL = "SELECT `from_who`, `message`, `date_send`, `status` FROM `" + login + "_messages` WHERE "
                    + "((`from_who` = " + from_id + " AND `to_whom` = " + id + ") OR "
                    + "(`from_who` = " + id + " AND `to_whom` = " + from_id + "))";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();

                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(myDataReader);

                myDataReader.Close();
                myConnection.Close();

                return dataTable;
            }
            catch
            {
                return null;
            }
        }

        //Отправка сообещния
        public void SendMessage(string login, int id, string friend_name, int friend_id, string message)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection("Database=messages;Data Source=195.114.3.231;User Id=tamagochi_m;Password=111;charset=utf8");
                string requestSQL = "INSERT INTO `" + login + "_messages`(`to_whom`, `from_who`, `message`, `status`, `date_send`) VALUES ("
                    + friend_id + ", " + id + ", '" + message + "', 0, CURRENT_DATE())";

                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();

                requestSQL = "INSERT INTO `" + friend_name + "_messages`(`to_whom`, `from_who`, `message`, `status`, `date_send`) VALUES ("
                    + friend_id + ", " + id + ", '" + message + "', 0, CURRENT_DATE())";

                myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();
            }
            catch
            {
                //--
            }
        }

        public void createDialog(string login, int id, string friend_name, int friend_id)
        {
            try
            {
                string requestSQL;
                MySqlConnection myConnection = new MySqlConnection("Database=messages;Data Source=195.114.3.231;User Id=tamagochi_m;Password=111;charset=utf8");
                if(!checkDialogExist(login, id))
                    requestSQL = "CREATE TABLE " + login + "_dialogues ( friend_id int(11), friend_name varchar(16));\n" +
                    "INSERT INTO `" + login + "_dialogues` (`friend_id`, `friend_name`) VALUES (" + friend_id + ", '" + friend_name + "')";
                else
                    requestSQL = "INSERT INTO `" + login + "_dialogues` (`friend_id`, `friend_name`) VALUES (" + friend_id + ", '" + friend_name + "')";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();

                if (!checkDialogExist(friend_name, friend_id))
                    requestSQL = "CREATE TABLE " + friend_name + "_dialogues ( friend_id int(11), friend_name varchar(16));\n" +
                    "INSERT INTO `" + friend_name + "_dialogues` (`friend_id`, `friend_name`) VALUES (" + login + ", '" + id + "')";
                else
                    requestSQL = "INSERT INTO `" + friend_name + "_dialogues` (`friend_id`, `friend_name`) VALUES (" + id + ", '" + login + "')";
                myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();
            }
            catch
            {
                //--
            }
        }

        private bool checkDialogExist(string user_name, int user_id)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection("Database=messages;Data Source=195.114.3.231;User Id=tamagochi_m;Password=111");
                string requestSQL = "SELECT 1 FROM information_schema.tables WHERE table_schema = 'messages' AND table_name = '" + user_name + "_dialogues';";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                if (myCommand.ExecuteScalar().ToString() == "1")
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
            catch
            {
                return false;
            }
        }

        public void createMessagesTable(string login, int id, string friend_name, int friend_id)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection("Database=messages;Data Source=195.114.3.231;User Id=tamagochi_m;Password=111");
                if (!checkMessagesTableExist(login, id))
                {
                    string requestSQL = "CREATE TABLE " + login + "_messages ( to_whom int(11), from_who int(11), message text, status int(11), date_send date);\n";

                    MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                    myConnection.Open();
                    myCommand.ExecuteScalar();

                    myConnection.Close();
                }
                if (!checkMessagesTableExist(friend_name, friend_id))
                {
                    string requestSQL = "CREATE TABLE " + friend_name + "_messages ( to_whom int(11), from_who int(11), message text, status int(11), date_send date);\n";

                    MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                    myConnection.Open();
                    myCommand.ExecuteScalar();

                    myConnection.Close();
                }
            }
            catch
            {
                //--
            }
        }

        private bool checkMessagesTableExist(string user_name, int user_id)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection("Database=messages;Data Source=195.114.3.231;User Id=tamagochi_m;Password=111");
                string requestSQL = "SELECT 1 FROM information_schema.tables WHERE table_schema = 'messages' AND table_name = '" + user_name + "_messages';";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                if (myCommand.ExecuteScalar().ToString() == "1")
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
            catch
            {
                return false;
            }
        }
    }
}
