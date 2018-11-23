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
            var formLogIn = new FormLogIn();
            formLogIn.Show();
        }
    }
}
