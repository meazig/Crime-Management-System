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

namespace crime_manag
{
    public partial class cases : Form
    {
        public cases()
        {
            InitializeComponent();
            ShowCases();
            OffnameTb.Text = Login.OffName;
            GetCriminals();
            CountCases();
        }
        int key = 0;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\crime.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowCases()

        {
            con.Open();
            string Query = "Select * from CaseTb1";
            SqlDataAdapter adp = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            var ds = new DataSet();
            adp.Fill(ds);
            CasesDGV.DataSource = ds.Tables[0];

            con.Close();
        }
        private void GetCriminals()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from CriminalTb1",con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CrCode", typeof(int));
            dt.Load(reader);
            CriminalCb.ValueMember = "CrCode";
            CriminalCb.DataSource = dt;
            con.Close();
            
        }
        private void GetCriminalName()
        {
            con.Open();
            string query = "select * from CriminalTb1 where CrCode=" + CriminalCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                CrimNameTb.Text = dr["CrName"].ToString();
            }


            con.Close();
        }
        private void label13_Click(object sender, EventArgs e)
        {
            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       private void Rest()
        {
            TypeCb.SelectedIndex = -1;
            CaseheadTb.Text = "";
            CaseDetailsTb.Text = "";
            CriminalCb.Text = "";
            CrimNameTb.Text = "";
            PlaceTb.Text = "";
            Date.Text = "";

            key = 0;

        }
        private void RecordBtn_Click(object sender, EventArgs e)
            {
            if (TypeCb.SelectedIndex== -1 || CaseheadTb.Text == "" || CaseDetailsTb.Text == "" || CriminalCb.Text == "" || CrimNameTb.Text == "" || PlaceTb.Text == "" || Date.Text == "") 
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CaseTb1(Ctype,CHading,CDetails,Cplace,CDate,Cperson,CpersonName,polname)values(@Ct,@Ch,@Cde,@Cp,@Cd,@Cpe,@Cpn,@Pon)", con);

                    cmd.Parameters.AddWithValue("@Ct", TypeCb.SelectedItem.ToString ());
                    cmd.Parameters.AddWithValue("@Ch", CaseheadTb.Text);
                    cmd.Parameters.AddWithValue("@Cde", CaseDetailsTb.Text);
                    cmd.Parameters.AddWithValue("@Cp", PlaceTb.Text);
                    cmd.Parameters.AddWithValue("@Cd", Date.Value.Date);
                    cmd.Parameters.AddWithValue("@Cpe", CriminalCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Cpn", CrimNameTb.Text);
                    cmd.Parameters.AddWithValue("@Pon", OffnameTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Case is Recorded!");
                    con.Close();
                    ShowCases();
                    Rest();


                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }


        }

        private void CriminalCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCriminalName();
        }
        
        private void EditBtn_Click(object sender, EventArgs e)
        {


            if (TypeCb.SelectedIndex == -1 || CaseheadTb.Text == "" || CaseDetailsTb.Text == "" || CriminalCb.Text == "" || CrimNameTb.Text == "" || PlaceTb.Text == "" || Date.Text == "")
            {
                MessageBox.Show("first select edited case!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update CaseTb1 set Ctype=@Ct,CHading=@Ch,CDetails=@Cde,Cplace=@Cp,CDate=@Cd,Cperson=@Cpe,CpersonName=@Cpn,polname=@Pon where CNum=@Key", con);
                    cmd.Parameters.AddWithValue("@Key", key);
                    cmd.Parameters.AddWithValue("@Ct", TypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Ch", CaseheadTb.Text);
                    cmd.Parameters.AddWithValue("@Cde", CaseDetailsTb.Text);
                    cmd.Parameters.AddWithValue("@Cp", PlaceTb.Text);
                    cmd.Parameters.AddWithValue("@Cd", Date.Value.Date);
                    cmd.Parameters.AddWithValue("@Cpe", CriminalCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Cpn", CrimNameTb.Text);
                    cmd.Parameters.AddWithValue("@Pon", OffnameTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("case is updated!");
                    con.Close();
                    ShowCases();
                    Rest();


                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }

        }
       
        private void CasesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                TypeCb.SelectedItem = CasesDGV.CurrentRow.Cells[1].Value.ToString();
                CaseheadTb.Text = CasesDGV.CurrentRow.Cells[2].Value.ToString();
                CaseDetailsTb.Text = CasesDGV.CurrentRow.Cells[3].Value.ToString();
                PlaceTb.Text = CasesDGV.CurrentRow.Cells[4].Value.ToString();
                Date.Text = CasesDGV.CurrentRow.Cells[5].Value.ToString();
                CriminalCb.SelectedItem = CasesDGV.CurrentRow.Cells[6].Value.ToString();
                CrimNameTb.Text = CasesDGV.CurrentRow.Cells[7].Value.ToString();
                if (CaseheadTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(CasesDGV.CurrentRow.Cells[0].Value.ToString());
                }

            }
            catch (Exception Ex)

            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Please Select Case!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CaseTb1 Where CNum=@CKey", con);
                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Are you sure case is deleted!");
                    con.Close();
                    ShowCases();
                    Rest();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            dashibored obj = new dashibored();
            obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            criminals obj = new criminals();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            charges obj = new charges();
            obj.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
        private void CountCases()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From CaseTb1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            labe.Text = dt.Rows[0][0].ToString() + " Cases";
            con.Close();
        }

        private void CriminalCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
    
 