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
            this.Controls.Add(new CreateObjects().createButton("LogIn", new System.Drawing.Point(12, 60)));
            this.Controls.Add(new CreateObjects().createButton("Registration", new System.Drawing.Point(12, 85)));


            Button buttonLogIn = (Button)this.Controls.Find("buttonLogIn", true).GetValue(0);
            buttonLogIn.Click += buttonOk_Click;

            Button buttonReg = (Button)this.Controls.Find("buttonRegistration", true).GetValue(0);
            buttonReg.Click += ButtonReg_Click;
        }

        private void ButtonReg_Click(object sender, System.EventArgs e)
        {
            this.Controls.RemoveByKey("labelLog");
            RegLogInClass reglogInClass = new RegLogInClass();
            this.Controls.Add(new CreateObjects().createLabel("Log", reglogInClass.regisrUser(((TextBox)this.Controls.Find("textBoxLogin", true).GetValue(0)).Text, ((TextBox)this.Controls.Find("textBoxPass", true).GetValue(0)).Text), new System.Drawing.Point(12, 110)));
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            this.Controls.RemoveByKey("labelLog");
            RegLogInClass reglogInClass = new RegLogInClass();
            this.Controls.Add(new CreateObjects().createLabel("Log", reglogInClass.checkLogin(((TextBox)this.Controls.Find("textBoxLogin", true).GetValue(0)).Text, ((TextBox)this.Controls.Find("textBoxPass", true).GetValue(0)).Text), new System.Drawing.Point(12, 110)));
        }

        private void FormFirst_Load(object sender, System.EventArgs e)
        {

        }
    }
}
