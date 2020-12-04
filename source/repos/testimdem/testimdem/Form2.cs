using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace testimdem
{

    public partial class Form2 : Form
    {
        private SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-TMSUA1O;Initial Catalog=ClientList;Integrated Security=True");
        private SqlDataAdapter sd = null;
        private DataTable dt = null;
        private SqlCommand cmd;

        private int PgSz = 100;
        int PgNb = 0;

        
        
        public Form2()
        {
            InitializeComponent();
            
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            using (SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-TMSUA1O;Initial Catalog=ClientList;Integrated Security=True"))
            {
                GetSql();
                sd = new SqlDataAdapter(cmd);

                dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[1].ReadOnly = true;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            sd = new SqlDataAdapter(cmd);
            cmd = new SqlCommand("SELECT * FROM * ", cn);
            comboBox1.Items.Add("10");
            comboBox1.Items.Add("20");
            comboBox1.Items.Add("50");
            comboBox1.Items.Add("100");
        }
        private void CalculateTotalPage()
        {
            

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                PgSz = 10;
                GetSql();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                PgSz = 20;
                GetSql();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                PgSz = 30;
                GetSql();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                PgSz = 40;
                GetSql();
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count < PgSz) return;

            PgNb++;
            using (SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-TMSUA1O;Initial Catalog=ClientList;Integrated Security=True"))
            {
                GetSql();
                sd = new SqlDataAdapter(cmd);

                dt.Rows.Clear();

                sd.Fill(dt);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (PgNb == 0) return;
            PgNb--;

            using (SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-TMSUA1O;Initial Catalog=ClientList;Integrated Security=True"))
            {
                GetSql();
                sd = new SqlDataAdapter(cmd);

                dt.Rows.Clear();

                sd.Fill(dt);
            }
        }
        private void GetSql()
        {
            cmd = new SqlCommand("SELECT ID AS Идентификатор, Name AS Пол, FirstName AS Имя, LastName as Фамилия, Patronymic as Отчество, " +
                "Birthday as [Дата Рождения], RegistrationDate as [Дата Добавления], Email as Email, Phone as Телефон from Client, " +
                "Gender ORDER BY ID OFFSET ((" + PgNb + ") * " + PgSz + ") " +
                "ROWS FETCH NEXT " + PgSz + "ROWS ONLY", cn);
            return;
        }
    }
}
