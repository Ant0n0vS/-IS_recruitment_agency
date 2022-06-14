using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace Recruitment_agency
{
    public partial class FilterForm : Form
    {
        private bool flag;

        public FilterForm(bool fl)
        {
            InitializeComponent();
            flag = fl;
        }

        private void filterData()
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"(Специальность LIKE " +
                $"'%{textBox3.Text}%' OR Специальность IS NULL) AND " +
                $"(Образование LIKE '%{textBox2.Text}%' OR Образование IS NULL) " +
                $"AND (Пол LIKE '%{textBox1.Text}%' OR Пол IS NULL) " +
                $"AND ([Степень владения ПК] LIKE '%{textBox4.Text}%' OR [Степень владения ПК] IS NULL) " +
                $"AND ([Дата рождения] > '{dateTimePicker1.Value}') " +
                $"AND ([Дата рождения] < '{dateTimePicker2.Value}')";
        }

        private void FilterForm_Load(object sender, EventArgs e)
        {
            Database dataBase = new Database();
            dataBase.openConnection();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM Clients", dataBase.getConnection());
            DataSet db = new DataSet();
            dataAdapter.Fill(db);
            dataGridView1.DataSource = db.Tables[0];
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            filterData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateDocs.CreateExcel(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = DateTime.Today;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateDocs.CreateTxt(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm f = new MainForm(flag);
            f.Show();
            this.Hide();
        }

        private void FilterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
