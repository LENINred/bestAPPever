using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace bestAPPever
{
    class MessagingListener
    {
        public event EventHandler<MessagingEventArgs> MessagingEvent;
        private int To_id, From_id;
        private string From_login, To_login;
        public void Method(int to_id, int from_id, string to_login, string from_login)
        {
            To_id = to_id;
            From_id = from_id;
            From_login = from_login;
            To_login = to_login;

            Thread myThread = new Thread(getNewMessages);
            myThread.IsBackground = true;
            myThread.Start();
        }

        private void getNewMessages()
        {
            while (true)
            {
                Thread.Sleep(3000);
                try
                {
                    MySqlConnection myConnection = new MySqlConnection("Database=messages;Data Source=195.114.3.231;User Id=tamagochi_m;Password=111");
                    string requestSQL = "SELECT `message`, `date_send`, `from_who` FROM `" + To_login + "_messages` WHERE `status` = 0 AND `from_who` = " + From_id;
                    MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                    myConnection.Open();
                    MySqlDataReader myDataReader = myCommand.ExecuteReader();
                    List<KeyValuePair<int, string>> messages_ids = new List<KeyValuePair<int, string>>();
                    while (myDataReader.Read())
                    {
                        messages_ids.Add(new KeyValuePair<int, string>(myDataReader.GetInt16(2), myDataReader.GetString(0)));
                    }
                    myDataReader.Close();
                    myConnection.Close();
                    updateMessageStatus();
                    MessagingEvent(this, new MessagingEventArgs(messages_ids));
                }
                catch
                {
                    //--
                }
            }
        }

        private void updateMessageStatus()
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection("Database=messages;Data Source=195.114.3.231;User Id=tamagochi_m;Password=111");
                string requestSQL = "UPDATE `" + To_login + "_messages` SET `status` = 1 WHERE ((`from_who` = " + From_id +
                    ") AND (`to_whom` = " + To_id + ") AND (`status` = 0))";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();

                requestSQL = "UPDATE `" + From_login + "_messages` SET `status` = 1 WHERE ((`from_who` = " + To_id +
                    ") AND (`to_whom` = " + From_id + ") AND (`status` = 0))";
                myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                myCommand.ExecuteScalar();
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class MessagingEventArgs : EventArgs
    {
        public List<KeyValuePair<int, string>> Messages_ids { get; set; }

        public MessagingEventArgs(List<KeyValuePair<int, string>> messages_ids)
        {
            Messages_ids = messages_ids;
        }
    }
}
