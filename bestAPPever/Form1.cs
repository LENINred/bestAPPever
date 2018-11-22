using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bestAPPever
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Комент ёобана
        bool a = true;
        private void button1_Click(object sender, EventArgs e)
        {
            if (a)
            {
                label1.Text = "Привет Илья, я кодер Саша";
                a = false;
                //dsds
            }
            else
            {
                label1.Text = "Зачем нажимать дважды а?!";
            }
        }
    }
}
