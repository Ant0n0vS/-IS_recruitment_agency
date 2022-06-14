using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Recruitment_agency
{
    class Spreadsheet
    {
        private Database dataBase = new Database();
        private List<int> nums = new List<int>();

        public void LoadData(DataGridView dataGridView1)
        {
            dataBase.openConnection();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM Clients", dataBase.getConnection());
            List<string[]> data = new List<string[]>();
            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                data.Add(new string[11]);
                nums.Add(Convert.ToInt32(sqlReader[0]));
                for (int i = 0; i < 11; i++)
                {
                    if (i == 1 || i == 3)
                        data[data.Count - 1][i] = $"{Convert.ToDateTime(sqlReader[i]):u}".Substring(0, 10);
                    else
                        data[data.Count - 1][i] = sqlReader[i].ToString().Trim();
                }
            }
            sqlReader.Close();
            dataBase.closeConnection();

            dataGridView1.Rows.Clear();
            foreach (string[] line in data)
                dataGridView1.Rows.Add(line);
        }

        public void ChangeData(DataGridView dataGridView1)
        {
            dataBase.openConnection();
            int rows = dataGridView1.Rows.Count;

            for (int i = 0; i < rows - 1; i++)
            {
                SqlCommand command1 = new SqlCommand("DELETE FROM Clients WHERE [Регистрационный номер] = @Num", dataBase.getConnection());
                SqlCommand command2 = new SqlCommand("INSERT INTO Clients ([Регистрационный номер], [Дата регистрации], ФИО, " +
                    "[Дата рождения], Пол, Адрес, Образование, Специальность, [Иностранный язык], [Степень владения ПК], " +
                    "[Наличие автомобиля]) " +
                    "VALUES(@Num, @RegDate, @Name, @Birthday, @Gender, @Address, @Edu, @Spec, @Lang, @PC, @Car)", dataBase.getConnection());
                command1.Parameters.AddWithValue("Num", Convert.ToInt32(dataGridView1[0, i].Value));
                nums.Remove(Convert.ToInt32(dataGridView1[0, i].Value));
                command1.ExecuteNonQuery();

                command2.Parameters.AddWithValue("Num", Convert.ToInt32(dataGridView1[0, i].Value));
                command2.Parameters.AddWithValue("RegDate", Convert.ToDateTime(dataGridView1[1, i].Value.ToString().Replace(".", "/")));
                command2.Parameters.AddWithValue("Name", dataGridView1[2, i].Value);
                command2.Parameters.AddWithValue("Birthday", Convert.ToDateTime(dataGridView1[3, i].Value.ToString().Replace(".", "/")));
                command2.Parameters.AddWithValue("Gender", dataGridView1[4, i].Value);
                command2.Parameters.AddWithValue("Address", dataGridView1[5, i].Value);
                command2.Parameters.AddWithValue("Edu", dataGridView1[6, i].Value);
                command2.Parameters.AddWithValue("Spec", dataGridView1[7, i].Value);
                command2.Parameters.AddWithValue("Lang", dataGridView1[8, i].Value);
                command2.Parameters.AddWithValue("PC", dataGridView1[9, i].Value);
                command2.Parameters.AddWithValue("Car", Convert.ToBoolean(dataGridView1[10, i].Value));
                command2.ExecuteNonQuery();
            }
            foreach (var num in nums)
            {
                SqlCommand command3 = new SqlCommand("DELETE FROM Clients WHERE [Регистрационный номер] = @Num", dataBase.getConnection());
                command3.Parameters.AddWithValue("Num", num);
                command3.ExecuteNonQuery();
            }
            dataBase.closeConnection();
        }
    }
}
