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
    public partial class charges : Form
    {
        public charges()
        {
            InitializeComponent();
            ShowCharegs();
          OfficeNTb.Text = Login.OffName;
            GetCase();
            Rest();
            CountCHarges();
        }
        int key = 0;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\crime.mdf;Integrated Security=True;Connect Timeout=30");
        private void ShowCharegs()

        {
            con.Open();
            string Query = "Select * from CharegsTb1";
            SqlDataAdapter adp = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            var ds = new DataSet();
            adp.Fill(ds);
            ChargesDGV.DataSource = ds.Tables[0];

            con.Close();
        }
        private void GetCase()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from CaseTb1", con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CNum", typeof(int));
            dt.Load(reader);
            CaseCb.ValueMember = "CNum";
            CaseCb.DataSource = dt;
            con.Close();

        }
        private void GetCaseName()
        {
            con.Open();
            string query = "select * from CaseTb1 where CNum=" + CaseCb.SelectedValue.ToString();
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
              CasehadeTb.Text = dr["CHading"].ToString();
            }


            con.Close();
           

        }
        private void Rest()
        {
            CasehadeTb.Text = "";
            ChargesheetTb.Text = "";
            RemarkTb.Text = "";
            CaseCb.SelectedIndex = -1;
            key = 0;
        }
        private void RecordBtn_Click(object sender, EventArgs e)
        {
            if (CasehadeTb.Text == "" || ChargesheetTb.Text == "" || RemarkTb.Text == "" )
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CharegsTb1(CaseCode,CaseHeading,ChargeSheet,Remarks,polname)values(@CC,@Ch,@Cs,@Rem,@Poln)", con);

                    cmd.Parameters.AddWithValue("@CC", CaseCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Ch", CasehadeTb.Text);
                    cmd.Parameters.AddWithValue("@Cs", ChargesheetTb.Text);
                    cmd.Parameters.AddWithValue("@Rem", RemarkTb.Text);
                    cmd.Parameters.AddWithValue("@Poln", OfficeNTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Charge is Recorded!");
                    con.Close();
                    ShowCharegs();
                    Rest();


                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void CaseCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCaseName();
        }

        private void ChargesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                CaseCb.SelectedValue = ChargesDGV.CurrentRow.Cells[1].Value.ToString();
               CasehadeTb.Text = ChargesDGV.CurrentRow.Cells[2].Value.ToString();
                ChargesheetTb.Text = ChargesDGV.CurrentRow.Cells[3].Value.ToString();
                RemarkTb.Text = ChargesDGV.CurrentRow.Cells[4].Value.ToString();
                
                if (CasehadeTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(ChargesDGV.CurrentRow.Cells[0].Value.ToString());
                }

            }
            catch (Exception Ex)

            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CasehadeTb.Text == "" || ChargesheetTb.Text == "" || RemarkTb.Text == "")
            {
                MessageBox.Show("Missing Information!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Update CharegsTb1 set CaseCode= @CC,CaseHeading= @Ch,ChargeSheet=@Cs,Remarks=@Rem,polname=@Poln where ChNum=@Chkey ", con);
                    cmd.Parameters.AddWithValue("@Chkey", key);
                    cmd.Parameters.AddWithValue("@CC", CaseCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Ch", CasehadeTb.Text);
                    cmd.Parameters.AddWithValue("@Cs", ChargesheetTb.Text);
                    cmd.Parameters.AddWithValue("@Rem", RemarkTb.Text);
                    cmd.Parameters.AddWithValue("@Poln", OfficeNTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Charge is edited!");
                    con.Close();
                    ShowCharegs();
                    Rest();


                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {

            if (key == 0)
            {
                MessageBox.Show("Please Select a Charges!!!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from CharegsTb1 Where ChNum=@ChKey", con);
                    cmd.Parameters.AddWithValue("@ChKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Are you sure charge deleted!");
                    con.Close();
                    ShowCharegs();
                    Rest();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void CountCHarges()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From CharegsTb1", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
           
            label9.Text = dt.Rows[0][0].ToString() + " Charges";
            con.Close();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            dashibored obj = new dashibored();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            criminals obj = new criminals();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            cases obj = new cases();
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

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
