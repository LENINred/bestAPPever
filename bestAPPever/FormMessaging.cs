using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace bestAPPever
{
    public partial class FormMessaging : Form
    {
        string Login;
        int Id;
        KeyValuePair<int, string> choosenDialog = new KeyValuePair<int, string>();
        public FormMessaging(string login, int id)
        {
            InitializeComponent();
            Login = login;
            Id = id;

            loadDialogues();
            choosenDialog = new KeyValuePair<int, string>((int)groupBoxDialogues.Controls[0].Tag, groupBoxDialogues.Controls[0].Text);
            loadMessages(Login, Id, choosenDialog.Key);
            MessagingListener messagingListener = new MessagingListener();
            messagingListener.MessagingEvent += MessagingListener_MessagingEvent;
            messagingListener.Method(Id, choosenDialog.Key, Login, choosenDialog.Value);
        }

        public FormMessaging(string login, int id, string user_login, int user_id)
        {
            InitializeComponent();
            Login = login;
            Id = id;

            new MessagingClass().createDialog(login, id, user_login, user_id);
            new MessagingClass().createMessagesTable(login, id, user_login, user_id);
            groupBoxMessages.Text = "Переписка с " + user_login;

            loadDialogues();
            choosenDialog = new KeyValuePair<int, string>(user_id, user_login);
            loadMessages(Login, Id, choosenDialog.Key);
            MessagingListener messagingListener = new MessagingListener();
            messagingListener.MessagingEvent += MessagingListener_MessagingEvent;
            messagingListener.Method(Id, choosenDialog.Key, Login, choosenDialog.Value);
        }

        private void FormMessaging_Load(object sender, System.EventArgs e)
        {
            //--
        }

        private void MessagingListener_MessagingEvent(object sender, MessagingEventArgs e)
        {
            addNewMessage(e.Messages_ids);
        }

        private void addNewMessage(List<KeyValuePair<int, string>> messages_ids)
        {
            if (this.InvokeRequired)
            {
                Invoke(new MethodInvoker(() => addNewMessage(messages_ids)));
            }
            else
            {
                foreach (KeyValuePair<int, string> message_id in messages_ids)
                {
                    if (message_id.Key == choosenDialog.Key)
                    {
                        Label label_message = new Label();
                        label_message.AutoSize = true;
                        label_message.Font = new Font("Arial", 10, FontStyle.Bold);
                        label_message.Text = message_id.Value;
                        label_message.Tag = message_id.Key;
                        label_message.Dock = DockStyle.Left;
                        tableLayoutPanelMessages.Controls.Add(label_message, 0, tableLayoutPanelMessages.Controls.Count);
                        tableLayoutPanelMessages.ScrollControlIntoView(label_message);
                    }
                    else
                    {
                        //Оповещение о новом сообщении
                    }
                }
            }
        }

        private void loadDialogues()
        {            
            List<KeyValuePair<int, string>> listDialogues = new MessagingClass().getListDialogues(Login);
            int y = 15;
            foreach(KeyValuePair<int, string> dialog in listDialogues)
            {
                Label username = new Label();
                username.Text = dialog.Value;
                username.Tag = dialog.Key;
                username.AutoSize = true;
                username.Font = new Font("Arial", 18, FontStyle.Bold);
                username.Location = new System.Drawing.Point(10, y);
                y += 45;
                username.Click += Username_Click;
                groupBoxDialogues.Controls.Add(username);
            }
            this.Controls.Add(groupBoxDialogues);
        }

        private void Username_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            groupBoxMessages.Text = "Переписка с " + label.Text;
            choosenDialog = new KeyValuePair<int, string>((int)label.Tag, label.Text);
            loadMessages(Login, Id, (int)label.Tag);
        }

        TableLayoutPanel tableLayoutPanelMessages;
        public void loadMessages(string login, int id, int from_id)
        {
            try { tableLayoutPanelMessages.Dispose(); }
            catch
            { 
                //--
            }

            tableLayoutPanelMessages = new TableLayoutPanel();
            tableLayoutPanelMessages.AutoScroll = true;
            tableLayoutPanelMessages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tableLayoutPanelMessages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tableLayoutPanelMessages.Dock = DockStyle.Fill;
            groupBoxMessages.Controls.Add(tableLayoutPanelMessages);

            DataTable dataTableMessages = new MessagingClass().getTableMessages(login, id, from_id);
            
            foreach (DataRow row in dataTableMessages.AsEnumerable())
            {
                Label message = new Label();
                message.AutoSize = true;
                message.Font = new Font("Arial", 10, FontStyle.Bold);
                message.Text = row[1].ToString();
                if ((int)row[0] == id)
                {
                    message.Dock = DockStyle.Right;
                    tableLayoutPanelMessages.Controls.Add(message, 1, row.Table.Rows.IndexOf(row));
                }
                else
                {
                    message.Dock = DockStyle.Left;
                    tableLayoutPanelMessages.Controls.Add(message, 0, row.Table.Rows.IndexOf(row));
                }
            }
            tableLayoutPanelMessages.ScrollControlIntoView(tableLayoutPanelMessages.Controls[tableLayoutPanelMessages.Controls.Count - 1]);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            new MessagingClass().SendMessage(Login, Id, choosenDialog.Value, choosenDialog.Key, textBoxMessage.Text);
            Label label_message = new Label();
            label_message.AutoSize = true;
            label_message.Font = new Font("Arial", 10, FontStyle.Bold);
            label_message.Text = textBoxMessage.Text;
            label_message.Dock = DockStyle.Right;
            tableLayoutPanelMessages.Controls.Add(label_message, 1, tableLayoutPanelMessages.Controls.Count);
            tableLayoutPanelMessages.ScrollControlIntoView(label_message);
            textBoxMessage.Text = "";
        }
    }
}
