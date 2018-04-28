using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();

        }

        private void button2_Click(object sender, EventArgs e)
        {
         SqlConnection con = new SqlConnection("Data Source=DESKTOP-DV19EEN\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            con.Open();
            bool status = false;
            if(comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            var sqlquery = "";
            if(IfProductsExists(con, textBox1.Text))
            {
                sqlquery = @"UPDATE [dbo].[Products]
   SET[Product Name] ='" + textBox2.Text + "',[Status] = '" + status + "'WHERE  [Product Code] = '" + textBox1.Text + "'";
            }
            else
            {
                sqlquery = @"INSERT INTO [dbo].[Products]
           ([Product Code]
           ,[Product Name]
           ,[Status])
                VALUES
           ('" + textBox1.Text + "','" + textBox2.Text + "','" + status + "')";
            }
            SqlCommand cmd = new SqlCommand(sqlquery, con);
            cmd.ExecuteNonQuery();
           con.Close();
            LoadData();
           
                 }
        private bool IfProductsExists(SqlConnection con, String ProductCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("select 1 from [Products] where [Product Code]='"+ ProductCode +"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public void LoadData()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-DV19EEN\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter("select * from [stock]. [dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Product Code"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Product Name"].ToString();
                if ((bool)item["Status"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString()=="Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
      SqlConnection con = new SqlConnection("Data Source=DESKTOP-DV19EEN\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            var sqlquery = "";
            if (IfProductsExists(con, textBox1.Text))
            {
                con.Open();
                sqlquery = @"Delete From [Products] WHERE  [Product Code] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                cmd.ExecuteNonQuery();
                con.Close();
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Record Not Exists......!");
            }
            
            LoadData();
        }
    }
    }
   
