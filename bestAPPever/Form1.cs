using System.Windows.Forms;

namespace bestAPPever
{
    public partial class FormFirst : Form
    {
        public FormFirst()
        {
            InitializeComponent();
        }

        private void buttonRegLog_Click(object sender, System.EventArgs e)
        {
            buttonRegLog.Visible = false;

            this.Controls.Add(addTextBox("Login", new System.Drawing.Point(12, 12)));
            this.Controls.Add(addTextBox("Pass", new System.Drawing.Point(12, 36)));
            this.Controls.Add(addButton("Ok", new System.Drawing.Point(12, 60)));


            Button buttonOk = (Button)this.Controls.Find("buttonOk", true).GetValue(0);
            buttonOk.Click += buttonOk_Click;
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            this.Controls.RemoveByKey("labelLog");
            LogInClass logInClass = new LogInClass();
            this.Controls.Add(new Label()
            {
                Location = new System.Drawing.Point(12, 85),
                Name = "labelLog",
                AutoSize = true,
                Text = logInClass.LogRegSQL(((TextBox)this.Controls.Find("textBoxLogin", true).GetValue(0)).Text, ((TextBox)this.Controls.Find("textBoxPass", true).GetValue(0)).Text)  
            });
        }

        private TextBox addTextBox(string name, System.Drawing.Point point)
        {
            TextBox textBox = new TextBox();
            textBox.Name = "textBox" + name;
            textBox.Location = point;
            textBox.Size = new System.Drawing.Size(100, 12);
            return textBox;
        }

        private Button addButton(string name, System.Drawing.Point point)
        {
            Button button = new Button();
            button.Name = "button" + name;
            button.Text = name;
            button.Location = point;
            button.Size = new System.Drawing.Size(100, 23);
            return button;
        }

        private void FormFirst_Load(object sender, System.EventArgs e)
        {

        }
    }
}
