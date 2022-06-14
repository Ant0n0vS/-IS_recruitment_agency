using System;
using System.Windows.Forms;


namespace Recruitment_agency
{
    public partial class MainForm : Form
    {
        private bool flag = false;
        
        private void button1_Click(object sender, EventArgs e)
        {
            Spreadsheet table = new Spreadsheet();
            table.ChangeData(dataGridView1);
            table.LoadData(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreateDocs doc = new CreateDocs();
            doc.CreateFirstDoc();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateDocs doc = new CreateDocs();
            doc.CreateSecondDoc();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CreateDocs doc = new CreateDocs();
            doc.CreateThirdDoc();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Spreadsheet table = new Spreadsheet();
            table.LoadData(dataGridView1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FilterForm f = new FilterForm(flag);
            f.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoginForm f = new LoginForm();
            f.Show();
            this.Hide();
        }

        public MainForm(bool data)
        {
            InitializeComponent();
            flag = data;
            dataGridView1.ReadOnly = !flag;
            dataGridView1.AllowUserToDeleteRows = flag;
            dataGridView1.AllowUserToAddRows = flag;
            button1.Visible = flag;
            button2.Visible = flag;
            button3.Visible = flag;
            button4.Visible = flag;
            button7.Visible = !flag;
            label2.Visible = flag;
            label3.Visible = flag;
            label4.Visible = flag;
            label12.Visible = flag;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
