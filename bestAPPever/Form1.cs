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

            this.Controls.Add(new CreateObjects().createTextBox("Login", new System.Drawing.Point(12, 12)));
            this.Controls.Add(new CreateObjects().createTextBox("Pass", new System.Drawing.Point(12, 36)));
            this.Controls.Add(new CreateObjects().createButton("Ok", new System.Drawing.Point(12, 60)));


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

        private void FormFirst_Load(object sender, System.EventArgs e)
        {

        }
    }
}
