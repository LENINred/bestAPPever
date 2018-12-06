using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace bestAPPever
{
    class MySqlListener
    {
        public event EventHandler<MyEventArgs> MyEvent;
        private int User_id;
        List<KeyValuePair<int, string>> listNewFriends = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> listFriends = new List<KeyValuePair<int, string>>();

        public void Method(int user_id, string Login)
        {
            User_id = user_id;

            listFriends = new ListUsers().getListFriends(Login);
            Thread myThread = new Thread(getFriendStatus);
            myThread.Start();

            listNewFriends = new ListUsers().getListNewFriends(Login);
            Thread myThread1 = new Thread(getNewFriendStatus);
            myThread1.Start();
        }

        private void getFriendStatus()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(3000);
                try
                {
                    MySqlConnection myConnection = new MySqlConnection("Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111");
                    string requestSQL = "SELECT `user_id`, `user_name` FROM `friends` WHERE ((`friend_id` = " + User_id + ") AND (status = 1))";
                    MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                    myConnection.Open();
                    MySqlDataReader myDataReader = myCommand.ExecuteReader();

                    while (myDataReader.Read())
                    {
                        if (!listNewFriends.Contains(new KeyValuePair<int, string>(myDataReader.GetInt16(0), myDataReader.GetString(1))))
                        {
                            listNewFriends.Add(new KeyValuePair<int, string>(myDataReader.GetInt16(0), myDataReader.GetString(1)));
                            MyEvent(this, new MyEventArgs(new KeyValuePair<int, string>(1, myDataReader.GetString(1))));
                        }
                    }

                }
                catch
                {
                    //--
                }
            }
        }
        
        private void getNewFriendStatus()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(3000);
                try
                {
                    MySqlConnection myConnection = new MySqlConnection("Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111");
                    string requestSQL = "SELECT `friend_id`, `friend_name` FROM `friends` WHERE ((`user_id` = " + User_id + ") AND (status = 2))";
                    MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                    myConnection.Open();
                    MySqlDataReader myDataReader = myCommand.ExecuteReader();
                    while (myDataReader.Read())
                    {
                        if (!listFriends.Contains(new KeyValuePair<int, string>(myDataReader.GetInt16(0), myDataReader.GetString(1))))
                        {
                            listFriends.Add(new KeyValuePair<int, string>(myDataReader.GetInt16(0), myDataReader.GetString(1)));
                            MyEvent(this, new MyEventArgs(new KeyValuePair<int, string>(2, myDataReader.GetString(1))));
                        }
                    }

                }
                catch
                {
                    //--
                }
            }
        }
    }

    public class MyEventArgs : EventArgs
    {
        public KeyValuePair<int, string> UserStatus { get; set; }

        public MyEventArgs(KeyValuePair<int, string> user_stat)
        {
            UserStatus = user_stat;
        }
    }

}
