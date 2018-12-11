using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading;

namespace bestAPPever
{
    class MySqlListener
    {
        public event EventHandler<MyEventArgs> MyEvent;
        private int User_id;
        public void Method(int user_id, string Login)
        {
            User_id = user_id;
            
            Thread myThread = new Thread(getFriendStatus);
            myThread.IsBackground = true;
            myThread.Start();

            Thread myThread1 = new Thread(getNewFriendStatus);
            myThread1.IsBackground = true;
            myThread1.Start();
        }

        private void getFriendStatus()
        {
            while (true)
            {
                Thread.Sleep(3000);
                try
                {
                    MySqlConnection myConnection = new MySqlConnection("Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111");
                    string requestSQL = "SELECT `user_id`, `user_name` FROM `friends` WHERE ((`friend_id` = " + User_id + ") AND (status = 1) AND (status_notification = 0))";
                    MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                    myConnection.Open();
                    MySqlDataReader myDataReader = myCommand.ExecuteReader();
                    while (myDataReader.Read())
                    {
                        updateNotificationStatus(myDataReader.GetInt16(0), User_id);
                        MyEvent(this, new MyEventArgs(new KeyValuePair<int, string>(1, myDataReader.GetString(1))));
                    }
                    myDataReader.Close();
                    myConnection.Close();
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
                Thread.Sleep(3000);
                try
                {
                    MySqlConnection myConnection = new MySqlConnection("Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111");
                    string requestSQL = "SELECT `friend_id`, `friend_name` FROM `friends` WHERE ((`user_id` = " + User_id + ") AND (status = 2) AND (status_notification = 0))";
                    MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                    myConnection.Open();
                    MySqlDataReader myDataReader = myCommand.ExecuteReader();
                    while (myDataReader.Read())
                    {
                        updateNotificationStatus(User_id, myDataReader.GetInt16(0));
                        MyEvent(this, new MyEventArgs(new KeyValuePair<int, string>(2, myDataReader.GetString(1))));
                    }
                    myDataReader.Close();
                    myConnection.Close();
                }
                catch
                {
                    //--
                }
            }
        }

        private void updateNotificationStatus(int user_id, int friend_id)
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection("Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111");
                string requestSQL = "UPDATE `friends` SET `status_notification` = 1 WHERE ((`user_id` = " + user_id +
                    ") AND (`friend_id` = '" + friend_id + "'))";
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
