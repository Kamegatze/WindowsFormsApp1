using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AdministorFormsInput : Form
    {
        private string NameLogin = "Administrator";
        private string Password = "12345";
        public AdministorFormsInput()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AdministorFormsInput_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Password == textBox1.Text && NameLogin == textBox2.Text)
            {
                AccessLevelChange accessLevelChange = new AccessLevelChange();
                accessLevelChange.Show();
                this.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
