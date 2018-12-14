using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace bestAPPever
{
    class NewRegLogInClass
    {
        string Connect = "Database=bestAPPever;Data Source=195.114.3.231;User Id=tamagochi;Password=111";
        MySqlConnection myConnection;
        string requestSQL;
        MySqlCommand myCommand;
        public List<KeyValuePair<string, object>> tryToLogIn(string login, string pass)
        {
            List<string> outs = new List<string>
            {
                "Успешный вход",
                "Пользователя не существует\nлибо логин и пароль\nвведены не верно"
            };

            List<KeyValuePair<string, object>> pairs = new List<KeyValuePair<string, object>>();
            if (checkValidLoginPass(login, pass))
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://h6429.ptzhost.net/php/tryToLogIn.php");
                    var postData = "login=" + login;
                    postData += "&pass=" + pass;
                    var data = Encoding.ASCII.GetBytes(postData);

                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.Method = "POST";

                    using (var stream = httpWebRequest.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        MatchCollection mc = new Regex(@"""(\w*)"":""(\w*)""").Matches(result);
                        foreach (Match m in mc)
                        {
                            pairs.Add(new KeyValuePair<string, object>(m.Groups[1].Value, m.Groups[2].Value));
                        }
                        pairs[0] = new KeyValuePair<string, object>(pairs[0].Key, outs[short.Parse(pairs[0].Value.ToString())]);
                    }
                }
                catch (Exception ex)
                {
                    pairs.Add(new KeyValuePair<string, object>("msg", ex.Message));
                    pairs.Add(new KeyValuePair<string, object>("logIn", false));
                    pairs.Add(new KeyValuePair<string, object>("user_id", -1));
                    pairs.Add(new KeyValuePair<string, object>("hasPers", false));
                }
                return pairs;
            }
            else
            {
                pairs.Add(new KeyValuePair<string, object>("msg", log));
                pairs.Add(new KeyValuePair<string, object>("logIn", false));
                pairs.Add(new KeyValuePair<string, object>("user_id", -1));
                pairs.Add(new KeyValuePair<string, object>("hasPers", false));
                return pairs;
            }
        }
        
        private bool checkLoginExist(string login)
        {
            try
            {
                myConnection = new MySqlConnection(Connect);
                requestSQL = "SELECT `name` FROM `users` WHERE `name` = '" + login + "'";
                myCommand = new MySqlCommand(requestSQL, myConnection);
                myConnection.Open();
                var responseSQL = myCommand.ExecuteScalar();
                if (responseSQL != null)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        
        public List<KeyValuePair<string, object>> regisrUser(string login, string pass)
        {
            List<string> outs = new List<string>
            {
                "Регистрация успешна",
                "Пользователь уже существует"
            };

            List<KeyValuePair<string, object>> pairs = new List<KeyValuePair<string, object>>();
            if (checkValidLoginPass(login, pass))
            {
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://h6429.ptzhost.net/php/registerUser.php");
                    var postData = "login=" + login;
                    var data = Encoding.ASCII.GetBytes(postData);

                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.Method = "POST";

                    using (var stream = httpWebRequest.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                        MatchCollection mc = new Regex(@"""(\w*)"":""(\w*)""").Matches(result);
                        foreach (Match m in mc)
                        {
                            pairs.Add(new KeyValuePair<string, object>(m.Groups[1].Value, m.Groups[2].Value));
                        }
                        pairs[0] = new KeyValuePair<string, object>(pairs[0].Key, outs[short.Parse(pairs[0].Value.ToString())]);
                    }
                }
                catch(Exception ex)
                {
                    pairs.Add(new KeyValuePair<string, object>("msg", ex.Message));
                    pairs.Add(new KeyValuePair<string, object>("logIn", false));
                    pairs.Add(new KeyValuePair<string, object>("user_id", -1));
                }
                return pairs;
            }
            else
            {
                pairs.Add(new KeyValuePair<string, object>("msg", log));
                pairs.Add(new KeyValuePair<string, object>("logIn", false));
                pairs.Add(new KeyValuePair<string, object>("user_id", -1));
                return pairs;
            }
        }

        //Проверка валидности логина/пароля
        string log = "";
        private bool checkValidLoginPass(string login, string password)
        {
            bool check = true;
            string symbols = "\"+,'{}[]()-*&?^:\\/%$;#№@!`~<>| ";
            foreach (char ch in login)
            {
                if (symbols.Contains(ch.ToString()))
                {
                    log += "Вы ввели недопустимый символ в имени\n";
                    check = false;
                    break;
                }
            }
            if ((login == "") || (login.Length < 3))
            {
                log += "Имя должно содержать больше 3-х символов\n";
                check = false;
            }
            if (password.Contains(" "))
            {
                log += "Пароль не может содержать пробелов\n";
                check = false;
            }

            if ((password == "") || (password.Length < 6))
            {
                log += "Пароль должен состоять из 6-сьти и более символов\n";
                check = false;
            }
            return check;
        }
    }
}