using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bestAPPever
{
    class ListUsers
    {
        string Connect = "Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111";

        public List<string> getListUsers()
        {
            try
            {
                MySqlConnection myConnection = new MySqlConnection(Connect);
                string requestSQL = "SELECT `name` FROM `tamagoches`";
                MySqlCommand myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                MySqlDataReader myDataReader = myCommand.ExecuteReader();
                List<string> name = new List<string>();
                while (myDataReader.Read())
                {
                    name.Add(myDataReader.GetString(0));
                    //name = myDataReader.GetString(0);                    
                }
                return name;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
