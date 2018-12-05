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
        private List<string> users_request = new List<string>();

        public void Method(int user_id)
        {
            User_id = user_id;
            Thread myThread = new Thread(getFriendStatus);
            myThread.Start();
        }

        private void getFriendStatus()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(3000);
                try
                {
                    MySqlConnection myConnection = new MySqlConnection("Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111");
                    string requestSQL = "SELECT `user_name`, `status` FROM `friends` WHERE `friend_id` = " + User_id;
                    MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                    myConnection.Open();
                    MySqlDataReader myDataReader = myCommand.ExecuteReader();
                    while (myDataReader.Read())
                    {
                        if ((myDataReader.GetInt16(1) == 1) && (users_request.IndexOf(myDataReader.GetString(0)) == -1))
                        {
                            users_request.Add(myDataReader.GetString(0));
                            MyEvent(this, new MyEventArgs(new KeyValuePair<int, string>(myDataReader.GetInt16(1), myDataReader.GetString(0))));
                        }
                    }

                }
                catch (Exception ex)
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
