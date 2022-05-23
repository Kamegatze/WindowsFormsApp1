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
    public partial class AccessLevelChange : Form
    {
        public AccessLevelChange()
        {
            InitializeComponent();
        }

        private void AccessLevelChange_Load(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UsersForm usersForm = new UsersForm();
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add("Файловый сервер", usersForm.listofUsers[listBox2.SelectedIndex].rights.fileServer.ToString());
            dataGridView1.Rows.Add("Сервер баз данных", usersForm.listofUsers[listBox2.SelectedIndex].rights.databaseServer.ToString());
            dataGridView1.Rows.Add("Сервер документов", usersForm.listofUsers[listBox2.SelectedIndex].rights.documentServer.ToString());
            dataGridView1.Rows.Add("Web - сервер", usersForm.listofUsers[listBox2.SelectedIndex].rights.webServer.ToString());
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
