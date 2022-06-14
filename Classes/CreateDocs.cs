using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlClient;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace Recruitment_agency
{
    class CreateDocs
    {
        private Database dataBase = new Database();
        private List<string> GetDataList(string name)
        {
            dataBase.openConnection();
            List<string> dataList = new List<string>();
            SqlCommand command = new SqlCommand("SELECT * FROM " + name, dataBase.getConnection());
            SqlDataReader sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                dataList.Add(sqlReader.GetString(0));
            }
            sqlReader.Close();
            dataBase.closeConnection();
            return dataList;
        }

        public void CreateFirstDoc()
        {
            List<string> speciality = GetDataList("Speciality");
            Document doc = new Document();
            Section section = doc.AddSection();
            string path = @"C:\Users\succe\Desktop";
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowDialog();
            if (FBD.SelectedPath != "")
                path = FBD.SelectedPath;
            string fileName = path + @"\Document1.docx";
            String[] Header = { "№ п/п", "Иностранный язык", "Количество" };
            dataBase.openConnection();
            foreach (var item in speciality)
            {
                SqlCommand command = new SqlCommand("SELECT [Иностранный язык], " +
                    "COUNT([Иностранный язык]) AS Количество " +
                    "FROM Clients WHERE Специальность = @spec " +
                    "GROUP BY [Иностранный язык]", dataBase.getConnection());
                command.Parameters.AddWithValue("Spec", item);
                command.ExecuteNonQuery();
                SqlDataReader sqlReader = command.ExecuteReader();
                if (!sqlReader.Read())
                {
                    sqlReader.Close();
                    continue;
                }
                Paragraph para = section.AddParagraph();
                para.AppendText("\n");
                para.AppendText(item);
                Table table = section.AddTable(true);
                table.ResetCells(1, Header.Length);
                TableRow FRow = table.Rows[0];
                FRow.IsHeader = true;
                FRow.Height = 23;
                for (int i = 0; i < Header.Length; i++)
                {
                    Paragraph p = FRow.Cells[i].AddParagraph();
                    FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    TextRange TR = p.AppendText(Header[i]);
                }
                int r = 0;
                do
                {
                    table.AddRow();
                    TableRow DataRow = table.Rows[r + 1];
                    DataRow.Height = 20;
                    DataRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    Paragraph p2 = DataRow.Cells[0].AddParagraph();
                    TextRange TR2 = p2.AppendText((r + 1).ToString());
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    DataRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p2 = DataRow.Cells[1].AddParagraph();
                    TR2 = p2.AppendText(sqlReader["Иностранный язык"].ToString());
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    DataRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p2 = DataRow.Cells[2].AddParagraph();
                    TR2 = p2.AppendText(sqlReader["Количество"].ToString());
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    r++;
                }
                while (sqlReader.Read());
                sqlReader.Close();
            }
            doc.SaveToFile(fileName, FileFormat.Docx);
            System.Diagnostics.Process.Start(fileName);
        }

        public void CreateSecondDoc()
        {
            List<string> speciality = GetDataList("Speciality");
            Document doc = new Document();
            Section section = doc.AddSection();
            string path = @"C:\Users\succe\Desktop";
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowDialog();
            if (FBD.SelectedPath != "")
                path = FBD.SelectedPath;
            string fileName = path + @"\Document2.docx";
            String[] Header = { "№ п/п", "Образование", "Количество", "Мужчин", "Женщин" };
            dataBase.openConnection();
            foreach (var item in speciality)
            {
                SqlCommand command = new SqlCommand("SELECT	Образование, " +
                    "COUNT([Образование]) AS Количество, " +
                    "COUNT(CASE Пол WHEN 'муж' THEN 1 ELSE NULL END) AS Мужчин, " +
                    "COUNT(CASE Пол WHEN 'жен' THEN 1 ELSE NULL END) AS Женщин " +
                    "FROM Clients WHERE Специальность = @spec " +
                    "GROUP BY[Образование]", dataBase.getConnection());
                command.Parameters.AddWithValue("Spec", item);
                command.ExecuteNonQuery();
                SqlDataReader sqlReader = command.ExecuteReader();
                if (!sqlReader.Read())
                {
                    sqlReader.Close();
                    continue;
                }
                Paragraph para = section.AddParagraph();
                para.AppendText("\n");
                para.AppendText(item);
                Table table = section.AddTable(true);
                table.ResetCells(1, Header.Length);
                TableRow FRow = table.Rows[0];
                FRow.IsHeader = true;
                FRow.Height = 23;
                for (int i = 0; i < Header.Length; i++)
                {
                    Paragraph p = FRow.Cells[i].AddParagraph();
                    FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    TextRange TR = p.AppendText(Header[i]);
                }
                int r = 0;
                do
                {
                    table.AddRow();
                    TableRow DataRow = table.Rows[r + 1];
                    DataRow.Height = 20;
                    DataRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    Paragraph p2 = DataRow.Cells[0].AddParagraph();
                    TextRange TR2 = p2.AppendText((r + 1).ToString());
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    DataRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p2 = DataRow.Cells[1].AddParagraph();
                    TR2 = p2.AppendText(sqlReader["Образование"].ToString());
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    DataRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p2 = DataRow.Cells[2].AddParagraph();
                    TR2 = p2.AppendText(sqlReader["Количество"].ToString());
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    DataRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p2 = DataRow.Cells[3].AddParagraph();
                    TR2 = p2.AppendText(sqlReader["Мужчин"].ToString());
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    DataRow.Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p2 = DataRow.Cells[4].AddParagraph();
                    TR2 = p2.AppendText(sqlReader["Женщин"].ToString());
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    r++;
                }
                while (sqlReader.Read());
                sqlReader.Close();
            }
            doc.SaveToFile(fileName, FileFormat.Docx);
            System.Diagnostics.Process.Start(fileName);
        }

        public void CreateThirdDoc()
        {
            List<string> speciality = GetDataList("Speciality");
            Document doc = new Document();
            Section section = doc.AddSection();
            string path = @"C:\Users\succe\Desktop";
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowDialog();
            if (FBD.SelectedPath != "")
                path = FBD.SelectedPath;
            string fileName = path + @"\Document3.docx";
            String[] Header = { "Пол", "Моложе 35", "Старше 35" };
            String[] Gender = { "Муж", "Жен" };
            dataBase.openConnection();
            foreach (var item in speciality)
            {
                SqlCommand command = new SqlCommand("SELECT	COUNT(CASE WHEN CAST(DATEDIFF(hh, [Дата рождения], " +
                    "GETDATE()) / 8766 AS int) < 35 THEN 1 ELSE NULL END) AS меньше, " +
                    "COUNT(CASE WHEN CAST(DATEDIFF(hh, [Дата рождения], GETDATE()) / 8766 AS int) > 35 THEN 1 ELSE NULL END) AS больше " +
                    "FROM Clients WHERE[Специальность] = @spec AND[Пол] = 'муж' " +
                    "UNION ALL " +
                    "SELECT  COUNT(CASE WHEN CAST(DATEDIFF(hh, [Дата рождения], GETDATE()) / 8766 AS int) < 35 THEN 1 ELSE NULL END) AS меньше, " +
                    "COUNT(CASE WHEN CAST(DATEDIFF(hh, [Дата рождения], GETDATE()) / 8766 AS int) > 35 THEN 1 ELSE NULL END) AS больше " +
                    "FROM Clients WHERE[Специальность] = @spec AND[Пол] = 'жен'", dataBase.getConnection());
                command.Parameters.AddWithValue("Spec", item);
                command.ExecuteNonQuery();
                SqlDataReader sqlReader = command.ExecuteReader();
                if (!sqlReader.Read())
                {
                    sqlReader.Close();
                    continue;
                }
                Paragraph para = section.AddParagraph();
                para.AppendText("\n");
                para.AppendText(item);
                Table table = section.AddTable(true);
                table.ResetCells(1, Header.Length);
                TableRow FRow = table.Rows[0];
                FRow.IsHeader = true;
                FRow.Height = 23;
                for (int i = 0; i < Header.Length; i++)
                {
                    Paragraph p = FRow.Cells[i].AddParagraph();
                    FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    TextRange TR = p.AppendText(Header[i]);
                }
                int r = 0;
                do
                {
                    table.AddRow();
                    TableRow DataRow = table.Rows[r + 1];
                    DataRow.Height = 20;
                    DataRow.Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    Paragraph p2 = DataRow.Cells[0].AddParagraph();
                    TextRange TR2 = p2.AppendText(Gender[r]);
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    DataRow.Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p2 = DataRow.Cells[1].AddParagraph();
                    TR2 = p2.AppendText(sqlReader["меньше"].ToString());
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    DataRow.Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p2 = DataRow.Cells[2].AddParagraph();
                    TR2 = p2.AppendText(sqlReader["больше"].ToString());
                    p2.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                    r++;
                }
                while (sqlReader.Read());
                sqlReader.Close();
            }
            doc.SaveToFile(fileName, FileFormat.Docx);
            System.Diagnostics.Process.Start(fileName);
        }

        public static void CreateExcel(DataGridView gridView)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook ExcelWorkBook;
            Worksheet ExcelWorkSheet;
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            ExcelWorkSheet = (Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
            for (int i = 1; i <= gridView.Rows.Count; i++)
            {
                for (int j = 0; j < gridView.ColumnCount; j++)
                {
                    ExcelApp.Cells[i + 1, j + 1] = gridView.Rows[i - 1].Cells[j].Value;
                    if (i == 1)
                        ExcelApp.Cells[1, j + 1] = gridView.Columns[j].HeaderText;
                }
            }
            ExcelApp.Columns.AutoFit();
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }

        public static void CreateTxt(DataGridView gridView)
        {
            string path = @"C:\Users\succe\Desktop";
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.ShowDialog();
            if (FBD.SelectedPath != "")
                path = FBD.SelectedPath;
            string fileName = path + @"\data.txt";
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs);
            int[] maxLenghts = new int[11];
            for (int j = 0; j < gridView.Columns.Count; j++)
            {
                maxLenghts[j] = gridView[j, 0].Value.ToString().Length;
                for (int i = 0; i < gridView.Rows.Count; i++)
                {
                    int l = gridView[j, i].Value.ToString().Length;
                    if (maxLenghts[j] < l)
                        maxLenghts[j] = l;
                }
            }
            try
            {
                for (int j = 0; j < gridView.Rows.Count; j++)
                {
                    for (int i = 0; i < gridView.Rows[j].Cells.Count; i++)
                    {
                        if (i == 1 || i == 3)
                            streamWriter.Write(gridView.Rows[j].Cells[i].Value.ToString().Substring(0,10) + "   ");
                        else
                            streamWriter.Write(gridView.Rows[j].Cells[i].Value.ToString().PadRight(maxLenghts[i]) + "   ");
                    }                     
                    streamWriter.WriteLine();
                }
                streamWriter.Close();
                fs.Close();
                MessageBox.Show("Файл успешно сохранен");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении файла!");
            }       
        }
    }
}
