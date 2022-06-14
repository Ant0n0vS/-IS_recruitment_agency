using System;
using System.Windows.Forms;

namespace Recruitment_agency
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const string login = "admin";
            const string password = "qwerty";
            if (textBox1.Text == login && textBox2.Text == password)
            {
                MainForm f = new MainForm(true);
                f.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Неправильный логин или пароль !");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm f = new MainForm(false);
            f.Show();
            this.Hide();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
